using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ACRPhone.Webhook.Models
{
    [Table("recordings")]
    public class Recording
    {
        [Column("id")]
        [Key] 
        public long Id { get; set; }

        [Column("source")] 
        public string Source { get; set; }

        [Column("file_name")]
        public string FileName { get; set; }

        [Column("note")]
        public string Note { get; set; }

        [Column("date")]
        public long Date { get; set; }

        [Column("file_size")]
        public long FileSize { get; set; }

        [Column("duration")]
        public long Duration { get; set; }

    }
}
