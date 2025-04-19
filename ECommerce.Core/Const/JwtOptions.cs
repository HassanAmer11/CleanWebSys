namespace ECommerce.Core.Const;

public class JwtOptions
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int TokenExpire { get; set; }
    public int RefreshTokenExpire { get; set; }
    public string SigningKey { get; set; }
    public bool validateAudience { get; set; }
    public bool validateIssuer { get; set; }
    public bool validateLifetime { get; set; }
    public bool validateIssuerSigningKey { get; set; }
}
