using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Bbdd.Bbdd;
using Microsoft.EntityFrameworkCore;
using Azure.Core;
using Aplicacion.Interfaces;
using Aplicacion.Dto;


namespace AplicacionBbdd.Modelos
{

    public class ClienteModelo : ICliente
    {
        private readonly PruebaContext _context;
        public ClienteModelo(PruebaContext context)
        {
            _context = context;
        }

        public async Task<List<ClienteDto>> GetAsync()
        {
            var dbCliente = await _context.Clientes
                 .OrderBy(i => i.IdCliente).Select(i => new ClienteDto
                 {
                     IdCliente = i.IdCliente,
                     NombreCliente = i.NombreCliente
                 })
                .Skip(0)
                .Take(10)
                 .ToListAsync();
            return dbCliente;
        }
        public async Task<ClienteDto?> GetClienteAsync(int id)
        {
            var dbCl = await _context.Clientes.
                Where(i => i.IdCliente == id).
                Select(i => new ClienteDto
                {
                    IdCliente = i.IdCliente,
                    NombreCliente = i.NombreCliente
                }).FirstOrDefaultAsync();
            if (dbCl is null)
            {
                return null;
            }
            return dbCl;
        }
        public async Task<ClienteDto> AddClienteAsync(ClienteDto request)
        {
            // var dbCl = await _context.Clientes.
            //    Where(i => i.IdCliente == request.IdCliente{

            // });
            var nuevoclient = new Cliente
            {
                IdCliente = request.IdCliente,
                NombreCliente = request.NombreCliente
            };
            _context.Clientes.Add(nuevoclient);
            await _context.SaveChangesAsync();
            return await GetClienteAsync(nuevoclient.IdCliente);

        }
        public async Task<ClienteDto?> UpdateClienteAsync(int id, ClienteDto request)
        {
            var dbCl = await _context.Clientes.
                Where(i => i.IdCliente == id).
                Select(i => new ClienteDto
                {
                    IdCliente = request.IdCliente,
                    NombreCliente = request.NombreCliente
                }).FirstOrDefaultAsync();

            var clienteupdate = new Cliente
            {
                IdCliente = dbCl.IdCliente,
                NombreCliente = dbCl.NombreCliente
            };
            _context.Clientes.Update(clienteupdate);
            await _context.SaveChangesAsync();
            return await GetClienteAsync(request.IdCliente);
        }
        public async Task<ClienteDto?> DeleteClienteAsync(int id)
        {
            var dbCl = await _context.Clientes.
                Where(i => i.IdCliente == id).
                Select(i => new ClienteDto
                {
                    IdCliente = i.IdCliente,
                    NombreCliente = i.NombreCliente
                }).FirstOrDefaultAsync();

            var clienteborrado = new Cliente
            {
                IdCliente = dbCl.IdCliente,
                NombreCliente = dbCl.NombreCliente
            };

            _context.Clientes.Remove(clienteborrado);
            await _context.SaveChangesAsync();
            return await GetClienteAsync(id);
        }
    }
}