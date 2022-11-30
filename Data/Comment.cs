using System.ComponentModel.DataAnnotations;

namespace CAFFEINE.Data
{
    public class Comment
    {
        [Key]
        public int DB_ID { get; set; }
        public string Creator { get; set; }
        public string Text { get; set; } 
    }
}
