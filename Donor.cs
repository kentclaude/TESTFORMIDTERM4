using System;
using System.Collections.Generic;
using System.Linq;

namespace CommunityFoodWasteSharing
{
    public class Donor
    {
        private string name;
        private string contact;
        private string barangay;
        private string donorId;
        private bool isRegistered = false;
        private FileManager fileManager = new FileManager();

        public void CreateAccount()
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("              CREATE DONOR ACCOUNT");
            Console.WriteLine(new string('=', 60));

            donorId = GenerateId();

            Console.Write("Name: ");
            name = Console.ReadLine();
            Console.Write("Contact: ");
            contact = Console.ReadLine();

            Console.WriteLine("\nSelect Barangay:");
            for (int i = 0; i < FileManager.BARANGAYS.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {FileManager.BARANGAYS[i]}");
            }
            Console.Write("Enter barangay number: ");
            int barangayIndex = int.Parse(Console.ReadLine()) - 1;
            barangay = FileManager.BARANGAYS[barangayIndex];

            fileManager.SaveDonor(donorId, name, contact, barangay);
            isRegistered = true;

            Console.WriteLine($"\n✓ Account created successfully!");
            Console.WriteLine(new string('=', 60));
            Console.WriteLine("              ACCOUNT DETAILS");
            Console.WriteLine(new string('=', 60));
            Console.WriteLine($"   Your Donor ID: {donorId}");
            Console.WriteLine($"   Name: {name}");
            Console.WriteLine($"   Contact: {contact}");
            Console.WriteLine($"   Barangay: {barangay}");
            Console.WriteLine(new string('=', 60));
            Console.WriteLine("\n⚠️  IMPORTANT: Please remember your Donor ID!");
            Console.WriteLine($"   You will need this ID ({donorId}) to log in.");
            Console.WriteLine("   Write it down or save it somewhere safe.");
            Console.WriteLine(new string('=', 60));
        }

        public bool Login()
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("                 DONOR LOGIN");
            Console.WriteLine(new string('=', 60));
            Console.Write("Donor ID: ");
            string inputId = Console.ReadLine();

            string donorInfo = fileManager.GetDonorInfo(inputId);

            if (donorInfo == null)
            {
                Console.WriteLine("❌ Donor ID not found. Please create an account first.");
                return false;
            }

            string[] parts = donorInfo.Split('|');
            donorId = inputId;
            name = parts[0];
            contact = parts[1];
            barangay = parts[2];
            isRegistered = true;

            Console.WriteLine($"✓ Welcome back, {name} from Barangay {barangay}!");
            return true;
        }

        public void DonateItem()
        {
            if (!isRegistered)
            {
                Console.WriteLine("❌ Please log in first.");
                return;
            }

            Donation donation = new Donation();
            donation.CreateDonation(donorId, name, barangay);
        }

        public void ViewMyDonations()
        {
            if (!isRegistered)
            {
                Console.WriteLine("❌ Please log in first.");
                return;
            }

            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("              YOUR DONATIONS");
            Console.WriteLine(new string('=', 60));

            List<string> donations = fileManager.GetDonationsByDonor(donorId);

            if (donations.Count == 0)
            {
                Console.WriteLine("No donations found for your account.");
                return;
            }
            Dictionary<string, int> categoryTotals = new Dictionary<string, int>();

            foreach (string donation in donations)
            {
                string[] parts = donation.Split('|');
                string category = parts[2];
                int quantity = int.Parse(parts[3]);

                if (categoryTotals.ContainsKey(category))
                {
                    categoryTotals[category] += quantity;
                }
                else
                {
                    categoryTotals[category] = quantity;
                }
            }

            Console.WriteLine($"\nTotal Donation Records: {donations.Count}");
            Console.WriteLine($"Total Categories: {categoryTotals.Count}\n");

            Console.WriteLine(new string('-', 60));
            Console.WriteLine($"{"Category",-20} | {"Total Quantity",-15}");
            Console.WriteLine(new string('-', 60));

            foreach (var kvp in categoryTotals.OrderBy(x => x.Key))
            {
                Console.WriteLine($"{kvp.Key,-20} | {kvp.Value,-15}");
            }

            Console.WriteLine(new string('-', 60));

            Console.WriteLine($"\n GRAND TOTAL: {categoryTotals.Values.Sum()} items donated");
        }

        private string GenerateId()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }
}