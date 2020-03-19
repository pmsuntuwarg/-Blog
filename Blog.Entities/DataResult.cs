using Blog.Common.Enums;

namespace Blog.Entities
{
    public class DataResult
    {
        public Status Status { get; set; }
        public string Message { get; set; }
        public string ReturnId { get; set; }
    }
}
