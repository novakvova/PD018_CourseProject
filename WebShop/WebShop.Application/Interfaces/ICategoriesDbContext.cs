using Microsoft.EntityFrameworkCore;
using WebShop.Domain.Entities.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Application.Interfaces {
    public interface ICategoriesDbContext {
        DbSet<CategoryEntity> Categories { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
