using System;
using System.Collections.Generic;

namespace EnglishCenter.Model
{
    public partial class Mark
    {
        public Mark()
        {
            Reviews = new HashSet<Review>();
        }

        public int Id { get; set; }
        public int? TestId { get; set; }
        public int? UserId { get; set; }
        public double? Mark1 { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual Test? Test { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
