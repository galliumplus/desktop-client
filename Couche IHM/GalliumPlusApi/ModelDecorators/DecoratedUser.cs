using Modeles;

namespace GalliumPlusApi.ModelDecorators
{
    internal class DecoratedUser : User
    {
        private string year;
        private decimal? deposit;
        private bool isMember;

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
            bool isMember
        )
        : base(id, nom, prenom, mail, password, role)
        {
            this.year = year;
            this.deposit = deposit;
            this.isMember = isMember;
        }
    }
}
