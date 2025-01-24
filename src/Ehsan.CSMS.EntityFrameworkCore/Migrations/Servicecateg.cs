using Ehsan.CSMS.Entities;
using Ehsan.CSMS.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ehsan.CSMS.Migrations
{
    internal class Servicecateg
    {
        ICategoryRepository _categoryrepository;
        public Servicecateg(ICategoryRepository categoryrepository) // .net fraemwork will pass a new object of the categoryrepository
        { 
            _categoryrepository = categoryrepository;
        }

        public async Task<Category> GetCategoryeAsync(string name)
        {

            var cate= await _categoryrepository.FindAsync(c => c.CategoryName == name);
            return cate!;
        }
        public async Task<Category> GetCategoryAsync(int id)
        {
            var cate = await _categoryrepository.FindAsync(c => c.Id == id);
            
            return cate!;
        }
    }
}
