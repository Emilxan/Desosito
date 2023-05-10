using Desosito.Domain.Enum;

namespace Desosito.Domain.Entity
{
    public class User
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public RoleEnum Role { get; set; }
    }
}
