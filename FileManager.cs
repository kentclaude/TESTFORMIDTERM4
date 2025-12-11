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
        private static readonly string PENDING_REQUESTS_FILE = Path.Combine(FOLDER_PATH, "pending_requests.txt");
        private static readonly string COMPLETED_REQUESTS_FILE = Path.Combine(FOLDER_PATH, "completed_requests.txt");

        public static readonly string[] BARANGAYS = {
            "Bakilid", "Banilad", "Basak", "Cabancalan", "Cambaro", "Canduman",
            "Casili", "Centro (Poblacion)", "Cubacub", "Guizo", "Ibabao-Estancia",
            "Jagobiao", "Labogon", "Looc", "Maguikay", "Mantuyong", "Opao",
            "Pagsabungan", "Subangdaku", "Tabok", "Tingub", "Tipolo", "Umapad",
            "Alang-alang", "Pakna-an", "Tawason"
        };

        public FileManager()
        {
            Directory.CreateDirectory(FOLDER_PATH);
        }

        
        public void SaveDonor(string donorId, string name, string contact, string barangay)
        {
            string data = $"{donorId}|{name}|{contact}|{barangay}";
            File.AppendAllText(DONORS_FILE, data + Environment.NewLine);
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
                    return $"{parts[1]}|{parts[2]}|{parts[3]}";
                }
            }
            return null;
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

        
        public void SaveDonation(string itemNumber, string donorId, string donorName, string category, int quantity, string barangay)
        {
            string data = $"{itemNumber}|{donorId}|{category}|{quantity}|{barangay}|{donorName}";
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

        public List<string> FindMatchingDonations(string barangay, string category)
        {
            List<string> matches = new List<string>();
            if (!File.Exists(DONATIONS_FILE)) return matches;

            string[] lines = File.ReadAllLines(DONATIONS_FILE);
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                string[] parts = line.Split('|');

                // Check if barangay and category match
                if (parts[4] == barangay && parts[2] == category)
                {
                    matches.Add(line);
                }
            }
            return matches;
        }

        public void UpdateDonationQuantity(string itemNumber, int newQuantity)
        {
            if (!File.Exists(DONATIONS_FILE)) return;

            string[] lines = File.ReadAllLines(DONATIONS_FILE);
            for (int i = 0; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue;
                string[] parts = lines[i].Split('|');

                if (parts[0] == itemNumber)
                {
                    parts[3] = newQuantity.ToString();
                    lines[i] = string.Join("|", parts);
                    break;
                }
            }
            File.WriteAllLines(DONATIONS_FILE, lines);
        }

        public void RemoveDonation(string itemNumber)
        {
            if (!File.Exists(DONATIONS_FILE)) return;

            List<string> lines = new List<string>(File.ReadAllLines(DONATIONS_FILE));
            lines.RemoveAll(line => !string.IsNullOrWhiteSpace(line) && line.Split('|')[0] == itemNumber);
            File.WriteAllLines(DONATIONS_FILE, lines);
        }

        public void SavePendingRequest(string requestId, string name, string barangay, string category, int quantity)
        {
            string data = $"{requestId}|{name}|{barangay}|{category}|{quantity}";
            File.AppendAllText(PENDING_REQUESTS_FILE, data + Environment.NewLine);
        }

        public void SaveCompletedRequest(string requestId, string name, string barangay, string category, int quantity)
        {
            string data = $"{requestId}|{name}|{barangay}|{category}|{quantity}";
            File.AppendAllText(COMPLETED_REQUESTS_FILE, data + Environment.NewLine);
        }

        public void RemovePendingRequest(string requestId)
        {
            if (!File.Exists(PENDING_REQUESTS_FILE)) return;

            List<string> lines = new List<string>(File.ReadAllLines(PENDING_REQUESTS_FILE));
            lines.RemoveAll(line => !string.IsNullOrWhiteSpace(line) && line.StartsWith(requestId + "|"));
            File.WriteAllLines(PENDING_REQUESTS_FILE, lines);
        }

        public List<string> LoadPendingRequests()
        {
            List<string> requests = new List<string>();
            if (!File.Exists(PENDING_REQUESTS_FILE)) return requests;

            string[] lines = File.ReadAllLines(PENDING_REQUESTS_FILE);
            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    requests.Add(line);
                }
            }
            return requests;
        }

        public List<string> LoadCompletedRequests()
        {
            List<string> requests = new List<string>();
            if (!File.Exists(COMPLETED_REQUESTS_FILE)) return requests;

            string[] lines = File.ReadAllLines(COMPLETED_REQUESTS_FILE);
            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    requests.Add(line);
                }
            }
            return requests;
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

        public List<string> SearchRequests(string keyword)
        {
            List<string> results = new List<string>();

            if (File.Exists(PENDING_REQUESTS_FILE))
            {
                string[] lines = File.ReadAllLines(PENDING_REQUESTS_FILE);
                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    if (line.ToLower().Contains(keyword.ToLower()))
                    {
                        results.Add(line + " (Pending)");
                    }
                }
            }

            // Search completed requests
            if (File.Exists(COMPLETED_REQUESTS_FILE))
            {
                string[] lines = File.ReadAllLines(COMPLETED_REQUESTS_FILE);
                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    if (line.ToLower().Contains(keyword.ToLower()))
                    {
                        results.Add(line + " (Completed)");
                    }
                }
            }

            return results;
        }
        public int CountTotalDistributions()
        {
            return LoadCompletedRequests().Count;
        }

        public int CountPeopleHelped()
        {
            return LoadCompletedRequests().Count;
        }

        public Dictionary<string, int> CountItemsDistributedByCategory()
        {
            Dictionary<string, int> categoryCounts = new Dictionary<string, int>
            {
                {"Food", 0},
                {"Water", 0},
                {"Clothes", 0}
            };

            List<string> completed = LoadCompletedRequests();
            foreach (string request in completed)
            {
                string[] parts = request.Split('|');
                string category = parts[3];
                int quantity = int.Parse(parts[4]);

                if (categoryCounts.ContainsKey(category))
                {
                    categoryCounts[category] += quantity;
                }
            }

            return categoryCounts;
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