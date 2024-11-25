namespace nac.OAUTHLogin.model;

public class OAUTHSettings
{
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public bool ForceSelectAccount { get; set; }
    public string RedirectUrl { get; set; }
    public string Scope { get; set; }
    public string State { get; set; }
    
    public string URL_Auth { get; set; }
    public string URL_GetTokenViaCode { get; set; }
}