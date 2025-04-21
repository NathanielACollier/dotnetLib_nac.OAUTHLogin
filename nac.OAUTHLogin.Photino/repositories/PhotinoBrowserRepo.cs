using System;
using System.Threading.Tasks;
using Photino.NET;

namespace nac.OAUTHLogin.Photino.repositories;

public class PhotinoBrowserRepo
{
    private System.Threading.SynchronizationContext mainContext;

    public PhotinoBrowserRepo()
    {
        this.mainContext = System.Threading.SynchronizationContext.Current;
    }

    private PhotinoWindow CreateWindow()
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

    public Task<PhotinoWindow> OpenAtUrl(string url)
    {
        var promise = new System.Threading.Tasks.TaskCompletionSource<PhotinoWindow>();
        
        this.mainContext.Post(_ =>
        {
            var window = CreateWindow();

            window.Load(new Uri(url));
            promise.SetResult(window);
        },null);
        
        return promise.Task;
    }
    
}