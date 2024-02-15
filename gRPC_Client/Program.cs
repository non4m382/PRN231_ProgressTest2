// using ProductOfferGrpcService.Protos;

using gRPC.Protos;
using Grpc.Net.Client;

while (true)
{
    Console.WriteLine(new string('*', 20));
    Console.WriteLine("1. Manage Category");
    Console.WriteLine("2. Manage Product");
    Console.WriteLine("0. Exit");
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
    }
}