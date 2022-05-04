using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Contract.Models
{
    public class Post : Entity
    {
        public string Content { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
