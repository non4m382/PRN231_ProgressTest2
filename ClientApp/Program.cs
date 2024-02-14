using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using WebAPI.Models;

namespace ClientApp
{
    internal class Program
    {
        static Manager m = new Manager();
        static RestSharpAPI r = new RestSharpAPI();

        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine(new string('*', 20));
                Console.WriteLine("1. Manage Category");
                Console.WriteLine("2. Manage Product");
                Console.WriteLine("3. Rest Sharp Category|Product");
                Console.WriteLine("0. Exit");
                Console.Write("Enter choice: ");
                int option = Convert.ToInt32(Console.ReadLine());
                switch (option)
                {
                    case 0:
                        return;
                    case 1:
                        await m.ManageCategory();
                        break;
                    case 2:
                        await m.ManageProduct();
                        break;
                    case 3:
                        await r.RestSharpCategoryProduct();
                        break;
                }
            }
        }

        internal class Manager
        {
            private const string LinkCategory = "http://localhost:5250/api/Category";
            private const string LinkProduct = "http://localhost:5250/api/Product";

            public async Task ManageCategory()
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
                    Console.WriteLine("0.Exit");
                    Console.Write("Enter choice: ");
                    var option = Convert.ToInt32(Console.ReadLine());
                    switch (option)
                    {
                        case 0:
                            return;
                        case 1:
                            {
                                Console.WriteLine("List category");
                                await m.ShowListCategoryAsync();
                                break;
                            }
                        case 2:
                            {
                                Console.Write("Enter id: ");
                                var id = Convert.ToInt32(Console.ReadLine());
                                await m.SearchCategoryById(id);
                                break;
                            }
                        case 3:
                            {
                                Console.Write("Enter name: ");
                                string name = Console.ReadLine();
                                await m.InsertCategoryDemo2(name);
                                break;
                            }
                        case 4:
                            {
                                Console.Write("Enter id: ");
                                int id = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Enter name: ");
                                string name = Console.ReadLine();
                                await m.UpdateCategory(id, name);
                                break;
                            }
                        case 5:
                            {
                                Console.Write("Enter id: ");
                                int id = Convert.ToInt32(Console.ReadLine());
                                await m.DeleteCategory(id);
                                break;
                            }
                    }
                }
            }

            public async Task ManageProduct()
            {
                while (true)
                {
                    Console.WriteLine("\n");
                    Console.WriteLine(new string('*', 20));
                    Console.WriteLine("1.Show list product");
                    Console.WriteLine("2.Search  by id");
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
                                await m.ShowListProductAsync();
                                break;
                            }
                        case 2:
                            {
                                Console.Write("Enter id: ");
                                var id = Convert.ToInt32(Console.ReadLine());
                                await m.SearchProductById(id);
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
                                await m.InsertProduct(name, price, quantity, image, categoryId);
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
                                await m.UpdateProduct(p);
                                break;
                            }
                        case 5:
                            {
                                Console.Write("Enter id: ");
                                int id = Convert.ToInt32(Console.ReadLine());
                                await m.DeleteProduct(id);
                                break;
                            }
                    }
                }
            }

            private async Task DeleteProduct(int id)
            {
                using HttpClient client = new HttpClient();
                UriBuilder uriBuilder = new UriBuilder(LinkProduct + $"/{id}");
                // uriBuilder.Query = $"categoryName={Uri.EscapeDataString(id + "")}";
                using HttpResponseMessage res = await client.DeleteAsync(uriBuilder.Uri);
                using HttpContent content = res.Content;

                string data = await content.ReadAsStringAsync();
                Console.WriteLine(data);
                // Product list = JsonConvert.DeserializeObject<Product>(data);
                // Console.WriteLine(list.CategoryId + "\t" + list.ProductName);
            }

            private async Task UpdateProduct(Product updatedProduct)
            {
                using HttpClient client = new HttpClient();

                using HttpResponseMessage res = await client.PutAsJsonAsync(LinkProduct, updatedProduct);
                using HttpContent content = res.Content;

                string data = await content.ReadAsStringAsync();
                Console.WriteLine(data);
                Product list = JsonConvert.DeserializeObject<Product>(data);
                Console.WriteLine(list.ProductId + "\t" + list.ProductName + "\t" + list.UnitPrice +
                                  "\t" + list.UnitsInStock);
            }

            private async Task InsertProduct(string name, decimal price, int quantity, string image, int categoryId)
            {
                using HttpClient client = new HttpClient();

                ProductInsertDTO product = new ProductInsertDTO()
                {
                    ProductName = name,
                    UnitPrice = price,
                    UnitsInStock = quantity,
                    CategoryId = categoryId,
                    Image = image
                };

                using HttpResponseMessage res = await client.PostAsJsonAsync(LinkProduct, product);
                using HttpContent content = res.Content;

                string data = await content.ReadAsStringAsync();
                Console.WriteLine(data);
                Product list = JsonConvert.DeserializeObject<Product>(data);
                Console.WriteLine(list.ProductId + "\t" + list.ProductName + "\t" + list.UnitPrice +
                                  "\t" + list.UnitsInStock);
            }

            private async Task SearchProductById(int id)
            {
                using HttpClient client = new HttpClient();
                using HttpResponseMessage res = await client.GetAsync($"{LinkProduct}/{id}");
                if (res.StatusCode != HttpStatusCode.OK)
                {
                    Console.WriteLine("Search failed");
                }

                using HttpContent content = res.Content;
                string data = await content.ReadAsStringAsync();
                Product list = JsonConvert.DeserializeObject<Product>(data);
                Console.WriteLine(list.ProductId + "\t" + list.ProductName + "\t" + list.UnitPrice +
                                  "\t" + list.UnitsInStock + "\t" + list.Category.CategoryName);
            }

            private async Task ShowListProductAsync()
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(LinkProduct))
                    {
                        using (HttpContent content = res.Content)
                        {
                            string data = await content.ReadAsStringAsync();
                            // JArray j = JArray.Parse(data);
                            List<Product> list = JsonConvert.DeserializeObject<List<Product>>(data);
                            foreach (Product item in list)
                            {
                                Console.WriteLine(item.ProductId + "\t" + item.ProductName + "\t" + item.UnitPrice +
                                                  "\t" + item.UnitsInStock + "\t" + item.Category.CategoryName);
                            }

                            // Console.WriteLine(j);
                        }
                    }
                }
            }

            public async Task ShowListCategoryAsync()
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(LinkCategory))
                    {
                        using (HttpContent content = res.Content)
                        {
                            string data = await content.ReadAsStringAsync();
                            JArray j = JArray.Parse(data);
                            // List<Category> list = JsonConvert.DeserializeObject<List<Category>>(data);
                            // foreach (Category item in list)
                            // {
                            //     Console.WriteLine(item.CategoryId + "\t" + item.CategoryName);
                            // }
                            Console.WriteLine(j);
                        }
                    }
                }
            }

            public async Task SearchCategoryById(int id)
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(LinkCategory + $"/{id}"))
                    {
                        // or !res.IsSuccessStatusCode
                        if (res.StatusCode != HttpStatusCode.OK)
                        {
                            Console.WriteLine("Search failed");
                        }

                        using (HttpContent content = res.Content)
                        {
                            string data = await content.ReadAsStringAsync();
                            // Console.WriteLine(data);
                            List<Category> list = JsonConvert.DeserializeObject<List<Category>>(data);
                            foreach (Category item in list)
                            {
                                Console.WriteLine(item.CategoryId + "\t" + item.CategoryName);
                            }
                        }
                    }
                }
            }

            public async Task InsertCategory(string name)
            {
                using (HttpClient client = new HttpClient())
                {
                    // Uri uri = new Uri(link);
                    // var payload = "{\"categoryName\":\"" + name + "\"}";
                    // HttpContent c = new StringContent(payload, Encoding.UTF8, "application/json");
                    UriBuilder uriBuilder = new UriBuilder(LinkCategory);
                    uriBuilder.Query = $"categoryName={name}";
                    using (HttpResponseMessage res = await client.PostAsync(uriBuilder.Uri, null))
                    {
                        using (HttpContent content = res.Content)
                        {
                            string data = await content.ReadAsStringAsync();
                            Console.WriteLine(data);
                            Category list = JsonConvert.DeserializeObject<Category>(data);
                            Console.WriteLine(list.CategoryId + "\t" + list.CategoryName);
                        }
                    }
                }
            }

            public async Task InsertCategoryDemo2(string? name)
            {
                using HttpClient client = new HttpClient();
                using HttpResponseMessage res = await client.PostAsJsonAsync(LinkCategory, name);
                using HttpContent content = res.Content;
                if (!res.IsSuccessStatusCode)
                {
                    Console.WriteLine("Add fail");
                }

                string data = await content.ReadAsStringAsync();
                Console.WriteLine(data);
                Category list = JsonConvert.DeserializeObject<Category>(data);
                Console.WriteLine(list.CategoryId + "\t" + list.CategoryName);
            }

            public async Task UpdateCategory(int id, string? name)
            {
                using (HttpClient client = new HttpClient())
                {
                    Uri uri = new Uri(LinkCategory);
                    var payload = "{\"categoryId\": \"" + id + "\",\"categoryName\": \"" + name + "\"}";
                    HttpContent c = new StringContent(payload, Encoding.UTF8, "application/json");
                    using (HttpResponseMessage res = await client.PutAsync(uri, c))
                    {
                        using (HttpContent content = res.Content)
                        {
                            string data = await content.ReadAsStringAsync();
                            Console.WriteLine(data);
                            Category list = JsonConvert.DeserializeObject<Category>(data);
                            Console.WriteLine(list.CategoryId + "\t" + list.CategoryName);
                        }
                    }
                }
            }

            public async Task DeleteCategory(int id)
            {
                using (HttpClient client = new HttpClient())
                {
                    UriBuilder uriBuilder = new UriBuilder(LinkCategory + $"/{id}");
                    // uriBuilder.Query = $"categoryName={Uri.EscapeDataString(id + "")}";
                    using (HttpResponseMessage res = await client.DeleteAsync(uriBuilder.Uri))
                    {
                        using (HttpContent content = res.Content)
                        {
                            string data = await content.ReadAsStringAsync();
                            Console.WriteLine(data);
                            Category list = JsonConvert.DeserializeObject<Category>(data);
                            Console.WriteLine(list.CategoryId + "\t" + list.CategoryName);
                        }
                    }
                }
            }
        }
        internal class RestSharpAPI
        {
            private const string link = "http://localhost:5250/api/Category";
            private const string linkProduct = "http://localhost:5250/api/Product";

            public async Task RestSharpCategoryProduct()
            {
                while (true)
                {
                    Console.WriteLine("1.Show List Category.");
                    Console.WriteLine("2.Search Category by Id.");
                    Console.WriteLine("3.Add Category to DB.");
                    Console.WriteLine("4.Update Category by Id.");
                    Console.WriteLine("5.Delete Category by Id.");
                    Console.WriteLine("6.Show List Product.");
                    Console.WriteLine("7.Search Product by Id.");
                    Console.WriteLine("8.Add Product to DB.");
                    Console.WriteLine("9.Update Product by Id.");
                    Console.WriteLine("10.Delete Product by Id.");
                    Console.WriteLine("0. Exit.");
                    int option = Convert.ToInt32(Console.ReadLine());
                    switch (option)
                    {
                        case 0: return;
                        case 1:
                            {
                                Console.WriteLine("List category");
                                await r.ShowListAsync();
                                Console.WriteLine("Press any key to continue ");
                                Console.ReadKey();
                                break;
                            }
                        case 2:
                            {
                                Console.WriteLine("Enter Id");
                                int id = Convert.ToInt32(Console.ReadLine());
                                await r.SearchByIdAsync(id);
                                Console.WriteLine("Press any key to continue ");
                                Console.ReadKey();
                                break;
                            }
                        case 3:
                            {
                                Console.WriteLine("Enter Name");
                                string name = Console.ReadLine();
                                await r.InsertAsync(name);
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
                                await r.UpdateAsync(id, name);
                                Console.WriteLine("Press any key to continue ");
                                Console.ReadKey();
                                break;
                            }
                        case 5:
                            {
                                Console.WriteLine("Enter Id");
                                int id = Convert.ToInt32(Console.ReadLine());
                                await r.DeleteAsync(id);
                                Console.WriteLine("Press any key to continue ");
                                Console.ReadKey();
                                break;
                            }
                        case 6:
                            {
                                Console.WriteLine("List Product");
                                await r.ShowListProductAsync();
                                Console.WriteLine("Press any key to continue ");
                                Console.ReadKey();
                                break;
                            }
                        case 7:
                            {
                                Console.WriteLine("Enter Id");
                                int id = Convert.ToInt32(Console.ReadLine());
                                await r.SearchByIdProductAsync(id);
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
                                await r.InsertProductAsync(p);
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
                                await r.UpdateProductAsync(p);
                                Console.WriteLine("Press any key to continue ");
                                Console.ReadKey();
                                break;
                            }
                        case 10:
                            {
                                Console.WriteLine("Enter Id");
                                int id = Convert.ToInt32(Console.ReadLine());
                                await r.DeleteProductAsync(id);
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
                var request = new RestRequest(""+id, Method.Get);
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
                var request = new RestRequest(""+id, Method.Delete);
                //request.AddParameter("id", id);

                var response = client.Execute<Category>(request);

                if (response.IsSuccessful)
                    Console.WriteLine("Delete success");
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
                    Console.WriteLine("Delete success");
                else
                    Console.WriteLine("Delete not success");
            }
        }

    }


}