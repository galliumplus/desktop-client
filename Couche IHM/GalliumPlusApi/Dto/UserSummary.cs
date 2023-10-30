/*using GalliumPlus.WebApi.Core.Data;
using GalliumPlus.WebApi.Core.Exceptions;
using GalliumPlus.WebApi.Core.Users;
using System.ComponentModel.DataAnnotations;

namespace GalliumPlus.WebApi.Dto
{
    public class UserSummary
    {
        [Required] public string Id { get; set; }
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }
        [Required] public string Email { get; set; }
        [Required] public int? Role { get; set; }
        [Required] public string Year { get; set; }
        public decimal? Deposit { get; set; }
        [Required] public bool? IsMember { get; set; }

        public UserSummary()
        {
            this.Id = String.Empty;
            this.FirstName = String.Empty;
            this.LastName = String.Empty;
            this.Email = String.Empty;
            this.Role = null;
            this.Year = String.Empty;
            this.Deposit = null;
            this.IsMember = null;
        }

        public class Mapper : Mapper<User, UserSummary>
        {
            private IRoleDao roleDao;

            public Mapper(IRoleDao roleDao)
            {
                this.roleDao = roleDao;
            }


            public override UserSummary FromModel(User user)
            {
                return new UserSummary
                {
                    Id = user.Id,
                    FirstName = user.Identity.FirstName,
                    LastName = user.Identity.LastName,
                    Email = user.Identity.Email,
                    Role = user.Role.Id,
                    Year = user.Identity.Year,
                    Deposit = user.Deposit,
                    IsMember = user.IsMember
                };
            }

            public override User ToModel(UserSummary summary)
            {
                Role role;
                try
                {
                    role = this.roleDao.Read(summary.Role!.Value);
                }
                catch (ItemNotFoundException)
                {
                    throw new InvalidItemException("Le rôle associé n'existe pas");
                }

                return new User(
                    summary.Id!,
                    new UserIdentity(summary.FirstName!, summary.LastName!, summary.Email!, summary.Year!),
                    role,
                    summary.Deposit!.Value,
                    summary.IsMember!.Value
                );
            }
        }
    }
}
*/