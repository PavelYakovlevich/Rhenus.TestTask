namespace Exceptions;

[Serializable]
public class RegistrationFailedException : OperationFailedException
{
    public RegistrationFailedException() 
    {
    }

    public RegistrationFailedException(string message)
        : base(message)
    {
    }
    
    public RegistrationFailedException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}