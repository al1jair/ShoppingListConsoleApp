namespace ShoppingListApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var shoppingList = new Dictionary<string, int>();

            while (true)
            {
                Console.WriteLine("Time to go to the groccery again...yay. See options below and create a list!");
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
                        AddItem(shoppingList);
                        break;
                    case 2:
                        RemoveItem(shoppingList);
                        break;
                    case 3:
                        AdjustQuantity(shoppingList);
                        break;
                    case 4:
                        ShowRemainingItems(shoppingList);
                        break;
                    case 5:
                        Console.WriteLine("Perfect! List is set lets head to the store and get this over with!");
                        Console.ReadLine();
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                        break;
                }
            }
        }

        static void AddItem(Dictionary<string, int> shoppingList)
        {
            Console.Write("Enter the item name: ");
            string item = Console.ReadLine().Trim();

            Console.Write("Enter the quantity: ");
            int quantity;
            if (!int.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)
            {
                Console.WriteLine("Invalid entry. Please enter a positive integer.");
                return;
            }

            if (shoppingList.ContainsKey(item))
                shoppingList[item] += quantity;
            else
                shoppingList[item] = quantity;

            Console.WriteLine($"{quantity} {item}(s) added to the shopping list.");
        }

        static void RemoveItem(Dictionary<string, int> shoppingList)
        {
            Console.Write("Enter the item name to remove: ");
            string item = Console.ReadLine().Trim();

            if (shoppingList.ContainsKey(item))
            {
                shoppingList.Remove(item);
                Console.WriteLine($"{item} removed from the shopping list.");
            }
            else
            {
                Console.WriteLine("Item not found in the shopping list.");
            }
        }

        static void AdjustQuantity(Dictionary<string, int> shoppingList)
        {
            Console.Write("Enter the item name to adjust quantity: ");
            string item = Console.ReadLine().Trim();

            if (shoppingList.ContainsKey(item))
            {
                Console.Write("Enter the new quantity: ");
                int newQuantity;
                if (!int.TryParse(Console.ReadLine(), out newQuantity) || newQuantity <= 0)
                {
                    Console.WriteLine("Invalid quantity. Please enter a positive integer.");
                    return;
                }

                shoppingList[item] = newQuantity;
                Console.WriteLine($"{item} quantity adjusted to {newQuantity}.");
            }
            else
            {
                Console.WriteLine("Item not found in the shopping list.");
            }
        }

        static void ShowRemainingItems(Dictionary<string, int> shoppingList)
        {
            if (shoppingList.Count == 0)
            {
                Console.WriteLine("Shopping list is empty.");
                return;
            }

            Console.WriteLine("Remaining items in the shopping list:");
            foreach (var item in shoppingList)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
        }
    }
}