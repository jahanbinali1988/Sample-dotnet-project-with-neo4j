using Sample.Api.Models;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sample.Api.Models.Currency;
using Sample.Application.Contract.Currency;
using Sample.Application.Contract.Currency.Command;
using Sample.Application.Contract.Currency.Query;

namespace Sample.Api.Controllers
{
    [ApiController]
    [Route("currency")]
    public class CurrencyController : BaseController
    {
        private readonly ILogger<CurrencyController> _logger;
        private readonly IMediator _mediator;
        public CurrencyController(IMediator mediator, ILogger<CurrencyController> logger) : base(logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<CurrencyViewModel>> CreateAsync([FromBody] CreateOrUpdateCurrencyRequest request)
        {
            var command = new CreateCurrencyCommand(request.Name);
            var currency = await _mediator.Send(command);

            return Created(currency.Adapt<CurrencyViewModel>());
        }

        [HttpPut("{currencyId}")]
        public async Task<ActionResult<CurrencyViewModel>> UpdateAsync([FromQuery]Guid currencyId, [FromBody] CreateOrUpdateCurrencyRequest request)
        {
            var updateCurrencyCommand = new UpdateCurrencyCommand(currencyId, request.Name);
            await _mediator.Send(updateCurrencyCommand);

            var query = new GetCurrencyByIdQuery(currencyId);
            var currrency = await _mediator.Send(query);

            return currrency.Adapt<CurrencyViewModel>();
        }

        [HttpDelete("{currencyId}")]
        public async Task<ActionResult<CurrencyViewModel>> DeleteAsync([FromBody] Guid currencyId)
        {
            var command = new DeleteCurrencyCommand(currencyId);
            await _mediator.Send(command);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CurrencyViewModel>>> GetCurrenciesListAsync([FromQuery] GetListRequest request)
        {
            var query = new GetCurrenciesListQuery(request.Offset, request.Count);

            var currencies = await _mediator.Send(query);

            return List<CurrencyResponseDto, CurrencyViewModel>(currencies);
        }

        [HttpGet("{currencyId}")]
        public async Task<ActionResult<CurrencyViewModel>> GetCurrencyByIdAsync([FromQuery] Guid currencyId)
        {
            var query = new GetCurrencyByIdQuery(currencyId);

            var currency = await _mediator.Send(query);

            return currency.Adapt<CurrencyViewModel>();
        }

        [HttpPatch("{currencyId}/rate")]
        public async Task<ActionResult<CurrencyRateViewModel>> AddRateAsync([FromQuery] Guid currencyId, [FromBody] AddRateToCurrencyRequest request)
        {
            var command = new AddRateToCurrencyCommand(currencyId, request.DestinationCurrencyId, request.Rate);
            var currrencyRate = await _mediator.Send(command);

            return Created(currrencyRate.Adapt<CurrencyRateViewModel>());
        }

        [HttpDelete("{currencyId}/rate/{destinationCurrencyId}")]
        public async Task<ActionResult<CurrencyRateViewModel>> DeleteRateAsync([FromQuery] Guid currencyId, [FromQuery] Guid destinationCurrencyId)
        {
            var command = new DeleteCurrencyRateCommand(currencyId, destinationCurrencyId);
            await _mediator.Send(command);

            return Ok();
        }

        [HttpGet("{currencyId}/rates")]
        public async Task<ActionResult<IEnumerable<CurrencyRateViewModel>>> GetCurrencyRatesListAsync([FromRoute] Guid currencyId, [FromQuery] GetListRequest request)
        {
            var query = new GetCurrencyRatesListQuery(currencyId, request.Offset, request.Count);

            var currencyRates = await _mediator.Send(query);

            return List<CurrencyRateResponseDto, CurrencyRateViewModel>(currencyRates);
        }

        [HttpGet("{currencyId}/amount")]
        public async Task<ActionResult<decimal>> ConvertAmount([FromRoute] Guid currencyId, [FromQuery] ConvertAmountRequest request)
        {
            var query = new ConvertAmountQuery(currencyId, request.DestinationCurrencyId, request.Amount);

            var amount = await _mediator.Send(query);

            return amount;
        }
    }
}
