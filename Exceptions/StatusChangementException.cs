namespace FastDeliveryApi.Exceptions;

public class StatusChangementException : ApplicationException
{
    public StatusChangementException(string message) : base(message)
    {
    }
}