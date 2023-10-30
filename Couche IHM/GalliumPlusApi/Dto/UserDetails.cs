using GalliumPlusApi.CompatibilityHelpers;
using Modeles;

namespace GalliumPlusApi.Dto
{
    public class UserDetails
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public RoleDetails Role { get; set; }
        public string Year { get; set; }
        public decimal? Deposit { get; set; }
        public bool IsMember { get; set; }

        public UserDetails()
        {
            Id = "";
            FirstName = "";
            LastName = "";
            Email = "";
            Role = new();
            Year = "";
            Deposit = null;
            IsMember = false;
        }

        public class Mapper : Mapper<User, UserDetails>
        {
            public override User ToModel(UserDetails details)
            {
                return new User(
                    UserIdRepository.Current.GetIdFor(details.Id),
                    details.LastName,
                    details.FirstName,
                    details.Email,
                    "",
                    details.Role.Id
                );
            }
        }
    }
}