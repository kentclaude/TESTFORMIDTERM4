using System;
using System.Collections.Generic;

namespace CommunityFoodWasteSharing
{
    public class Donor : User
    {
        private string contact;
        private string address;
        private string barangay;
        private string donorId;
        private bool isRegistered = false;
        private FileManager fileManager = new FileManager();

        public Donor() : base() { }

        public void CreateAccount()
        {
            Console.WriteLine("\n=== Create Donor Account ===");
            Console.Write("Donor ID: ");
            donorId = Console.ReadLine();

            if (fileManager.DonorExists(donorId))
            {
                Console.WriteLine("❌ This Donor ID already exists! Please login instead.");
                return;
            }

            Console.Write("Name: ");
            Name = Console.ReadLine();
            Console.Write("Contact: ");
            contact = Console.ReadLine();
            Console.Write("Address: ");
            address = Console.ReadLine();

            Console.WriteLine("\nSelect Barangay:");
            for (int i = 0; i < FileManager.BARANGAYS.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {FileManager.BARANGAYS[i]}");
            }
            Console.Write("Enter barangay number: ");
            int barangayIndex = int.Parse(Console.ReadLine()) - 1;
            barangay = FileManager.BARANGAYS[barangayIndex];

            fileManager.SaveDonor(donorId, Name, contact, address, barangay);
            isRegistered = true;
            Console.WriteLine($"✓ Account created for {Name} from Barangay {barangay}!");
        }

        public bool Login()
        {
            Console.WriteLine("\n=== Donor Login ===");
            Console.Write("Donor ID: ");
            string inputId = Console.ReadLine();

            string donorInfo = fileManager.GetDonorInfo(inputId);

            if (donorInfo == null)
            {
                Console.WriteLine("❌ Donor ID not found. Please create an account first.");
                return false;
            }

            string[] parts = donorInfo.Split('|');
            Name = parts[0];
            contact = parts[1];
            address = parts[2];
            barangay = parts[3];
            donorId = inputId;
            isRegistered = true;

            Console.WriteLine($"✓ Welcome back, {Name} from Barangay {barangay}!");
            return true;
        }

        public void RecordDonation()
        {
            if (!isRegistered)
            {
                Console.WriteLine("❌ Please log in first.");
                return;
            }

            Donation donation = new Donation();
            donation.AddDonation(donorId, barangay);
        }

        public void ViewDonationHistory()
        {
            if (!isRegistered)
            {
                Console.WriteLine("❌ Please log in first.");
                return;
            }

            Console.WriteLine("\n╔════════════════════════════════════════════════╗");
            Console.WriteLine("║         YOUR DONATION HISTORY                 ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝");
            List<string> donations = fileManager.GetDonationsByDonor(donorId);

            if (donations.Count == 0)
            {
                Console.WriteLine("No donations found for your account.");
                return;
            }

            Console.WriteLine($"\nTotal Donations: {donations.Count}\n");

            // Table Header
            Console.WriteLine(new string('-', 110));
            Console.WriteLine($"{"Donation ID",-13} | {"Food Type",-20} | {"Quantity",-10} | {"Barangay",-15} | {"Date/Time",-20} | {"Status",-12}");
            Console.WriteLine(new string('-', 110));

            // Table Rows
            foreach (string donation in donations)
            {
                string[] parts = donation.Split('|');
                Console.WriteLine($"{parts[0],-13} | {parts[2],-20} | {parts[3],-10} | {parts[4],-15} | {parts[5],-20} | {parts[6],-12}");
            }
            Console.WriteLine(new string('-', 110));
        }
    }
}