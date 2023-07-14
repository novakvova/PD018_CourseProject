using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Domain.Entities.Base {
    public abstract class BaseEntity : IEntity {
        public Guid Id { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }

        public BaseEntity() {
            Id = Guid.NewGuid();
            Deleted = false;
            DateCreated = DateTime.UtcNow;
            DateUpdated = null;
        }
    }
}
