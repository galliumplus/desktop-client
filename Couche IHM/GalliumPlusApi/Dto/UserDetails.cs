using GalliumPlusApi.CompatibilityHelpers;
using Modeles;
using System.Text.Json.Serialization;

namespace GalliumPlusApi.Dto
{
    public class UserDetails
    {
        public string Id { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public RoleDetails Role { get; set; } = new();
        public string Year { get; set; } = "";
        public decimal? Deposit { get; set; } = null;
        public bool IsMember { get; set; } = false;

        [JsonIgnore]
        public UserSummary AsUserSummary => new()
        {
            Deposit = Deposit,
            Email = Email,
            FirstName = FirstName,
            LastName = LastName,
            Id = Id,
            IsMember = IsMember,
            Role = Role.Id,
            Year = Year,
        };

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