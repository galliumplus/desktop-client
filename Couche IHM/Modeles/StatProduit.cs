
namespace Modeles
{
    public class StatProduit
    {
        #region attributes
        private int id;
        private int product_id;
        private int number_sales;
        private DateTime date;
        #endregion

        #region constructor
        /// <summary>
        /// Constructeur du modèle statproduit
        /// </summary>
        /// <param name="id">id de la stat</param>
        /// <param name="date">date de la stat</param>
        /// <param name="number_sales">nombre de ventes</param>
        /// <param name="product_id">id du produit</param>
        public StatProduit(int id,DateTime date, int number_sales, int product_id)
        {
            this.product_id = product_id;
            this.number_sales = number_sales;
            this.id = id;
            this.date = date;
        }
        #endregion

        #region properties
        public int Product_id { get => product_id; set => product_id = value; }
        public int Number_sales { get => number_sales; set => number_sales = value; }
        public int Id { get => id; set => id = value; }
        public DateTime Date { get => date; set => date = value; }
        #endregion
    }
}
