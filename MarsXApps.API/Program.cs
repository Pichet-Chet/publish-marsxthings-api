using System.Text;
using System.Threading.RateLimiting;
using MarsXApps.API.Auth;
using MarsXApps.API.Controllers;
using MarsXApps.Models;
using MarsXApps.Models.Constants;
using MarsXApps.Module.Environment.Authorized;
using MarsXApps.Module.Environment.Connection;
using MarsXApps.Module.Environment.LongdoMap;
using MarsXApps.Module.Environment.MasterData;
using MarsXApps.Module.Promotion.Repositories;
using MarsXApps.Module.ServiceAfterSale.Repositories;
using MarsXApps.Module.ServicePrice.Repositories;
using MarsXApps.Service;
using MarsXApps.Service.Models;
using MarsXApps.Service.Validate;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WatchDog;
using WatchDog.src.Enums;
using WatchDog.src.Models;

IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();

var connectionString = configuration.GetConnectionString("ConnectionStr");

var connectionStrWatchdog = configuration.GetConnectionString("ConnectionStrWatchdog");


var builder = WebApplication.CreateBuilder(args);

var endpoint = configuration["APP:END_POINT"];

//var coreUrl = configuration["APP:CORE"];

var title = configuration["APP:TITLE"];
var version = configuration["APP:VERSION"];
var versionDescription = configuration["APP:VERSION_DESCRIPTION"];


#region Logging Watchdog Zone

builder.Logging.AddWatchDogLogger();

builder.Services.AddWatchDogServices();
builder.Services.AddWatchDogServices(opt =>
{
    opt.IsAutoClear = true;
    opt.ClearTimeSchedule = WatchDogAutoClearScheduleEnum.Every6Hours;

    opt.SetExternalDbConnString = connectionStrWatchdog;
    opt.DbDriverOption = WatchDogDbDriverEnum.PostgreSql;
});


#endregion


builder.Services.AddDbContext<MarscommuContext>();

// For Entity Framework
builder.Services.AddDbContext<MarscommuContext>(options => options.UseNpgsql(configuration.GetConnectionString("ConnectionStrings:ConnectionStr")));

// For Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<MarscommuContext>()
    .AddDefaultTokenProviders();

// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
});


//Configure Authorization
builder.Services.AddControllersWithViews(options =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});


// Add JWT authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,

            ValidAudience = configuration["JWT:ValidAudience"],
            ValidIssuer = configuration["JWT:ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SECRET"]))
        };
    });



// Add services to the container.


System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);

var location = assembly.Location;
DateTime setFileCreated = DateTime.Now;
DateTime setFileLastModified = DateTime.Now;
var bytes = new byte[2048];

using (var file = new FileStream(location, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
{
    file.Read(bytes, 0, bytes.Length);

    FileInfo fi = new FileInfo(location);
    setFileCreated = fi.CreationTime;
    setFileLastModified = fi.LastWriteTime;
}

// Add Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("internal", new OpenApiInfo
    {
        Title = $"{title}",
        Version = $"{version}",
        Description = $"{versionDescription} build at {setFileLastModified.ToString("dd/MM/yyyy HH:mm")}",
    });

    c.SwaggerDoc("outsource", new OpenApiInfo
    {
        Title = $"{title}",
        Version = $"{version}",
        Description = $"{versionDescription} build at {setFileLastModified.ToString("dd/MM/yyyy HH:mm")}",
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter the JWT token in the field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
            factory: partition => new FixedWindowRateLimiterOptions
            {
                AutoReplenishment = true,
                PermitLimit = Convert.ToInt32(configuration["RATE_LIMIT:PERMIT_LIMIT"]),
                QueueLimit = Convert.ToInt32(configuration["RATE_LIMIT:QUEUE_LIMIT"]),
                Window = TimeSpan.FromSeconds(Convert.ToInt32(configuration["RATE_LIMIT:SECONDS"]))
            }));

    options.OnRejected = async (context, token) =>
    {
        context.HttpContext.Response.StatusCode = 429;

        if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
        {
            await context.HttpContext.Response.WriteAsync(
                $"Too many requests. Please try again after {retryAfter.TotalMinutes} minute(s). " +
                $"Read more about our rate limits at.", cancellationToken: token);
        }
        else
        {
            await context.HttpContext.Response.WriteAsync(
                "Too many requests. Please try again later. " +
                "Read more about our rate limits at.", cancellationToken: token);
        }
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});



builder.Services.AddDbContext<MarscommuContext>(options => options.UseNpgsql(connectionString));


#region Scope zone

builder.Services.AddScoped<IMasterDataValidationService, MasterDataValidationService>();

builder.Services.AddScoped<ISasAddressService, SasAddressService>();
builder.Services.AddScoped<ISasAddressRepositories, SasAddressRepositories>();

builder.Services.AddScoped<ISysCustomerAddressService, SysCustomerAddressService>();
builder.Services.AddScoped<ISysCustomerAddressRepositories, SysCustomerAddressRepositories>();

builder.Services.AddScoped<ISysCustomerMachineService, SysCustomerMachineService>();
builder.Services.AddScoped<ISysCustomerMachineRepositories, SysCustomerMachineRepositories>();

builder.Services.AddScoped<ISasOrderService, SasOrderService>();
builder.Services.AddScoped<ISasOrderRepositories, SasOrderRepositories>();

builder.Services.AddScoped<ISasOrderPaymentService, SasOrderPaymentService>();

builder.Services.AddScoped<ISasOrderTransactionService, SasOrderTransactionService>();
builder.Services.AddScoped<ISasOrderTransactionRepositories, SasOrderTransactionRepositories>();

builder.Services.AddScoped<ISasPeriodTimeService, SasPeriodTimeService>();
builder.Services.AddScoped<ISasPeriodTimeRepositories, SasPeriodTimeRepositories>();

builder.Services.AddScoped<IMasterPaymentChannelService, MasterPaymentChannelService>();
builder.Services.AddScoped<IMasterPaymentChannelRepositories, MasterPaymentChannelRepositories>();

builder.Services.AddScoped<ISasStatusService, SasStatusService>();
builder.Services.AddScoped<ISasStatusRepositories, SasStatusRepositories>();

builder.Services.AddScoped<ISysCustomerService, SysCustomerService>();
builder.Services.AddScoped<ISysCustomerRepositories, SysCustomerRepositories>();

builder.Services.AddScoped<IMasterBranchService, MasterBranchService>();
builder.Services.AddScoped<IMasterBranchRepositories, MasterBranchRepositories>();

builder.Services.AddScoped<ISysDocumentControlService, SysDocumentControlService>();

builder.Services.AddScoped<ISysTermsAndConditionService, SysTermsAndConditionService>();
builder.Services.AddScoped<ISysTermsAndConditionRepositories, SysTermsAndConditionRepositories>();

builder.Services.AddScoped<IServicePriceService, ServicePriceService>();
builder.Services.AddScoped<IServicePriceRepository, ServicePriceRepository>();

builder.Services.AddScoped<ISysTermsAndConditionsTransactionService, SysTermsAndConditionsTransactionService>();
builder.Services.AddScoped<ISysTermsAndConditionsTransactionRepository, SysTermsAndConditionsTransactionRepository>();

builder.Services.AddScoped<ISysFaqService, SysFaqService>();
builder.Services.AddScoped<ISysFaqRepositories, SysFaqRepositories>();

builder.Services.AddScoped<ISysPrivacyService, SysPrivacyService>();
builder.Services.AddScoped<ISysPrivacyRepositories, SysPrivacyRepositories>();

builder.Services.AddScoped<IPromotionService, PromotionService>();
builder.Services.AddScoped<IPromotionRepository, PromotionRepository>();

builder.Services.AddScoped<ICmsContentService, CmsContentService>();
builder.Services.AddScoped<ICmsContentRepositories, CmsContentRepositories>();

builder.Services.AddScoped<ICmsScreenService, CmsScreenService>();
builder.Services.AddScoped<ICmsScreenRepositories, CmsScreenRepositories>();

builder.Services.AddScoped<ICmsWidgetService, CmsWidgetService>();
builder.Services.AddScoped<ICmsWidgetRepositories, CmsWidgetRepositories>();

builder.Services.AddScoped<ILogAccessApplicationTokenService, LogAccessApplicationTokenService>();
builder.Services.AddScoped<ILogAccessApplicationTokenRepositories, LogAccessApplicationTokenRepositories>();

builder.Services.AddScoped<IConnectionService, ConnectionService>();
builder.Services.AddScoped<IConnectionRepositories, ConnectionRepositories>();

builder.Services.AddScoped<IAuthorizedService, AuthorizedService>();
builder.Services.AddScoped<IAuthorizedRepositories, AuthorizedRepositories>();

builder.Services.AddScoped<IPostingCommentService, PostingCommentService>();
builder.Services.AddScoped<IPostingCommentRepositories, PostingCommentRepositories>();

builder.Services.AddScoped<IPostingFavoriteTransactionService, PostingFavoriteTransactionService>();
builder.Services.AddScoped<IPostingFavoriteTransactionRepositories, PostingFavoriteTransactionRepositories>();

builder.Services.AddScoped<IPostingLikeService, PostingLikeService>();
builder.Services.AddScoped<IPostingLikeRepositories, PostingLikeRepositories>();

builder.Services.AddScoped<IPostingLimitService, PostingLimitService>();
builder.Services.AddScoped<IPostingLimitRepositories, PostingLimitRepositories>();

builder.Services.AddScoped<IPostingTransactionService, PostingTransactionService>();
builder.Services.AddScoped<IPostingTransactionRepositories, PostingTransactionRepositories>();

builder.Services.AddScoped<IPostingTypeService, PostingTypeService>();
builder.Services.AddScoped<IPostingTypeRepositories, PostingTypeRepositories>();

builder.Services.AddScoped<ISysUnitPriceRepositories, SysUnitPriceRepositories>();
builder.Services.AddScoped<ISysUnitPriceService, SysUnitPriceService>();

builder.Services.AddScoped<IMasterConfigurationService, MasterConfigurationService>();

builder.Services.AddScoped<ILongdoMapRepositories, LongdoMapRepositories>();
builder.Services.AddScoped<ILongdoMapService, LongdoMapService>();

#endregion

var app = builder.Build();

app.UseSwagger();
//app.UseSwaggerUI();

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwaggerUI(options =>
//    {
//        options.SwaggerEndpoint(endpoint + "swagger/internal/swagger.json", "Internal");
//        options.SwaggerEndpoint(endpoint + "swagger/outsource/swagger.json", "OutSource");
//    });
//}

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint(endpoint + "swagger/internal/swagger.json", "Internal");
    options.SwaggerEndpoint(endpoint + "swagger/outsource/swagger.json", "OutSource");
});




app.UseCors("AllowAll");

//app.UseCors(builder => builder
//    .AllowAnyHeader()
//    .AllowAnyMethod()
//    .AllowAnyOrigin()
//);

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.UseRateLimiter();

app.UseWatchDog(opt =>
{
    opt.WatchPageUsername = configuration["WatchDogLogging:username"];
    opt.WatchPagePassword = configuration["WatchDogLogging:password"];
});


app.Run();


public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var resp = new Response
        {
            Status = Constants.StatusError,
            Code = Constants.HttpCode500.ToString(),
            Message = Constants.HttpCode500Message,
            InnerException = ex.InnerException == null ? ex.Message : ex.InnerException.ToString()
        };

        var controllerName = context.GetRouteValue("controller")?.ToString();
        var actionName = context.GetRouteValue("action")?.ToString();

        WatchLogger.LogError(resp.InnerException, ex.StackTrace, controllerName + "|" + actionName);

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        return context.Response.WriteAsJsonAsync(resp);
    }
}

