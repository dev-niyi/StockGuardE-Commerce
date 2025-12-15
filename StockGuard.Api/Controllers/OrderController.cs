using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKenel;
using StockGuard.Application.Orders.Create;
using StockGuard.Application.Orders.Get;

namespace StockGuard.Api.Controllers;


[Route("api/orders")]
[ApiController]
public class OrdersController : ControllerBase
{
	private readonly IMediator _mediator;

	public OrdersController(IMediator mediator)
	{
		_mediator = mediator;
	}

	/// <summary>
	/// Place a new order with one or more products
	/// </summary>
	[HttpPost]
	[ProducesResponseType(typeof(CreateOrderResponse), StatusCodes.Status201Created)]
	[ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> PlaceOrder(
		[FromBody] CreateOrderCommand command,
		CancellationToken cancellationToken)
	{
		var result = await _mediator.Send(command, cancellationToken);

		if (result.IsSuccess)
		{
			return CreatedAtAction(
				nameof(GetOrderById),
				new { id = result.Value.OrderId },
				result.Value);
		}

		return BadRequest(result.Error);
	}

	/// <summary>
	/// Get order details by ID
	/// </summary>
	[HttpGet("{id}")]
	[ProducesResponseType(typeof(OrderDetailResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
	public async Task<IActionResult> GetOrderById(
		Guid id,
		CancellationToken cancellationToken)
	{
		var query = new GetOrderByIdQuery(id);
		var result = await _mediator.Send(query, cancellationToken);

		return result.IsSuccess
			? Ok(result.Value)
			: NotFound(result.Error);
	}
}