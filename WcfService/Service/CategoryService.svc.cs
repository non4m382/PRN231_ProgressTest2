﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfService.Model;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CategoryService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CategoryService.svc or CategoryService.svc.cs at the Solution Explorer and start debugging.
    public class CategoryService : ICategoryService
    {
        public string DoWork()
        {
            //List<Category> categories = new List<Category>();
            //try
            //{
            //    using (var conn = new SqlConnection("server=(local);database=MySaleDB;uid=sa;pwd=123123;TrustServerCertificate=True"))
            //    {
            //        conn.Open();
            //        using (SqlCommand cmd = conn.CreateCommand())
            //        {
            //            cmd.CommandText = "SELECT * FROM Categories";
            //            using (SqlDataReader reader = cmd.ExecuteReader())
            //            {
            //                while (reader.Read())
            //                {
            //                    Category category = new Category();
            //                    category.Id = (int)reader["CategoryID"];
            //                    category.Name = reader["CategoryName"].ToString();
            //                    categories.Add(category);
            //                }
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw new FaultException(ex.Message);
            //}
            string data = "";
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
            category = context.Categories.Add(categoryCreate);
            context.SaveChanges();
            return category;
        }

        public Category Update(Category category)
        {
            MySaleDBEntities context = new MySaleDBEntities();
            Category categoryUpdate = context.Categories.Find(category.CategoryID);
            context.Configuration.ProxyCreationEnabled = false;
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
