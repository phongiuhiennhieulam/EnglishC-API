using System;
using System.Collections.Generic;

namespace EnglishCenter.Model
{
    public partial class TestQuestion
    {
        public int? TestId { get; set; }
        public int? QuestionId { get; set; }

        public virtual Question? Question { get; set; }
        public virtual Test? Test { get; set; }
    }
}
