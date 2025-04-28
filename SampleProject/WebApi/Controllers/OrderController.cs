using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using BusinessEntities;
using Core.Services.Orders;
using WebApi.Models.Orders;

namespace WebApi.Controllers
{
    [RoutePrefix("orders")]
    public class OrderController : BaseApiController
    {
        private readonly ICreateOrderService _createOrderService;
        private readonly IDeleteOrderService _deleteOrderService;
        private readonly IGetOrderService _getOrderService;
        private readonly IUpdateOrderService _updateOrderService;

        public OrderController(ICreateOrderService createOrderService, IDeleteOrderService deleteOrderService, IGetOrderService getOrderService, IUpdateOrderService updateOrderService)
        {
            _createOrderService = createOrderService;
            _deleteOrderService = deleteOrderService;
            _getOrderService = getOrderService;
            _updateOrderService = updateOrderService;
        }

        [Route("{orderId:guid}/create")]
        [HttpPost]
        public HttpResponseMessage CreateOrder(Guid orderId, [FromBody] OrderModel model)
        {
            var order = _createOrderService.Create(orderId, model.CustomerName, model.OrderDate, model.Status, model.Items);
            return Found(new OrderData(order));
        }

        [Route("{orderId:guid}/update")]
        [HttpPost]
        public HttpResponseMessage UpdateOrder(Guid orderId, [FromBody] OrderModel model)
        {
            if (model == null)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, "Request body cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(model.CustomerName))
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, "Customer name is required.");
            }

            var order = _getOrderService.GetOrder(orderId);
            if (order == null)
            {
                return DoesNotExist();
            }

            try
            {
                _updateOrderService.Update(order, model.CustomerName, model.OrderDate, model.Status, model.Items);
            }
            catch (Raven.Abstractions.Exceptions.ConcurrencyException)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.Conflict);
            }

            return Found(new OrderData(order));
        }

        [Route("{orderId:guid}/delete")]
        [HttpDelete]
        public HttpResponseMessage DeleteOrder(Guid orderId)
        {
            var order = _getOrderService.GetOrder(orderId);
            if (order == null)
            {
                return DoesNotExist();
            }
            _deleteOrderService.Delete(order);
            return Found();
        }

        [Route("{orderId:guid}")]
        [HttpGet]
        public HttpResponseMessage GetOrder(Guid orderId)
        {
            var order = _getOrderService.GetOrder(orderId);

            if (order == null)
            {
                return DoesNotExist();
            }

            return Found(new OrderData(order));
        }

        [Route("list")]
        [HttpGet]
        public HttpResponseMessage GetOrders(int skip, int take, DateTime? fromDate = null, DateTime? toDate = null, string customerName = null, string status = null)
        {
            var orders = _getOrderService.GetOrders(fromDate, toDate, customerName, status)
                                         .Skip(skip).Take(take)
                                         .Select(q => new OrderData(q))
                                         .ToList();
            return Found(orders);
        }

        [Route("clear")]
        [HttpDelete]
        public HttpResponseMessage DeleteAllOrders()
        {
            _deleteOrderService.DeleteAll();
            return Found();
        }
    }
}
