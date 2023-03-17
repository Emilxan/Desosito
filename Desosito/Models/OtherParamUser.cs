namespace Desosito.Models
{
    public class OtherParamUser : ParamUser
    {
        public string UserName { get; set; }

        public bool SeniorManager { get; set; } = false;

        public RoleEnum Role { get; set; }
    }
}
