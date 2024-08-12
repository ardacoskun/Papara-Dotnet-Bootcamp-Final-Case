using AutoMapper;
using Para.Schema.Entities.DTOs.Cart;
using Para.Schema.Entities.DTOs.Category;
using Para.Schema.Entities.DTOs.Coupon;
using Para.Schema.Entities.DTOs.Order;
using Para.Schema.Entities.DTOs.Payment;
using Para.Schema.Entities.DTOs.Product;
using Para.Schema.Entities.DTOs.Report;
using Para.Schema.Entities.DTOs.User;
using Para.Schema.Entities.Models;

namespace Para.Business.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDTO>().ReverseMap();

        CreateMap<UserRegisterDTO, User>()
              .ForMember(dest => dest.Id, opt => opt.Ignore())
              .ForMember(dest => dest.WalletBalance, opt => opt.MapFrom(src => 0))
              .ForMember(dest => dest.PointsBalance, opt => opt.MapFrom(src => 0))
              .ReverseMap();

        // Product Mapping
        CreateMap<Product, ProductDto>()
                  .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                  .ReverseMap();
        CreateMap<CreateProductDto, Product>().ReverseMap();

        CreateMap<Category, CategoryDto>()
               .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products ?? new List<Product>()))
               .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
               .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
               .ReverseMap();

        CreateMap<CreateCategoryDto, Category>()
              .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
              .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
              .ReverseMap();

        // Cart Mapping
        CreateMap<Product, CartProductDto>()
           .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
           .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
           .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
           .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
           .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

        CreateMap<CartItem, CartProductDto>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Product.Description))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Product.CategoryId))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Product.Category.Name))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));

        CreateMap<Cart, CartDTO>()
            .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.CartItems))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.CartItems.Sum(ci => ci.Quantity * ci.Product.Price)));

        // Payment Mapping
        CreateMap<Payment, PaymentDTO>()
            .ForMember(dest => dest.DiscountedAmount, opt => opt.MapFrom(src => src.DiscountedAmount))
            .ForMember(dest => dest.CouponAmount, opt => opt.MapFrom(src => src.CouponAmount))
            .ForMember(dest => dest.PointsUsed, opt => opt.MapFrom(src => src.PointsUsed))
            .ForMember(dest => dest.CouponName, opt => opt.MapFrom(src => src.CouponName ?? ""))
            .ReverseMap();

        CreateMap<PaymentDetail, PaymentItemDTO>().ReverseMap();

        CreateMap<Coupon, CouponDTO>().ReverseMap();
        CreateMap<CreateCouponDTO, Coupon>();

        // Order Mapping
        CreateMap<Order, OrderDTO>()
     .ForMember(dest => dest.Amount, opt => opt.Ignore()) 
     .ForMember(dest => dest.DiscountedAmount, opt => opt.MapFrom(src => src.TotalAmount))
     .ReverseMap();
        CreateMap<OrderDetail, OrderDetailDTO>().ReverseMap();
        CreateMap<CreateOrderDTO, Order>().ReverseMap();
        CreateMap<OrderDetailDTO, OrderDetail>().ReverseMap();

        CreateMap<Order, UserOrderSummaryDTO>().ReverseMap();
    }
}
