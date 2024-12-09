using MediatR;
using System.Net;

namespace BuildingBlocks.CQRS;

public interface ICommand : ICommand <Unit>
{

}

public interface ICommand<out TResponse>  : IRequest<TResponse> 
{   

}