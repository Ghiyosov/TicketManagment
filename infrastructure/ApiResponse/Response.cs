using System.Net;

namespace infrastructure.ApiResponse;

public class Response<T>
{
    public int StatusCode { get; set; }
    public T? Data { get; set; }
    public string Massage { get; set; }

    public Response(T date)
    {
        Data = date;
        StatusCode = 200;
        Massage = "";
    }

    public Response(HttpStatusCode statusCode, string massage)
    {
        Data = default;
        StatusCode = (int)statusCode;
        Massage = massage;
    }
}