using Desosito.Domain.Enum;

namespace Desosito.Domain.ViewModel
{
    public class UserProfileVM
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? Status { get; set; }

    }
}
