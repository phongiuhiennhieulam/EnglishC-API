using System;
using System.Collections.Generic;

namespace EnglishCenter.Model
{
    public partial class User
    {
        public User()
        {
            Marks = new HashSet<Mark>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool? Status { get; set; }
        public int? RoleId { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }

        public virtual Role? Role { get; set; }
        public virtual ICollection<Mark> Marks { get; set; }
    }
}
