using MyStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Business.CategoryService;

public interface ICategoryService
{
    Task<List<Category>> GetAllCategories();
    Task<Category> GetById(int id);
    Task Create(Category category);
    Task Update(Category category);
    Task Delete(int categoryId);
}
