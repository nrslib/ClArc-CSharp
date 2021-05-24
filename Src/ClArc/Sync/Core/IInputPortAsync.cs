using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClArc.Sync.Core
{
    public interface IInputPortAsync<in TInputData, out TOutputData>
        where TInputData : IInputData<TOutputData>
        where TOutputData : IOutputData
    {
        Task<IOutputData> HandleAsync(TInputData request);
    }
}
