using System;
using System.Threading.Tasks;
using Xunit;

namespace Tests;

[CollectionDefinition(nameof(__MSTest_Setup))]
public class __MSTest_Setup : IDisposable, ICollectionFixture<__MSTest_Setup>
{
    private static nac.Logging.Logger log = new();

    private static nac.http.logging.har.lib.HARLogManager harManager = new("http.har");
    
    public __MSTest_Setup()
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


    public void Dispose()
    {

    }
}