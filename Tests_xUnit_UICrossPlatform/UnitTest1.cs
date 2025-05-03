using System.Threading.Tasks;
using Xunit;

namespace Tests;

[Collection(nameof(__MSTest_Setup))]
public class UnitTest1
{
    [UIFact]
    public async Task TestMicrosoftLogin()
    {
        var microsoftSettings = nac.OAUTHLogin.repositories.VendorOAUTHSettings.GetMicrosoftOAUTHSettings();
        microsoftSettings.ClientId = lib.settings.OAUTH_Microsoft_ClientId;
        microsoftSettings.ClientSecret = lib.settings.OAUTH_Microsoft_ClientSecret;
        microsoftSettings.Scope = "https://graph.microsoft.com/.default";

        string token = await nac.OAUTHLogin.Photino.OAUTH.GetToken(microsoftSettings);
        
        Assert.True(!string.IsNullOrWhiteSpace(token) &&
                      token.Length > 20);
    }



    [UIFact]
    public async Task TestGoogleLogin()
    {
        var oauthSettings = nac.OAUTHLogin.repositories.VendorOAUTHSettings.GetGoogleOAUTHSettings();
        oauthSettings.ClientId = lib.settings.OAUTH_Google_ClientId;
        oauthSettings.ClientSecret = lib.settings.OAUTH_Google_ClientSecret;
        oauthSettings.Scope = "email";

        string token = await nac.OAUTHLogin.Photino.OAUTH.GetToken(oauthSettings);
        
        Assert.True(!string.IsNullOrWhiteSpace(token) &&
                      token.Length > 20);
    }



    [UIFact]
    public async Task PhotinoBrowserGoogleTest()
    {
        var photinoRepo = new nac.OAUTHLogin.Photino.repositories.PhotinoBrowserRepo();
        
        await photinoRepo.OpenAtUrl("https://www.google.com/");
        
        Assert.True(1==1);
    }
    
    
    
}