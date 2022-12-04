using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace CAFFEINE.Data
{
    public class Ciff
    {
        [Key]
        public int DB_ID { get; set; }
        public byte[] Pixels { get; set; }
        public List<Tag> Tags { get; set; }
        public string Caption { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Duration { get; set; }
        [NotMapped]
        public Image Png { get; set; }
    }
}
