namespace Shop.Common.Models
{
    using Infrastructure.Enums;

    public class UserPrincipalSerializeModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }

        public UserRoles UserRole { get; set; }
    }
}
