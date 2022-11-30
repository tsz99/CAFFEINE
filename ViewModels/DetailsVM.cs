using CAFFEINE.Data;
using System.Collections.Generic;

namespace CAFFEINE.ViewModels
{
    public class DetailsVM
    {
        public List<Comment> Comments { get; set; }
        public string CaffCreator { get; set; }
        public List<Tag> Tags { get; set; }
        public string Caption { get; set; }

    }
}
