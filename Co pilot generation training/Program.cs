using System;
using System.Collections.Generic;

namespace Co_pilot_generation_training
{
    class Program
    {
        private sealed class Library
        {
            private readonly string?[] slots;
            public Library(int capacity) => slots = new string?[capacity];
            public bool IsEmpty => Array.TrueForAll(slots, s => string.IsNullOrEmpty(s));
            public bool IsFull => Array.TrueForAll(slots, s => !string.IsNullOrEmpty(s));

            public IReadOnlyList<(int Index, string Title)> ListBooks()
            {
                var list = new List<(int, string)>();
                for (int i = 0; i < slots.Length; i++)
                    if (!string.IsNullOrEmpty(slots[i]))
                        list.Add((i + 1, slots[i]!));
                return list;
            }

            public bool Add(string title)
            {
                for (int i = 0; i < slots.Length; i++)
                {
                    if (string.IsNullOrEmpty(slots[i]))
                    {
                        slots[i] = title;
                        return true;
                    }
                }
                return false;
            }

            public string? RemoveAt(int indexOneBased)
            {
                int i = indexOneBased - 1;
                if (i < 0 || i >= slots.Length) return null;
                var old = slots[i];
                if (string.IsNullOrEmpty(old)) return null;
                slots[i] = null;
                return old;
            }

            public int RemoveByTitle(string title)
            {
                int removed = 0;
                for (int i = 0; i < slots.Length; i++)
                {
                    if (!string.IsNullOrEmpty(slots[i]) && string.Equals(slots[i], title, StringComparison.OrdinalIgnoreCase))
                    {
                        slots[i] = null;
                        removed++;
                    }
                }
                return removed;
            }

            public IReadOnlyList<(int Index, string Title)> Search(string query)
            {
                var res = new List<(int, string)>();
                for (int i = 0; i < slots.Length; i++)
                {
                    if (!string.IsNullOrEmpty(slots[i]) && string.Equals(slots[i], query, StringComparison.OrdinalIgnoreCase))
                        res.Add((i + 1, slots[i]!));
                }
                return res;
            }
        }

        static void Main(string[] args)
        {
            var library = new Library(5);
            var borrowed = new List<string>(); // track user's borrowed books (max 3)

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Book Management (optimized) ===\n");

                PrintBooks(library);

                Console.WriteLine();
                Console.WriteLine("Actions:");
                Console.WriteLine(" 1) Add");
                Console.WriteLine(" 2) Remove");
                Console.WriteLine(" 3) Display");
                Console.WriteLine(" 4) Search");
                Console.WriteLine(" 5) Borrow");
                Console.WriteLine(" 6) Exit");
                Console.Write("Enter choice (1-6 or word): ");

                var choice = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(choice)) { Console.WriteLine("Invalid input."); Pause(); continue; }

                var cmd = choice.ToLowerInvariant();

                if (cmd == "1" || cmd == "add")
                {
                    if (library.IsFull) { Console.WriteLine("Library full — remove an item first."); Pause(); continue; }
                    Console.Write("Enter title to add: ");
                    var title = Console.ReadLine()?.Trim();
                    if (string.IsNullOrEmpty(title)) { Console.WriteLine("Title cannot be empty."); Pause(); continue; }
                    library.Add(title);
                    Console.WriteLine("Added."); Pause();
                }
                else if (cmd == "2" || cmd == "remove")
                {
                    if (library.IsEmpty) { Console.WriteLine("Library empty — nothing to remove."); Pause(); continue; }
                    Console.Write("Enter title or number to remove: ");
                    var arg = Console.ReadLine()?.Trim();
                    if (string.IsNullOrEmpty(arg)) { Console.WriteLine("Invalid input."); Pause(); continue; }
                    if (int.TryParse(arg, out var n))
                    {
                        var removed = library.RemoveAt(n);
                        Console.WriteLine(removed == null ? "No book at that number." : $"Removed: {removed}");
                        Pause();
                        continue;
                    }
                    var count = library.RemoveByTitle(arg);
                    Console.WriteLine(count > 0 ? $"Removed {count} item(s)." : "Book not found.");
                    Pause();
                }
                else if (cmd == "3" || cmd == "display")
                {
                    PrintBooks(library); Pause();
                }
                else if (cmd == "4" || cmd == "search")
                {
                    // New search behavior requested:
                    if (library.IsEmpty)
                    {
                        Console.WriteLine("There are no books yet.");
                        Pause();
                        continue;
                    }

                    Console.Write("Enter book title to search: ");
                    var q = Console.ReadLine()?.Trim();
                    if (string.IsNullOrEmpty(q)) { Console.WriteLine("Invalid input."); Pause(); continue; }

                    var matches = library.Search(q);
                    if (matches.Count > 0)
                    {
                        Console.WriteLine($"Found {matches.Count} match(es):");
                        foreach (var (idx, t) in matches) Console.WriteLine($"{idx}. {t}");
                        Console.WriteLine("This book is available.");
                    }
                    else
                    {
                        Console.WriteLine("Book title is not in the collection.");
                    }
                    Pause();
                }
                else if (cmd == "5" || cmd == "borrow")
                {
                    BorrowBook(library, borrowed);
                }
                else if (cmd == "6" || cmd == "exit")
                {
                    Console.WriteLine("Goodbye.");
                    return;
                }
                else
                {
                    Console.WriteLine("Unknown command."); Pause();
                }
            }
        }

        static void BorrowBook(Library library, List<string> borrowed)
        {
            Console.WriteLine("\nYour borrowed books:");
            if (borrowed.Count == 0)
            {
                Console.WriteLine("(No books borrowed yet)");
            }
            else
            {
                for (int i = 0; i < borrowed.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {borrowed[i]}");
                }
            }
            Console.WriteLine($"\nYou can borrow {3 - borrowed.Count} more book(s).\n");

            if (borrowed.Count >= 3)
            {
                Console.WriteLine("You have reached the borrow limit (3 books). Return a book before borrowing more.");
                Pause();
                return;
            }

            if (library.IsEmpty)
            {
                Console.WriteLine("There are no books to borrow.");
                Pause();
                return;
            }

            Console.WriteLine("Available books to borrow:");
            PrintBooks(library);

            Console.Write("Enter title or number to borrow: ");
            var arg = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(arg)) { Console.WriteLine("Invalid input."); Pause(); return; }

            if (int.TryParse(arg, out var n))
            {
                var b = library.RemoveAt(n);
                if (b == null) Console.WriteLine("No book at that number.");
                else { borrowed.Add(b); Console.WriteLine($"You borrowed: {b}"); }
                Console.WriteLine($"Borrowed count: {borrowed.Count}/3");
                Pause();
                return;
            }

            var found = library.Search(arg);
            if (found.Count > 0)
            {
                var idx = found[0].Index;
                var b2 = library.RemoveAt(idx);
                if (b2 != null) { borrowed.Add(b2); Console.WriteLine($"You borrowed: {b2}"); }
                else Console.WriteLine("Could not borrow the book.");
            }
            else Console.WriteLine("Book title is not in the collection.");

            Console.WriteLine($"Borrowed count: {borrowed.Count}/3");
            Pause();
        }

        static void PrintBooks(Library lib)
        {
            var list = lib.ListBooks();
            if (list.Count == 0) Console.WriteLine("(No books in the library)");
            else
            {
                Console.WriteLine("Current books:");
                foreach (var (i, t) in list) Console.WriteLine($"{i}. {t}");
            }
        }

        static void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }
    }
}
