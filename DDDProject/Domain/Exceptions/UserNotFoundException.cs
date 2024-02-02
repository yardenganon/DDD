namespace DDDProject.Domain.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException(Guid id) : base($"UserId {id} is not found") { }
}