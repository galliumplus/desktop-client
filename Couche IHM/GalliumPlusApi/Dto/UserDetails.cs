using GalliumPlusApi.CompatibilityHelpers;
using Modeles;
using System.Text.Json.Serialization;

namespace GalliumPlusApi.Dto
{
    public class AccountDetails
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
        public AccountSummary AsUserSummary => new()
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

        public class Mapper : Mapper<Account, AccountDetails>
        {
            public override Account ToModel(AccountDetails details)
            {
                return new Account(
                    UserIdMapper.Current.GetIdFor(details.Id),
                    details.Id,
                    details.LastName,
                    details.FirstName,
                    details.Email,
                    0.00f,
                    details.Year,
                    details.IsMember,
                    details.Role.Id
                );
            }
        }
    }
}