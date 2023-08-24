using Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Couche_IHM.VueModeles
{
    public class PodiumProduit
    {
        private ProductViewModel productViewModel;
        private int purchaseCount;

        public PodiumProduit(StatProduit stat,ProductViewModel product)
        {
            this.purchaseCount = stat.Number_sales;
            this.productViewModel = product;
        }

        public ProductViewModel ProductViewModel { get => productViewModel; set => productViewModel = value; }
        public int PurchaseCount { get => purchaseCount; set => purchaseCount = value; }
    }
}
