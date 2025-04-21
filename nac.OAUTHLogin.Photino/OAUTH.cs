using System.Threading.Tasks;

namespace nac.OAUTHLogin.Photino;

public static class OAUTH
{

    public static async Task<string> GetAuthTokenViaDefaultBrowser(OAUTHLogin.model.OAUTHSettings settings)
    {
        var desktopFlow = new Photino.repositories.DesktopAppLocalServerFlowRepo(settings);

        string token = await desktopFlow.GetAuthorizationToken_ViaPhotino();
        return token;
    }


}