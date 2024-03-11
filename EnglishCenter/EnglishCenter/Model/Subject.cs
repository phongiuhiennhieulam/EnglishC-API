using System;
using System.Collections.Generic;

namespace EnglishCenter.Model
{
    public partial class Subject
    {
        public Subject()
        {
            Tests = new HashSet<Test>();
        }

        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Test> Tests { get; set; }
    }
}
