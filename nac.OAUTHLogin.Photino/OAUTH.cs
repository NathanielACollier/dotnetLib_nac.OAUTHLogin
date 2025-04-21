using System.Threading.Tasks;
using photinoNet = Photino.NET; // do it this way because of a namespace conflict

namespace nac.OAUTHLogin.Photino;

public static class OAUTH
{

    public static async Task<string> GetAuthTokenViaDefaultBrowser(OAUTHLogin.model.OAUTHSettings settings)
    {
        var desktopFlow = new OAUTHLogin.repositories.DesktopAppLocalServerFlowRepo(settings);
        
        photinoNet.PhotinoWindow localBrowserWindow = null;
        
        string oauthToken = await desktopFlow.GetAuthorizationToken(loginUrl =>
        {
            localBrowserWindow = repositories.PhotinoBrowserRepo.OpenAtUrl(loginUrl);
        });
        
        localBrowserWindow.Close();

        return oauthToken;
    }


}