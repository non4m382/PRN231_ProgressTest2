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
        static RestSharpManager r = new RestSharpManager();

        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine(new string('*', 20));
                Console.WriteLine("1. Using HttpClient");
                Console.WriteLine("2. Using WebClient");
                Console.WriteLine("3. Using Rest Sharp");
                Console.WriteLine("4. Using Service References");
                Console.WriteLine("5. Using gRPC");
                Console.WriteLine("0. Exit");
                Console.Write("Enter choice: ");
                int option = Convert.ToInt32(Console.ReadLine());
                switch (option)
                {
                    case 0:
                        return;
                    case 1:
                        await m.ManageCategoryProduct();
                        break;
                    case 2:
                        await m.ManageCategoryProduct();
                        break;
                    case 3:
                        await r.RestSharpCategoryProduct();
                        break;
                    case 4:
                        await r.RestSharpCategoryProduct();
                        break;
                    case 5:
                        await r.RestSharpCategoryProduct();
                        break;
                }
            }
        }
        
      
    }


}