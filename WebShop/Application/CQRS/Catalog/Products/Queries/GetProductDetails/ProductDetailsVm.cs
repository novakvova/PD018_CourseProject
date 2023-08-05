using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Common.Mappings;
using WebShop.Application.CQRS.Catalog.Categories.Queries.GetCategoryDetails;
using WebShop.Application.CQRS.Catalog.Categories.Queries.GetCategoryList;
using WebShop.Domain.Entities;

namespace WebShop.Application.CQRS.Catalog.Products.Queries.GetProductDetails {
    public class ProductDetailsVm : IMapWith<ProductEntity> {
        public int Id { get; set; }
        public string? Title { get; set; }
        public List<string>? Images { get; set; }
        public string? Details { get; set; }
        public float? Price { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? LastModified { get; set; }
        public CategoryLookupDto Category { get; set; } = null!;

        public void Mapping(Profile profile) {
            profile.CreateMap<ProductEntity, ProductDetailsVm>()
                .ForMember(vm => vm.Title, opt => opt.MapFrom(product => product.Title))
                .ForMember(vm => vm.Images, opt => opt.MapFrom(product => product.Images.Select(i => i.Uri)))
                .ForMember(vm => vm.Details, opt => opt.MapFrom(product => product.Details))
                .ForMember(vm => vm.Category, opt => opt.MapFrom(product => product.Category))
                .ForMember(vm => vm.Price, opt => opt.MapFrom(product => product.Price))
                .ForMember(vm => vm.Created, opt => opt.MapFrom(product => product.Created))
                .ForMember(vm => vm.LastModified, opt => opt.MapFrom(product => product.LastModified))
                .ForMember(vm => vm.Id, opt => opt.MapFrom(product => product.Id));
        }
    }
}
