using MediatR;

namespace Sample.SharedKernel.Application
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {

    }
}
