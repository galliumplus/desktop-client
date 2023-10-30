/*using GalliumPlus.WebApi.Core.Data;
using GalliumPlus.WebApi.Core.Orders;
using System.ComponentModel.DataAnnotations;

namespace GalliumPlus.WebApi.Dto
{
    public class OrderItemSummary
    {
        [Required] public int? Product { get; set; }
        [Required] public int? Quantity { get; set; }

        public class Mapper : Mapper<OrderItem, OrderItemSummary>
        {
            private IProductDao productDao;

            public Mapper(IProductDao productDao)
            {
                this.productDao = productDao;
            }

            public override OrderItemSummary FromModel(OrderItem model)
            {
                // ne sort jamais du serveur !
                throw new NotImplementedException();
            }

            public override OrderItem ToModel(OrderItemSummary dto)
            {
                return new OrderItem(this.productDao.Read(dto.Product!.Value), dto.Quantity!.Value);
            }
        }
    }
}
*/