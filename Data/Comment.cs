using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAFFEINE.Data
{
    public class Comment
    {
        [Key]
        public int DB_ID { get; set; }
        public string Creator { get; set; }
        public DateTime Created { get; set; }
        public string Text { get; set; }
        [ForeignKey("Caffs")]
        public int CaffDB_ID { get; set; }
        public DateTime DT_Created { get; set; }
    }
}
