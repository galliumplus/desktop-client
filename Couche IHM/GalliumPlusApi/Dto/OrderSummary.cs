/*using GalliumPlus.WebApi.Core.Data;
using GalliumPlus.WebApi.Core.Exceptions;
using GalliumPlus.WebApi.Core.Orders;
using GalliumPlus.WebApi.Core.Users;
using System.ComponentModel.DataAnnotations;

namespace GalliumPlus.WebApi.Dto
{
    public class OrderSummary
    {
        [Required] public string PaymentMethod { get; set; }
        public string? Customer { get; set; }
        [Required] public List<OrderItemSummary>? Items { get; set; }

        public OrderSummary()
        {
            PaymentMethod = String.Empty;
            Items = null;
        }

        public class Mapper : Mapper<Order, OrderSummary>
        {
            private OrderItemSummary.Mapper orderItemMapper;
            private IUserDao userDao;

            public Mapper(IUserDao userDao, IProductDao productDao)
            {
                this.userDao = userDao;
                this.orderItemMapper = new(productDao);
            }

            public override OrderSummary FromModel(Order model)
            {
                // ne sort jamais du serveur !
                throw new NotImplementedException();
            }

            public override Order ToModel(OrderSummary dto)
            {
                PaymentMethodFactory factory = new(this.userDao);

                User? customer;
                if (dto.Customer == null)
                {
                    customer = null;
                }
                else if (dto.Customer == Order.ANONYMOUS_MEMBER_ID)
                {
                    customer = BuildAnonymousMember();
                }
                else
                {
                    try
                    {
                        customer = this.userDao.Read(dto.Customer);
                    }
                    catch (ItemNotFoundException)
                    {
                        throw new InvalidItemException($"L'utilisateur « {dto.Customer} » n'existe pas");
                    }
                }

                return new Order(
                    factory.Create(dto.PaymentMethod, dto.Customer),
                    dto.Items!.Select(saleItemDto => this.orderItemMapper.ToModel(saleItemDto)),
                    customer
                );
            }

            private static User BuildAnonymousMember()
            {
                return new User(
                    "anonymousmember00000000000", // pas possible d'être rentré en BDD
                    new UserIdentity("Anonyme", "", "", ""),
                    new Role(-1, "Membre anonyme", Permissions.NONE),
                    0.00m,
                    false
                );
            }
        }
    }
}
*/