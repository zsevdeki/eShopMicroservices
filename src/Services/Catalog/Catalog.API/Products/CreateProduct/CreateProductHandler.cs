
using Microsoft.Extensions.Logging;

namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);

public class CreateCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Image File is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");

    }
}
public class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        //logger.LogInformation("UpdateProductCommandHandler.Handle called with {@Command}", command);

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


        // Save to db

        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);


        // return CreateProductResult as a result

        return new CreateProductResult(product.Id);







    }
}
