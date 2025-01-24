using AutoMapper;
using Ehsan.CSMS.Dtos;
using Ehsan.CSMS.Entities;

namespace Ehsan.CSMS;

public class CSMSApplicationAutoMapperProfile : Profile
{
    public CSMSApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        //CreateMap<Cashier, CashierDto>()
        //   //.ForMember(x=>x.CashierName,c=>c.MapFrom(s=>s.CashierName))
        //   .ReverseMap(); // if CashierName is different in the entity and the dto 
        CreateMap<Cashier, CashierDto>()
            .ReverseMap();
        CreateMap<Category, CategoryDto>()
                    .ReverseMap();
        CreateMap<Customer, CostumerDto>()
                    .ReverseMap();
        CreateMap<OrderDetail, OrderDeatilsDto>()
                   .ReverseMap();
        CreateMap<Order, OrderDto>()
                  .ReverseMap();
        CreateMap<Product, ProductDto>()
                  .ReverseMap();
    }
}
