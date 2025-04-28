using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using BusinessEntities;
using Core.Services.Products;
using WebApi.Models.Products;

namespace WebApi.Controllers
{
    [RoutePrefix("products")]
    public class ProductController : BaseApiController
    {
        private readonly ICreateProductService _createProductService;
        private readonly IDeleteProductService _deleteProductService;
        private readonly IGetProductService _getProductService;
        private readonly IUpdateProductService _updateProductService;

        public ProductController(ICreateProductService createProductService, IDeleteProductService deleteProductService, IGetProductService getProductService, IUpdateProductService updateProductService)
        {
            _createProductService = createProductService;
            _deleteProductService = deleteProductService;
            _getProductService = getProductService;
            _updateProductService = updateProductService;
        }

        [Route("{productId:guid}/create")]
        [HttpPost]
        public HttpResponseMessage CreateProduct(Guid productId, [FromBody] ProductModel model)
        {
            var existingProduct = _getProductService.GetProduct(productId);
            if (existingProduct != null)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.Conflict, "Product already exists.");
            }

            var product = _createProductService.Create(productId, model.Name, model.Description, model.Category, model.Price, model.StockQuantity, model.Tags);
            return Found(new ProductData(product));
        }

        [Route("{productId:guid}/update")]
        [HttpPost]
        public HttpResponseMessage UpdateProduct(Guid productId, [FromBody] ProductModel model)
        {
            if (model == null)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, "Request body cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(model.Name))
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, "Name is required.");
            }

            var product = _getProductService.GetProduct(productId);
            if (product == null)
            {
                return DoesNotExist();
            }

            try
            {
                _updateProductService.Update(product, model.Name, model.Description, model.Category, model.Price, model.StockQuantity, model.Tags);
            }
            catch (Raven.Abstractions.Exceptions.ConcurrencyException)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.Conflict);
            }

            return Found(new ProductData(product));
        }

        [Route("{productId:guid}/delete")]
        [HttpDelete]
        public HttpResponseMessage DeleteProduct(Guid productId)
        {
            var product = _getProductService.GetProduct(productId);
            if (product == null)
            {
                return DoesNotExist();
            }
            _deleteProductService.Delete(product);
            return Found();
        }

        [Route("{productId:guid}")]
        [HttpGet]
        public HttpResponseMessage GetProduct(Guid productId)
        {
            var product = _getProductService.GetProduct(productId);

            if (product == null)
            {
                return DoesNotExist();
            }

            return Found(new ProductData(product));
        }

        [Route("list")]
        [HttpGet]
        public HttpResponseMessage GetProducts(int skip, int take, string category = null, string name = null, decimal? minPrice = null, decimal? maxPrice = null)
        {
            var products = _getProductService.GetProducts(category, name, minPrice, maxPrice)
                                             .Skip(skip).Take(take)
                                             .Select(q => new ProductData(q))
                                             .ToList();
            return Found(products);
        }

        [Route("clear")]
        [HttpDelete]
        public HttpResponseMessage DeleteAllProducts()
        {
            _deleteProductService.DeleteAll();
            return Found();
        }
    }
}
