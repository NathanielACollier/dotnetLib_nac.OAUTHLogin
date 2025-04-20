using System;
using Photino.NET;

namespace nac.OAUTHLogin.Photino.repositories;

public static class PhotinoBrowserRepo
{


    private static PhotinoWindow CreateWindow()
    {
        var window = new PhotinoWindow()
            .SetTitle("Authentication")
            // set the window to be a specific size
            .SetUseOsDefaultSize(false)
            .SetSize(new System.Drawing.Size(1000, 800))
            // Center window in the middle of the screen
            .Center();
        
        return window;
    }

    public static PhotinoWindow OpenAtUrl(string url)
    {
        var window = CreateWindow();

        window.Load(new Uri(url));
        
        return window;
    }
    
}