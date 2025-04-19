namespace ECommerce.Application.Helpers;

public class JwtOptions
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int TokenExpire { get; set; }
    public string SigningKey { get; set; }

}
