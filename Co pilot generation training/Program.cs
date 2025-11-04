using System;

namespace Co_pilot_generation_training
{
    class Program
    {
        static void Main(string[] args)
        {
            var books = new string?[5]; // allow nulls for empty slots

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Book Management System (optimized) ===\n");

                DisplayBooks(books);

                Console.WriteLine();
                Console.WriteLine("Choose an action:");
                Console.WriteLine("  1) Add    (or type 'add')");
                Console.WriteLine("  2) Remove (or type 'remove')");
                Console.WriteLine("  3) Display (or type 'display')");
                Console.WriteLine("  4) Exit   (or type 'exit')");
                Console.Write("Enter choice: ");

                var choice = Console.ReadLine();
                if (choice == null) break;

                var cmd = choice.Trim().ToLowerInvariant();

                // map short/number inputs to commands
                if (cmd == "1" || cmd == "add" || cmd == "a")
                {
                    if (!HasEmptySlot(books))
                    {
                        Console.WriteLine("\nERROR: All slots are full. Remove a book before adding another.");
                        Pause();
                        continue;
                    }

                    Console.Write("\nEnter book title to add: ");
                    var title = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(title))
                    {
                        Console.WriteLine("ERROR: Title cannot be empty.");
                        Pause();
                        continue;
                    }

                    title = title.Trim();
                    AddBook(books, title);
                    Console.WriteLine("Book added successfully!");
                    Pause();
                }
                else if (cmd == "2" || cmd == "remove" || cmd == "r")
                {
                    if (!HasAnyBook(books))
                    {
                        Console.WriteLine("\nERROR: The library is empty. Nothing to remove.");
                        Pause();
                        continue;
                    }

                    Console.Write("\nEnter the title of the book to remove (or enter its number): ");
                    var input = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(input)) { Console.WriteLine("ERROR: Title cannot be empty."); Pause(); continue; }

                    input = input.Trim();
                    // try parse as index first
                    if (int.TryParse(input, out var idx))
                    {
                        if (idx >= 1 && idx <= books.Length && !string.IsNullOrEmpty(books[idx - 1]))
                        {
                            Console.WriteLine($"Removed: {books[idx - 1]}");
                            books[idx - 1] = null;
                        }
                        else Console.WriteLine("No book at that number.");
                        Pause();
                        continue;
                    }

                    var removed = RemoveByTitle(books, input);
                    Console.WriteLine(removed ? "Book removed successfully!" : "Book not found.");
                    Pause();
                }
                else if (cmd == "3" || cmd == "display" || cmd == "d")
                {
                    Console.WriteLine();
                    DisplayBooks(books);
                    Pause();
                }
                else if (cmd == "4" || cmd == "exit" || cmd == "e")
                {
                    Console.WriteLine("\nGoodbye!");
                    Pause();
                    return;
                }
                else
                {
                    Console.WriteLine("\nERROR: Invalid choice. Please enter 1-4 or a command word (add/remove/display/exit).");
                    Pause();
                }
            }
        }

    static void DisplayBooks(string?[] books)
        {
            Console.WriteLine("Current Books:");
            var any = false;
            for (int i = 0; i < books.Length; i++)
            {
                if (!string.IsNullOrEmpty(books[i]))
                {
                    Console.WriteLine($"{i + 1}. {books[i]}");
                    any = true;
                }
            }
            if (!any) Console.WriteLine("(No books in the library)");
        }

        static bool HasEmptySlot(string?[] books) => Array.Exists(books, b => string.IsNullOrEmpty(b));

        static bool HasAnyBook(string?[] books) => Array.Exists(books, b => !string.IsNullOrEmpty(b));

        static void AddBook(string?[] books, string title)
        {
            for (int i = 0; i < books.Length; i++)
            {
                if (string.IsNullOrEmpty(books[i]))
                {
                    books[i] = title;
                    return;
                }
            }
        }

        static bool RemoveByTitle(string?[] books, string title)
        {
            var removed = false;
            for (int i = 0; i < books.Length; i++)
            {
                if (!string.IsNullOrEmpty(books[i]) && string.Equals(books[i], title, StringComparison.OrdinalIgnoreCase))
                {
                    books[i] = null;
                    removed = true;
                }
            }
            return removed;
        }

        static void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
