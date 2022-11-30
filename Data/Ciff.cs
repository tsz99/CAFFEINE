using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CAFFEINE.Data
{
    public class Ciff
    {
        [Key]
        public int DB_ID { get; set; }
        public byte[] Pixels { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Caption> Captions { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Duration { get; set; }
    }
}
