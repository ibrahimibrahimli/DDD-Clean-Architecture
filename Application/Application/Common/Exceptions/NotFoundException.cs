namespace Application.Common.Exceptions
{
    public sealed class NotFoundException : Exception
    {
        public NotFoundException(string name, object key) : base($"Entity \"{name}\" ({key}) was not found"){}
        public NotFoundException(string messasge) :  base(messasge) { }
    }
}
