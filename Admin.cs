using System;
using System.Collections.Generic;
using System.Linq;

namespace CommunityFoodWasteSharing
{
    public class Admin
    {
        private const string LGU_USERNAME = "LGU";
        private const string LGU_PASSWORD = "MANDAUE2024";
        private const string MAYOR_USERNAME = "MAYOR";
        private const string MAYOR_PASSWORD = "MAYOR2025";
        private FileManager fileManager = new FileManager();
        private string currentUser = "";

        public bool Login(string u, string p)
        {
            bool isValid = false;

            if (u == LGU_USERNAME && p == LGU_PASSWORD)
            {
                isValid = true;
                currentUser = "LGU Staff";
                Console.WriteLine($"✓ Welcome, Mandaue City LGU Staff!");
            }
            else if (u == MAYOR_USERNAME && p == MAYOR_PASSWORD)
            {
                isValid = true;
                currentUser = "Mayor";
                Console.WriteLine($"✓ Welcome, Mayor of Mandaue City!");
            }

            return isValid;
        }

        public void ViewAllDonors()
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("           ALL REGISTERED DONORS");
            Console.WriteLine(new string('=', 60));
            List<string> donors = fileManager.LoadDonors();

            if (donors.Count == 0)
            {
                Console.WriteLine("No donors registered yet.");
                return;
            }

            Console.WriteLine($"\nTotal Donors: {donors.Count}\n");

            Console.WriteLine(new string('-', 110));
            Console.WriteLine($"{"Donor Name",-25} | {"Barangay",-20} | {"Item Category",-20} | {"Quantity",-15}");
            Console.WriteLine(new string('-', 110));

            foreach (string donor in donors)
            {
                string[] donorParts = donor.Split('|');
                string donorId = donorParts[0];
                string donorName = donorParts[1];
                string barangay = donorParts[3];

                List<string> donations = fileManager.GetDonationsByDonor(donorId);

                if (donations.Count > 0)
                {
                    Dictionary<string, int> categoryTotals = new Dictionary<string, int>();

                    foreach (string donation in donations)
                    {
                        string[] donationParts = donation.Split('|');
                        string category = donationParts[2];
                        int quantity = int.Parse(donationParts[3]);

                        if (categoryTotals.ContainsKey(category))
                        {
                            categoryTotals[category] += quantity;
                        }
                        else
                        {
                            categoryTotals[category] = quantity;
                        }
                    }

                    foreach (var kvp in categoryTotals)
                    {
                        Console.WriteLine($"{donorName,-25} | {barangay,-20} | {kvp.Key,-20} | {kvp.Value,-15}");
                    }
                }
                else
                {
                    Console.WriteLine($"{donorName,-25} | {barangay,-20} | {"No donations yet",-20} | {"0",-15}");
                }
            }
            Console.WriteLine(new string('-', 110));
        }

        public void ViewAllDonations()
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("                ALL DONATIONS");
            Console.WriteLine(new string('=', 60));
            List<string> donations = fileManager.LoadDonations();

            if (donations.Count == 0)
            {
                Console.WriteLine("No donations recorded yet.");
                return;
            }

            Console.WriteLine($"\nTotal Donations: {donations.Count}\n");

            Console.WriteLine(new string('-', 90));
            Console.WriteLine($"{"Item Number",-15} | {"Category",-15} | {"Quantity",-10} | {"Barangay",-20}");
            Console.WriteLine(new string('-', 90));

            foreach (string donation in donations)
            {
                string[] parts = donation.Split('|');
                Console.WriteLine($"{parts[0],-15} | {parts[2],-15} | {parts[3],-10} | {parts[4],-20}");
            }
            Console.WriteLine(new string('-', 90));
        }

        public void ViewPendingRequests()
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("              PENDING REQUESTS");
            Console.WriteLine(new string('=', 60));

            List<string> pendingRequests = fileManager.LoadPendingRequests();

            if (pendingRequests.Count == 0)
            {
                Console.WriteLine("No pending requests.");
                return;
            }

            Console.WriteLine($"\nTotal Pending Requests: {pendingRequests.Count}\n");

            Console.WriteLine(new string('-', 100));
            Console.WriteLine($"{"Request ID",-12} | {"Name",-25} | {"Barangay",-20} | {"Category",-15} | {"Quantity",-10}");
            Console.WriteLine(new string('-', 100));

            foreach (string request in pendingRequests)
            {
                string[] parts = request.Split('|');
                Console.WriteLine($"{parts[0],-12} | {parts[1],-25} | {parts[2],-20} | {parts[3],-15} | {parts[4],-10}");
            }
            Console.WriteLine(new string('-', 100));
        }

        public void ProcessPendingRequests()
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("           PROCESS PENDING REQUESTS");
            Console.WriteLine(new string('=', 60));

            List<string> pendingRequests = fileManager.LoadPendingRequests();

            if (pendingRequests.Count == 0)
            {
                Console.WriteLine("❌ No pending requests to process.");
                return;
            }

            
            Console.WriteLine($"\n📋 PENDING REQUESTS: {pendingRequests.Count}\n");
            Console.WriteLine(new string('-', 100));
            Console.WriteLine($"{"Request ID",-12} | {"Name",-25} | {"Barangay",-20} | {"Category",-15} | {"Quantity",-10}");
            Console.WriteLine(new string('-', 100));

            foreach (string request in pendingRequests)
            {
                string[] parts = request.Split('|');
                Console.WriteLine($"{parts[0],-12} | {parts[1],-25} | {parts[2],-20} | {parts[3],-15} | {parts[4],-10}");
            }
            Console.WriteLine(new string('-', 100));

            Console.Write("\nEnter Request ID to process: ");
            string requestId = Console.ReadLine();
         string selectedRequest = null;
            foreach (string request in pendingRequests)
            {
                if (request.StartsWith(requestId + "|"))
                {
                    selectedRequest = request;
                    break;
                }
            }

            if (selectedRequest == null)
            {
                Console.WriteLine("❌ Request ID not found.");
                return;
            }

            string[] requestParts = selectedRequest.Split('|');
            string requesterName = requestParts[1];
            string barangay = requestParts[2];
            string category = requestParts[3];
            int quantityNeeded = int.Parse(requestParts[4]);

            List<string> matchingDonations = fileManager.FindMatchingDonations(barangay, category);

            if (matchingDonations.Count == 0)
            {
                Console.WriteLine($"\n❌ No matching donations found for {category} in {barangay}.");
                Console.WriteLine("   Request will remain pending.");
                return;
            }

            Console.WriteLine($"\n🔔 Matching donations found!");
            Console.WriteLine(new string('-', 90));
            Console.WriteLine($"{"Item Number",-15} | {"Category",-15} | {"Quantity",-10} | {"Barangay",-20} | {"Donor",-20}");
            Console.WriteLine(new string('-', 90));

            foreach (string donation in matchingDonations)
            {
                string[] parts = donation.Split('|');
                Console.WriteLine($"{parts[0],-15} | {parts[2],-15} | {parts[3],-10} | {parts[4],-20} | {parts[5],-20}");
            }
            Console.WriteLine(new string('-', 90));

            Console.Write("\n⚠️  Do you want to distribute from these donations? (Y/N): ");
            string response = Console.ReadLine()?.ToUpper();

            if (response == "Y")
            {
                int remainingNeed = quantityNeeded;
                int totalDistributed = 0;

                foreach (string donation in matchingDonations)
                {
                    if (remainingNeed <= 0) break;

                    string[] donationParts = donation.Split('|');
                    string donationItemNumber = donationParts[0];
                    int donationQuantity = int.Parse(donationParts[3]);

                    if (donationQuantity >= remainingNeed)
                    {
                        
                        int newQuantity = donationQuantity - remainingNeed;
                        totalDistributed += remainingNeed;

                        if (newQuantity == 0)
                        {
                           
                            fileManager.RemoveDonation(donationItemNumber);
                            Console.WriteLine($"✓ Used full donation (Item: {donationItemNumber})");
                        }
                        else
                        {

                            fileManager.UpdateDonationQuantity(donationItemNumber, newQuantity);
                            Console.WriteLine($"✓ Partial use from Item {donationItemNumber}. Remaining: {newQuantity}");
                        }

                        remainingNeed = 0;
                    }
                    else
                    {
                       
                        totalDistributed += donationQuantity;
                        remainingNeed -= donationQuantity;
                        fileManager.RemoveDonation(donationItemNumber);
                        Console.WriteLine($"✓ Used full donation (Item: {donationItemNumber}): {donationQuantity} units");
                    }
                }


                fileManager.SaveCompletedRequest(requestId, requesterName, barangay, category, totalDistributed);
                fileManager.RemovePendingRequest(requestId);

                Console.WriteLine($"\n✓ Request {requestId} completed!");
                Console.WriteLine($"   {requesterName} from {barangay} received {totalDistributed} {category}");

                if (remainingNeed > 0)
                {
                    Console.WriteLine($"   ⚠️  Partial fulfillment. Still needed: {remainingNeed}");
                }
            }
            else
            {
                Console.WriteLine("❌ Distribution cancelled. Request remains pending.");
            }
        }

        public void SearchRecords()
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("                SEARCH RECORDS");
            Console.WriteLine(new string('=', 60));
            Console.WriteLine("Search by: name, barangay, or category (Food/Water/Clothes)");
            Console.Write("Enter search keyword: ");
            string keyword = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                Console.WriteLine("❌ Search keyword cannot be empty.");
                return;
            }

            Console.WriteLine($"\n Searching for: '{keyword}'...\n");

            List<string> donorResults = fileManager.SearchDonors(keyword);
            if (donorResults.Count > 0)
            {
                Console.WriteLine($" DONORS FOUND ({donorResults.Count}):");
                Console.WriteLine(new string('-', 90));
                Console.WriteLine($"{"Donor ID",-12} | {"Name",-25} | {"Contact",-15} | {"Barangay",-20}");
                Console.WriteLine(new string('-', 90));
                foreach (string donor in donorResults)
                {
                    string[] parts = donor.Split('|');
                    Console.WriteLine($"{parts[0],-12} | {parts[1],-25} | {parts[2],-15} | {parts[3],-20}");
                }
                Console.WriteLine(new string('-', 90));
                Console.WriteLine();
            }

            List<string> donationResults = fileManager.SearchDonations(keyword);
            if (donationResults.Count > 0)
            {
                Console.WriteLine($"📦 DONATIONS FOUND ({donationResults.Count}):");
                Console.WriteLine(new string('-', 100));
                Console.WriteLine($"{"Item Number",-15} | {"Donor ID",-12} | {"Category",-15} | {"Quantity",-10} | {"Barangay",-20}");
                Console.WriteLine(new string('-', 100));
                foreach (string donation in donationResults)
                {
                    string[] parts = donation.Split('|');
                    Console.WriteLine($"{parts[0],-15} | {parts[1],-12} | {parts[2],-15} | {parts[3],-10} | {parts[4],-20}");
                }
                Console.WriteLine(new string('-', 100));
                Console.WriteLine();
            }

            List<string> requestResults = fileManager.SearchRequests(keyword);
            if (requestResults.Count > 0)
            {
                Console.WriteLine($" REQUESTS FOUND ({requestResults.Count}):");
                Console.WriteLine(new string('-', 110));
                Console.WriteLine($"{"Request ID",-12} | {"Name",-25} | {"Barangay",-20} | {"Category",-15} | {"Quantity",-10} | {"Status",-10}");
                Console.WriteLine(new string('-', 110));
                foreach (string request in requestResults)
                {
                    // Remove the status suffix to parse
                    string cleanRequest = request.Replace(" (Pending)", "").Replace(" (Completed)", "");
                    string[] parts = cleanRequest.Split('|');
                    string status = request.Contains("(Pending)") ? "Pending" : "Completed";
                    Console.WriteLine($"{parts[0],-12} | {parts[1],-25} | {parts[2],-20} | {parts[3],-15} | {parts[4],-10} | {status,-10}");
                }
                Console.WriteLine(new string('-', 110));
                Console.WriteLine();
            }

            int totalResults = donorResults.Count + donationResults.Count + requestResults.Count;
            if (totalResults == 0)
            {
                Console.WriteLine("❌ No results found.");
            }
            else
            {
                Console.WriteLine($"✓ Total results: {totalResults}");
            }
        }

        public void GenerateSummary()
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("       MANDAUE CITY SYSTEM SUMMARY REPORT");
            Console.WriteLine(new string('=', 60));

            int totalDistributions = fileManager.CountTotalDistributions();
            int peopleHelped = fileManager.CountPeopleHelped();
            Dictionary<string, int> itemsByCategory = fileManager.CountItemsDistributedByCategory();

            Console.WriteLine("\n" + new string('-', 60));
            Console.WriteLine($"{"CATEGORY",-30} | {"COUNT",-10}");
            Console.WriteLine(new string('-', 60));

            Console.WriteLine($"\n DISTRIBUTIONS");
            Console.WriteLine(new string('-', 60));
            Console.WriteLine($"{"Total Distributions Completed",-30} | {totalDistributions,-10}");
            Console.WriteLine($"{"Total People Helped",-30} | {peopleHelped,-10}");
            Console.WriteLine(new string('-', 60));

            Console.WriteLine($"\nITEMS DISTRIBUTED BY CATEGORY");
            Console.WriteLine(new string('-', 60));
            Console.WriteLine($"{"Food",-30} | {itemsByCategory["Food"],-10}");
            Console.WriteLine($"{"Water",-30} | {itemsByCategory["Water"],-10}");
            Console.WriteLine($"{"Clothes",-30} | {itemsByCategory["Clothes"],-10}");
            Console.WriteLine(new string('-', 60));

            Console.WriteLine("\n✓ Summary generated successfully!");
            Console.WriteLine($"   Generated by: {currentUser}");
        }

        public void ClearData()
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("                  CLEAR DATA");
            Console.WriteLine(new string('=', 60));
            Console.WriteLine("\n⚠️  WARNING: This will permanently delete data!");

            Console.WriteLine("\n" + new string('-', 50));
            Console.WriteLine($"{"OPTION",-10} | {"DESCRIPTION",-35}");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"{"1",-10} | {"Clear All Donors",-35}");
            Console.WriteLine($"{"2",-10} | {"Clear All Donations",-35}");
            Console.WriteLine($"{"3",-10} | {"Clear All Requests",-35}");
            Console.WriteLine($"{"4",-10} | {"Clear Everything (All Data)",-35}");
            Console.WriteLine($"{"5",-10} | {"Cancel",-35}");
            Console.WriteLine(new string('-', 50));

            Console.Write("\nChoose: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("\n⚠️  Are you sure you want to delete ALL DONORS? (yes/no): ");
                    if (Console.ReadLine()?.ToLower() == "yes")
                    {
                        fileManager.ClearFile("donors.txt");
                        Console.WriteLine("✓ All donors cleared successfully!");
                    }
                    else
                    {
                        Console.WriteLine("❌ Operation cancelled.");
                    }
                    break;

                case "2":
                    Console.Write("\n⚠️  Are you sure you want to delete ALL DONATIONS? (yes/no): ");
                    if (Console.ReadLine()?.ToLower() == "yes")
                    {
                        fileManager.ClearFile("donations.txt");
                        Console.WriteLine("✓ All donations cleared successfully!");
                    }
                    else
                    {
                        Console.WriteLine("❌ Operation cancelled.");
                    }
                    break;

                case "3":
                    Console.Write("\n⚠️  Are you sure you want to delete ALL REQUESTS? (yes/no): ");
                    if (Console.ReadLine()?.ToLower() == "yes")
                    {
                        fileManager.ClearFile("pending_requests.txt");
                        fileManager.ClearFile("completed_requests.txt");
                        Console.WriteLine("✓ All requests cleared successfully!");
                    }
                    else
                    {
                        Console.WriteLine("❌ Operation cancelled.");
                    }
                    break;

                case "4":
                    Console.Write("\n🚨 Are you ABSOLUTELY SURE you want to delete ALL DATA? (yes/no): ");
                    if (Console.ReadLine()?.ToLower() == "yes")
                    {
                        Console.Write("Type 'DELETE' to confirm: ");
                        if (Console.ReadLine() == "DELETE")
                        {
                            fileManager.ClearFile("donors.txt");
                            fileManager.ClearFile("donations.txt");
                            fileManager.ClearFile("pending_requests.txt");
                            fileManager.ClearFile("completed_requests.txt");
                            Console.WriteLine("✓ All data cleared successfully! System reset to empty state.");
                        }
                        else
                        {
                            Console.WriteLine("❌ Confirmation failed. Operation cancelled.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("❌ Operation cancelled.");
                    }
                    break;

                case "5":
                    Console.WriteLine("❌ Operation cancelled.");
                    break;

                default:
                    Console.WriteLine("❌ Invalid choice.");
                    break;
            }
        }
    }
}