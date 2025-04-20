using nac.Forms;

namespace TestApp_UITester.repos.TestFunctions;

public class TestFunctions
{
    private static nac.Logging.Logger log = new();
    
    public static void MicrosoftLogin(Form f)
    {
        f.Text("Token")
            .TextBoxFor("tokenField");
        
        var microsoftSettings = nac.OAUTHLogin.repositories.VendorOAUTHSettings.GetMicrosoftOAUTHSettings();
        microsoftSettings.ClientId = lib.settings.OAUTH_Microsoft_ClientId;
        microsoftSettings.ClientSecret = lib.settings.OAUTH_Microsoft_ClientSecret;
        microsoftSettings.Scope = "https://graph.microsoft.com/.default";

        Task.Run(async () =>
        {
            string token = await nac.OAUTHLogin.Photino.OAUTH.GetAuthTokenViaDefaultBrowser(microsoftSettings);

            if (string.IsNullOrWhiteSpace(token) || token.Length < 25)
            {
                log.Error("Microsoft Login - Token generation failed");
            }
            
            f.setModelValue("tokenField", token);
            

        });
        
    }


    public static void GoogleLogin(Form f)
    {
        f.Text("Token")
            .TextBoxFor("tokenField");
        
        var oauthSettings = nac.OAUTHLogin.repositories.VendorOAUTHSettings.GetGoogleOAUTHSettings();
        oauthSettings.ClientId = lib.settings.OAUTH_Google_ClientId;
        oauthSettings.ClientSecret = lib.settings.OAUTH_Google_ClientSecret;
        oauthSettings.Scope = "email";

        Task.Run(async () =>
        {
            string token = await nac.OAUTHLogin.Photino.OAUTH.GetAuthTokenViaDefaultBrowser(oauthSettings);

            if (string.IsNullOrWhiteSpace(token) || token.Length < 25)
            {
                log.Error("Microsoft Login - Token generation failed");
            }
            
            f.setModelValue("tokenField", token);
            

        });
    }



    public static void ShowGoogleHomepage(Form f)
    {
        var win = nac.OAUTHLogin.Photino.repositories.PhotinoBrowserRepo.OpenAtUrl("https://www.google.com/");
        
        win.WaitForClose();
    }
    
    
}