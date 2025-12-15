using MediatR;
using SharedKenel;
using Microsoft.AspNetCore.Mvc;
using StockGuard.Application.Products.Create;
using StockGuard.Application.Products.Delete;
using StockGuard.Application.Products.Get;
using StockGuard.Application.Products.GetById;
using StockGuard.Application.Products.Update;

namespace StockGuard.Api.Controllers
{
	[Route("api/products")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IMediator _mediator;

		public ProductController(IMediator mediator)
		{
			_mediator = mediator;
		}

		/// <summary>
		/// Create product
		/// </summary>
		[HttpPost]
		[ProducesResponseType(typeof(Result), StatusCodes.Status201Created)]
		[ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
		//[ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command, CancellationToken cancellationToken)
		{
			var result = await _mediator.Send(command, cancellationToken);

			if (result.IsSuccess)
			{
				return Ok(result.Value);
			}
			return BadRequest(result.Error);
		}


		/// <summary>
		/// Get a product by the product id
		/// </summary>
		[HttpGet("{id}")]
		[ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(Error), (StatusCodes.Status400BadRequest))]
		[ProducesResponseType(typeof(Error), (StatusCodes.Status404NotFound))]
		public async Task<IActionResult> GetProductById(Guid id, CancellationToken cancellationToken)
		{
			var query = new GetProductByIdQuery(id);

			var result = await _mediator.Send(query, cancellationToken);
			return result.IsSuccess
				? Ok(result.Value)
				: NotFound(result.Error);
		}


		/// <summary>
		/// Returns a result of all the products
		/// </summary>
		[HttpGet()]
		[ProducesResponseType(typeof(ProductsResponse), (StatusCodes.Status200OK))]
		[ProducesResponseType(typeof(Error), (StatusCodes.Status400BadRequest))]
		[ProducesResponseType(typeof(Error), (StatusCodes.Status404NotFound))]
		public async Task<IActionResult> GetAllProductsAsync(CancellationToken cancellationToken)
		{
			var query = new GetProductsQuery();
			var result = await _mediator.Send(query, cancellationToken);
			return result.IsSuccess
				? Ok(result.Value)
				: NotFound(result.Error);
		}

		/// <summary>
		/// Updates a single product
		/// </summary>
		[HttpPut("{id}")]
		[ProducesResponseType(typeof(UpdateProductResponse), (StatusCodes.Status200OK))]
		[ProducesResponseType(typeof(Error), (StatusCodes.Status404NotFound))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> UpdateProductAsync(Guid id, [FromBody] UpdateProductCommand command, CancellationToken cancellationToken)
		{
			var updatedCommand = command with { ProductId = id };
			var result = await _mediator.Send(updatedCommand, cancellationToken);
			return result.IsSuccess
				? Ok(result.Value)
				: BadRequest(result.Error);
		}

		/// <summary>
		/// Deletes a single product
		/// </summary>
		[HttpDelete("{id}")]
		[ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(Error), (StatusCodes.Status400BadRequest))]
		[ProducesResponseType(typeof(Error), (StatusCodes.Status404NotFound))]
		public async Task<IActionResult> DeleteProductAsync(Guid id, CancellationToken cancellationToken)
		{
			var deleteProduct = new DeleteProductCommand(id);
			var result = await _mediator.Send(deleteProduct, cancellationToken);
			return result.IsSuccess
				? Ok(result)
				: BadRequest(result.Error);
		}
	}
}

