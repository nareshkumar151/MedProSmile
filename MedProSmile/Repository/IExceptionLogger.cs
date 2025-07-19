namespace MedProSmile.Repository
{
    public interface IExceptionLogger
    {
        Task LogExceptionAsync(Exception ex, string methodName);
    }
}
