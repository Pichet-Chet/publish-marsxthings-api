using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MarsXApps.Models
{
    public class Response
    {
        [JsonPropertyName("status")]
        public bool Status { get; set; }

        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }

        [JsonPropertyName("httpCode")]
        public int HttpCode { get; set; }

        [JsonPropertyName("httpMessage")]
        public string? HttpMessage { get; set; }

        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; } = "";

        [JsonPropertyName("errorMessage")]
        public string? ErrorMessage { get; set; }

        [JsonPropertyName("errorStack")]
        public string? ErrorStack { get; set; }

        [JsonPropertyName("innerException")]
        public string? InnerException { get; set; }

        [JsonPropertyName("currentMethod")]
        public string? CurrentMethod { get; set; }

        [JsonPropertyName("pageNumber")]
        public int? PageNumber { get; set; }

        [JsonPropertyName("pageSize")]
        public int? PageSize { get; set; }

        [JsonPropertyName("effectRow")]
        public int? EffectRow { get; set; }

        [JsonPropertyName("output")]
        public object? Output { get; set; }

        
    }
}
