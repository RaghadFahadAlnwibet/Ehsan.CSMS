using AutoMapper;
using Ehsan.CSMS.Dtos.CashierDto;
using Ehsan.CSMS.Dtos.CategoryDto;
using Ehsan.CSMS.Dtos.CustomerDto;
using Ehsan.CSMS.Dtos.InvoiceDto;
using Ehsan.CSMS.Dtos.OrderDetailsDto;
using Ehsan.CSMS.Dtos.OrderDto;
using Ehsan.CSMS.Dtos.ProductDto;
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

        // Order Map
        //CreateMap<Order, OrderAddRequest>()
        //    .ReverseMap();
        //CreateMap<Order, OrderResponse>()
        //    .ForMember(or => or.InvoiceId, o=> o.MapFrom(o=> o.Invoice.Id))
        //    .ReverseMap();  


        // Cashier 
        CreateMap<Cashier, CashierAddRequest>()
            .ReverseMap();
        CreateMap<Cashier, CashierUpdateRequest>()
             .ReverseMap();
        CreateMap<Cashier, CashierResponse>()
            .ReverseMap();

        // Category 
        CreateMap<Category, CategoryAddRequest>()
            .ReverseMap();
        CreateMap<Category, CategoryUpdateRequest>()
            .ReverseMap();
        CreateMap<Category, CategoryResponse>()
              .ReverseMap();

        // Customer
        CreateMap<Customer, CustomerAddRequest>()
            .ReverseMap();
        CreateMap<Customer, CustomerUpdateRequest>()
            .ReverseMap();
        CreateMap<Customer, CustomerResponse>()
            .ReverseMap();
        CreateMap<CustomerUpdateRequest, CustomerAddRequest>()
                .ReverseMap();



        // Invoice
        CreateMap<Invoice, InvoiceResponse>()
            .ReverseMap();

        // OrderDetails
        CreateMap<OrderDetail, OrderDetailsAddRequest>()
            .ReverseMap();

        CreateMap<OrderDetail, OrderDetailsUpdateRequest>()
              .ReverseMap();
        CreateMap<OrderDetail, OrderDetailsResponse>()
             .ReverseMap();

        // Order
        CreateMap<Order, OrderAddRequest>()
             .ForMember(o => o.OrderDetailsRequests, or => or.MapFrom(src => src.OrderDetails))
             .ReverseMap();

        CreateMap<Order, OrderUpdateRequest>()
            .ForMember(o => o.OrderDetailsUpdateRequest, or => or.MapFrom(src => src.OrderDetails))
            .ReverseMap();

        CreateMap<Order, OrderResponse>()
            .ForMember(or => or.OrderDetailsResponses, opt => opt.MapFrom(src => src.OrderDetails))
            .ForMember(dest => dest.Invoice, opt => opt.MapFrom(src => src.Invoice))
            .ReverseMap();

        // Product 
        CreateMap<Product, ProductAddRequest>()
            .ReverseMap();
        CreateMap<Product, ProductUpdateRequest>()
            .ReverseMap();
        CreateMap<Product, ProductResponse>()
            .ReverseMap();


    }
}
