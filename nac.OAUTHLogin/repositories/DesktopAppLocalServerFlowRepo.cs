

using System.Threading.Tasks;

namespace nac.OAUTHLogin.repositories;

public class DesktopAppLocalServerFlowRepo
{
    private repositories.Logger log = new();
    private readonly model.OAUTHSettings oauthSettings;
    private repositories.OAuthAPIRepo oauthAPI;

    public DesktopAppLocalServerFlowRepo(model.OAUTHSettings oauthSettings)
    {
        this.oauthSettings = oauthSettings;
        this.oauthAPI = new repositories.OAuthAPIRepo(this.oauthSettings);
    }

    public async Task<string> GetAuthorizationToken()
    {
        TaskCompletionSource<model.OauthCodeReceivedResult> waitOnCodeReceivedByLocalWebServer = new();
        var codeReceiver = new repositories.LocalWebServerOAUTHCodeReceiverRepo();

        codeReceiver.onOAUTHCodeReceived += (_s, _oauthCode) =>
        {
            waitOnCodeReceivedByLocalWebServer.SetResult(_oauthCode);
        };
        
        // now that the local code receiver is up, we need to update to account for it's URL
        this.oauthAPI.UpdateRedirectUrl(codeReceiver.Url); // the local code receiver is where OAUTH is going to send the code to, so it needs the URL that is nown known

        string loginUrl = this.oauthAPI.GetLoginUrl();
        log.Info($"Got Login Url: {loginUrl}");
        
        log.Info("Opening default browser so user can login");
        // now we need to show that URL to the user in a web browser
        nac.WebServer.lib.BrowserUtility.OpenBrowser(loginUrl);
        
        log.Info("Wait on Code to be received");
        var oauthCode = await waitOnCodeReceivedByLocalWebServer.Task;
        log.Info($"Got code back from OAUTH.   Code size: {oauthCode.Code.Length}");
        
        log.Info("Use the code to get an Authorization Token");
        string token = await this.oauthAPI.GetBearerTokenFromOauthCode(oauthCode.Code);

        return token;
    }
    
    
    
}