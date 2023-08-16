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
        private bool visible;

        public Category(Category cat)
        {
            this.idCat = cat.idCat;
            this.nomCategory = cat.nomCategory;
            this.visible = cat.Visible;
        }

        public Category(int idCat, string nomCategory,bool visible)
        {
            this.idCat = idCat;
            this.nomCategory = nomCategory;
            this.visible=visible;
        }

        public int IdCat { get => idCat; set => idCat = value; }
        public string NomCategory { get => nomCategory; set => nomCategory = value; }
        public bool Visible { get => visible; set => visible = value; }
    }
}
