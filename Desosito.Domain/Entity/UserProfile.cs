using Desosito.Domain.Enum;

namespace Desosito.Domain.Entity
{
    public class UserProfile
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? StatusText { get; set; }
    }
}
