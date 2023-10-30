using GalliumPlusApi.CompatibilityHelpers;
using GalliumPlusApi.ModelDecorators;
using Microsoft.VisualBasic;
using Modeles;
using System.ComponentModel.DataAnnotations;

namespace GalliumPlusApi.Dto
{
    public class UserSummary
    {
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Role { get; set; } = -1;
        public string Year { get; set; } = string.Empty;
        public decimal? Deposit { get; set; } = null;
        public bool IsMember { get; set; } = false;

        public class UserMapper : Mapper<User, UserSummary>
        {
            public override UserSummary FromModel(User user)
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

                return new UserSummary
                {
                    Id = UserIdRepository.Current.FindUsernameOf(user.ID),
                    FirstName = user.Prenom,
                    LastName = user.Nom,
                    Email = user.Mail,
                    Role = user.IdRole,
                    Year = year,
                    Deposit = deposit,
                    IsMember = isMember,
                };
            }

            public UserSummary PatchWithModel(UserDetails originialUser, User patch)
            {
                return new UserSummary
                {
                    Id = originialUser.Id,
                    FirstName = patch.Prenom,
                    LastName = patch.Nom,
                    Email = patch.Mail,
                    Role = patch.IdRole,
                    Year = originialUser.Year,
                    Deposit = originialUser.Deposit,
                    IsMember = originialUser.IsMember,
                };
            }

            public override User ToModel(UserSummary summary)
            {
                return new DecoratedUser(
                    id: UserIdRepository.Current.GetIdFor(summary.Id),
                    nom: summary.LastName,
                    prenom: summary.FirstName,
                    mail: summary.Email,
                    password: "",
                    role: summary.Role,
                    year: summary.Year,
                    deposit: summary.Deposit,
                    isMember: summary.IsMember
                );
            }
        }

        public class AcompteMapper : Mapper<Acompte, UserSummary>
        {
            public override UserSummary FromModel(Acompte model)
            {
                string email = "";
                int roleId = -1;
                if (model is DecoratedAcompte deco)
                {
                    email = deco.Email;
                    roleId = deco.RoleId;
                }

                return new UserSummary
                {
                    Deposit = Format.FloatToMonetary(model.Argent),
                    Email = email,
                    FirstName = model.Prenom,
                    Id = model.Identifiant,
                    IsMember = model.StillAdherent,
                    LastName = model.Nom,
                    Role = roleId,
                    Year = model.Formation,
                };
            }

            public UserSummary PatchWithModel(UserDetails originalUser, Acompte patch)
            {
                return new UserSummary
                {
                    Deposit = Format.FloatToMonetary(patch.Argent),
                    Email = originalUser.Email,
                    FirstName = patch.Prenom,
                    Id = patch.Identifiant,
                    IsMember = patch.StillAdherent,
                    LastName = patch.Nom,
                    Role = originalUser.Role.Id,
                    Year = patch.Formation,
                };
            }

            public override Acompte ToModel(UserSummary dto)
            {
                return new DecoratedAcompte(
                    id: UserIdRepository.Current.GetIdFor(dto.Id),
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