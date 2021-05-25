using System.Threading.Tasks;

namespace ClArc.Async.Core
{
    /// <summary>
    /// Interface for business logic.
    /// </summary>
    /// <typeparam name="TInputData"></typeparam>
    public interface IInputPort<in TInputData, out TOutputData, out TOutputDataTask>
        where TInputData : IInputData<TOutputData>
        where TOutputData : IOutputDataAsync
        where TOutputDataTask : Task<TOutputData>
    {
        TOutputDataTask Handle(TInputData request);
    }
}