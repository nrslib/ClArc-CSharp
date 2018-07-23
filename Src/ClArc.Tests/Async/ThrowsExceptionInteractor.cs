using System;
using ClArc.Async.Core;

namespace ClArc.Tests.Async
{
    public class ThrowsExceptionInteractor : IUseCase<Request>
    {
        public void Handle(Request request)
        {
            throw new Exception();
        }
    }
}
