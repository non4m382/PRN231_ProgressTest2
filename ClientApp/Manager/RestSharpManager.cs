using ClientApp.Models;
using Newtonsoft.Json;
using RestSharp;

namespace ClientApp.Manager
{
    internal class RestSharpManager
    {
        private const string link = "http://localhost:5250/api/Category";
        private const string linkProduct = "http://localhost:5250/api/Product";

        public async Task RestSharpCategoryProduct()
        {
            while (true)
            {
                Console.WriteLine("\n");
                Console.WriteLine(new string('*', 20));
                Console.WriteLine("1.Show list Category");
                Console.WriteLine("2.Search category by id");
                Console.WriteLine("3.Add category to DB");
                Console.WriteLine("4.Update category by id");
                Console.WriteLine("5.Delete category by id");
                Console.WriteLine("6.Show list product");
                Console.WriteLine("7.Search  by id");
                Console.WriteLine("8.Add product to DB");
                Console.WriteLine("9.Update product by id");
                Console.WriteLine("10.Delete product by id");
                Console.WriteLine("0.Exit");
                Console.Write("Enter choice: ");
                int option = Convert.ToInt32(Console.ReadLine());
                switch (option)
                {
                    case 0: return;
                    case 1:
                        {
                            Console.WriteLine("List category");
                            await ShowListAsync();
                            Console.WriteLine("Press any key to continue ");
                            Console.ReadKey();
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("Enter Id");
                            int id = Convert.ToInt32(Console.ReadLine());
                            await SearchByIdAsync(id);
                            Console.WriteLine("Press any key to continue ");
                            Console.ReadKey();
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Enter Name");
                            string name = Console.ReadLine();
                            await InsertAsync(name);
                            Console.WriteLine("Press any key to continue ");
                            Console.ReadKey();
                            break;
                        }
                    case 4:
                        {
                            Console.WriteLine("Enter Id");
                            int id = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter Name");
                            string name = Console.ReadLine();
                            await UpdateAsync(id, name);
                            Console.WriteLine("Press any key to continue ");
                            Console.ReadKey();
                            break;
                        }
                    case 5:
                        {
                            Console.WriteLine("Enter Id");
                            int id = Convert.ToInt32(Console.ReadLine());
                            await DeleteAsync(id);
                            Console.WriteLine("Press any key to continue ");
                            Console.ReadKey();
                            break;
                        }
                    case 6:
                        {
                            Console.WriteLine("List Product");
                            await ShowListProductAsync();
                            Console.WriteLine("Press any key to continue ");
                            Console.ReadKey();
                            break;
                        }
                    case 7:
                        {
                            Console.WriteLine("Enter Id");
                            int id = Convert.ToInt32(Console.ReadLine());
                            await SearchByIdProductAsync(id);
                            Console.WriteLine("Press any key to continue ");
                            Console.ReadKey();
                            break;
                        }
                    case 8:
                        {
                            Console.WriteLine("Enter Product Name");
                            string name = Console.ReadLine();

                            Console.WriteLine("Enter Unit Price");
                            decimal price = Convert.ToDecimal(Console.ReadLine());
                            Console.WriteLine("Enter Unit InStock");
                            int inStock = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter Category Id");
                            int CategoryId = Convert.ToInt32(Console.ReadLine());

                            Product p = new Product()
                            {
                                ProductName = name,
                                UnitPrice = price,
                                UnitsInStock = inStock,
                                CategoryId = CategoryId,
                            };
                            await InsertProductAsync(p);
                            Console.WriteLine("Press any key to continue ");
                            Console.ReadKey();
                            break;
                        }
                    case 9:
                        {
                            Console.WriteLine("Enter Product Id");
                            int id = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter Product Name");
                            string name = Console.ReadLine();

                            Console.WriteLine("Enter Unit Price");
                            decimal price = Convert.ToDecimal(Console.ReadLine());
                            Console.WriteLine("Enter Unit InStock");
                            int inStock = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter Category Id");
                            int CategoryId = Convert.ToInt32(Console.ReadLine());

                            Product p = new Product()
                            {
                                ProductId = id,
                                ProductName = name,
                                UnitPrice = price,
                                UnitsInStock = inStock,
                                CategoryId = CategoryId,
                            };
                            await UpdateProductAsync(p);
                            Console.WriteLine("Press any key to continue ");
                            Console.ReadKey();
                            break;
                        }
                    case 10:
                        {
                            Console.WriteLine("Enter Id");
                            int id = Convert.ToInt32(Console.ReadLine());
                            await DeleteProductAsync(id);
                            Console.WriteLine("Press any key to continue ");
                            Console.ReadKey();
                            break;
                        }
                }
            }
        }

        internal async Task ShowListAsync()
        {
            var client = new RestClient(link);
            var request = new RestRequest("", Method.Get);
            var response = client.Execute<List<Category>>(request);

            if (!response.IsSuccessful)
            {
                return;
            }

            var list = JsonConvert.DeserializeObject<List<Category>>(response.Content);

            foreach (Category item in list)
            {
                Console.WriteLine(item.CategoryId + "\t" + item.CategoryName);
            }
        }

        internal async Task SearchByIdAsync(int id)
        {
            var client = new RestClient(link);
            var request = new RestRequest("" + id, Method.Get);
            //    request.AddParameter("id", id);

            var response = client.Execute<Category>(request);
            var cate = JsonConvert.DeserializeObject<Category>(response.Content);
            Console.WriteLine(cate.CategoryId + "\t" + cate.CategoryName);
        }

        internal async Task InsertAsync(string name)
        {
            var client = new RestClient(link);
            var request = new RestRequest("", Method.Post);
            request.AddParameter("application/json", JsonConvert.SerializeObject(name), ParameterType.RequestBody);

            var response = client.Execute<Category>(request);
            if (response.IsSuccessful)
                Console.WriteLine("Add success");
            else
                Console.WriteLine("Add not success");
        }

        internal async Task UpdateAsync(int id, string name)
        {
            var client = new RestClient(link);
            var request = new RestRequest("", Method.Put);
            Category ca = new Category()
            {
                CategoryId = id,
                CategoryName = name
            };
            request.AddParameter("application/json", JsonConvert.SerializeObject(ca), ParameterType.RequestBody);

            var response = client.Execute<Category>(request);

            if (response.IsSuccessful)
                Console.WriteLine("Update success");
            else
                Console.WriteLine("Update not success");
        }

        internal async Task DeleteAsync(int id)
        {
            var client = new RestClient(link);
            var request = new RestRequest("" + id, Method.Delete);
            //request.AddParameter("id", id);

            var response = client.Execute<Category>(request);

            if (response.IsSuccessful)
                Console.WriteLine("Delete  success");
            else
                Console.WriteLine("Delete not success");
        }

        //-----------------------------------------------------------------------------------------------------------------
        internal async Task ShowListProductAsync()
        {
            var client = new RestClient(linkProduct);
            var request = new RestRequest("", Method.Get);

            var response = client.Execute<List<Product>>(request);
            Console.WriteLine($"Status: {response.StatusCode}");

            if (!response.IsSuccessful)
            {
                return;
            }

            var list = JsonConvert.DeserializeObject<List<Product>>(response.Content);

            foreach (Product item in list)
            {
                Console.WriteLine(item.ProductId + "\t" + item.ProductName + "\t" + item.UnitPrice
                                  + "\t" + item.UnitsInStock + "\t" + item.Image + "\t" + item.CategoryId);
            }
        }

        internal async Task SearchByIdProductAsync(int id)
        {
            var client = new RestClient(linkProduct);
            var request = new RestRequest("" + id, Method.Get);
            //request.AddParameter("id", id);

            var response = client.Execute<Product>(request);
            var p = JsonConvert.DeserializeObject<Product>(response.Content);
            Console.WriteLine(p.ProductId + "\t" + p.ProductName + "\t" + p.UnitPrice
                              + "\t" + p.UnitsInStock + "\t" + p.Image + "\t" + p.CategoryId);
        }

        internal async Task InsertProductAsync(Product p)
        {
            var client = new RestClient(linkProduct);
            var request = new RestRequest("", Method.Post);
            request.AddParameter("application/json", JsonConvert.SerializeObject(p), ParameterType.RequestBody);

            var response = client.Execute<Product>(request);
            if (response.IsSuccessful)
                Console.WriteLine("Add success");
            else
                Console.WriteLine("Add not success");
        }

        internal async Task UpdateProductAsync(Product p)
        {
            var client = new RestClient(linkProduct);
            var request = new RestRequest("", Method.Put);

            request.AddParameter("application/json", JsonConvert.SerializeObject(p), ParameterType.RequestBody);

            var response = client.Execute<Product>(request);

            if (response.IsSuccessful)
                Console.WriteLine("Update success");
            else
                Console.WriteLine("Update not success");
        }

        internal async Task DeleteProductAsync(int id)
        {
            var client = new RestClient(linkProduct);
            var request = new RestRequest("" + id, Method.Delete);
            //request.AddParameter("id", id);

            var response = client.Execute<Product>(request);

            if (response.IsSuccessful)
                Console.WriteLine("Delete not success");
            else
                Console.WriteLine("Delete  success");
        }
    }
}