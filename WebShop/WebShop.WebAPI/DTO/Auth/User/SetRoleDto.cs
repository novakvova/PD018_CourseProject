using AutoMapper;
using WebShop.Application.Common.Mappings;
using WebShop.Application.CQRS.Identity.Users.Commands.SetRole;

namespace WebShop.WebAPI.DTO.Auth.User
{
    public class SetRoleDto : IMapWith<SetRoleCommand>
    {
        public int UserId { get; set; }
        public string RoleName { get; set; } = null!;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SetRoleDto, SetRoleCommand>()
                .ForMember(command => command.UserId, opt => opt.MapFrom(dto => dto.UserId))
                .ForMember(command => command.RoleName, opt => opt.MapFrom(dto => dto.RoleName));
        }
    }
}