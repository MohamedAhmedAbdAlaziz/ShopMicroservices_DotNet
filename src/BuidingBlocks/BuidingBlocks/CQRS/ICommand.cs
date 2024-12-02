using MediatR;
using System.Net;

namespace BuidingBlocks.CQRS;

public interface ICommand : ICommand <Unit>
{

}

public interface ICommand<out TResponse>  : IRequest<TResponse> 
{   

}