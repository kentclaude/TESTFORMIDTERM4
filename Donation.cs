using System;

namespace CommunityFoodWasteSharing
{
    public class Donation
    {
        private string donationItemNumber;
        private string donorId;
        private string donorName;
        private string itemCategory;
        private int quantity;
        private string barangay;
        private FileManager fileManager = new FileManager();

        public void CreateDonation(string donorId, string donorName, string barangay)
        {
            this.donorId = donorId;
            this.donorName = donorName;
            this.barangay = barangay;

            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("              DONATE ITEM");
            Console.WriteLine(new string('=', 60));

            donationItemNumber = GenerateItemNumber();

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

            Console.Write("Quantity: ");
            quantity = int.Parse(Console.ReadLine());

            fileManager.SaveDonation(donationItemNumber, donorId, donorName, itemCategory, quantity, barangay);

            Console.WriteLine($"\n✓ Donation recorded successfully!");
            Console.WriteLine(new string('-', 60));
            Console.WriteLine($"{"FIELD",-20} | {"VALUE",-35}");
            Console.WriteLine(new string('-', 60));
            Console.WriteLine($"{"Item Number",-20} | {donationItemNumber,-35}");
            Console.WriteLine($"{"Category",-20} | {itemCategory,-35}");
            Console.WriteLine($"{"Quantity",-20} | {quantity,-35}");
            Console.WriteLine($"{"Barangay",-20} | {barangay,-35}");
            Console.WriteLine(new string('-', 60));
        }

        private string GenerateItemNumber()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }
}