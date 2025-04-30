//using System.Security.Claims;
//using System.IdentityModel.Tokens.Jwt;
//using Microsoft.IdentityModel.Tokens;
//using System.Text;
//using Microsoft.Extensions.Configuration;
//using System.Security.Cryptography;

//namespace MarsXApps.Module.Authorized.Repositories
//{

//    public interface IJWTManagerRepositories
//    {
//        JwtSecurityToken CreateToken(List<Claim> authClaims);
//        string GenerateRefreshToken();
//        ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
//    }

//    public class JWTManagerRepositories : IJWTManagerRepositories
//    {
//        DateTime ServerTime = DateTime.Now;

//        IConfigurationRoot configuration = new ConfigurationBuilder()
//                   .SetBasePath(Directory.GetCurrentDirectory())
//                   .AddJsonFile("appsettings.json")
//                   .Build();

//        public JwtSecurityToken CreateToken(List<Claim> authClaims)
//        {
//            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SECRET"]));
//            _ = int.TryParse(configuration["JWT:TokenValidityInDays"], out int TokenValidityInDays);

//            var token = new JwtSecurityToken(
//                issuer: configuration["JWT:ValidIssuer"],
//                audience: configuration["JWT:ValidAudience"],
//                expires: ServerTime.AddDays(TokenValidityInDays),
//                claims: authClaims,
//                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
//                );

//            return token;
//        }

//        public string GenerateRefreshToken()
//        {
//            var randomNumber = new byte[64];

//            using var rng = RandomNumberGenerator.Create();

//            rng.GetBytes(randomNumber);

//            return Convert.ToBase64String(randomNumber);
//        }

//        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
//        {
//            var tokenValidationParameters = new TokenValidationParameters
//            {
//                ValidateAudience = false,
//                ValidateIssuer = false,
//                ValidateIssuerSigningKey = true,
//                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SECRET"])),
//                ValidateLifetime = false
//            };

//            var tokenHandler = new JwtSecurityTokenHandler();
//            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
//            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
//                throw new SecurityTokenException("Invalid token");

//            return principal;
//        }



//        #region IDispose Zone

//        private bool DisposedValue;

//        protected virtual void Dispose(bool disposing)
//        {
//            if (!DisposedValue)
//            {
//                if (disposing)
//                {

//                }

//                DisposedValue = true;
//            }
//        }

//        public void Dispose()
//        {
//            Dispose(disposing: true);
//            GC.SuppressFinalize(this);
//        }











//        #endregion

//    }
//}

