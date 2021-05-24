namespace ClArc.Async.Core
{
    public interface IInputData<out TOutputDataAsync> where TOutputDataAsync : IOutputDataAsync
    {
    }
}