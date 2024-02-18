using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CategoryService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CategoryService.svc or CategoryService.svc.cs at the Solution Explorer and start debugging.
    public class CategoryService : ICategoryService
    {
        public string DoWork()
        {
            string data = "";
            MySaleDBEntities context = new MySaleDBEntities();
            context.Configuration.LazyLoadingEnabled = false;
            List<Category> categories = context.Categories.ToList();
            foreach (var c in categories)
            {
                data += $"Id: {c.CategoryID}, Name: {c.CategoryName}";
            }
            return data;
        }

        public List<Category> GetCategories()
        {
            List<Category> list = new List<Category>();
            MySaleDBEntities context = new MySaleDBEntities();
            context.Configuration.ProxyCreationEnabled = false;
            List<Category> categories = context.Categories.ToList();
            foreach (var c in categories)
            {
                Category category = new Category();
                category.CategoryID = c.CategoryID;
                category.CategoryName = c.CategoryName;
                list.Add(category);
            }
            try
            {
                
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                throw;
            }
            return categories;
        }
        public Category GetCategoryById(int id)
        {
            MySaleDBEntities context = new MySaleDBEntities();
            context.Configuration.ProxyCreationEnabled = false;
            var entity = context.Categories.SingleOrDefault(c => c.CategoryID == id);
            Category category = new Category();
            category.CategoryID = entity.CategoryID;
            category.CategoryName = entity.CategoryName;
            return category;
        }

        public Category Create(Category category)
        {
            MySaleDBEntities context = new MySaleDBEntities();
            Category categoryCreate = new Category()
            {
                CategoryName = category.CategoryName
            };
            context.Configuration.ProxyCreationEnabled = false;
            context.Categories.Add(categoryCreate);
            context.SaveChanges();
            return category;
        }

        public Category Update(Category category)
        {
            MySaleDBEntities context = new MySaleDBEntities();
            context.Configuration.ProxyCreationEnabled = false;
            Category categoryUpdate = GetCategoryById(category.CategoryID);
            categoryUpdate.CategoryName = category.CategoryName;
            context.SaveChanges();
            return category;
        }

        public string Delete(int id)
        {
            MySaleDBEntities context = new MySaleDBEntities();
            context.Configuration.ProxyCreationEnabled = false;
            Category category = context.Categories.Find(id);
            context.Categories.Remove(category);
            context.SaveChanges();
            return "Delete Successfull";
        }
    }
}
