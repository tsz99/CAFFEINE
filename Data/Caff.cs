using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CAFFEINE.Data
{
    public class Caff
    {
        [Key]
        public int DB_ID { get; set; }
        public List<Ciff> Ciffs { get; set; }
        public string Creator { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public List<Comment> Comments { get; set; }


    }
}
