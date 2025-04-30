using System;
using MarsXApps.Models;
using MarsXApps.Models.Constants;

namespace MarsXApps.API.Helper
{
    public class AppHelper
    {
        public AppHelper()
        {
        }

        public static object GetResponseController(Response? response)
        {
            if (response?.HttpCode == Constants.HttpCode200)
            {
                return new
                {
                    response?.Status,
                    response?.Message,
                    response?.PageNumber,
                    response?.PageSize,
                    response?.EffectRow,
                    response?.Output,
                };
            }
            else if (response?.HttpCode == Constants.HttpCode204)
            {
                return new
                {
                    response?.Status,
                    response?.Message,
                    response?.InnerException,
                    response?.Output,
                };
            }
            else if (response?.HttpCode == Constants.HttpCode500)
            {
                return new
                {
                    response?.Status,
                    response?.Message,
                    response?.InnerException
                };
            }
            else
            {
                return new
                {
                    response?.Status,
                    response?.Message
                };
            }
        }
    }
}

