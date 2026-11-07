using Project.Application.Services;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Project.Infrastructure
{
    internal class AuthHeaderHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(AuthTokenHolder.Token))
            {
                
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AuthTokenHolder.Token);
            }

            
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
