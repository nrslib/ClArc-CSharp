using ClArc.Sync.Core;

namespace ClArc.Tests.Sync
{
    public class DefinedInterfaceInteractor : IDefinedInterfaceUseCase
    {
        public Response Handle(Request request)
        {
            return new Response();
        }
    }

    public interface IDefinedInterfaceUseCase : IUseCase<Request, Response>
    {
    }
}