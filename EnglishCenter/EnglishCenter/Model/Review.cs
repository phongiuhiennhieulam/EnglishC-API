using System;
using System.Collections.Generic;

namespace EnglishCenter.Model
{
    public partial class Review
    {
        public int Id { get; set; }
        public int? QuestionId { get; set; }
        public int? AnswerId { get; set; }
        public int? MarkId { get; set; }

        public virtual Answer? Answer { get; set; }
        public virtual Mark? Mark { get; set; }
        public virtual Question? Question { get; set; }
    }
}
