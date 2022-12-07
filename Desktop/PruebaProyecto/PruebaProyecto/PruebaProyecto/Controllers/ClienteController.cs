using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Azure.Core;
using Aplicacion.Interfaces;
using Aplicacion.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;
using Bbdd.Bbdd;

namespace ClienteController.Controllers
{
   // [Authorize]
    [ApiController]
    [Route("[controller]/[action]")]

    public class ClienteController : ControllerBase
    {

        private readonly ICliente _contextCliente;
        

        public ClienteController( ICliente clienteservicio)

        {
            _contextCliente = clienteservicio;
        }

        [HttpGet]
        public async Task<ActionResult<List<ClienteDto>>> GetAsync()
        {
            return await _contextCliente.GetAsync();
        }

        [HttpGet]
        public async Task<ActionResult<List<ClienteDto>>> GetClienteAsync(int id)
        {
            var get = await _contextCliente.GetClienteAsync(id);
            if (get is null)
            {
                return NotFound("Cliente no encontrado");
            }
            return Ok(get);
        }
        [HttpPost]

        public async Task<ActionResult<List<ClienteDto>>> AddClienteAsync(ClienteDto request)
        {
           
            var add = await _contextCliente.AddClienteAsync(request);
            return Ok(add);
        }


        [HttpPut]

        public async Task<ActionResult<List<ClienteDto>>> UpdateClienteAsync(int id, ClienteDto request)
        {
            var update = await _contextCliente.UpdateClienteAsync(id, request);
            if (update is null)
            {
                return NotFound("Cliente no encontrado");
            }
            return Ok(update);
        }

        [HttpDelete]

        public async Task<ActionResult<List<ClienteDto>>> DeleteClienteAsync(int id)
        {
            var delete = await _contextCliente.DeleteClienteAsync(id);
            if (delete is null)
            {
                return Ok("Cliente eliminado");
               
            }
            return Ok(delete);
        }
    }
}