﻿using MediatR;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category ,string Description, string ImageFile, decimal Price): IRequest<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
    {
        public Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            // Bussiness logic
            throw new NotImplementedException();
        }
    }
}
