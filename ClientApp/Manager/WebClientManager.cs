using ClientApp.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Manager
{
    internal class WebClientManager
    {
        static WebClientManageCategory category = new WebClientManageCategory();
        static WebClientManageProduct product = new WebClientManageProduct();

        private const string LinkCategory = "http://localhost:5250/api/Category";
        private const string LinkProduct = "http://localhost:5250/api/Product";

        #region ManageMenu
        public async Task Manage()
        {
            while (true)
            {
                Console.WriteLine("\n");
                Console.WriteLine(new string('*', 20));
                Console.WriteLine("1. Manage Category");
                Console.WriteLine("2. Manage Product");
                Console.WriteLine("0. Exit");
                Console.Write("Enter choice: ");
                var option = Convert.ToInt32(Console.ReadLine());
                switch (option)
                {
                    case 0:
                        return;
                    case 1:
                        {
                            Console.WriteLine("Manage Category");
                            await ManageCategory();
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("Manage Product");
                            await ManageProduct();
                            break;
                        }
                }
            }
        }
        #endregion

        #region ManageCategoryMenu
        internal async Task ManageCategory()
        {
            while (true)
            {
                Console.WriteLine(new string('*', 20));
                Console.WriteLine("1.Show list Category");
                Console.WriteLine("2.Search category by id");
                Console.WriteLine("3.Add category to DB");
                Console.WriteLine("4.Update category by id");
                Console.WriteLine("5.Delete category by id");
                Console.WriteLine("0.Exit");
                Console.Write("Enter choice: ");
                var option = Convert.ToInt32(Console.ReadLine());
                switch (option)
                {
                    case 0:
                        return;
                    case 1:
                        {
                            Console.WriteLine(new string('*', 20));
                            Console.WriteLine("List category");
                            await category.ShowListCategoryAsync();
                            break;
                        }
                    case 2:
                        {
                            Console.Write("Enter id: ");
                            var id = Convert.ToInt32(Console.ReadLine());
                            await category.SearchCategoryById(id);
                            break;
                        }
                    case 3:
                        {
                            Console.Write("Enter name: ");
                            string name = Console.ReadLine();
                            await category.InsertCategory(name);
                            break;
                        }
                    case 4:
                        {
                            Console.Write("Enter id: ");
                            int id = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter name: ");
                            string name = Console.ReadLine();
                            await category.UpdateCategory(id, name);
                            break;
                        }
                    case 5:
                        {
                            Console.Write("Enter id: ");
                            int id = Convert.ToInt32(Console.ReadLine());
                            await category.DeleteCategory(id);
                            break;
                        }
                }
            }
        }
        #endregion

        #region ManageProductMenu
        internal async Task ManageProduct()
        {
            while (true)
            {
                Console.WriteLine(new string('*', 20));
                Console.WriteLine("1.Show list Product");
                Console.WriteLine("2.Search product by id");
                Console.WriteLine("3.Add product to DB");
                Console.WriteLine("4.Update product by id");
                Console.WriteLine("5.Delete product by id");
                Console.WriteLine("0.Exit");
                Console.Write("Enter choice: ");
                var option = Convert.ToInt32(Console.ReadLine());
                switch (option)
                {
                    case 0:
                        return;
                    case 1:
                        {
                            Console.WriteLine("List product");
                            await product.ShowListProductAsync();
                            break;
                        }
                    case 2:
                        {
                            Console.Write("Enter id: ");
                            var id = Convert.ToInt32(Console.ReadLine());
                            await product.SearchProductById(id);
                            break;
                        }
                    case 3:
                        {
                            Console.Write("Enter name: ");
                            string name = Console.ReadLine();
                            Console.Write("Enter price: ");
                            var price = Convert.ToDecimal(Console.ReadLine());
                            Console.Write("Enter quantity: ");
                            var quantity = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter image: ");
                            string image = Console.ReadLine();
                            Console.Write("Enter categoryId: ");
                            var categoryId = Convert.ToInt32(Console.ReadLine());
                            await product.InsertProduct(name, price, quantity, image, categoryId);
                            break;
                        }
                    case 4:
                        {
                            Console.Write("Enter id: ");
                            int id = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter name: ");
                            string name = Console.ReadLine();
                            Console.Write("Enter price: ");
                            var price = Convert.ToDecimal(Console.ReadLine());
                            Console.Write("Enter quantity: ");
                            var quantity = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter image: ");
                            string image = Console.ReadLine();
                            Console.Write("Enter categoryId: ");
                            var categoryId = Convert.ToInt32(Console.ReadLine());
                            Product p = new Product()
                            {
                                ProductId = id,
                                ProductName = name,
                                UnitPrice = price,
                                UnitsInStock = quantity,
                                Image = image,
                                CategoryId = categoryId
                            };
                            await product.UpdateProduct(p);
                            break;
                        }
                    case 5:
                        {
                            Console.Write("Enter id: ");
                            int id = Convert.ToInt32(Console.ReadLine());
                            await product.DeleteProduct(id);
                            break;
                        }
                }
            }
        }
        #endregion

        #region WebClientManageCategory
        internal class WebClientManageCategory
        {
            public async Task ShowListCategoryAsync()
            {
                using (System.Net.WebClient webClient = new System.Net.WebClient())
                {
                    var json = webClient.DownloadString(LinkCategory);
                    JArray j = JArray.Parse(json);
                    Console.WriteLine(j);
                }
            }

            public async Task SearchCategoryById(int id)
            {
                using (System.Net.WebClient webClient = new System.Net.WebClient())
                {
                    var json = webClient.DownloadString(LinkCategory + "/" + id);
                    var result = JsonConvert.DeserializeObject<Category>(json);
                    if (result == null)
                    {
                        Console.WriteLine("Not found!");
                    }
                    else
                    {
                        Console.WriteLine(result.CategoryId + "\t" + result.CategoryName);
                    }
                }
            }

            public async Task InsertCategory(string? name)
            {
                using (System.Net.WebClient webClient = new System.Net.WebClient())
                {
                    // Set the "Content-Type" header to indicate that we are sending JSON data
                    webClient.Headers.Add("Content-Type", "application/json");

                    // Serialize the "name" parameter to JSON
                    string data = JsonConvert.SerializeObject(name);

                    try
                    {
                        // Make the POST request and upload the JSON data
                        //var response = webClient.UploadString(LinkCategory, data);
                        var response = webClient.UploadString(LinkCategory, "POST", data);

                        var result = JsonConvert.DeserializeObject<Category>(response);
                        Console.WriteLine(result.CategoryId + "\t" + result.CategoryName);
                    }
                    catch (System.Net.WebException ex)
                    {
                        if (ex.Response is System.Net.HttpWebResponse errorResponse)
                        {
                            if (errorResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
                            {
                                Console.WriteLine("Not found!");
                            }
                            else
                            {
                                Console.WriteLine(errorResponse.StatusCode);
                            }
                        }
                    }
                }
            }

            public async Task UpdateCategory(int id, string? name)
            {
                Category category = new Category { CategoryId = id, CategoryName = name };

                using (System.Net.WebClient webClient = new System.Net.WebClient())
                {
                    webClient.Headers.Add("Content-Type", "application/json");
                    string data = JsonConvert.SerializeObject(category);
                    try
                    {
                        var response = webClient.UploadString(LinkCategory, "PUT", data);
                        var result = JsonConvert.DeserializeObject<Category>(response);
                        Console.WriteLine(result.CategoryId + "\t" + result.CategoryName);
                    }
                    catch (System.Net.WebException ex)
                    {
                        if (ex.Response is System.Net.HttpWebResponse errorResponse)
                        {
                            if (errorResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
                            {
                                Console.WriteLine("Not found!");
                            }
                            else
                            {
                                Console.WriteLine(errorResponse.StatusCode);
                            }
                        }
                    }
                }
            }

            public async Task DeleteCategory(int id)
            {
                using (System.Net.WebClient webClient = new System.Net.WebClient())
                {
                    try
                    {
                        var json = webClient.UploadString(LinkCategory + "/" + id, "DELETE", "");
                        var result = JsonConvert.DeserializeObject<Category>(json);
                        Console.WriteLine(result.CategoryId + "\t" + result.CategoryName);
                    }
                    catch (System.Net.WebException ex)
                    {
                        if (ex.Response is System.Net.HttpWebResponse errorResponse)
                        {
                            if (errorResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
                            {
                                Console.WriteLine("Not found!");
                            }
                            else
                            {
                                Console.WriteLine(errorResponse.StatusCode);
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region WebClientManageProduct
        internal class WebClientManageProduct
        {
            public async Task ShowListProductAsync()
            {
                using (System.Net.WebClient webClient = new System.Net.WebClient())
                {
                    var json = webClient.DownloadString(LinkProduct);
                    List<Product> list = JsonConvert.DeserializeObject<List<Product>>(json);
                    foreach (Product result in list)
                    {
                        Console.WriteLine(
                        result.ProductId + "\t" +
                        result.ProductName + "\t" +
                        result.UnitPrice + "\t" +
                        result.UnitsInStock + "\t" +
                        result.Image + "\t" +
                        result.Category.CategoryName);
                    }
                }
            }

            public async Task SearchProductById(int id)
            {
                using (System.Net.WebClient webClient = new System.Net.WebClient())
                {
                    var json = webClient.DownloadString(LinkProduct + "/" + id);
                    var result = JsonConvert.DeserializeObject<Product>(json);
                    Console.WriteLine(
                        result.ProductId + "\t" +
                        result.ProductName + "\t" +
                        result.UnitPrice + "\t" +
                        result.UnitsInStock + "\t" +
                        result.Image + "\t" +
                        result.Category.CategoryName);
                }
            }


            public async Task InsertProduct(string? name, decimal price, int quantity, string? image, int categoryId)
            {
                Product product = new Product
                {
                    ProductName = name,
                    UnitPrice = price,
                    UnitsInStock = quantity,
                    Image = image,
                    CategoryId = categoryId
                };

                using (System.Net.WebClient webClient = new System.Net.WebClient())
                {
                    webClient.Headers.Add("Content-Type", "application/json");

                    string data = JsonConvert.SerializeObject(product);

                    var response = webClient.UploadString(LinkProduct, "POST", data);

                    var result = JsonConvert.DeserializeObject<Product>(response);
                    Console.WriteLine(
                        result.ProductId + "\t" +
                        result.ProductName + "\t" +
                        result.UnitPrice + "\t" +
                        result.UnitsInStock + "\t" +
                        result.Image + "\t" +
                        result.Category.CategoryName);
                }
            }

            public async Task UpdateProduct(Product product)
            {
                using (System.Net.WebClient webClient = new System.Net.WebClient())
                {
                    webClient.Headers.Add("Content-Type", "application/json");
                    string data = JsonConvert.SerializeObject(product);
                    var response = webClient.UploadString(LinkProduct, "PUT", data);
                    var result = JsonConvert.DeserializeObject<Product>(response);
                    Console.WriteLine(
                        result.ProductId + "\t" +
                        result.ProductName + "\t" +
                        result.UnitPrice + "\t" +
                        result.UnitsInStock + "\t" +
                        result.Image + "\t" +
                        result.CategoryId);
                }
            }

            public async Task DeleteProduct(int id)
            {
                using (System.Net.WebClient webClient = new System.Net.WebClient())
                {
                    var json = webClient.UploadString(LinkProduct + "/" + id, "DELETE", "");
                    Console.WriteLine(json);
                }
            }
        }
        #endregion
    }
}
