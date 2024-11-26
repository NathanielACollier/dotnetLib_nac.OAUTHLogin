using System;

namespace nac.OAUTHLogin.repositories;

public static class PhotinoBrowserRepo
{


    private static Photino.NET.PhotinoWindow CreateWindow()
    {
        var window = new Photino.NET.PhotinoWindow()
            .SetTitle("Authentication")
            // set the window to be a specific size
            .SetUseOsDefaultSize(false)
            .SetSize(new System.Drawing.Size(1000, 800))
            // Center window in the middle of the screen
            .Center();
        
        return window;
    }

    public static Photino.NET.PhotinoWindow OpenAtUrl(string url)
    {
        var window = CreateWindow();

        window.Load(new Uri(url));
        
        return window;
    }
    
}