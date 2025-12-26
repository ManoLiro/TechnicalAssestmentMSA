using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechnicalAssestmentMSA.API.Models.Clientes;
using TechnicalAssestmentMSA.Application.Clientes.Commands;
using TechnicalAssestmentMSA.Application.Clientes.Queries;
using TechnicalAssestmentMSA.Application.Repositories;

namespace TechnicalAssestmentMSA.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>Cria um novo cliente.</summary>
        [HttpPost]
        [ProducesResponseType(typeof(CriarClienteResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CriarClienteRequest request, CancellationToken ct)
        {
            try
            {
                var comando = new CriaClienteCommand(request.NomeFantasia, request.Cnpj, request.Ativo);
                var resultado = await _mediator.Send(comando, ct);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }           
        }

        /// <summary>Obtém um cliente por Id.</summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPorId([FromRoute] Guid id, CancellationToken ct)
        {
            var cliente = await _mediator.Send(new ObtemClientePorIdQuery(id), ct);

            if (cliente is null)
                return NotFound();

            return Ok(cliente);
        }
    }
}
