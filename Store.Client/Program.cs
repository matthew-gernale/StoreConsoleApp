
using Microsoft.Extensions.DependencyInjection;
using Store.Client.Pages;

class Program
{
    static async Task Main()
    {
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5157/")
        };

        var inventoryManager = new ClientInventoryManager(httpClient);
        var menu = new Menu(inventoryManager);

        await menu.ShowMainMenu();
    }
}
