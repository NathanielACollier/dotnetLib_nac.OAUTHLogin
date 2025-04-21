using System.Threading.Tasks;
using Xunit;

namespace Tests_MacOS;

[Collection(nameof(__MSTest_Setup))]
public class UnitTest1
{
    /*
     For how to get these to work refer to this project on github
     + https://github.com/AArnott/Xunit.StaFact/blob/18c45ca7fc5f7acaee8739e32bad85d01e33b776/test/Xunit.StaFact.Tests_xUnit_UICrossPlatform.Mac/Xunit.StaFact.Tests_xUnit_UICrossPlatform.Mac.csproj
     */
    
    
    [CocoaFact]
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



    [CocoaFact]
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



    [CocoaFact]
    public async Task PhotinoBrowserGoogleTest()
    {
        var win = nac.OAUTHLogin.Photino.repositories.PhotinoBrowserRepo.OpenAtUrl("https://www.google.com/");
        
        win.WaitForClose();
        
        Assert.True(1==1);
    }


}