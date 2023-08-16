
namespace Modeles
{
    /// <summary>
    /// Produit
    /// </summary>
    public class Product
    {
        #region attributes
        private int id;
        private string nomProduit = "";
        private int quantite;
        private float prixAdherent;
        private float prixNonAdherent;
        private int categorie;
        #endregion

        #region properties

        /// <summary>
        /// Id du produit
        /// </summary>
        public int ID
        {
            get { return id; }
        }

        /// <summary>
        /// Nom du produit
        /// </summary>
        public string NomProduit 
        { 
            get => nomProduit; 
            set => nomProduit = value; 
        }

        /// <summary>
        /// Quantité du produit
        /// </summary>
        public int Quantite 
        { 
            get => quantite; 
            set => quantite = value; 
        }

        /// <summary>
        /// Prix adhérent
        /// </summary>
        public float PrixAdherent 
        { 
            get => prixAdherent; 
            set => prixAdherent = value; 
        }

      

        /// <summary>
        /// Categorie du produit
        /// </summary>
        public int Categorie 
        { 
            get => categorie; 
            set => categorie = value; 
        }

        /// <summary>
        /// Renvoie le prix non adhérent
        /// </summary>
        public float PrixNonAdherent 
        { 
            get => prixNonAdherent; 
            set => prixNonAdherent = value; 
        }


        #endregion

        #region constructors
        /// <summary>
        /// Futur constructeur naturelle
        public Product(int id, string nomProduit, int quantite, float prixAdherent, float prixNonAdherent, int categorie)
        {
            this.id = id;
            this.nomProduit = nomProduit;
            this.quantite = quantite;
            this.prixAdherent = prixAdherent;
            this.prixNonAdherent = prixNonAdherent;
            this.categorie = categorie;
            this.PrixNonAdherent = this.prixNonAdherent;
        }

        /// <summary>
        /// Constructeur vide du produit
        /// </summary>
        public Product() { }

        /// <summary>
        /// Constructeur naturelle copiant l'objet
        /// </summary>
        /// <param name="p"> produit à copier </param>
        public Product(Product p)
        {
            this.id = p.ID;
            this.nomProduit = p.NomProduit;
            this.quantite = p.Quantite;
            this.prixAdherent = p.PrixAdherent;
            this.prixNonAdherent = p.PrixNonAdherent;
            this.categorie = p.Categorie;
        }

        #endregion

        public override string ToString()
        {
            return $"{this.nomProduit} {this.quantite} {this.PrixAdherent} {categorie}";
        }
    }
}
