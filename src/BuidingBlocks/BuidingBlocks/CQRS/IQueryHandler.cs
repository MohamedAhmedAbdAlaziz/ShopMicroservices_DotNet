using MediatR;


namespace BuidingBlocks.CQRS;

public interface IQueryHandler<in TQuery,TReponse>
                :IRequestHandler<TQuery, TReponse>
    where TQuery : IOuery<TReponse>
    where  TReponse :notnull
{

}

