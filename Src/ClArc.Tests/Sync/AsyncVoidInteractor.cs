using ClArc.Sync.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClArc.Tests.Sync
{
    class AsyncVoidInteractor : IInputPortAsyncVoidOutput<InputDataVoidOutput>
    {
        public async Task Handle(InputDataVoidOutput request)
        {
            await Task.FromResult(true);
            return;
        }
    }
}
