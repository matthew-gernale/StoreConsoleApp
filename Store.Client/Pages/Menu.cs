
using System.Globalization;
using System.Net;

namespace Store.Client.Pages
{
    public class Menu
    {
        private readonly IClientInventoryManager _inventoryManager;
        public Menu(IClientInventoryManager inventoryManager)
        {
            _inventoryManager = inventoryManager;
        }

        public async Task ShowMainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===============================");
                Console.WriteLine("  Inventory Management System");
                Console.WriteLine("===============================");
                Console.WriteLine();
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. Remove Product");
                Console.WriteLine("3. Update Product Quantity");
                Console.WriteLine("4. List Products");
                Console.WriteLine("5. Get Total Inventory Value");
                Console.WriteLine("6. Exit");
                Console.WriteLine();
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        await ShowAddProductMenu();
                        break;
                    case "2":
                        await ShowRemoveProductMenu();
                        break;
                    case "3":
                        await ShowUpdateProductMenu();
                        break;
                    case "4":
                        await ShowAllProducts();
                        break;
                    case "5":
                        await DisplayTotalValue();
                        break;
                    case "6":
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private async Task ShowAddProductMenu()
        {
            Console.Clear();
            Console.Write("Enter product name: ");
            string name = Console.ReadLine();

            Console.Write("Enter quantity: ");
            int quantity = int.Parse(Console.ReadLine());

            Console.Write("Enter price: ");
            decimal price = decimal.Parse(Console.ReadLine());

            var response = await _inventoryManager.AddProduct(
                new Product
                {
                    Name = name ?? "Unknown",
                    QuantityInStock = quantity,
                    Price = price
                });

            if (response.IsSuccess) Console.WriteLine($"Successfully added {name} to the product list.");
            else Console.WriteLine(response.ErrorMessage);

            Console.WriteLine();
            Console.Write("Press any key to return...");
            Console.ReadKey();
        }

        private async Task ShowRemoveProductMenu()
        {
            Console.Clear();
            Console.Write("Enter product ID to remove: ");
            int productId = int.Parse(Console.ReadLine());

            var response = await _inventoryManager.RemoveProduct(productId);
            if (response.IsSuccess) Console.WriteLine($"Successfully removed product #{productId}.");
            else Console.WriteLine(response.ErrorMessage);

            Console.WriteLine();
            Console.Write("Press any key to return...");
            Console.ReadKey();
        }

        private async Task ShowUpdateProductMenu()
        {
            Console.Clear();
            Console.Write("Enter product ID to update: ");
            int productId = int.Parse(Console.ReadLine());

            Console.Write("Enter new quantity: ");
            int newQuantity = int.Parse(Console.ReadLine());

            var response = await _inventoryManager.UpdateProduct(productId, newQuantity);
            if (response.IsSuccess) Console.WriteLine($"Successfully updated product #{productId}.");
            else Console.WriteLine(response.ErrorMessage);

            Console.WriteLine();
            Console.Write("Press any key to return...");
            Console.ReadKey();
        }

        private async Task ShowAllProducts()
        {
            Console.Clear();
            var response = await _inventoryManager.ListProducts();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                const int idWidth = 5, nameWidth = 25, priceWidth = 12, stockWidth = 8;
                int totalWidth = idWidth + nameWidth + priceWidth + stockWidth + 12;

                // table header
                Console.WriteLine(new string('-', totalWidth));
                Console.WriteLine($"| {"ID",-idWidth} | {"Name",-nameWidth} | {"Price",-priceWidth} | {"Stock",-stockWidth}|");
                Console.WriteLine(new string('-', totalWidth));

                // table data
                foreach (ProductDTO product in response.Data)
                {
                    Console.WriteLine($"| {product.Id,-idWidth} | {product.Name,-nameWidth} | P {product.Price,-(priceWidth - 2)} | {product.QuantityInStock,-stockWidth}|");
                }

                
                Console.WriteLine(new string('-', totalWidth));
            }
            else Console.WriteLine("No product yet.");

            Console.WriteLine();
            Console.Write("Press any key to return...");
            Console.ReadKey();
        }


        private async Task DisplayTotalValue()
        {
            Console.Clear();
            var response = await _inventoryManager.GetTotalValue();
            if (response.IsSuccess) Console.WriteLine($"Total value: {response.Data}");
            else Console.WriteLine(response.ErrorMessage);

            Console.WriteLine();
            Console.Write("Press any key to return...");
            Console.ReadKey();
        }
    }
}
