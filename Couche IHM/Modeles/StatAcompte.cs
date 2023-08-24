using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeles
{
    public class StatAcompte
    {
        private int acompte_id;
        private float amount_money;
        private int sale_id;
        private DateTime date;

        public StatAcompte(int sale_id,DateTime date, float argent, int acompte_id)
        {
            this.acompte_id = acompte_id;
            this.amount_money = argent;
            this.sale_id = sale_id;
            this.date = date;
        }

        public int Aompte_Id { get => acompte_id; set => acompte_id = value; }
        public float Amount_money { get => amount_money; set => amount_money = value; }
        public int Sale_id { get => sale_id; set => sale_id = value; }
        public DateTime Date { get => date; set => date = value; }
    }
}
