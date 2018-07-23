using ClArc.Async.Core;

namespace ClArc.Tests.Async
{
    public class DefinedInterfaceInteractor : IDefinedInterfaceUseCase
    {
        public void Handle(Request request)
        {
        }
    }

    public interface IDefinedInterfaceUseCase : IUseCase<Request>
    {
    }
}
