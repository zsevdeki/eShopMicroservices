
namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name, List<string> Category ,string Description, string ImageFile, decimal Price): ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);
public class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        // Bussiness logic

        // Create Product entity from Command object

        var product = new Product
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price,

        };


        // Save to db (skip this step)

        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);


        // return CreateProductResult as a result

        return new CreateProductResult(product.Id);





    
    
    }
}
