namespace bll.interfaces
{
    public interface ICDNService
    {
        byte[]? GetFileBytes(string fileName);
        public string? GetContentType(string fileName);
    }
}