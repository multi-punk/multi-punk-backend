using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http.Extensions;

namespace MultiApi.Middleware;

public class SQLInjectionHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        using (StreamReader reader = new StreamReader(context.Request.Body))  
        {  
            Console.WriteLine(context.Request.GetDisplayUrl().IndexOfAny("';".ToCharArray()) + "im there");

            string line;
            bool hasAnInvalidCharecter = await CheckForInvaledCherecters(context.Request.GetDisplayUrl());  

            while ((line = await reader.ReadLineAsync()) != null)  
            {  
                if(await CheckForInvaledCherecters(line))
                {
                    hasAnInvalidCharecter = true;
                    break;
                }
            }

            foreach(var header in context.Request.Headers)
                if(await CheckForInvaledCherecters(header.Value) || await CheckForInvaledCherecters(header.Key))
                    hasAnInvalidCharecter = true;

            if(hasAnInvalidCharecter) 
            {
                Console.WriteLine(context.Request.GetDisplayUrl() + "im there");
                context.Response.StatusCode = 408;
                await context.Response.WriteAsync("suck my dick fucking sql injector");
                return;
            }
        } 
        await next(context); 
    }

    private async Task<bool> CheckForInvaledCherecters(string value)
    {
        return value.IndexOfAny("';".ToCharArray()) != -1;
    }
}
