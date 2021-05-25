using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ClArc.Sync.Core;

namespace ClArc.Tests.Sync
{
    class AsyncInteractor : IInputPortAsync<InputData, OutputData>
    {
        public async Task<IOutputData> HandleAsync(InputData request)
        {
            return (IOutputData) await Task.FromResult(new OutputData());
        }
    }
}
