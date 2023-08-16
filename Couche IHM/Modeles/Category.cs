using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeles
{
    public class Category
    {
        private int idCat;
        private string nomCategory;

        public Category(Category cat)
        {
            this.idCat = cat.idCat;
            this.nomCategory = cat.nomCategory;
        }

        public Category(int idCat, string nomCategory)
        {
            this.idCat = idCat;
            this.nomCategory = nomCategory;
        }

        public int IdCat { get => idCat; set => idCat = value; }
        public string NomCategory { get => nomCategory; set => nomCategory = value; }
    }
}
