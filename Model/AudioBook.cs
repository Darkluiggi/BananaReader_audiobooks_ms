using System.ComponentModel.DataAnnotations.Schema;

namespace BananaReader_audiobooks_ms.Model
{
    [Table("AudioBook")]
    public class AudioBook
    {
        public int id { get; set; }
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public string? path { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    }
}