using System.ComponentModel.DataAnnotations;

namespace CAFFEINE.Data
{
    public class Caption
    {
        [Key]
        public int DB_ID { get; set; }
        public string Text { get; set; }
    }
}
