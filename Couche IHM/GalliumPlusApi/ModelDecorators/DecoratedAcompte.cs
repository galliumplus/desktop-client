using Modeles;

namespace GalliumPlusApi.ModelDecorators
{
    public class DecoratedAcompte : Acompte
    {
        private string email;
        private int roleId;

        public string Email => email;

        public int RoleId => roleId;

        public DecoratedAcompte(
            int id,
            string identifiant,
            string nom,
            string prenom,
            float argent,
            string formation,
            bool stillAdherent,
            string email,
            int roleId
        )
        : base(id, identifiant, nom, prenom, argent, formation, stillAdherent)
        {
            this.email = email;
            this.roleId = roleId;
        }
    }
}
