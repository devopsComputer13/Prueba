using Aplicacion.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces
{
    public interface ICliente
    {
        Task<List<ClienteDto>> GetAsync();

        Task<ClienteDto?> GetClienteAsync(int id);

        Task<ClienteDto> AddClienteAsync(ClienteDto request);

        Task<ClienteDto?> UpdateClienteAsync(int id, ClienteDto request);

        Task<ClienteDto?> DeleteClienteAsync(int id);


    }
}