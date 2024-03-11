using System;
using System.Collections.Generic;

namespace EnglishCenter.Model
{
    public partial class Question
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
            Reviews = new HashSet<Review>();
        }

        public int Id { get; set; }
        public string? QuestionContent { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
