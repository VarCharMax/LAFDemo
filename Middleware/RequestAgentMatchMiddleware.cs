using LAF.Models.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace LAF
{
    namespace Middleware
    {
        public class RequestAgentMatchMiddleware
        {
            private readonly RequestDelegate nextDelegate;

            public RequestAgentMatchMiddleware(RequestDelegate next)
            {
                nextDelegate = next;
            }

            public async Task InvokeAsync(HttpContext context, IAgentMatchLogProvider logger)
            {
                if (context.Request.Path == "/match")
                {
                    string licenceNo = context.Request.Form["agentLicenceNo"].ToString();
                    //ToDo: Check for null etc;
                    //ToDo: Log request to memory-based db like Redis.
                    await logger.LogMatchAgentAsync(licenceNo);
                }

                await nextDelegate(context);
            }
        }
    }
}
