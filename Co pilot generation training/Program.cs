using System;
using System.Collections.Generic;
using System.Linq;

namespace Co_pilot_generation_training
{
    class Program
    {
        private sealed class Library
        {
            private class BookSlot
            {
                public string? Title { get; set; }
                public bool IsCheckedOut { get; set; }
            }

            private readonly BookSlot[] slots;
            public Library(int capacity)
            {
                slots = new BookSlot[capacity];
                for (int i = 0; i < capacity; i++)
                    slots[i] = new BookSlot();
            }

            public bool IsEmpty => Array.TrueForAll(slots, s => string.IsNullOrEmpty(s.Title));
            public bool IsFull => Array.TrueForAll(slots, s => !string.IsNullOrEmpty(s.Title));

            public IReadOnlyList<(int Index, string Title, bool IsCheckedOut)> ListBooks()
            {
                var list = new List<(int, string, bool)>();
                for (int i = 0; i < slots.Length; i++)
                    if (!string.IsNullOrEmpty(slots[i].Title))
                        list.Add((i + 1, slots[i].Title!, slots[i].IsCheckedOut));
                return list;
            }

            public IReadOnlyList<(int Index, string Title)> ListAvailableBooks()
            {
                var list = new List<(int, string)>();
                for (int i = 0; i < slots.Length; i++)
                    if (!string.IsNullOrEmpty(slots[i].Title) && !slots[i].IsCheckedOut)
                        list.Add((i + 1, slots[i].Title!));
                return list;
            }

            public IReadOnlyList<(int Index, string Title)> ListCheckedOutBooks()
            {
                var list = new List<(int, string)>();
                for (int i = 0; i < slots.Length; i++)
                    if (!string.IsNullOrEmpty(slots[i].Title) && slots[i].IsCheckedOut)
                        list.Add((i + 1, slots[i].Title!));
                return list;
            }

            public bool Add(string title)
            {
                // Check if the book already exists (case-insensitive)
                for (int i = 0; i < slots.Length; i++)
                {
                    if (!string.IsNullOrEmpty(slots[i].Title) &&
                        string.Equals(slots[i].Title, title, StringComparison.OrdinalIgnoreCase))
                    {
                        return false; // Duplicate found
                    }
                }

                // Find first empty slot and add the book
                for (int i = 0; i < slots.Length; i++)
                {
                    if (string.IsNullOrEmpty(slots[i].Title))
                    {
                        slots[i].Title = title;
                        slots[i].IsCheckedOut = false;
                        return true;
                    }
                }
                return false; // Library is full
            }

            public string? RemoveAt(int indexOneBased)
            {
                int i = indexOneBased - 1;
                if (i < 0 || i >= slots.Length) return null;
                var old = slots[i].Title;
                if (string.IsNullOrEmpty(old)) return null;
                slots[i].Title = null;
                slots[i].IsCheckedOut = false;
                return old;
            }

            public int RemoveByTitle(string title)
            {
                int removed = 0;
                for (int i = 0; i < slots.Length; i++)
                {
                    if (!string.IsNullOrEmpty(slots[i].Title) && string.Equals(slots[i].Title, title, StringComparison.OrdinalIgnoreCase))
                    {
                        slots[i].Title = null;
                        slots[i].IsCheckedOut = false;
                        removed++;
                    }
                }
                return removed;
            }

            public IReadOnlyList<(int Index, string Title, bool IsCheckedOut)> Search(string query)
            {
                var res = new List<(int, string, bool)>();
                for (int i = 0; i < slots.Length; i++)
                {
                    if (!string.IsNullOrEmpty(slots[i].Title) && string.Equals(slots[i].Title, query, StringComparison.OrdinalIgnoreCase))
                        res.Add((i + 1, slots[i].Title!, slots[i].IsCheckedOut));
                }
                return res;
            }

            public bool CheckOut(int indexOneBased)
            {
                int i = indexOneBased - 1;
                if (i < 0 || i >= slots.Length || string.IsNullOrEmpty(slots[i].Title)) return false;
                if (slots[i].IsCheckedOut) return false; // Already checked out
                slots[i].IsCheckedOut = true;
                return true;
            }

            public bool CheckIn(int indexOneBased)
            {
                int i = indexOneBased - 1;
                if (i < 0 || i >= slots.Length || string.IsNullOrEmpty(slots[i].Title)) return false;
                if (!slots[i].IsCheckedOut) return false; // Not checked out
                slots[i].IsCheckedOut = false;
                return true;
            }

            public bool IsBookCheckedOut(int indexOneBased)
            {
                int i = indexOneBased - 1;
                if (i < 0 || i >= slots.Length || string.IsNullOrEmpty(slots[i].Title)) return false;
                return slots[i].IsCheckedOut;
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
                Console.WriteLine(" 5) Borrow");
                Console.WriteLine(" 6) Check In");
                Console.WriteLine(" 7) Exit");
                Console.Write("Enter choice (1-7 or word): ");

                var choice = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(choice)) { Console.WriteLine("Invalid input."); Pause(); continue; }

                var cmd = choice.ToLowerInvariant();

                if (cmd == "1" || cmd == "add")
                {
                    AddBook(library);
                }
                else if (cmd == "2" || cmd == "remove")
                {
                    RemoveBook(library);
                }
                else if (cmd == "3" || cmd == "display")
                {
                    DisplayBooks(library);
                }
                else if (cmd == "4" || cmd == "search")
                {
                    SearchBook(library);
                }
                else if (cmd == "5" || cmd == "borrow")
                {
                    BorrowBook(library);
                }
                else if (cmd == "6" || cmd == "check" || cmd == "checkin" || cmd == "check in")
                {
                    CheckInBook(library);
                }
                else if (cmd == "7" || cmd == "exit")
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

        static void AddBook(Library library)
        {
            if (library.IsFull)
            {
                Console.WriteLine("Library full — remove an item first.");
                Pause();
                return;
            }
            Console.Write("Enter title to add: ");
            var title = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(title))
            {
                Console.WriteLine("Title cannot be empty.");
                Pause();
                return;
            }
            if (library.Add(title))
            {
                Console.WriteLine("Added.");
            }
            else
            {
                Console.WriteLine("This book already exists in the library. Duplicates are not allowed.");
            }
            Pause();
        }

        static void RemoveBook(Library library)
        {
            if (library.IsEmpty)
            {
                Console.WriteLine("There is nothing to remove - the library is empty.");
                Pause();
                return;
            }

            Console.WriteLine("\nCurrent books available for removal:");
            PrintBooks(library);
            Console.WriteLine();
            Console.Write("Enter title or number to remove: ");
            var arg = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(arg))
            {
                Console.WriteLine("Invalid input.");
                Pause();
                return;
            }
            if (int.TryParse(arg, out var n))
            {
                var removed = library.RemoveAt(n);
                Console.WriteLine(removed == null ? "No book at that number." : $"Removed: {removed}");
                Pause();
                return;
            }
            var count = library.RemoveByTitle(arg);
            Console.WriteLine(count > 0 ? $"Removed {count} item(s)." : "Book not found.");
            Pause();
        }

        static void DisplayBooks(Library library)
        {
            PrintBooks(library);
            Pause();
        }

        static void CheckInBook(Library library)
        {
            var checkedOutBooks = library.ListCheckedOutBooks();

            if (checkedOutBooks.Count == 0)
            {
                Console.WriteLine("\nYou haven't borrowed any books yet.");
                Pause();
                return;
            }

            Console.WriteLine("\nYour borrowed books:");
            foreach (var (idx, title) in checkedOutBooks)
            {
                Console.WriteLine($"{idx}. {title}");
            }

            Console.Write("\nEnter the number of the book you want to check in: ");
            var input = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Invalid input.");
                Pause();
                return;
            }

            if (int.TryParse(input, out var bookNum))
            {
                if (library.CheckIn(bookNum))
                {
                    var bookList = library.ListBooks();
                    var book = bookList.FirstOrDefault(b => b.Index == bookNum);
                    if (book.Title != null)
                    {
                        Console.WriteLine($"\nSuccessfully checked in: {book.Title}");
                        Console.WriteLine("The book is now available for borrowing again.");
                    }
                }
                else
                {
                    Console.WriteLine("\nCould not check in - book is not checked out or doesn't exist.");
                }
            }
            else
            {
                Console.WriteLine("\nInvalid book number.");
            }
            Pause();
        }

        static void SearchBook(Library library)
        {
            if (library.IsEmpty)
            {
                Console.WriteLine("There are no books yet.");
                Pause();
                return;
            }

            Console.Write("Enter book title to search: ");
            var q = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(q))
            {
                Console.WriteLine("Invalid input.");
                Pause();
                return;
            }

            var matches = library.Search(q);
            if (matches.Count > 0)
            {
                Console.WriteLine($"Found {matches.Count} match(es):");
                foreach (var (idx, t, checkedOut) in matches)
                {
                    var status = checkedOut ? " [CHECKED OUT]" : " [AVAILABLE]";
                    Console.WriteLine($"{idx}. {t}{status}");
                }
            }
            else
            {
                Console.WriteLine("Book title is not in the collection.");
            }
            Pause();
        }

        static void BorrowBook(Library library)
        {
            var checkedOutBooks = library.ListCheckedOutBooks();
            Console.WriteLine("\nYour borrowed books:");
            if (checkedOutBooks.Count == 0)
            {
                Console.WriteLine("(No books borrowed yet)");
            }
            else
            {
                foreach (var (idx, title) in checkedOutBooks)
                {
                    Console.WriteLine($"{idx}. {title}");
                }
            }
            Console.WriteLine($"\nYou can borrow {3 - checkedOutBooks.Count} more book(s).\n");

            if (checkedOutBooks.Count >= 3)
            {
                Console.WriteLine("You have reached the borrow limit (3 books). Return a book before borrowing more.");
                Pause();
                return;
            }

            var availableBooks = library.ListAvailableBooks();
            if (availableBooks.Count == 0)
            {
                Console.WriteLine("There are no books available to borrow.");
                Pause();
                return;
            }

            Console.WriteLine("Available books to borrow:");
            foreach (var (idx, title) in availableBooks)
            {
                Console.WriteLine($"{idx}. {title}");
            }

            Console.Write("\nEnter title or number to borrow: ");
            var arg = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(arg)) { Console.WriteLine("Invalid input."); Pause(); return; }

            if (int.TryParse(arg, out var n))
            {
                if (library.IsBookCheckedOut(n))
                {
                    Console.WriteLine("This book is already checked out.");
                }
                else if (library.CheckOut(n))
                {
                    var bookList = library.ListBooks();
                    var book = bookList.FirstOrDefault(b => b.Index == n);
                    if (book.Title != null)
                    {
                        Console.WriteLine($"You borrowed: {book.Title}");
                        Console.WriteLine($"Borrowed count: {checkedOutBooks.Count + 1}/3");
                    }
                }
                else
                {
                    Console.WriteLine("No book at that number.");
                }
                Pause();
                return;
            }

            var found = library.Search(arg);
            if (found.Count > 0)
            {
                var (idx, title, isCheckedOut) = found[0];
                if (isCheckedOut)
                {
                    Console.WriteLine("This book is already checked out.");
                }
                else if (library.CheckOut(idx))
                {
                    Console.WriteLine($"You borrowed: {title}");
                    Console.WriteLine($"Borrowed count: {checkedOutBooks.Count + 1}/3");
                }
                else
                {
                    Console.WriteLine("Could not borrow the book.");
                }
            }
            else
            {
                Console.WriteLine("Book title is not in the collection.");
            }
            Pause();
        }

        static void PrintBooks(Library lib)
        {
            var list = lib.ListBooks();
            if (list.Count == 0) Console.WriteLine("(No books in the library)");
            else
            {
                Console.WriteLine("Current books:");
                foreach (var (i, t, checkedOut) in list)
                {
                    var status = checkedOut ? " [CHECKED OUT]" : "";
                    Console.WriteLine($"{i}. {t}{status}");
                }
            }
        }

        static void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }
    }
}
