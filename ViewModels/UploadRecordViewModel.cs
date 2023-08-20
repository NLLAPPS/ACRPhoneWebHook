using Microsoft.AspNetCore.Http;
using ACRPhone.Webhook.Models;

namespace ACRPhone.Webhook.ViewModels
{
    public class UploadRecordViewModel
    {
        public string Source { get; set; }
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public string Secret { get; set; }
        public long Date { get; set; }
        public long Duration { get; set; }
        public string Note { get; set; }
    }
}
