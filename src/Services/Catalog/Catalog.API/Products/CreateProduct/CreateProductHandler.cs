﻿namespace Catalog.API.Products.CreateProduct
{
    //Represents data needed to create a new product
    public record CreateProductCommand(string Name,
        List<string> Category,
        string Description,
        string ImageFile,
        decimal Price) : ICommand<CreateProductResult>;

    //Represents the response object
    public record CreateProductResult(Guid Id);

    internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            //Business logic to create a new product

            //create Product entity from command object
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };

            //TODO
            //save to database
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            //return CreateProductResult object
            return new CreateProductResult(product.Id);
        }
    }
}
