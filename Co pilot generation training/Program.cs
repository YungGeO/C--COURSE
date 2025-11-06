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
                Console.WriteLine(" 5) Exit");
                Console.Write("Enter choice (1-5 or word): ");

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
                else if (cmd == "5" || cmd == "exit")
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
