using System;

namespace Co_pilot_generation_training
{
    class Program
    {
        static void Main(string[] args)
        {
            // Five book title slots
            string bookTitle1 = "";
            string bookTitle2 = "";
            string bookTitle3 = "";
            string bookTitle4 = "";
            string bookTitle5 = "";

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Book Management System ===\n");

                // Show current books (only non-empty)
                Console.WriteLine("Current Books:");
                bool any = false;
                if (bookTitle1 != "") { Console.WriteLine($"1. {bookTitle1}"); any = true; }
                if (bookTitle2 != "") { Console.WriteLine($"2. {bookTitle2}"); any = true; }
                if (bookTitle3 != "") { Console.WriteLine($"3. {bookTitle3}"); any = true; }
                if (bookTitle4 != "") { Console.WriteLine($"4. {bookTitle4}"); any = true; }
                if (bookTitle5 != "") { Console.WriteLine($"5. {bookTitle5}"); any = true; }
                if (!any) Console.WriteLine("(No books in the library)");

                Console.WriteLine();
                Console.WriteLine("Choose an action:");
                Console.WriteLine("  1) Add    (or type 'add')");
                Console.WriteLine("  2) Remove (or type 'remove')");
                Console.WriteLine("  3) Display (or type 'display')");
                Console.WriteLine("  4) Exit   (or type 'exit')");
                Console.Write("Enter choice: ");

                string? choice = Console.ReadLine();
                if (choice == null)
                    break;

                string normalized = choice.Trim().ToLower();
                var valid = new[] { "1", "2", "3", "4", "add", "remove", "display", "exit", "a", "r", "d", "e" };
                if (!valid.Contains(normalized))
                {
                    Console.WriteLine("\nERROR: Invalid choice. Please enter 1-4 or a command word (add/remove/display/exit).");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    continue;
                }

                switch (normalized)
                {
                    case "1":
                    case "add":
                    case "a":
                        // Only allow adding if there's at least one empty slot
                        if (bookTitle1 != "" && bookTitle2 != "" && bookTitle3 != "" && bookTitle4 != "" && bookTitle5 != "")
                        {
                            Console.WriteLine("\nERROR: All slots are full. Remove a book before adding another.");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;
                        }

                        Console.Write("\nEnter book title to add: ");
                        string? newTitle = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(newTitle))
                        {
                            Console.WriteLine("\nERROR: Title cannot be empty.");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;
                        }

                        newTitle = newTitle.Trim();
                        if (bookTitle1 == "") { bookTitle1 = newTitle; }
                        else if (bookTitle2 == "") { bookTitle2 = newTitle; }
                        else if (bookTitle3 == "") { bookTitle3 = newTitle; }
                        else if (bookTitle4 == "") { bookTitle4 = newTitle; }
                        else if (bookTitle5 == "") { bookTitle5 = newTitle; }

                        Console.WriteLine("Book added successfully!");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;

                    case "2":
                    case "remove":
                    case "r":
                        // Only allow removing if there's at least one book
                        if (bookTitle1 == "" && bookTitle2 == "" && bookTitle3 == "" && bookTitle4 == "" && bookTitle5 == "")
                        {
                            Console.WriteLine("\nERROR: The library is empty. Nothing to remove.");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;
                        }

                        Console.Write("\nEnter the title of the book to remove: ");
                        string? removeTitle = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(removeTitle))
                        {
                            Console.WriteLine("\nERROR: Title cannot be empty.");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;
                        }

                        removeTitle = removeTitle.Trim();
                        bool removed = false;
                        if (string.Equals(bookTitle1, removeTitle, StringComparison.OrdinalIgnoreCase)) { bookTitle1 = ""; removed = true; }
                        if (string.Equals(bookTitle2, removeTitle, StringComparison.OrdinalIgnoreCase)) { bookTitle2 = ""; removed = true; }
                        if (string.Equals(bookTitle3, removeTitle, StringComparison.OrdinalIgnoreCase)) { bookTitle3 = ""; removed = true; }
                        if (string.Equals(bookTitle4, removeTitle, StringComparison.OrdinalIgnoreCase)) { bookTitle4 = ""; removed = true; }
                        if (string.Equals(bookTitle5, removeTitle, StringComparison.OrdinalIgnoreCase)) { bookTitle5 = ""; removed = true; }

                        Console.WriteLine(removed ? "Book removed successfully!" : "Book not found.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;

                    case "3":
                    case "display":
                    case "d":
                        Console.WriteLine("\nCurrent Books:");
                        bool any2 = false;
                        if (bookTitle1 != "") { Console.WriteLine($"- {bookTitle1}"); any2 = true; }
                        if (bookTitle2 != "") { Console.WriteLine($"- {bookTitle2}"); any2 = true; }
                        if (bookTitle3 != "") { Console.WriteLine($"- {bookTitle3}"); any2 = true; }
                        if (bookTitle4 != "") { Console.WriteLine($"- {bookTitle4}"); any2 = true; }
                        if (bookTitle5 != "") { Console.WriteLine($"- {bookTitle5}"); any2 = true; }
                        if (!any2) Console.WriteLine("(No books in the library)");
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;

                    case "4":
                    case "exit":
                    case "e":
                        Console.WriteLine("\nGoodbye!");
                        Console.WriteLine("Press any key to exit...");
                        Console.ReadKey();
                        return;
                }
            }
        }
    }
}
