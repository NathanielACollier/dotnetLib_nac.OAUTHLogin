namespace nac.OAUTHLogin.model;

public class OAUTHSettings
{
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public bool ForceSelectAccount { get; set; }
    public string Tenant { get; set; }
    public string RedirectUrl { get; set; }
    public string Scope { get; set; }
    public string State { get; set; }
}