namespace ClArc.Sync.Core
{
    /// <summary>
    /// Interface for business logic.
    /// </summary>
    /// <typeparam name="TInputData"></typeparam>
    /// <typeparam name="TOutputData"></typeparam>
    public interface IInputPort<in TInputData, out TOutputData>
        where TInputData : IInputData<TOutputData>
        where TOutputData : IOutputData
    {
        TOutputData Handle(TInputData request);
    }
}