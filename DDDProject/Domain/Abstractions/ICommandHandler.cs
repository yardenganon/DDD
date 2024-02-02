using MediatR;

namespace DDDProject.Domain.Abstractions;

public interface ICommandHandler<T> : IRequestHandler<T> where T : IRequest
{
    
}