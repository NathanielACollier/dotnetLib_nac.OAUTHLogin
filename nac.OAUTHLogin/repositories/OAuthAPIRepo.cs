using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace nac.OAUTHLogin.repositories;

public class OAuthAPIRepo
{

    private model.OAUTHSettings oauthSettings;    
    
    
    public OAuthAPIRepo(model.OAUTHSettings settings)
    {
        this.oauthSettings = settings;
    }



    public void UpdateRedirectUrl(string redirectUrl)
    {
        this.oauthSettings.RedirectUrl = redirectUrl;
    }
    
    
    public string GetLoginUrl()
    {

        var loginUrl = new nac.utilities.Url(oauthSettings.URL_Auth);

        // add the params
        loginUrl.AddQueryParametersFromDictionary(new Dictionary<string, string>{
            {"client_id", oauthSettings.ClientId},
            {"response_type", "code"},
            {"redirect_uri", oauthSettings.RedirectUrl},
            {"scope", oauthSettings.Scope},
            {"response_mode", "query"},
            {"state", oauthSettings.State}
        });

        if (oauthSettings.ForceSelectAccount)
        {
            loginUrl.AddQuery("prompt", "select_account");
        }

        return loginUrl.ToString();
    }


    public async Task<string> GetBearerTokenFromOauthCode(string code)
    {
        // add the params
        var values = new Dictionary<string, string>{
            {"grant_type","authorization_code" },
            {"client_id", oauthSettings.ClientId},
            {"client_secret",oauthSettings.ClientSecret },    
            {"scope", oauthSettings.Scope},
            {"code", code},
            {"redirect_uri", oauthSettings.RedirectUrl},
        };
        var httpClient = new nac.http.HttpClient(baseUrl: oauthSettings.URL_GetTokenViaCode,
            useWindowsAuth: false);

        var response = await httpClient.PostFormUrlEncodeAsync<System.Text.Json.Nodes.JsonNode>("",
            values: values);

        string token = response["access_token"].Deserialize<string>();

        return token;
    }
    
    
    
    
}