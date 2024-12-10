namespace Document_Repository.Models
{
    public class Document
    {
        public int DocumentId { get; set; }
        public string DocumentName { get; set; } = "";
        public string DocumentCode { get; set; } = "";
        public string FilePath { get; set; } = "";
        public decimal FileSize { get; set; }
        public string FileExtension { get; set; } = "";
        public string UploadedBy { get; set; } = "";
        public DateTime UploadedDateTime { get; set;}

    }
}
