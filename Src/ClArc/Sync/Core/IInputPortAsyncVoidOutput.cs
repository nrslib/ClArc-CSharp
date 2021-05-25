using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClArc.Sync.Core
{
    public interface IInputPortAsyncVoidOutput<in TInputDataVoidOutput>
        where TInputDataVoidOutput : IInputDataVoidOutput
    {
        Task Handle(TInputDataVoidOutput request);
    }
}
