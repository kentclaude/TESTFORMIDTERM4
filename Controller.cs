using System;

namespace CommunityFoodWasteSharing
{
    public class Controller
    {
        
        private string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                
                if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return password;
        }

        public void Start()
        {
            while (true)
            {
                Console.Clear();

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
                Console.WriteLine($"{"2",-10} | {"Requester",-35}");
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
                        RequesterMenu();
                        break;
                    case "3":
                        AdminMenu();
                        break;
                    case "4":
                        Console.WriteLine("\n✓ Thank you for using the system. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("❌ Invalid option.");
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void DonorMenu()
        {
            Console.Clear();

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
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                loggedIn = true;
            }
            else if (option == "2")
            {
                loggedIn = donor.Login();
                if (loggedIn)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }

            if (!loggedIn) return;

            while (true)
            {
                Console.Clear();

                Console.WriteLine("\n" + new string('=', 60));
                Console.WriteLine("                 DONOR MENU");
                Console.WriteLine(new string('=', 60));

                Console.WriteLine(new string('-', 50));
                Console.WriteLine($"{"OPTION",-10} | {"ACTION",-35}");
                Console.WriteLine(new string('-', 50));
                Console.WriteLine($"{"1",-10} | {"Donate Item",-35}");
                Console.WriteLine($"{"2",-10} | {"View My Donations",-35}");
                Console.WriteLine($"{"3",-10} | {"Back",-35}");
                Console.WriteLine(new string('-', 50));

                Console.Write("Choose: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    donor.DonateItem();
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
                else if (choice == "2")
                {
                    donor.ViewMyDonations();
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
                else if (choice == "3") return;
                else
                {
                    Console.WriteLine("❌ Invalid choice.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        private void RequesterMenu()
        {
            Console.Clear();

            Request request = new Request();
            request.SubmitRequest();

            Console.WriteLine("\nPress any key to return to main menu...");
            Console.ReadKey();
        }

        private void AdminMenu()
        {
            Console.Clear();

            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("           Mandaue City LGU Admin Login");
            Console.WriteLine(new string('=', 60));
            Console.Write("Username: ");
            string user = Console.ReadLine();
            Console.Write("Password: ");
            string pass = ReadPassword();

            Admin admin = new Admin();

            if (!admin.Login(user, pass))
            {
                Console.WriteLine("❌ Login failed.");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();

            while (true)
            {
                Console.Clear();

                Console.WriteLine("\n" + new string('=', 60));
                Console.WriteLine("           MANDAUE CITY LGU ADMIN PANEL");
                Console.WriteLine(new string('=', 60));

                Console.WriteLine(new string('-', 60));
                Console.WriteLine($"{"OPTION",-10} | {"ACTION",-45}");
                Console.WriteLine(new string('-', 60));
                Console.WriteLine($"{"1",-10} | {"View All Donors",-45}");
                Console.WriteLine($"{"2",-10} | {"View All Donations",-45}");
                Console.WriteLine($"{"3",-10} | {"View Pending Requests",-45}");
                Console.WriteLine($"{"4",-10} | {"Process Pending Requests",-45}");
                Console.WriteLine($"{"5",-10} | {"Search Records",-45}");
                Console.WriteLine($"{"6",-10} | {"Generate Summary",-45}");
                Console.WriteLine($"{"7",-10} | {"Clear Data",-45}");
                Console.WriteLine($"{"8",-10} | {"Back",-45}");
                Console.WriteLine(new string('-', 60));

                Console.Write("Choose: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    admin.ViewAllDonors();
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
                else if (choice == "2")
                {
                    admin.ViewAllDonations();
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
                else if (choice == "3")
                {
                    admin.ViewPendingRequests();
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
                else if (choice == "4")
                {
                    admin.ProcessPendingRequests();
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
                else if (choice == "5")
                {
                    admin.SearchRecords();
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
                else if (choice == "6")
                {
                    admin.GenerateSummary();
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
                else if (choice == "7")
                {
                    admin.ClearData();
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
                else if (choice == "8") return;
                else
                {
                    Console.WriteLine("❌ Invalid choice.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
        }
    }
}