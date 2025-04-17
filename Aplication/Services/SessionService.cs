using Application.Interfaces;
using Domain.DTO;
using Domain.Request;
using Domain.Response;
using Infrastructure.Repository.Interfaces;
using Infrastructure.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services;

public class SessionService(IRepositorySession _repository) : ISessionService
{
    #region Builder
    private readonly ResponseApi responseApi = new();
    #endregion

    public async Task<ResponseApi> ObtenerSesionesActivasAsync()
    {
        try
        {
            responseApi.Data = await _repository.ObtenerSesionesActivasAsync();
            responseApi.Success = true;
            responseApi.Mensajes.Add($"Lista de Sesiones Activas, leida correctamente");
        }
        catch (Exception ex)
        {
            responseApi.Success = false;
            responseApi.Mensajes.Add(ex.Message);
        }
        return responseApi;
    }

    public async Task<ResponseApi> IniciarSesionAsync(IniciarSesionRequest request)
    {
        try
        {
            var capacidadMax = await _repository.ObtenerCapacidadMaxAsync(request.RecursoId);
            var capacidadOcupada = await _repository.ObtenerCapacidadOcupadaAsync(request.RecursoId);

            if (capacidadMax == null)
            {
                responseApi.Success = false;
                responseApi.Mensajes.Add($"No se encontro información para ese recurso {request.RecursoId}.");
                return responseApi;
            }

            if (capacidadOcupada != null)
            {
                var diferenciaCap = capacidadMax.CapacidadMaxima - capacidadOcupada.CantidadRequerida;
                if (request.CantidadRequerida >= diferenciaCap)
                {
                    responseApi.Success = false;
                    responseApi.Mensajes.Add($"No hay capacidad disponible para iniciar una nueva sesión.");
                    return responseApi;
                }
            }
            Session session = new()
            {
                UserId = 3,
                RecursoId = request.RecursoId,
                CantidadRequerida = request.CantidadRequerida
            };

            await _repository.PostSesionAsync(session);

            responseApi.Data = await _repository.ObtenerSesionesActivasAsync();
            responseApi.Success = true;
            responseApi.Mensajes.Add($"Sesiones Activas.");
        }
        catch (Exception ex)
        {
            responseApi.Success = false;
            responseApi.Mensajes.Add(ex.Message);
        }
        return responseApi;
    }

    public async Task<ResponseApi> FinalizarSesionAsync(FinalizarSesionRequest request)
    {
        try
        {
            Session session = new()
            {
                Id = request.SessionId,
                RecursoId = request.RecursoId
            };

            await _repository.PostFinalizaSesionAsync(session);

            responseApi.Data = await _repository.ObtenerSesionesActivasAsync();
            responseApi.Success = true;
            responseApi.Mensajes.Add($"Sesion finalizada con exito.");
        }
        catch (Exception ex)
        {
            responseApi.Success = false;
            responseApi.Mensajes.Add(ex.Message);
        }
        return responseApi;
    }

}
