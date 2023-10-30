/*using GalliumPlus.WebApi.Core.Data;
using GalliumPlus.WebApi.Core.Stocks;

namespace GalliumPlus.WebApi.Dto
{
    public class ProductDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal NonMemberPrice { get; set; }
        public decimal MemberPrice { get; set; }
        public Availability Availability { get; set; }
        public bool Available { get; set; }
        public CategoryDetails Category { get; set; }

        public ProductDetails(
            int id,
            string name,
            int stock,
            decimal nonMemberPrice,
            decimal memberPrice,
            Availability availability,
            bool available,
            CategoryDetails category)
        {
            this.Id = id;
            this.Name = name;
            this.Stock = stock;
            this.NonMemberPrice = nonMemberPrice;
            this.MemberPrice = memberPrice;
            this.Availability = availability;
            this.Available = available;
            this.Category = category;
        }

        public class Mapper : Mapper<Product, ProductDetails>
        {
            private CategoryDetails.Mapper categoryMapper = new();

            public override ProductDetails FromModel(Product model)
            {
                return new ProductDetails(
                    model.Id,
                    model.Name,
                    model.Stock,
                    model.NonMemberPrice,
                    model.MemberPrice,
                    model.Availability,
                    model.Available,
                    this.categoryMapper.FromModel(model.Category)
                );
            }

            public override Product ToModel(ProductDetails dto)
            {
                return new Product(
                    dto.Id,
                    dto.Name,
                    dto.Stock,
                    dto.NonMemberPrice,
                    dto.MemberPrice,
                    dto.Availability,
                    this.categoryMapper.ToModel(dto.Category)
                );
            }
        }
    }
}
*/