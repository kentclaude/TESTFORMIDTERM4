using System;
using System.Collections.Generic;

namespace CommunityFoodWasteSharing
{
    public class Request
    {
        private string requestId;
        private string requesterName;
        private string barangay;
        private string itemCategory;
        private int quantity;
        private FileManager fileManager = new FileManager();

        public void SubmitRequest()
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("              SUBMIT REQUEST");
            Console.WriteLine(new string('=', 60));

            requestId = GenerateId();

            Console.Write("Your Name: ");
            requesterName = Console.ReadLine();

            Console.WriteLine("\nSelect Barangay:");
            for (int i = 0; i < FileManager.BARANGAYS.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {FileManager.BARANGAYS[i]}");
            }
            Console.Write("Enter barangay number: ");
            int barangayIndex = int.Parse(Console.ReadLine()) - 1;
            barangay = FileManager.BARANGAYS[barangayIndex];

            Console.WriteLine("\nSelect Item Category:");
            Console.WriteLine("1. Food");
            Console.WriteLine("2. Water");
            Console.WriteLine("3. Clothes");
            Console.Write("Choose (1-3): ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    itemCategory = "Food";
                    break;
                case "2":
                    itemCategory = "Water";
                    break;
                case "3":
                    itemCategory = "Clothes";
                    break;
                default:
                    Console.WriteLine("❌ Invalid choice. Defaulting to Food.");
                    itemCategory = "Food";
                    break;
            }

            Console.Write("Quantity Needed: ");
            quantity = int.Parse(Console.ReadLine());

          
            fileManager.SavePendingRequest(requestId, requesterName, barangay, itemCategory, quantity);

            Console.WriteLine($"\n✓ Request submitted successfully!");
            Console.WriteLine($"   Request ID: {requestId}");
            Console.WriteLine($"   Name: {requesterName}");
            Console.WriteLine($"   Barangay: {barangay}");
            Console.WriteLine($"   Item: {itemCategory} (Qty: {quantity})");
            Console.WriteLine($"   Status: Pending");
            Console.WriteLine("\n   Your request will be reviewed by LGU admin.");
        }

        private string GenerateId()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }
}