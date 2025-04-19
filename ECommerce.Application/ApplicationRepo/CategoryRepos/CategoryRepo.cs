using ECommerce.Core.Entities.Model;
using ECommerce.Infrastructure.Context;
using ECommerce.Infrastructure.Repositories.BasRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.ApplicationRepo.CategoryRepos
{
    public class CategoryRepo : RepositoryApp<Category>, ICategoryRepo
    {
        public CategoryRepo(ApplicationDbContext context) : base(context)
        { }
    }

}
