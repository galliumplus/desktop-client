using Modeles;

namespace GalliumPlusApi.ModelDecorators
{
    public class DecoratedAccount : Account
    {


        public DecoratedAccount(
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
        : base(id, identifiant, nom, prenom, email,argent, formation, stillAdherent,roleId)
        {

        }
    }
}
