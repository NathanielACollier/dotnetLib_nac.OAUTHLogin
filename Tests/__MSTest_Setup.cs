using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests;

[TestClass]
public class __MSTest_Setup
{
    private static nac.Logging.Logger log = new();

    private static nac.http.logging.har.lib.HARLogManager harManager = new("http.har");

    [AssemblyInitialize()]
    public static async Task MyTestInitialize(TestContext testContext)
    {
        nac.Logging.Appenders.Debug.Setup();

        // curl logging
        nac.http.logging.curl.LoggingHandler.isEnabled = true;
        nac.http.logging.curl.LoggingHandler.onMessage += (__s, _args) =>
        {
            log.Debug(_args.ToString());
        };

        log.Info("Tests Starting");

    }

    [AssemblyCleanup]
    public static void TearDown()
    {

    }
}