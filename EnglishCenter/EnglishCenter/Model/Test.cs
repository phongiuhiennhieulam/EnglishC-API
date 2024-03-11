using System;
using System.Collections.Generic;

namespace EnglishCenter.Model
{
    public partial class Test
    {
        public Test()
        {
            Marks = new HashSet<Mark>();
        }

        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? Time { get; set; }
        public int? SubjectId { get; set; }
        public bool? Status { get; set; }

        public virtual Subject? Subject { get; set; }
        public virtual ICollection<Mark> Marks { get; set; }
    }
}
