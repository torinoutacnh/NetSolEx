using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Contract
{
    public abstract class Entity
    {
        [Key]
        public string Id { get; set; }

        public DateTimeOffset CreatedTime { get; set; }
        public DateTimeOffset UpdatedTime { get; set; }
        public DateTimeOffset? DeletedTime { get; set; }

        protected Entity()
        {
            Id = Guid.NewGuid().ToString();
            CreatedTime = DateTimeOffset.UtcNow;
        }
    }
}
