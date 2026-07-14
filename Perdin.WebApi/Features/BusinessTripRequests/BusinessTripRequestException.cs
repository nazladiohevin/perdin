namespace Perdin.WebApi.Features.BusinessTripRequests;

public class BusinessTripRequestException : Exception
{
    public int StatusCode { get; }

    public BusinessTripRequestException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}
