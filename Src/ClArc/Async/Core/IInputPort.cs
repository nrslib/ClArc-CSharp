namespace ClArc.Async.Core
{
    /// <summary>
    /// Interface for business logic.
    /// </summary>
    /// <typeparam name="TInputData"></typeparam>
    public interface IInputPort<in TInputData>
        where TInputData : IInputData
    {
        void Handle(TInputData request);
    }
}