using ClientApp.Manager;

namespace ClientApp
{
    internal class Program
    {
        static HttpClientManager http = new HttpClientManager();
        static RestSharpManager rest = new RestSharpManager();

        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine(new string('*', 20));
                Console.WriteLine("1. Use HttpClient");
                Console.WriteLine("2. Use RestSharp");
                Console.WriteLine("0. Exit");
                Console.Write("Enter choice: ");
                int option = Convert.ToInt32(Console.ReadLine());
                switch (option)
                {
                    case 0:
                        return;
                    case 1:
                        await http.Manage();
                        break;
                    case 2:
                        await rest.RestSharpCategoryProduct();
                        break;
                }
            }
        }
    }
}