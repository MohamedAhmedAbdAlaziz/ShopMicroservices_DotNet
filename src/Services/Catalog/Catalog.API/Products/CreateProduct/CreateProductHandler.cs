﻿using BuildingBlocks.CQRS;
using Catalog.API.Models;
using FluentValidation;
using Marten;
using MediatR;
using System.Windows.Input;

namespace Catalog.API.Products.CreateProduct
{

    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Price is required");
        }

        public class CreateProductHandler(IDocumentSession documentSession, IValidator<CreateProductCommand> validator) : ICommandHandler<CreateProductCommand, CreateProductResult>
        {
            public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
            {
                var result = await validator.ValidateAsync(command, cancellationToken);
                var errors = result.Errors.Select(x => x.ErrorMessage).ToList();
                if (errors.Any())
                {
                    throw new ValidationException(errors.FirstOrDefault());
                }
                var product = new Product
                {
                    Name = command.Name,
                    Category = command.Category,
                    Description = command.Description,
                    ImageFile = command.ImageFile,
                    Price = command.Price,
                };


                documentSession.Store(product);
                await documentSession.SaveChangesAsync(cancellationToken);
                return new CreateProductResult(product.Id);



            }
        }
    }
}
