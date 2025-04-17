
namespace Domain.Response;

public class ResponseApi
{
    public List<string> Mensajes { get; set; }
    public bool Success { get; set; }
    public dynamic? Data { get; set; }
    public bool Exito { get; set; }
    public IAsyncEnumerable<char>? Mensaje { get; set; }
    public IAsyncEnumerable<char>? Estado { get; set; }

    public ResponseApi()
    {
        Success = true;
        Mensajes = [];
    }
}
