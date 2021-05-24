using System.Threading.Tasks;

namespace ClArc.Async.Core
{
    /// <summary>
    /// Interface for business logic.
    /// </summary>
    /// <typeparam name="TInputData"></typeparam>
    public interface IInputPort<in TInputData, out TOutputDataAsync>
        where TInputData : IInputData<TOutputDataAsync>
        where TOutputDataAsync : IOutputDataAsync
    {
        Task<IOutputDataAsync> Handle(TInputData request);
    }
}