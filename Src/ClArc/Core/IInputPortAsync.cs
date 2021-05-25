using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClArc.Core
{
    public interface IInputPortAsync<in TInputData, out TOutputData, out TOutputDataTask>
        where TOutputData : IOutputData
        where TInputData : IInputData<TOutputData>
        where TOutputDataTask : Task<TOutputData>
    {
        TOutputDataTask Handle(TInputData request);
    }
}
