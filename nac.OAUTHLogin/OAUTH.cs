using System.Threading.Tasks;

namespace nac.OAUTHLogin;

public static class OAUTH
{

    public static async Task<string> GetAuthTokenViaDefaultBrowser(model.OAUTHSettings settings)
    {
        var desktopFlow = new repositories.DesktopAppLocalServerFlowRepo(settings);

        string token = await desktopFlow.GetAuthorizationToken(loginUrl =>
        {
            // now we need to show that URL to the user in a web browser
            nac.WebServer.lib.BrowserUtility.OpenBrowser(loginUrl);
        });

        return token;
    }


}