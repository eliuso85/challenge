using Domain.DTO;
using Domain.Request;
using Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface ILoginService
{
    Task<ResponseApi> LoginUserSync(LoginRequest userLogin);
    
}
