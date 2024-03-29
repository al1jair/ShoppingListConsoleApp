using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;

namespace ShoppingListApp
{
    public class ShoppingItem
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Quantity { get; set; }
    }

    public class ShoppingListContext : DbContext
    {
        public DbSet<ShoppingItem> ShoppingItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=shoppinglist.db");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new ShoppingListContext())
            {
                context.Database.EnsureCreated();
            }

            while (true)
            {
                Console.WriteLine("Time to go to the grocery again... yay. See options below and create a list!");
                Console.WriteLine("1) Add an item");
                Console.WriteLine("2) Remove an item");
                Console.WriteLine("3) Adjust quantity of an item");
                Console.WriteLine("4) Check items on the list");
                Console.WriteLine("5) Exit");

                Console.Write("Enter your choice: ");
                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        AddItem();
                        break;
                    case 2:
                        RemoveItem();
                        break;
                    case 3:
                        AdjustQuantity();
                        break;
                    case 4:
                        ShowRemainingItems();
                        break;
                    case 5:
                        Console.WriteLine("Perfect! List is set. Let's head to the store and get this over with!");
                        Console.ReadLine();
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                        break;
                }
            }
        }

        static void AddItem()
        {
            using (var context = new ShoppingListContext())
            {
                Console.Write("Enter the item name: ");
                string itemName = Console.ReadLine().Trim();

                Console.Write("Enter the quantity: ");
                int quantity;
                if (!int.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)
                {
                    Console.WriteLine("Invalid entry. Please enter a positive integer.");
                    return;
                }

                var existingItem = context.ShoppingItems.FirstOrDefault(item => item.Name == itemName);
                if (existingItem != null)
                {
                    existingItem.Quantity += quantity;
                }
                else
                {
                    context.ShoppingItems.Add(new ShoppingItem { Name = itemName, Quantity = quantity });
                }

                context.SaveChanges();

                Console.WriteLine($"{quantity} {itemName}(s) added to the shopping list.");
            }
        }

        static void RemoveItem()
        {
            using (var context = new ShoppingListContext())
            {
                Console.Write("Enter the item name to remove: ");
                string itemName = Console.ReadLine().Trim();

                var itemToRemove = context.ShoppingItems.FirstOrDefault(item => item.Name == itemName);
                if (itemToRemove != null)
                {
                    context.ShoppingItems.Remove(itemToRemove);
                    context.SaveChanges();
                    Console.WriteLine($"{itemName} removed from the shopping list.");
                }
                else
                {
                    Console.WriteLine("Item not found in the shopping list.");
                }
            }
        }

        static void AdjustQuantity()
        {
            using (var context = new ShoppingListContext())
            {
                Console.Write("Enter the item name to adjust quantity: ");
                string itemName = Console.ReadLine().Trim();

                var itemToAdjust = context.ShoppingItems.FirstOrDefault(item => item.Name == itemName);
                if (itemToAdjust != null)
                {
                    Console.Write("Enter the new quantity: ");
                    int newQuantity;
                    if (!int.TryParse(Console.ReadLine(), out newQuantity) || newQuantity <= 0)
                    {
                        Console.WriteLine("Invalid quantity. Please enter a positive integer.");
                        return;
                    }

                    itemToAdjust.Quantity = newQuantity;
                    context.SaveChanges();
                    Console.WriteLine($"{itemName} quantity adjusted to {newQuantity}.");
                }
                else
                {
                    Console.WriteLine("Item not found in the shopping list.");
                }
            }
        }

        static void ShowRemainingItems()
        {
            using (var context = new ShoppingListContext())
            {
                var remainingItems = context.ShoppingItems.ToList();

                if (remainingItems.Count == 0)
                {
                    Console.WriteLine("Shopping list is empty.");
                    return;
                }

                Console.WriteLine("Remaining items in the shopping list:");
                foreach (var item in remainingItems)
                {
                    Console.WriteLine($"{item.Name}: {item.Quantity}");
                }
            }
        }
    }
}