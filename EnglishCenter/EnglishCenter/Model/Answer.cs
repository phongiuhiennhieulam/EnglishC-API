using System;
using System.Collections.Generic;

namespace EnglishCenter.Model
{
    public partial class Answer
    {
        public Answer()
        {
            Reviews = new HashSet<Review>();
        }

        public int Id { get; set; }
        public int? QuestId { get; set; }
        public string? AnsContent { get; set; }
        public bool? IsTrue { get; set; }

        public virtual Question? Quest { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
