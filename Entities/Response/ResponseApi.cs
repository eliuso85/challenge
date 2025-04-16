namespace Domain.Response;

public class ResponseApi
{
    public List<string> Mensajes { get; set; }
    public bool Success { get; set; }
    public dynamic? Data { get; set; }

    public ResponseApi()
    {
        Success = true;
        Mensajes = [];
    }
}
