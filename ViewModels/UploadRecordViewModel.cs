using Microsoft.AspNetCore.Http;


namespace ACRPhone.Webhook.ViewModels
{
    public class UploadRecordViewModel
    {
        public required string Source { get; set; }
        public IFormFile? File { get; set; }
        public string? FileName { get; set; }
        public required string Secret { get; set; }
        public int? Date { get; set; }
        public int? Duration { get; set; }
        public string? Note { get; set; }
    }
}
