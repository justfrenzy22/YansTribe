namespace dal.dto
{
    public class MediaJsonDTO
    {
        public required Guid media_id { get; set; }
        public required string media_src { get; set; }
        public required string media_type { get; set; }
        public required int order { get; set; }
    }
}