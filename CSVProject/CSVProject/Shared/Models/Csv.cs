using System.ComponentModel.DataAnnotations;

namespace CSVProject.Shared.Models
{
    public class Csv
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public string FilePath { get; set; }
    }
}
