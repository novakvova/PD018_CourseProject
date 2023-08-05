using AutoMapper;
using WebShop.Application.Common.Mappings;
using WebShop.Application.CQRS.Catalog.Categories.Commands.CreateCategory;
using WebShop.Application.CQRS.Identity.Users.Commands.CreateUser;
using WebShop.Dto.Catalog.Category;

namespace WebShop.WebAPI.DTO.Auth.User
{
    public class SignUpDto : IMapWith<CreateUserCommand>
    {
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SignUpDto, CreateUserCommand>()
                .ForMember(command => command.Password, opt => opt.MapFrom(dto => dto.Password))
                .ForMember(command => command.Email, opt => opt.MapFrom(dto => dto.Email))
                .ForMember(command => command.FirstName, opt => opt.MapFrom(dto => dto.FirstName))
                .ForMember(command => command.LastName, opt => opt.MapFrom(dto => dto.LastName));
        }
    }
}
