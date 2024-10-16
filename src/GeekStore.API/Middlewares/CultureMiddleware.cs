﻿using System.Globalization;

namespace GeekStore.API.Middlewares;

public class CultureMiddleware
{
    private readonly RequestDelegate _next;

    public CultureMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context)
    {
        var supportedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures);

        var requestedCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();

        if (string.IsNullOrWhiteSpace(requestedCulture) || 
            !supportedLanguages.Any(c => c.Name.Equals(requestedCulture)))
        {
            await _next(context);
        }

        var cultureInfo = new CultureInfo(requestedCulture);

        CultureInfo.CurrentCulture = cultureInfo;
        CultureInfo.CurrentUICulture = cultureInfo;

        await _next(context);
    }
}