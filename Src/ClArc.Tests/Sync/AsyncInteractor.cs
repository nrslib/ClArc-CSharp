using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ClArc.Sync.Core;

namespace ClArc.Tests.Sync
{
    class AsyncInteractor : IInputPortAsync<InputData, OutputData, Task<OutputData>>
    {
        public async Task<OutputData> Handle(InputData request)
        {
            return await Task.FromResult(new OutputData());
        }
    }
}
