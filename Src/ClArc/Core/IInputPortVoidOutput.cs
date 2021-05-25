using System;
using System.Collections.Generic;
using System.Text;

namespace ClArc.Core
{
    public interface IInputPortVoidOutput<in TInputDataVoidOutput>
        where TInputDataVoidOutput : IInputDataVoidOutput
    {
        void Handle(TInputDataVoidOutput request);
    }
}
