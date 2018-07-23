using ClArc.Sync.Core;

namespace ClArc.Tests.Sync
{
    public class NormalInteractor : IUseCase<Request, Response>
    {
        public Response Handle(Request request)
        {
            return new Response();
        }
    }
}
