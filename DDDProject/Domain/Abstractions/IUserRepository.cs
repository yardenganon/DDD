namespace DDDProject.Domain.Abstractions;

public interface IUserRepository
{
    Task<User?> GetUserById(Guid id, CancellationToken ct = default);
}