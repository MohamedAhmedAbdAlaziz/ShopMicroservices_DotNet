using BuidingBlocks.CQRS;
using Carter;
using Mapster;
using MediatR;

namespace Catalog.API.Products
{
    public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<CreateProductResult>;

    public record CreateProductResponse(Guid Id);

    public class CreateProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.Map("/products",
               async (CreateProductRequest request, ISender sender) =>
               {
                   var command = request.Adapt<CreateProductCommand>();
                   var result = await sender.Send(command);
                   var response = result.Adapt<CreateProductResponse>();
                   return Results.Created($"/products/{response.Id}", response);
               }).WithName("CreateProduct")
                .Produces<CreateProductResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create Product")
                .WithDescription("Create Product");

        }
    }
}
