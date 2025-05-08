using System.Threading.Tasks;

namespace nac.OAUTHLogin.model;

public class SetupLocalServerResult
{
    public Task LogonUrlReady { get; set; }
    public Task<OAUTHLogin.model.OauthCodeReceivedResult> CodeReceived { get; set; }
    
    public Task WebServerFinished { get; set; }
}