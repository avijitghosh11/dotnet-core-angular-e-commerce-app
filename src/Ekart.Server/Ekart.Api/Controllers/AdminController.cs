﻿using Ekart.Api.DTOs;
using Ekart.Api.Extensions;
using Ekart.Core.Entites.OrderAggregate;
using Ekart.Core.Interfaces;
using Ekart.Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ekart.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController(IUnitOfWork unit, IPaymentService paymentService) : BaseApiController
    {
        [HttpGet("orders")]
        public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetOrders([FromQuery] OrderSpecParams specParams)
        {
            var spec = new OrderSpecification(specParams);

            return await CreatePagedResult(unit.Repository<Order>(), spec, specParams.PageIndex,
                specParams.PageSize, o => o.ToDto());
        }

        [HttpGet("orders/{id:int}")]
        public async Task<ActionResult<OrderDto>> GetOrderById(int id)
        {
            var spec = new OrderSpecification(id);

            var order = await unit.Repository<Order>().GetWithSpec(spec);

            if (order == null) return BadRequest("No order with that id");

            return order.ToDto();
        }

        [HttpPost("orders/refund/{id:int}")]
        public async Task<ActionResult<OrderDto>> RefundOrder(int id)
        {
            var spec = new OrderSpecification(id);

            var order = await unit.Repository<Order>().GetWithSpec(spec);

            if (order == null) return BadRequest("No order with that id");

            if (order.Status == OrderStatus.Pending)
                return BadRequest("Payment not received for this order");

            var result = await paymentService.RefundPayment(order.PaymentIntentId);

            if (result == "succeeded")
            {
                order.Status = OrderStatus.Refunded;

                await unit.Complete();

                return order.ToDto();
            }

            return BadRequest("Problem refunding order");
        }
    }
}
