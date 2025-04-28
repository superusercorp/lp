using System;
using System.Collections.Generic;
using BusinessEntities;
using Common;
using Core.Factories;
using Data.Repositories;

namespace Core.Services.Products
{
    [AutoRegister]
    public class CreateProductService : ICreateProductService
    {
        private readonly IUpdateProductService _updateProductService;
        private readonly IIdObjectFactory<Product> _productFactory;
        private readonly IProductRepository _productRepository;

        public CreateProductService(IIdObjectFactory<Product> productFactory, IProductRepository productRepository, IUpdateProductService updateProductService)
        {
            _productFactory = productFactory;
            _productRepository = productRepository;
            _updateProductService = updateProductService;
        }

        public Product Create(Guid id, string name, string description, string category, decimal price, int stockQuantity, IEnumerable<string> tags)
        {
            var product = _productFactory.Create(id);
            _updateProductService.Update(product, name, description, category, price, stockQuantity, tags);
            _productRepository.Save(product);
            return product;
        }
    }
}
