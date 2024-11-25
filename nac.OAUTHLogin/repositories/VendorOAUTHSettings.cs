namespace nac.OAUTHLogin.repositories;

public static class VendorOAUTHSettings
{
    public static model.OAUTHSettings GetMicrosoftOAUTHSettings(string tenant = "common")
    {
        var msLoginUrl = new nac.utilities.Url("https://login.microsoftonline.com/");

        msLoginUrl.Path = $"{tenant}/oauth2/v2.0/authorize";


        var msAuthUrl = new nac.utilities.Url("https://login.microsoftonline.com/");
        msAuthUrl.Path = $"{tenant}/oauth2/v2.0/token";

        var settings = new model.OAUTHSettings
        {
            URL_Auth = msLoginUrl.ToString(),
            URL_GetTokenViaCode = msAuthUrl.ToString()
        };

        return settings;
    }


    public static model.OAUTHSettings GetGoogleOAUTHSettings()
    {
        var settings = new model.OAUTHSettings
        {
            URL_Auth = "https://accounts.google.com/o/oauth2/auth",
            URL_GetTokenViaCode = "https://accounts.google.com/o/oauth2/token"
        };

        return settings;
    }
}