using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace CAFFEINE.Data
{
    public class Tag
    {
        [Key]
        public int DB_ID { get; set; }
        public string Text { get; set; }
    }
}
