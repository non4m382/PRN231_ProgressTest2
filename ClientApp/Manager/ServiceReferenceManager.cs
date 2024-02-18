
using ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Manager
{
    internal class ServiceReferenceManager
    {
        public async Task Manage()
        {
            while (true)
            {
                Console.WriteLine("\n");
                Console.WriteLine(new string('*', 20));
                Console.WriteLine("1. List Category");
                Console.WriteLine("2. Search Category by id");
                Console.WriteLine("3. Add Category");
                Console.WriteLine("4. Update Category");
                Console.WriteLine("5. Delete Category by Id");
                Console.WriteLine("0. Exit");
                Console.WriteLine(new string('*', 20));
                Console.Write("Enter choice: ");
                int option = Convert.ToInt32(Console.ReadLine());
                var proxy = new CategoryServiceClient();
                switch (option)
                {
                    case 0:
                        return;
                    case 1:
                        {
                            Category[] categories = await proxy.GetCategoriesAsync();
                            try
                            {
                                foreach (var c in categories)
                                {
                                    Console.WriteLine($"Id: {c.CategoryID}, Name: {c.CategoryName}");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error: " + ex.Message);
                            }
                            finally
                            {
                                if (proxy.State == System.ServiceModel.CommunicationState.Faulted)
                                    proxy.Abort();
                                else
                                    proxy.Close();
                            }
                        }                       
                        break;
                    case 2:
                        {
                            Console.WriteLine("Enter number id: ");
                            int id = Convert.ToInt32(Console.ReadLine());
                            Category category = await proxy.GetCategoryByIdAsync(id);
                            Console.WriteLine($"Id: {category.CategoryID}, Name: {category.CategoryName}");
                        }
                        break;
                    case 3:
                        {
                            Console.WriteLine("Enter category's name: ");
                            string name = Console.ReadLine();
                            Category categoryCreate = new Category()
                            {
                                CategoryName = name
                            };
                            Category categoryAfterAdd = await proxy.CreateAsync(categoryCreate);
                            Console.WriteLine($"Id: {categoryAfterAdd.CategoryID}, Name: {categoryAfterAdd.CategoryName}");

                        }
                        break;
                    case 4:
                        {
                            Console.WriteLine("Enter number id: ");
                            int id = Convert.ToInt32(Console.ReadLine());
                            Category categoryBeforeUpdate = await proxy.GetCategoryByIdAsync(id);
                            Console.WriteLine("Enter update name: ");
                            string name = Console.ReadLine();
                            Category categoryUpdate = new Category()
                            {
                                CategoryID = id,
                                CategoryName = name
                            };
                            Category categoryAfterUpdate = await proxy.UpdateAsync(categoryUpdate);
                            Console.WriteLine($"Before update, Id: {categoryBeforeUpdate.CategoryID}, Name: {categoryBeforeUpdate.CategoryName}");
                            Console.WriteLine($"After update, Id: {categoryAfterUpdate.CategoryID}, Name: {categoryAfterUpdate.CategoryName}");

                        }
                        break;
                    case 5:
                        {
                            Console.WriteLine("Enter number id: ");
                            int id = Convert.ToInt32(Console.ReadLine());
                            string afterDelete = await proxy.DeleteAsync(id);
                            Console.WriteLine(afterDelete);
                        }
                        break;
                }
            }
        }
    }
}
