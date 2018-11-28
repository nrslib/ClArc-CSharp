using System;
using ClArc.Sync.Core;

namespace ClArc.Tests.Sync
{
    public class ThrowsExceptionInteractor : IUseCase<Request, Response>
    {
        public Response Handle(Request request)
        {
            throw new Exception();
        }
    }
}