using System;
using nac.WebServer.lib;

namespace nac.OAUTHLogin.repositories;

public class LocalWebServerOAUTHCodeReceiverRepo
{
    public event EventHandler<model.OauthCodeReceivedResult> onOAUTHCodeReceived;
    private nac.WebServer.WebServerManager webServer;
    private repositories.Logger log = new();

    public string Url
    {
        get
        {
            return this.webServer.Url;
        }
    }
    

    public LocalWebServerOAUTHCodeReceiverRepo()
    {
        log.Info("Starting local web server");
        startWebServerToHandleOauthRedirect();
        log.Info($"Local web server running: {this.webServer.Url}");
    }

    
    private nac.WebServer.WebServerManager startWebServerToHandleOauthRedirect(){
        this.webServer = new nac.WebServer.WebServerManager();

        this.webServer.OnNewRequest += (_s, args) =>
        {
            if (args.Request.Url.LocalPath == "/")
            {
                string code = args.Request.QueryString["code"];

                onOAUTHCodeReceived?.Invoke(this, new model.OauthCodeReceivedResult
                {
                    Code = code,
                    RedirectUrl = webServer.Url
                });
            
                args.Response.WriteHtmlResponse(@"
                <div style='color:green;font-weight:bold;'>You can close the browser now.  OAuth Code has been retrieved.</div>
            ");
            }

        };
    
        this.webServer.Start();

        return this.webServer;
    }
    
    

}