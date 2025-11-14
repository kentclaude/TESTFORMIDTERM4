using System;
using System.Collections.Generic;
using System.IO;

namespace CommunityFoodWasteSharing
{
    public class FileManager
    {
        private static readonly string FOLDER_PATH = "FoodWasteData";
        private static readonly string DONORS_FILE = Path.Combine(FOLDER_PATH, "donors.txt");
        private static readonly string DONATIONS_FILE = Path.Combine(FOLDER_PATH, "donations.txt");
        private static readonly string REPORTS_FILE = Path.Combine(FOLDER_PATH, "reports.txt");

        public static readonly string[] BARANGAYS = {
            "Bakilid", "Banilad", "Basak", "Cabancalan", "Cambaro", "Canduman",
            "Casili", "Centro (Poblacion)", "Cubacub", "Guizo", "Ibabao-Estancia",
            "Jagobiao", "Labogon", "Looc", "Maguikay", "Mantuyong", "Opao",
            "Pagsabungan", "Subangdaku", "Tabok", "Tingub", "Tipolo", "Umapad",
            "Alang-alang", "Bakilid", "Pakna-an", "Tawason"
        };

        public FileManager()
        {
            Directory.CreateDirectory(FOLDER_PATH);
        }

        public void SaveDonor(string donorId, string name, string contact, string address, string barangay)
        {
            string data = $"{donorId}|{name}|{contact}|{address}|{barangay}";
            File.AppendAllText(DONORS_FILE, data + Environment.NewLine);
        }

        public bool DonorExists(string donorId)
        {
            if (!File.Exists(DONORS_FILE)) return false;

            string[] lines = File.ReadAllLines(DONORS_FILE);
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                string[] parts = line.Split('|');
                if (parts[0] == donorId) return true;
            }
            return false;
        }

        public string GetDonorInfo(string donorId)
        {
            if (!File.Exists(DONORS_FILE)) return null;

            string[] lines = File.ReadAllLines(DONORS_FILE);
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                string[] parts = line.Split('|');
                if (parts[0] == donorId)
                {
                    return $"{parts[1]}|{parts[2]}|{parts[3]}|{parts[4]}";
                }
            }
            return null;
        }

        public string GetDonorName(string donorId)
        {
            if (!File.Exists(DONORS_FILE)) return "Unknown";

            string[] lines = File.ReadAllLines(DONORS_FILE);
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                string[] parts = line.Split('|');
                if (parts[0] == donorId) return parts[1];
            }
            return "Unknown";
        }

        public List<string> LoadDonors()
        {
            List<string> donors = new List<string>();
            if (!File.Exists(DONORS_FILE)) return donors;

            string[] lines = File.ReadAllLines(DONORS_FILE);
            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    donors.Add(line);
                }
            }
            return donors;
        }

        public bool DonationExists(string donationId)
        {
            if (!File.Exists(DONATIONS_FILE)) return false;

            string[] lines = File.ReadAllLines(DONATIONS_FILE);
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                string[] parts = line.Split('|');
                if (parts[0] == donationId) return true;
            }
            return false;
        }

        public void SaveDonation(string donationId, string donorId, string foodType, int quantity,
                                 string barangay, DateTime dateTime, string status)
        {
            string data = $"{donationId}|{donorId}|{foodType}|{quantity}|{barangay}|{dateTime}|{status}";
            File.AppendAllText(DONATIONS_FILE, data + Environment.NewLine);
        }

        public List<string> GetDonationsByDonor(string donorId)
        {
            List<string> donations = new List<string>();
            if (!File.Exists(DONATIONS_FILE)) return donations;

            string[] lines = File.ReadAllLines(DONATIONS_FILE);
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                string[] parts = line.Split('|');
                if (parts[1] == donorId)
                {
                    donations.Add(line);
                }
            }
            return donations;
        }

        public List<string> LoadDonations()
        {
            List<string> donations = new List<string>();
            if (!File.Exists(DONATIONS_FILE)) return donations;

            string[] lines = File.ReadAllLines(DONATIONS_FILE);
            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    donations.Add(line);
                }
            }
            return donations;
        }

        public void UpdateDonationStatus(string donationId, string newStatus)
        {
            if (!File.Exists(DONATIONS_FILE)) return;

            string[] lines = File.ReadAllLines(DONATIONS_FILE);
            for (int i = 0; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue;
                string[] parts = lines[i].Split('|');
                if (parts[0] == donationId)
                {
                    parts[6] = newStatus;
                    lines[i] = string.Join("|", parts);
                    break;
                }
            }
            File.WriteAllLines(DONATIONS_FILE, lines);
        }

        public bool IsDonationMatched(string donationId)
        {
            if (!File.Exists(DONATIONS_FILE)) return false;

            string[] lines = File.ReadAllLines(DONATIONS_FILE);
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                string[] parts = line.Split('|');
                if (parts[0] == donationId && parts[6].Equals("Matched", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        public bool ReportExists(string reportId)
        {
            if (!File.Exists(REPORTS_FILE)) return false;

            string[] lines = File.ReadAllLines(REPORTS_FILE);
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                string[] parts = line.Split('|');
                if (parts[0] == reportId) return true;
            }
            return false;
        }

        public void SaveReport(string reportId, string description, string barangay, DateTime observed, string status)
        {
            string data = $"{reportId}|{description}|{barangay}|{observed}|{status}";
            File.AppendAllText(REPORTS_FILE, data + Environment.NewLine);
        }

        public List<string> LoadReports()
        {
            List<string> reports = new List<string>();
            if (!File.Exists(REPORTS_FILE)) return reports;

            string[] lines = File.ReadAllLines(REPORTS_FILE);
            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    reports.Add(line);
                }
            }
            return reports;
        }

        public void UpdateReportStatus(string reportId, string newStatus)
        {
            if (!File.Exists(REPORTS_FILE)) return;

            string[] lines = File.ReadAllLines(REPORTS_FILE);
            for (int i = 0; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue;
                string[] parts = lines[i].Split('|');
                if (parts[0] == reportId)
                {
                    parts[4] = newStatus;
                    lines[i] = string.Join("|", parts);
                    break;
                }
            }
            File.WriteAllLines(REPORTS_FILE, lines);
        }

        public List<string> SearchDonors(string keyword)
        {
            List<string> results = new List<string>();
            if (!File.Exists(DONORS_FILE)) return results;

            string[] lines = File.ReadAllLines(DONORS_FILE);
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                if (line.ToLower().Contains(keyword.ToLower()))
                {
                    results.Add(line);
                }
            }
            return results;
        }

        public List<string> SearchDonations(string keyword)
        {
            List<string> results = new List<string>();
            if (!File.Exists(DONATIONS_FILE)) return results;

            string[] lines = File.ReadAllLines(DONATIONS_FILE);
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                if (line.ToLower().Contains(keyword.ToLower()))
                {
                    results.Add(line);
                }
            }
            return results;
        }

        public List<string> SearchReports(string keyword)
        {
            List<string> results = new List<string>();
            if (!File.Exists(REPORTS_FILE)) return results;

            string[] lines = File.ReadAllLines(REPORTS_FILE);
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                if (line.ToLower().Contains(keyword.ToLower()))
                {
                    results.Add(line);
                }
            }
            return results;
        }

        public int CountTotalDonors()
        {
            return LoadDonors().Count;
        }

        public int CountTotalDonations()
        {
            return LoadDonations().Count;
        }

        public int CountDistributedDonations()
        {
            if (!File.Exists(DONATIONS_FILE)) return 0;

            int count = 0;
            string[] lines = File.ReadAllLines(DONATIONS_FILE);
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                string[] parts = line.Split('|');
                if (parts[6].Equals("Distributed", StringComparison.OrdinalIgnoreCase))
                {
                    count++;
                }
            }
            return count;
        }

        public int CountTotalReports()
        {
            return LoadReports().Count;
        }

        public int CountPendingReports()
        {
            if (!File.Exists(REPORTS_FILE)) return 0;

            int count = 0;
            string[] lines = File.ReadAllLines(REPORTS_FILE);
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                string[] parts = line.Split('|');
                if (parts[4].Equals("Pending", StringComparison.OrdinalIgnoreCase))
                {
                    count++;
                }
            }
            return count;
        }

        public void ClearFile(string filename)
        {
            string filePath = Path.Combine(FOLDER_PATH, filename);
            if (File.Exists(filePath))
            {
                File.WriteAllText(filePath, string.Empty);
            }
        }
    }
}