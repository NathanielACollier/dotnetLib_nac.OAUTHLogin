

using System.Threading.Tasks;

namespace nac.OAUTHLogin.Photino.repositories;

public class DesktopAppLocalServerFlowRepo
{
    private repositories.Logger log = new();
    private readonly OAUTHLogin.model.OAUTHSettings oauthSettings;
    private OAUTHLogin.repositories.OAuthAPIRepo oauthAPI;

    public DesktopAppLocalServerFlowRepo(OAUTHLogin.model.OAUTHSettings oauthSettings)
    {
        this.oauthSettings = oauthSettings;
        this.oauthAPI = new OAUTHLogin.repositories.OAuthAPIRepo(this.oauthSettings);
    }

    public async Task<string> GetAuthorizationToken()
    {
        var codeReceiverResult = SetupLoginUrlAndLocalCodeReceiverServer();
        
        await codeReceiverResult.LogonUrlReady;
        
        string loginUrl = this.oauthAPI.GetLoginUrl();
        log.Info($"Got Login Url: {loginUrl}");
        
        log.Info("Opening default browser so user can login");
        // now we need to show that URL to the user in a web browser
        //nac.WebServer.lib.BrowserUtility.OpenBrowser(loginUrl);
        var localBrowserWindow = repositories.PhotinoBrowserRepo.OpenAtUrl(loginUrl);
        
        log.Info("Wait on Code to be received");
        var oauthCode = await codeReceiverResult.CodeReceived;
        log.Info($"Got code back from OAUTH.   Code size: {oauthCode.Code.Length}");
        localBrowserWindow.Close();
        
        log.Info("Use the code to get an Authorization Token");
        string token = await this.oauthAPI.GetBearerTokenFromOauthCode(oauthCode.Code);

        return token;
    }




    private model.SetupLocalServerResult SetupLoginUrlAndLocalCodeReceiverServer()
    {
        var result = new model.SetupLocalServerResult();
        
        var codeReceivedSource = new TaskCompletionSource<OAUTHLogin.model.OauthCodeReceivedResult>();
        var logonUrlReadySource = new TaskCompletionSource<bool>();

        result.CodeReceived = codeReceivedSource.Task;
        result.LogonUrlReady = logonUrlReadySource.Task;
        
        result.WebServerFinished = Task.Run(async () =>
        {
            var codeReceiver = new OAUTHLogin.repositories.LocalWebServerOAUTHCodeReceiverRepo();

            codeReceiver.onOAUTHCodeReceived += (_s, _oauthCode) =>
            {
                codeReceivedSource.SetResult(_oauthCode);
            };
            
                    
            // now that the local code receiver is up, we need to update to account for it's URL
            this.oauthAPI.UpdateRedirectUrl(codeReceiver.Url); // the local code receiver is where OAUTH is going to send the code to, so it needs the URL that is nown known

            logonUrlReadySource.SetResult(true);
        });
        
        return result;
    }
    
    
    
}