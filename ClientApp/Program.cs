using ClientApp.Manager;

namespace ClientApp
{
    internal class Program
    {
        private static readonly WebClientManager Web = new();
        private static readonly HttpClientManager Http = new();
        private static readonly RestSharpManager Rest = new();
        private static readonly GRpcManager GRpc = new();

        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine(new string('*', 20));
                Console.WriteLine("1. Use WebClient");
                Console.WriteLine("2. Use HttpClient");
                Console.WriteLine("3. Use RestSharp");
                Console.WriteLine("4. Use WCF");
                Console.WriteLine("5. Use GRPC");
                Console.WriteLine("0. Exit");
                Console.Write("Enter choice: ");
                int option = Convert.ToInt32(Console.ReadLine());
                switch (option)
                {
                    case 0:
                        return;
                    case 1:
                        await Web.Manage();
                        break;
                    case 2:
                        await Http.Manage();
                        break;
                    case 3:
                        await Rest.RestSharpCategoryProduct();
                        break;
                    case 4:
                        break;
                    case 5:
                        await GRpc.Manage();
                        break;
                }
            }
        }
    }
}