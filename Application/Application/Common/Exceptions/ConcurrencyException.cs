namespace Application.Common.Exceptions
{
    public sealed class ConcurrencyException : Exception
    {
        public ConcurrencyException()
            : base("A concurrency conflict occurred. The entity you are trying to update has been modified by another user.")
        {
        }
        public ConcurrencyException(string message)
        : base(message)
        {
        }
    }
}
