using ClientApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace ClientApp.Manager
{
    class HttpClientManager
    {
        private const string LinkCategory = "http://localhost:5250/api/Category";
        private const string LinkProduct = "http://localhost:5250/api/Product";

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
                            Console.Write("Manage Product");
                            await ManageProduct();
                            break;
                        }
                }
            }
        }

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
                            await ShowListCategoryAsync();
                            break;
                        }
                    case 2:
                        {
                            Console.Write("Enter id: ");
                            var id = Convert.ToInt32(Console.ReadLine());
                            await SearchCategoryById(id);
                            break;
                        }
                    case 3:
                        {
                            Console.Write("Enter name: ");
                            string name = Console.ReadLine();
                            await InsertCategoryDemo2(name);
                            break;
                        }
                    case 4:
                        {
                            Console.Write("Enter id: ");
                            int id = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter name: ");
                            string name = Console.ReadLine();
                            await UpdateCategory(id, name);
                            break;
                        }
                    case 5:
                        {
                            Console.Write("Enter id: ");
                            int id = Convert.ToInt32(Console.ReadLine());
                            await DeleteCategory(id);
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
                            await ShowListProductAsync();
                            break;
                        }
                    case 2:
                        {
                            Console.Write("Enter id: ");
                            var id = Convert.ToInt32(Console.ReadLine());
                            await SearchProductById(id);
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
                            await InsertProduct(name, price, quantity, image, categoryId);
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
                            await UpdateProduct(p);
                            break;
                        }
                    case 5:
                        {
                            Console.Write("Enter id: ");
                            int id = Convert.ToInt32(Console.ReadLine());
                            await DeleteProduct(id);
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
}