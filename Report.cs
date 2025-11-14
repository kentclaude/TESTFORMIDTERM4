using System;
using System.Collections.Generic;

namespace CommunityFoodWasteSharing
{
    public class Report
    {
        private string reportId;
        private string description;
        private string barangay;
        private DateTime observed;
        private string status;
        private FileManager fileManager = new FileManager();

        public void SubmitReport()
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("                 SUBMIT REPORT");
            Console.WriteLine(new string('=', 60));

            while (true)
            {
                Console.Write("\nReport ID: ");
                reportId = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(reportId))
                {
                    Console.WriteLine("❌ Report ID cannot be empty. Please try again.");
                    continue;
                }

                if (fileManager.ReportExists(reportId))
                {
                    Console.WriteLine($"❌ Report ID '{reportId}' already exists! Please use a different ID.");
                    continue;
                }

                break;
            }

            Console.Write("Person/Description: ");
            description = Console.ReadLine();

            Console.WriteLine("\nSelect Barangay:");
            for (int i = 0; i < FileManager.BARANGAYS.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {FileManager.BARANGAYS[i]}");
            }
            Console.Write("Enter barangay number: ");
            int barangayIndex = int.Parse(Console.ReadLine()) - 1;
            barangay = FileManager.BARANGAYS[barangayIndex];

            observed = DateTime.Now;
            status = "Pending";

            fileManager.SaveReport(reportId, description, barangay, observed, status);

            Console.WriteLine($"✓ Report submitted for Barangay {barangay}!");
        }

        public void ViewAllReports()
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("             ALL SUBMITTED REPORTS");
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

        public string GetReportDetails()
        {
            return $"{reportId}|{description}|{barangay}|{observed}|{status}";
        }

        public void UpdateStatus(string newStatus)
        {
            status = newStatus;
        }
    }
}