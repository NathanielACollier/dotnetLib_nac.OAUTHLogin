using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public async Task TestMicrosoftLogin()
    {
        var microsoftSettings = nac.OAUTHLogin.repositories.VendorOAUTHSettings.GetMicrosoftOAUTHSettings();
        microsoftSettings.ClientId = lib.settings.OAUTH_Microsoft_ClientId;
        microsoftSettings.ClientSecret = lib.settings.OAUTH_Microsoft_ClientSecret;
        microsoftSettings.Scope = "https://graph.microsoft.com/.default";

        string token = await nac.OAUTHLogin.OAUTH.GetAuthTokenViaDefaultBrowser(microsoftSettings);
        
        Assert.IsTrue(!string.IsNullOrWhiteSpace(token) &&
                      token.Length > 20);
    }
}