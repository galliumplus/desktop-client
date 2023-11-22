using GalliumPlusApi.CompatibilityHelpers;
using GalliumPlusApi.ModelDecorators;
using Microsoft.VisualBasic;
using Modeles;
using System.ComponentModel.DataAnnotations;

namespace GalliumPlusApi.Dto
{
    public class AccountSummary
    {
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Role { get; set; } = -1;
        public string Year { get; set; } = string.Empty;
        public decimal? Deposit { get; set; } = null;
        public bool IsMember { get; set; } = false;

        public class UserMapper : Mapper<Account, AccountSummary>
        {
            public override AccountSummary FromModel(Account user)
            {
                string year = "?";
                decimal? deposit = null;
                bool isMember = true;
                if (user is DecoratedUser deco)
                {
                    year = deco.Year;
                    deposit = deco.Deposit;
                    isMember = deco.IsMember;
                }

                return new AccountSummary
                {
                    Id = user.Identifiant,
                    FirstName = user.Prenom,
                    LastName = user.Nom,
                    Email = user.Mail,
                    Role = user.RoleId,
                    Year = year,
                    Deposit = deposit,
                    IsMember = isMember,
                };
            }

            public AccountSummary PatchWithModel(AccountDetails originialUser, Account patch)
            {
                return new AccountSummary
                {
                    Id = originialUser.Id,
                    FirstName = patch.Prenom,
                    LastName = patch.Nom,
                    Email = patch.Mail,
                    Role = patch.RoleId,
                    Year = originialUser.Year,
                    Deposit = originialUser.Deposit,
                    IsMember = originialUser.IsMember,
                };
            }

            public override Account ToModel(AccountSummary summary)
            {
                return new DecoratedUser(
                    id: UserIdMapper.Current.GetIdFor(summary.Id),
                    nom: summary.LastName,
                    prenom: summary.FirstName,
                    mail: summary.Email,
                    password: "",
                    role: summary.Role,
                    year: summary.Year,
                    deposit: summary.Deposit,
                    isMember: summary.IsMember,
                    identifiant: summary.Id
                );
            }
        }

        public class AccountMapper : Mapper<Account, AccountSummary>
        {
            public override AccountSummary FromModel(Account model)
            {
                string email = "UKN";
                int roleId = SessionStorage.Current.Get<int>("adherentRoleId");


                return new AccountSummary
                {
                    Deposit = Format.FloatToMonetary(model.Argent),
                    Email = model.Mail,
                    FirstName = model.Prenom,
                    Id = model.Identifiant,
                    IsMember = model.IsMember,
                    LastName = model.Nom,
                    Role = model.RoleId,
                    Year = model.Formation,
                };
            }

            public AccountSummary PatchWithModel(AccountDetails originalUser, Account patch)
            {
                return new AccountSummary
                {
                    Deposit = Format.FloatToMonetary(patch.Argent),
                    Email = originalUser.Email,
                    FirstName = patch.Prenom,
                    Id = patch.Identifiant,
                    IsMember = patch.IsMember,
                    LastName = patch.Nom,
                    Role = originalUser.Role.Id,
                    Year = patch.Formation,
                };
            }

            public override Account ToModel(AccountSummary dto)
            {
                return new DecoratedAccount(
                    id: UserIdMapper.Current.GetIdFor(dto.Id),
                    identifiant: dto.Id,
                    nom: dto.LastName,
                    prenom: dto.FirstName,
                    argent: (float)dto.Deposit!,
                    formation: dto.Year,
                    stillAdherent: dto.IsMember,
                    email: dto.Email,
                    roleId: dto.Role
                );
            }
        }
    }
}