using GalliumPlusApi.CompatibilityHelpers;
using Modeles;

namespace GalliumPlusApi.Dto
{
    public class ProductSummary
    {
        public int Id { get; set; } = -1;
        public string Name { get; set; } = "";
        public int Stock { get; set; } = 0;
        public decimal NonMemberPrice { get; set; } = -1;
        public decimal MemberPrice { get; set; } = -1;
        public string Availability { get; set; } = "";
        public int Category { get; set; } = -1;

        public class Mapper : Mapper<Product, ProductSummary>
        {
            public override ProductSummary FromModel(Product model)
            {
                return new ProductSummary
                {
                    Id = model.ID,
                    Name = model.NomProduit,
                    Stock = model.Quantite,
                    NonMemberPrice = Format.FloatToMonetary(model.PrixNonAdherent),
                    MemberPrice = Format.FloatToMonetary(model.PrixAdherent),
                    Availability = "AUTO",
                    Category = model.Categorie
                };
            }

            public override Product ToModel(ProductSummary dto)
            {
                return new Product(
                    id: dto.Id,
                    nomProduit: dto.Name,
                    quantite: dto.Stock,
                    prixAdherent: (float)dto.MemberPrice,
                    prixNonAdherent: (float)dto.NonMemberPrice,
                    categorie: dto.Category
                );
            }
        }
    }
}