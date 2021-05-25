using ClArc.Sync.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClArc.Tests.Sync
{
    public class VoidInteractor : IInputPortVoidOutput<InputDataVoidOutput>
    {
        public void Handle(InputDataVoidOutput request)
        {
            return;
        }
    }
}
