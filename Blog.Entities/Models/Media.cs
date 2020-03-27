namespace Blog.Entities.Models
{
    public class Media : BaseModel
    {
        public string FileName { get; set; }

        public string FileType { get; set; }

        public string FileUrl { get; set; }

        public string PreviewUrl { get; set; }

        public bool IsHomeImage { get; set; } = false;
    }
}
