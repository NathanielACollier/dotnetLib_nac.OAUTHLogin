using System.Threading.Tasks;

namespace nac.OAUTHLogin;

public static class OAUTH
{

    public static async Task<string> GetAuthTokenViaDefaultBrowser(model.OAUTHSettings settings)
    {
        var desktopFlow = new repositories.DesktopAppLocalServerFlowRepo(settings);

        string token = await desktopFlow.GetAuthorizationToken();
        return token;
    }


}