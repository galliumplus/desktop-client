using Modeles;

namespace GalliumPlusApi.ModelDecorators
{
    internal class DecoratedUser : Account
    {
        private string year;
        private decimal? deposit;
        private bool isMember;
        private string identifiant;

        public string Identifiant => identifiant;
        public string Year => year;

        public decimal? Deposit => deposit;

        public bool IsMember => isMember;

        public DecoratedUser(
            int id,
            string nom,
            string prenom,
            string mail,
            string password,
            int role,
            string year,
            decimal? deposit,
            bool isMember,
            string identifiant
        )
        : base(id, nom, prenom, mail, password, role,identifiant)
        {
            this.year = year;
            this.deposit = deposit;
            this.isMember = isMember;
        }
    }
}
