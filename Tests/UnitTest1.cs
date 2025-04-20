using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public async Task TestMicrosoftLogin()
    {
        var microsoftSettings = nac.OAUTHLogin.repositories.VendorOAUTHSettings.GetMicrosoftOAUTHSettings();
        microsoftSettings.ClientId = lib.settings.OAUTH_Microsoft_ClientId;
        microsoftSettings.ClientSecret = lib.settings.OAUTH_Microsoft_ClientSecret;
        microsoftSettings.Scope = "https://graph.microsoft.com/.default";

        string token = await nac.OAUTHLogin.OAUTH.GetAuthTokenViaDefaultBrowser(microsoftSettings);
        
        Assert.IsTrue(!string.IsNullOrWhiteSpace(token) &&
                      token.Length > 20);
    }



    [TestMethod]
    public async Task TestGoogleLogin()
    {
        var oauthSettings = nac.OAUTHLogin.repositories.VendorOAUTHSettings.GetGoogleOAUTHSettings();
        oauthSettings.ClientId = lib.settings.OAUTH_Google_ClientId;
        oauthSettings.ClientSecret = lib.settings.OAUTH_Google_ClientSecret;
        oauthSettings.Scope = "email";

        string token = await nac.OAUTHLogin.OAUTH.GetAuthTokenViaDefaultBrowser(oauthSettings);
        
        Assert.IsTrue(!string.IsNullOrWhiteSpace(token) &&
                      token.Length > 20);
    }
    
    
    
    
}