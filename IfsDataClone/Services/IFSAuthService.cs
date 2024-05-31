using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IfsDataClone.Services;

public static class IFSAuthData
{
    public static string AccessToken { get; set; }
    public static int AccessTokenExpireTime { get; set; }
    public static string RefreshToken { get; set; }
    public static int RefreshTokenExpireTime { get; set; }
    public static string TokenType { get; set; }

}
public class IFSAuthService
{
    const string TOKEN_URL = "https://erp.ozyer.com/auth/realms/ozyer-prod/protocol/openid-connect/token";
    const string CLIENT_ID = "OZYER_POS";
    const string CLIENT_SECRET = "bjs9vqJAehED2zVWKTPfkd7Z03qDxgyq";


    public async Task<bool> Login()
    {

        var client = new HttpClient();
        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("grant_type", "client_credentials"),
            new KeyValuePair<string, string>("scope", "openid"),
            new KeyValuePair<string, string>("client_id", CLIENT_ID),
            new KeyValuePair<string, string>("client_secret", CLIENT_SECRET),
        });

        var request = new HttpRequestMessage(HttpMethod.Post, TOKEN_URL);
        request.Headers.Add("Host", "erp.ozyer.com");
        request.Headers.Add("Accept", "*/*");
        request.Content = content;

        var response = await client.SendAsync(request);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            string responseContent = await response.Content.ReadAsStringAsync();
            dynamic data = JObject.Parse(responseContent);
            IFSAuthData.AccessToken = data.access_token;
            IFSAuthData.RefreshToken = data.refresh_token;
            IFSAuthData.AccessTokenExpireTime = data.expires_in;
            IFSAuthData.RefreshTokenExpireTime = data.refresh_expires_in;
            IFSAuthData.TokenType = data.token_type;

            return true;
        }
        return false;



    }
}
