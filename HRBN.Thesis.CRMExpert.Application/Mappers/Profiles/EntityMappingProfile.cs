using AutoMapper;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Contact;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Order;
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

        public void CreateMapForOrder()
        {
            CreateMap<Order, OrderDto>().ReverseMap();

            CreateMap<Order, AddOrderCommand>().ReverseMap();
            CreateMap<OrderDto, AddOrderCommand>().ReverseMap();

            CreateMap<Order, EditOrderCommand>().ReverseMap();
            CreateMap<OrderDto, EditOrderCommand>().ReverseMap();
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
            CreateMap<Todo, TodoDto>().ReverseMap();

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
            CreateMapForOrder();
            CreateMapForRole();
            CreateMapForTodo();
            CreateMapForUser();
        }
    }
}
