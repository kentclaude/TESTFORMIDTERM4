using System;

namespace CommunityFoodWasteSharing
{
    public class Donation
    {
        private string donationId;
        private string donorId;
        private string foodType;
        private string barangay;
        private DateTime dateTimeReported;
        private int quantity;
        private string status;
        private FileManager fileManager = new FileManager();

        public void AddDonation(string donorId, string barangay)
        {
            this.donorId = donorId;
            this.barangay = barangay;

            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("              RECORD NEW DONATION");
            Console.WriteLine(new string('=', 60));

            while (true)
            {
                Console.Write("\nDonation ID: ");
                donationId = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(donationId))
                {
                    Console.WriteLine("❌ Donation ID cannot be empty. Please try again.");
                    continue;
                }

                if (fileManager.DonationExists(donationId))
                {
                    Console.WriteLine($"❌ Donation ID '{donationId}' already exists! Please use a different ID.");
                    continue;
                }

                break;
            }

            Console.Write("Food Type: ");
            foodType = Console.ReadLine();
            Console.Write("Quantity: ");
            quantity = int.Parse(Console.ReadLine());
            dateTimeReported = DateTime.Now;
            status = "Pending";

            fileManager.SaveDonation(donationId, donorId, foodType, quantity,
                                     barangay, dateTimeReported, status);

            Console.WriteLine($"\n✓ Donation recorded successfully!");
            Console.WriteLine(new string('-', 60));
            Console.WriteLine($"{"FIELD",-20} | {"VALUE",-35}");
            Console.WriteLine(new string('-', 60));
            Console.WriteLine($"{"Donation ID",-20} | {donationId,-35}");
            Console.WriteLine($"{"Food Type",-20} | {foodType,-35}");
            Console.WriteLine($"{"Quantity",-20} | {quantity,-35}");
            Console.WriteLine($"{"Barangay",-20} | {barangay,-35}");
            Console.WriteLine($"{"Status",-20} | {status,-35}");
            Console.WriteLine(new string('-', 60));
        }

        public string GetDonationDetails()
        {
            return $"{donationId}|{donorId}|{foodType}|{quantity}|{barangay}|{dateTimeReported}|{status}";
        }
    }
}