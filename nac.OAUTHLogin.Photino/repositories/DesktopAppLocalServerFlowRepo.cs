

using System.Threading.Tasks;
using photinoNet = Photino.NET; // do it this way because of a namespace conflict

namespace nac.OAUTHLogin.Photino.repositories;

public class DesktopAppLocalServerFlowRepo : nac.OAUTHLogin.repositories.DesktopAppLocalServerFlowRepo
{
    public DesktopAppLocalServerFlowRepo(OAUTHLogin.model.OAUTHSettings oauthSettings) : base(oauthSettings){}

    public async Task<string> GetAuthorizationToken_ViaPhotino()
    {
        photinoNet.PhotinoWindow localBrowserWindow = null;
        
        string oauthToken = await GetAuthorizationToken(loginUrl =>
        {
            localBrowserWindow = repositories.PhotinoBrowserRepo.OpenAtUrl(loginUrl);
        });
        
        localBrowserWindow.Close();

        return oauthToken;
    }

    
    
    
    
}