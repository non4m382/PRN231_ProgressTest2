using ClientApp.Manager;

namespace ClientApp
{
    internal class Program
    {
        static HttpClientManager m = new HttpClientManager();

        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine(new string('*', 20));
                Console.WriteLine("1. Manage Category");
                Console.WriteLine("2. Manage Product");
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
                }
            }
        }
    }
}