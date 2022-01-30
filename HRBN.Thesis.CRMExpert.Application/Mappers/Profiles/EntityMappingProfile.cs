using System.Reflection;
using AutoMapper;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Contact;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Customer;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Discount;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Order;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Permission;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Product;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Role;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Todo;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.User;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;
using HRBN.Thesis.CRMExpert.Domain.Core.Entities;

namespace HRBN.Thesis.CRMExpert.Application.Mappers.Profiles
{
    public class EntityMappingProfile : Profile
    {
        public void CreateMapForContact()
        {
            CreateMap<Contact, ContactDto>().ReverseMap();

            CreateMap<Contact, AddContactCommand>().ReverseMap();
            CreateMap<ContactDto, AddContactCommand>().ReverseMap();

            CreateMap<Contact, EditContactCommand>().ReverseMap();
            CreateMap<ContactDto, EditContactCommand>().ReverseMap();
        }

        public void CreateMapForCustomer()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();

            CreateMap<Customer, AddCustomerCommand>().ReverseMap();
            CreateMap<CustomerDto, AddCustomerCommand>().ReverseMap();

            CreateMap<Customer, EditCustomerCommand>().ReverseMap();
            CreateMap<CustomerDto, EditCustomerCommand>().ReverseMap();
        }

        public void CreateMapForDiscount()
        {
            CreateMap<Discount, DiscountDto>().ReverseMap();

            CreateMap<Discount, AddDiscountCommand>().ReverseMap();
            CreateMap<DiscountDto, AddDiscountCommand>().ReverseMap();

            CreateMap<Discount, EditDiscountCommand>().ReverseMap();
            CreateMap<DiscountDto, EditDiscountCommand>().ReverseMap();
        }

        public void CreateMapForOrder()
        {
            CreateMap<Order, OrderDto>().ReverseMap();

            CreateMap<Order, AddOrderCommand>().ReverseMap();
            CreateMap<OrderDto, AddOrderCommand>().ReverseMap();

            CreateMap<Order, EditOrderCommand>().ReverseMap();
            CreateMap<OrderDto, EditOrderCommand>().ReverseMap();
        }

        public void CreateMapForPermission()
        {
            CreateMap<Permission, PermissionDto>()
                .ForMember(p => p.Username, c => c.MapFrom(s => s.User.Username))
                .ForMember(p => p.RoleName, c => c.MapFrom(s => s.Role.Name))
                .ReverseMap();

            CreateMap<Permission, AddPermissionCommand>().ReverseMap();
            CreateMap<PermissionDto, AddPermissionCommand>().ReverseMap();

            CreateMap<Permission, EditPermissionCommand>().ReverseMap();
            CreateMap<PermissionDto, EditPermissionCommand>().ReverseMap();
        }

        public void CreateMapForProduct()
        {
            CreateMap<Product, ProductDto>().ReverseMap();

            CreateMap<Product, AddProductCommand>().ReverseMap();
            CreateMap<ProductDto, AddProductCommand>().ReverseMap();

            CreateMap<Product, EditProductCommand>().ReverseMap();
            CreateMap<ProductDto, EditProductCommand>().ReverseMap();
        }

        public void CreateMapForRole()
        {
            CreateMap<Role, RoleDto>().ReverseMap();

            CreateMap<Role, AddRoleCommand>().ReverseMap();
            CreateMap<RoleDto, AddRoleCommand>().ReverseMap();

            CreateMap<Role, EditRoleCommand>().ReverseMap();
            CreateMap<RoleDto, EditRoleCommand>().ReverseMap();
        }

        public void CreateMapForTodo()
        {
            CreateMap<Todo, TodoDto>()
                .ForMember(p => p.CustomerName, c => c.MapFrom(s => s.Contact.FirstName + " " + s.Contact.LastName))
                .ReverseMap();

            CreateMap<Todo, AddTodoCommand>().ReverseMap();
            CreateMap<TodoDto, AddTodoCommand>().ReverseMap();

            CreateMap<Todo, EditTodoCommand>().ReverseMap();
            CreateMap<TodoDto, EditTodoCommand>().ReverseMap();
        }

        public void CreateMapForUser()
        {
            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<User, AddUserCommand>().ReverseMap();
            CreateMap<UserDto, AddUserCommand>().ReverseMap();

            CreateMap<User, EditUserCommand>().ReverseMap();
            CreateMap<UserDto, EditUserCommand>().ReverseMap();
        }

        public EntityMappingProfile()
        {
            CreateMapForContact();
            CreateMapForCustomer();
            CreateMapForDiscount();
            CreateMapForOrder();
            CreateMapForPermission();
            CreateMapForProduct();
            CreateMapForRole();
            CreateMapForTodo();
            CreateMapForUser();
        }
    }
}