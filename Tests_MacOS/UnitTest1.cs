using System.Threading.Tasks;
using Xunit;

namespace Tests_MacOS;

public class UnitTest1
{
    [CocoaFact]
    public async Task TestMicrosoftLogin()
    {
        var microsoftSettings = nac.OAUTHLogin.repositories.VendorOAUTHSettings.GetMicrosoftOAUTHSettings();
        microsoftSettings.ClientId = lib.settings.OAUTH_Microsoft_ClientId;
        microsoftSettings.ClientSecret = lib.settings.OAUTH_Microsoft_ClientSecret;
        microsoftSettings.Scope = "https://graph.microsoft.com/.default";

        string token = await nac.OAUTHLogin.OAUTH.GetAuthTokenViaDefaultBrowser(microsoftSettings);
        
        Assert.True(!string.IsNullOrWhiteSpace(token) &&
                    token.Length > 20);
    }



    [CocoaFact]
    public async Task TestGoogleLogin()
    {
        var oauthSettings = nac.OAUTHLogin.repositories.VendorOAUTHSettings.GetGoogleOAUTHSettings();
        oauthSettings.ClientId = lib.settings.OAUTH_Google_ClientId;
        oauthSettings.ClientSecret = lib.settings.OAUTH_Google_ClientSecret;
        oauthSettings.Scope = "email";

        string token = await nac.OAUTHLogin.OAUTH.GetAuthTokenViaDefaultBrowser(oauthSettings);
        
        Assert.True(!string.IsNullOrWhiteSpace(token) &&
                    token.Length > 20);
    }



    [CocoaFact]
    public async Task PhotinoBrowserGoogleTest()
    {
        var win = nac.OAUTHLogin.repositories.PhotinoBrowserRepo.OpenAtUrl("https://www.google.com/");
        
        win.WaitForClose();
        
        Assert.True(1==1);
    }


}