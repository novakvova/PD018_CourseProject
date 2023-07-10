using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Application.Common.Exceptions {
    public class NotFoundException : Exception {
        public NotFoundException(string entityName, object key) :
            base($"Entity \"{entityName}\" with key ({key}) not found") { }
    }
}
