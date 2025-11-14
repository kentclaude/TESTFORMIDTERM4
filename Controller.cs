using System;

namespace CommunityFoodWasteSharing
{
    public class Controller
    {
        public void Start()
        {
            while (true)
            {
                Console.WriteLine("\n");
                Console.WriteLine(" ███╗   ███╗ █████╗ ███╗   ██╗██████╗  █████╗ ██╗   ██╗███████╗");
                Console.WriteLine(" ████╗ ████║██╔══██╗████╗  ██║██╔══██╗██╔══██╗██║   ██║██╔════╝");
                Console.WriteLine(" ██╔████╔██║███████║██╔██╗ ██║██║  ██║███████║██║   ██║█████╗  ");
                Console.WriteLine(" ██║╚██╔╝██║██╔══██║██║╚██╗██║██║  ██║██╔══██║██║   ██║██╔══╝  ");
                Console.WriteLine(" ██║ ╚═╝ ██║██║  ██║██║ ╚████║██████╔╝██║  ██║╚██████╔╝███████╗");
                Console.WriteLine(" ╚═╝     ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝╚═════╝ ╚═╝  ╚═╝ ╚═════╝ ╚══════╝");
                Console.WriteLine("                  Food Waste Sharing System");
                Console.WriteLine();

                Console.WriteLine(new string('-', 60));
                Console.WriteLine($"{"OPTION",-10} | {"ROLE",-35}");
                Console.WriteLine(new string('-', 60));
                Console.WriteLine($"{"1",-10} | {"Donor",-35}");
                Console.WriteLine($"{"2",-10} | {"Reporter",-35}");
                Console.WriteLine($"{"3",-10} | {"Admin (LGU Staff/Mayor)",-35}");
                Console.WriteLine($"{"4",-10} | {"Exit",-35}");
                Console.WriteLine(new string('-', 60));

                Console.Write("Select role: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DonorMenu();
                        break;
                    case "2":
                        ReporterMenu();
                        break;
                    case "3":
                        AdminMenu();
                        break;
                    case "4":
                        Console.WriteLine("\n✓ Thank you for using the system. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("❌ Invalid option.");
                        break;
                }
            }
        }

        private void DonorMenu()
        {
            Donor donor = new Donor();

            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("                 DONOR OPTIONS");
            Console.WriteLine(new string('=', 60));

            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"{"OPTION",-10} | {"ACTION",-35}");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"{"1",-10} | {"Create Account",-35}");
            Console.WriteLine($"{"2",-10} | {"Log In",-35}");
            Console.WriteLine(new string('-', 50));

            Console.Write("Choose: ");
            string option = Console.ReadLine();

            bool loggedIn = false;

            if (option == "1")
            {
                donor.CreateAccount();
                loggedIn = true;
            }
            else if (option == "2")
            {
                loggedIn = donor.Login();
            }

            if (!loggedIn) return;

            while (true)
            {
                Console.WriteLine("\n" + new string('=', 60));
                Console.WriteLine("                 DONOR MENU");
                Console.WriteLine(new string('=', 60));

                Console.WriteLine(new string('-', 50));
                Console.WriteLine($"{"OPTION",-10} | {"ACTION",-35}");
                Console.WriteLine(new string('-', 50));
                Console.WriteLine($"{"1",-10} | {"Record Donation",-35}");
                Console.WriteLine($"{"2",-10} | {"View Donation History",-35}");
                Console.WriteLine($"{"3",-10} | {"Back",-35}");
                Console.WriteLine(new string('-', 50));

                Console.Write("Choose: ");
                string choice = Console.ReadLine();

                if (choice == "1") donor.RecordDonation();
                else if (choice == "2") donor.ViewDonationHistory();
                else if (choice == "3") return;
                else Console.WriteLine("❌ Invalid choice.");
            }
        }

        private void ReporterMenu()
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("                 REPORTER MENU");
            Console.WriteLine(new string('=', 60));

            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"{"OPTION",-10} | {"ACTION",-35}");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"{"1",-10} | {"Submit New Report",-35}");
            Console.WriteLine($"{"2",-10} | {"View All Reports",-35}");
            Console.WriteLine($"{"3",-10} | {"Back",-35}");
            Console.WriteLine(new string('-', 50));

            Console.Write("Choose: ");
            string choice = Console.ReadLine();

            Report report = new Report();

            if (choice == "1")
                report.SubmitReport();
            else if (choice == "2")
                report.ViewAllReports();
            else if (choice == "3")
                return;
            else
                Console.WriteLine("❌ Invalid choice.");
        }

        private void AdminMenu()
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("           Mandaue City LGU Admin Login");
            Console.WriteLine(new string('=', 60));
            Console.Write("Username: ");
            string user = Console.ReadLine();
            Console.Write("Password: ");
            string pass = Console.ReadLine();

            Admin admin = new Admin();

            if (!admin.Login(user, pass))
            {
                Console.WriteLine("❌ Login failed.");
                return;
            }

            while (true)
            {
                Console.WriteLine("\n" + new string('=', 60));
                Console.WriteLine("           MANDAUE CITY LGU ADMIN PANEL");
                Console.WriteLine(new string('=', 60));

                Console.WriteLine(new string('-', 60));
                Console.WriteLine($"{"OPTION",-10} | {"ACTION",-45}");
                Console.WriteLine(new string('-', 60));
                Console.WriteLine($"{"1",-10} | {"View All Donors",-45}");
                Console.WriteLine($"{"2",-10} | {"View All Donations",-45}");
                Console.WriteLine($"{"3",-10} | {"View All Reports",-45}");
                Console.WriteLine($"{"4",-10} | {"Search Records",-45}");
                Console.WriteLine($"{"5",-10} | {"Match Donation to Report",-45}");
                Console.WriteLine($"{"6",-10} | {"Mark Donation as Distributed",-45}");
                Console.WriteLine($"{"7",-10} | {"Generate Summary",-45}");
                Console.WriteLine($"{"8",-10} | {"Clear Data",-45}");
                Console.WriteLine($"{"9",-10} | {"Back",-45}");
                Console.WriteLine(new string('-', 60));

                Console.Write("Choose: ");
                string choice = Console.ReadLine();

                if (choice == "1") admin.ViewAllDonors();
                else if (choice == "2") admin.ViewAllDonations();
                else if (choice == "3") admin.ViewAllReports();
                else if (choice == "4") admin.SearchRecords();
                else if (choice == "5") admin.MatchDonationToReport();
                else if (choice == "6") admin.MarkAsDistributed();
                else if (choice == "7") admin.GenerateSummary();
                else if (choice == "8") admin.ClearData();
                else if (choice == "9") return;
                else Console.WriteLine("❌ Invalid choice.");
            }
        }
    }
}