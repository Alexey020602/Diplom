using System.Net;
using System.Net.Http.Headers;
using Client.Services.Authorization;

namespace Client.AuthProviders;

public class MyDelegatingHandler(ITokenStorage authorizationStorage) : DelegatingHandler
{
    private readonly ITokenStorage authorizationStorage = authorizationStorage;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        Console.WriteLine("Begin get authorization");
        var auth = request.Headers.Authorization;
        // if (auth is not null)
        // {
        var authorization = await authorizationStorage.GetAuthorization();
        if (authorization is not null)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue(authorization.Scheme, authorization.Token);
        }

        Console.WriteLine(request.Headers.Authorization);
        // }
        Console.WriteLine("End");
        return await base.SendAsync(request, cancellationToken);
    }
}