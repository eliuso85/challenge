using Application.Interfaces;
using Domain.DTO;
using Domain.Response;
using Infrastructure.Repository.Interfaces;
using Infrastructure.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services;

public class GestionService(IRepositoryGestion _repositoryGestion): IGestionService
{
    #region Builder
    private readonly ResponseApi responseApi = new();
    #endregion

    public async Task<ResponseApi> SaveUserSync(User user)
    {
        try
        {
            responseApi.Data = await _repositoryGestion.saveUserAsync(user);

            await Utils.SendEmailAsync(user.Email, user.Nombre );

            responseApi.Success = true; 
            responseApi.Mensajes.Add($"El Usuario con el correo: {user.Email} se inserto correctamente");
        }
        catch (Exception ex)
        {
            responseApi.Success = false;
            responseApi.Mensajes.Add(ex.Message);
        }
        return responseApi;
    }
}
