using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ACRPhoneWebHook.Models;
using ACRPhoneWebHook.Utils;

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

        public RecordingFormatted asFormattedRecording()
        {
            string DateString;
            if (Date <= Int32.MaxValue)
            {
                DateString = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(Date).ToString();
            }
            else
            {
                DateString = "";
            }


            var FileSizeString = HumanReadableSize.Convert(FileSize, HumanReadableSize.SizeUnits.MB);
            var DurationString = TimeSpan.FromMilliseconds(Duration).ToString(@"hh\:mm\:ss");

            return new RecordingFormatted
            {
                Id = Id,
                Source = Source,
                FileName = FileName,
                Note = Note,
                Date = DateString,
                FileSize = FileSizeString,
                Duration = DurationString

            };
        }

    }
}
