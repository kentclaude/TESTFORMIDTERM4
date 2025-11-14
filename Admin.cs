using System;
using System.Collections.Generic;

namespace CommunityFoodWasteSharing
{
    public class Admin : User
    {
        private const string LGU_USERNAME = "LGU";
        private const string LGU_PASSWORD = "MANDAUE2024";
        private const string MAYOR_USERNAME = "MAYOR";
        private const string MAYOR_PASSWORD = "MAYOR2025";
        private FileManager fileManager = new FileManager();
        private string currentUser = "";

        public Admin() : base(LGU_USERNAME) { }

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

            // Table Header
            Console.WriteLine(new string('-', 110));
            Console.WriteLine($"{"Donor ID",-12} | {"Name",-20} | {"Contact",-15} | {"Address",-30} | {"Barangay",-15}");
            Console.WriteLine(new string('-', 110));

            // Table Rows
            foreach (string donor in donors)
            {
                string[] parts = donor.Split('|');
                Console.WriteLine($"{parts[0],-12} | {parts[1],-20} | {parts[2],-15} | {parts[3],-30} | {parts[4],-15}");
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

            // Table Header
            Console.WriteLine(new string('-', 130));
            Console.WriteLine($"{"Donation ID",-13} | {"Donor",-20} | {"Food Type",-15} | {"Quantity",-10} | {"Barangay",-15} | {"Date/Time",-20} | {"Status",-12}");
            Console.WriteLine(new string('-', 130));

            // Table Rows
            foreach (string donation in donations)
            {
                string[] parts = donation.Split('|');
                string donorName = fileManager.GetDonorName(parts[1]);
                Console.WriteLine($"{parts[0],-13} | {donorName,-20} | {parts[2],-15} | {parts[3],-10} | {parts[4],-15} | {parts[5],-20} | {parts[6],-12}");
            }
            Console.WriteLine(new string('-', 130));
        }

        public void ViewAllReports()
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("        ALL REPORTS (INDIVIDUALS IN NEED)");
            Console.WriteLine(new string('=', 60));
            List<string> reports = fileManager.LoadReports();

            if (reports.Count == 0)
            {
                Console.WriteLine("No reports submitted yet.");
                return;
            }

            Console.WriteLine($"\nTotal Reports: {reports.Count}\n");

            // Table Header
            Console.WriteLine(new string('-', 110));
            Console.WriteLine($"{"Report ID",-12} | {"Description",-35} | {"Barangay",-15} | {"Observed",-20} | {"Status",-12}");
            Console.WriteLine(new string('-', 110));

            // Table Rows
            foreach (string report in reports)
            {
                string[] parts = report.Split('|');
                Console.WriteLine($"{parts[0],-12} | {parts[1],-35} | {parts[2],-15} | {parts[3],-20} | {parts[4],-12}");
            }
            Console.WriteLine(new string('-', 110));
        }

        public void SearchRecords()
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("                SEARCH RECORDS");
            Console.WriteLine(new string('=', 60));
            Console.WriteLine("Search by: barangay name, person name, food type, status, etc.");
            Console.Write("Enter search keyword: ");
            string keyword = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                Console.WriteLine("❌ Search keyword cannot be empty.");
                return;
            }

            Console.WriteLine($"\n🔍 Searching for: '{keyword}'...\n");

            List<string> donorResults = fileManager.SearchDonors(keyword);
            if (donorResults.Count > 0)
            {
                Console.WriteLine($"📋 DONORS FOUND ({donorResults.Count}):");
                Console.WriteLine(new string('-', 90));
                Console.WriteLine($"{"Donor ID",-12} | {"Name",-25} | {"Contact",-15} | {"Barangay",-15}");
                Console.WriteLine(new string('-', 90));
                foreach (string donor in donorResults)
                {
                    string[] parts = donor.Split('|');
                    Console.WriteLine($"{parts[0],-12} | {parts[1],-25} | {parts[2],-15} | {parts[4],-15}");
                }
                Console.WriteLine(new string('-', 90));
                Console.WriteLine();
            }

            List<string> donationResults = fileManager.SearchDonations(keyword);
            if (donationResults.Count > 0)
            {
                Console.WriteLine($"📦 DONATIONS FOUND ({donationResults.Count}):");
                Console.WriteLine(new string('-', 100));
                Console.WriteLine($"{"Donation ID",-13} | {"Food Type",-20} | {"Barangay",-15} | {"Status",-12}");
                Console.WriteLine(new string('-', 100));
                foreach (string donation in donationResults)
                {
                    string[] parts = donation.Split('|');
                    Console.WriteLine($"{parts[0],-13} | {parts[2],-20} | {parts[4],-15} | {parts[6],-12}");
                }
                Console.WriteLine(new string('-', 100));
                Console.WriteLine();
            }

            List<string> reportResults = fileManager.SearchReports(keyword);
            if (reportResults.Count > 0)
            {
                Console.WriteLine($"📝 REPORTS FOUND ({reportResults.Count}):");
                Console.WriteLine(new string('-', 90));
                Console.WriteLine($"{"Report ID",-12} | {"Description",-30} | {"Barangay",-15} | {"Status",-12}");
                Console.WriteLine(new string('-', 90));
                foreach (string report in reportResults)
                {
                    string[] parts = report.Split('|');
                    Console.WriteLine($"{parts[0],-12} | {parts[1],-30} | {parts[2],-15} | {parts[4],-12}");
                }
                Console.WriteLine(new string('-', 90));
                Console.WriteLine();
            }

            int totalResults = donorResults.Count + donationResults.Count + reportResults.Count;
            if (totalResults == 0)
            {
                Console.WriteLine("❌ No results found.");
            }
            else
            {
                Console.WriteLine($"✓ Total results: {totalResults}");
            }
        }

        public void MatchDonationToReport()
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("           MATCH DONATION TO REPORT");
            Console.WriteLine(new string('=', 60));

            List<string> donations = fileManager.LoadDonations();
            if (donations.Count == 0)
            {
                Console.WriteLine("❌ No donations available to match.");
                return;
            }

            Console.WriteLine("\n📦 Available Donations:");
            Console.WriteLine(new string('-', 90));
            Console.WriteLine($"{"Donation ID",-13} | {"Food Type",-20} | {"Barangay",-15} | {"Status",-12}");
            Console.WriteLine(new string('-', 90));
            foreach (string donation in donations)
            {
                string[] parts = donation.Split('|');
                Console.WriteLine($"{parts[0],-13} | {parts[2],-20} | {parts[4],-15} | {parts[6],-12}");
            }
            Console.WriteLine(new string('-', 90));

            Console.Write("\nEnter Donation ID to match: ");
            string donationId = Console.ReadLine();

            List<string> reports = fileManager.LoadReports();
            if (reports.Count == 0)
            {
                Console.WriteLine("❌ No reports available to match.");
                return;
            }

            Console.WriteLine("\n📝 Reports (Individuals in Need):");
            Console.WriteLine(new string('-', 80));
            Console.WriteLine($"{"Report ID",-12} | {"Description",-30} | {"Barangay",-15} | {"Status",-12}");
            Console.WriteLine(new string('-', 80));
            foreach (string report in reports)
            {
                string[] parts = report.Split('|');
                Console.WriteLine($"{parts[0],-12} | {parts[1],-30} | {parts[2],-15} | {parts[4],-12}");
            }
            Console.WriteLine(new string('-', 80));

            Console.Write("\nEnter Report ID to match: ");
            string reportId = Console.ReadLine();

            fileManager.UpdateDonationStatus(donationId, "Matched");
            fileManager.UpdateReportStatus(reportId, "Matched");

            Console.WriteLine($"\n✓ Successfully matched Donation {donationId} with Report {reportId}!");
        }

        public void MarkAsDistributed()
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("          MARK DONATION AS DISTRIBUTED");
            Console.WriteLine(new string('=', 60));

            List<string> donations = fileManager.LoadDonations();
            if (donations.Count == 0)
            {
                Console.WriteLine("❌ No donations available.");
                return;
            }

            Console.WriteLine("\n📦 Matched Donations (Eligible for Distribution):");
            List<string> matchedDonations = new List<string>();

            Console.WriteLine(new string('-', 80));
            Console.WriteLine($"{"Donation ID",-13} | {"Food Type",-20} | {"Barangay",-15} | {"Status",-12}");
            Console.WriteLine(new string('-', 80));

            foreach (string donation in donations)
            {
                string[] parts = donation.Split('|');
                if (parts[6].Equals("Matched", StringComparison.OrdinalIgnoreCase))
                {
                    matchedDonations.Add(donation);
                    Console.WriteLine($"{parts[0],-13} | {parts[2],-20} | {parts[4],-15} | {parts[6],-12}");
                }
            }
            Console.WriteLine(new string('-', 80));

            if (matchedDonations.Count == 0)
            {
                Console.WriteLine("\n❌ No matched donations available to distribute.");
                Console.WriteLine("   Please match donations to reports first using option 5.");
                return;
            }

            Console.Write("\nEnter Donation ID to mark as distributed: ");
            string donationId = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(donationId))
            {
                Console.WriteLine("❌ Donation ID cannot be empty.");
                return;
            }

            if (!fileManager.IsDonationMatched(donationId))
            {
                Console.WriteLine("❌ This donation must be matched to a report before it can be distributed!");
                Console.WriteLine("   Please use 'Match Donation to Report' option first.");
                return;
            }

            fileManager.UpdateDonationStatus(donationId, "Distributed");
            Console.WriteLine($"\n✓ Donation {donationId} marked as Distributed!");
        }

        public void GenerateSummary()
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("       MANDAUE CITY SYSTEM SUMMARY REPORT");
            Console.WriteLine(new string('=', 60));

            int totalDonors = fileManager.CountTotalDonors();
            int totalDonations = fileManager.CountTotalDonations();
            int distributedDonations = fileManager.CountDistributedDonations();
            int totalReports = fileManager.CountTotalReports();
            int pendingReports = fileManager.CountPendingReports();

            Console.WriteLine("\n" + new string('-', 60));
            Console.WriteLine($"{"CATEGORY",-30} | {"COUNT",-10}");
            Console.WriteLine(new string('-', 60));

            Console.WriteLine($"\n📊 DONORS");
            Console.WriteLine(new string('-', 60));
            Console.WriteLine($"{"Total Registered Donors",-30} | {totalDonors,-10}");
            Console.WriteLine(new string('-', 60));

            Console.WriteLine($"\n📦 DONATIONS");
            Console.WriteLine(new string('-', 60));
            Console.WriteLine($"{"Total Donations",-30} | {totalDonations,-10}");
            Console.WriteLine($"{"Distributed",-30} | {distributedDonations,-10}");
            Console.WriteLine($"{"Pending/Matched",-30} | {totalDonations - distributedDonations,-10}");
            Console.WriteLine(new string('-', 60));

            Console.WriteLine($"\n📋 REPORTS");
            Console.WriteLine(new string('-', 60));
            Console.WriteLine($"{"Total Reports",-30} | {totalReports,-10}");
            Console.WriteLine($"{"Pending",-30} | {pendingReports,-10}");
            Console.WriteLine($"{"Matched/Resolved",-30} | {totalReports - pendingReports,-10}");
            Console.WriteLine(new string('-', 60));

            Console.WriteLine($"\n💚 IMPACT");
            Console.WriteLine(new string('-', 60));
            Console.WriteLine($"{"Individuals Helped",-30} | {totalReports - pendingReports,-10}");
            Console.WriteLine($"{"Food Waste Prevented (donations)",-30} | {distributedDonations,-10}");
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
            Console.WriteLine($"{"3",-10} | {"Clear All Reports",-35}");
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
                    Console.Write("\n⚠️  Are you sure you want to delete ALL REPORTS? (yes/no): ");
                    if (Console.ReadLine()?.ToLower() == "yes")
                    {
                        fileManager.ClearFile("reports.txt");
                        Console.WriteLine("✓ All reports cleared successfully!");
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
                            fileManager.ClearFile("reports.txt");
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