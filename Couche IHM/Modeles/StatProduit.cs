using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeles
{
    public class StatProduit
    {
        private int product_id;
        private int number_sales;
        private int sale_id;
        private DateTime date;

        public StatProduit(int sale_id,DateTime date, int number_sales, int product_id)
        {
            this.product_id = product_id;
            this.number_sales = number_sales;
            this.sale_id = sale_id;
            this.date = date;
        }

        public int Product_id { get => product_id; set => product_id = value; }
        public int Number_sales { get => number_sales; set => number_sales = value; }
        public int Sale_id { get => sale_id; set => sale_id = value; }
        public DateTime Date { get => date; set => date = value; }
    }
}
