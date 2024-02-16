using gRPC.Protos;
using Grpc.Net.Client;

namespace ClientApp.Manager
{
    class GRpcManager
    {
        public async Task Manage()
        {
            while (true)
            {
                Console.WriteLine("\n");
                Console.WriteLine(new string('*', 20));
                Console.WriteLine("1. List Category");
                Console.WriteLine("2. Search Category by Id");
                Console.WriteLine("3. Add Category");
                Console.WriteLine("4. Update Category");
                Console.WriteLine("5. Delete Category by Id");
                Console.WriteLine("0. Exit");
                Console.WriteLine(new string('*', 20));
                Console.Write("Enter choice: ");
                int option = Convert.ToInt32(Console.ReadLine());
                var channel = GrpcChannel.ForAddress("http://localhost:5134");
                var client = new CategoryService.CategoryServiceClient(channel);
                switch (option)
                {
                    case 0:
                        return;
                    case 1:
                        var serverReply = client.GetCategoryListAsync(new Empty { });
                        Console.WriteLine(serverReply.ResponseAsync.Result);
                        break;
                    case 2:
                        // await m.ManageProduct();
                        Console.Write("Enter categoryId: ");
                        int categoryId = Convert.ToInt32(Console.ReadLine());
                        var request = new GetCategoryDetailRequest
                        {
                            CategoryId = categoryId
                        };
                        var searchResponse = client.GetCategory(request);
                        Console.WriteLine(searchResponse.ToString());
                        break;
                    case 3:
                        Console.Write("Enter categoryName: ");
                        string? categoryName = Console.ReadLine();
                        var createRequest = new CreateCategoryDetailRequest
                        {
                            Category = new CategoryDetail()
                            {
                                CategoryName = categoryName
                            }
                        };
                        var createResponse = await client.CreateCategoryAsync(createRequest);
                        Console.WriteLine(createResponse.ToString());
                        break;
                    case 4:
                        Console.Write("Enter categoryId: ");
                        int updateCategoryId = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter categoryName: ");
                        string? updateCategoryName = Console.ReadLine();
                        var updateRequest = new UpdateCategoryDetailRequest()
                        {
                            Category = new CategoryDetail()
                            {
                                CategoryId = updateCategoryId,
                                CategoryName = updateCategoryName
                            }
                        };
                        var updateResponse = await client.UpdateCategoryAsync(updateRequest);
                        Console.WriteLine(updateResponse.ToString());
                        break;
                    case 5:
                        Console.Write("Enter categoryId: ");
                        int deleteCategoryId = Convert.ToInt32(Console.ReadLine());
                        var deleteRequest = new DeleteCategoryDetailRequest()
                        {
                            CategoryId = deleteCategoryId
                        };
                        var deleteResponse = await client.DeleteCategoryAsync(deleteRequest);
                        Console.WriteLine(deleteResponse.ToString());
                        break;
                }
            }
        }
    }
}