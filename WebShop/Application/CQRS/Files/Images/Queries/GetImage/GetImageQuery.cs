namespace WebShop.Application.CQRS.Files.Images.Queries.GetImage {
    public class GetImageQuery : IRequest<GetImageQueryResult> {
        public string? Context { get; set; }
        public string? FileName { get; set; }
    }
}
