namespace Desosito
{
    public class JWTSettings
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audienct { get; set; }
    }
}
