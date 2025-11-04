﻿namespace Co_pilot_generation_training;

class Program
{
    static void Main(string[] args)
    {
        // Declaring variables with empty strings
        string bookTitle1 = "";
        string bookTitle2 = "";
        string bookTitle3 = "";
        string bookTitle4 = "";
        string bookTitle5 = "";

        while (true)
        {
            Console.Clear(); // Clear the console for better readability
            Console.WriteLine("=== Book Management System ===\n");

            // Display current books
            Console.WriteLine("Current Books:");
            bool booksExist = false;
            if (bookTitle1 != "") { Console.WriteLine($"1. {bookTitle1}"); booksExist = true; }
            if (bookTitle2 != "") { Console.WriteLine($"2. {bookTitle2}"); booksExist = true; }
            if (bookTitle3 != "") { Console.WriteLine($"3. {bookTitle3}"); booksExist = true; }
            if (bookTitle4 != "") { Console.WriteLine($"4. {bookTitle4}"); booksExist = true; }
            if (bookTitle5 != "") { Console.WriteLine($"5. {bookTitle5}"); booksExist = true; }
            
            if (!booksExist)
            {
                Console.WriteLine("(No books in the library)");
            }
            Console.WriteLine();

            // Display menu
            Console.WriteLine("\nWhat would you like to do?");
            Console.WriteLine("1. Add a book");
            Console.WriteLine("2. Remove a book");
            Console.WriteLine("3. Display books");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice (1-4): ");
            
            string? choice = Console.ReadLine();

            if (choice == null)
                break;

            // Validate user input
            if (choice == null || !new[] { "1", "2", "3", "4" }.Contains(choice))
            {
                Console.WriteLine("\nERROR: Invalid choice!");
                Console.WriteLine("You must enter a number between 1 and 4:");
                Console.WriteLine("1 - Add a book");
                Console.WriteLine("2 - Remove a book");
                Console.WriteLine("3 - Display books");
                Console.WriteLine("4 - Exit");
                Console.WriteLine("\nPress any key to try again...");
                Console.ReadKey();
                continue;
            }

            switch (choice)
            {
                case "1":
                    // Check if there are any empty slots available
                    if (bookTitle1 != "" && bookTitle2 != "" && bookTitle3 != "" && bookTitle4 != "" && bookTitle5 != "")
                    {
                        Console.WriteLine("\nERROR: Cannot add more books!");
                        Console.WriteLine("All slots are full. Please remove a book first.");
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        continue;
                    }

                    // Add a book
                    Console.WriteLine("\nEnter a book title:");
                    string? input = Console.ReadLine();

                    if (input == null || input.Trim() == "")
                    {
                        Console.WriteLine("\nERROR: Book title cannot be empty!");
                        Console.WriteLine("Press any key to try again...");
                        Console.ReadKey();
                        continue;
                    }

                    // Find first empty variable using switch
                    switch (true)
                    {
                        case bool _ when bookTitle1 == "":
                            bookTitle1 = input;
                            break;
                        case bool _ when bookTitle2 == "":
                            bookTitle2 = input;
                            break;
                        case bool _ when bookTitle3 == "":
                            bookTitle3 = input;
                            break;
                        case bool _ when bookTitle4 == "":
                            bookTitle4 = input;
                            break;
                        case bool _ when bookTitle5 == "":
                            bookTitle5 = input;
                            break;
                        default:
                            Console.WriteLine("All variables are full! Cannot add more books.");
                            continue;
                    }
                    Console.WriteLine("Book added successfully!");
                    break;

                case "2":
                    // Check if there are any books to remove
                    if (bookTitle1 == "" && bookTitle2 == "" && bookTitle3 == "" && bookTitle4 == "" && bookTitle5 == "")
                    {
                        Console.WriteLine("\nERROR: Cannot remove books!");
                        Console.WriteLine("The library is empty. Please add some books first.");
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        continue;
                    }

                    // Remove a book
                    Console.WriteLine("\nEnter the title of the book to remove:");
                    string? titleToRemove = Console.ReadLine();

                    if (titleToRemove == null || titleToRemove.Trim() == "")
                    {
                        Console.WriteLine("\nERROR: Book title cannot be empty!");
                        Console.WriteLine("Press any key to try again...");
                        Console.ReadKey();
                        continue;
                    }

                    bool bookFound = false;

                    // Check and remove book using switch
                    switch (titleToRemove)
                    {
                        case var title when title == bookTitle1:
                            bookTitle1 = "";
                            bookFound = true;
                            break;
                        case var title when title == bookTitle2:
                            bookTitle2 = "";
                            bookFound = true;
                            break;
                        case var title when title == bookTitle3:
                            bookTitle3 = "";
                            bookFound = true;
                            break;
                        case var title when title == bookTitle4:
                            bookTitle4 = "";
                            bookFound = true;
                            break;
                        case var title when title == bookTitle5:
                            bookTitle5 = "";
                            bookFound = true;
                            break;
                    }

                    Console.WriteLine(bookFound ? "Book removed successfully!" : "Book not found!");
                    break;

                case "3":
                    // Display non-empty books
                    Console.WriteLine("\nCurrent Books:");
                    bool hasBooks = false;
                    
                    // Check each book and display if not empty
                    if (bookTitle1 != "")
                    {
                        Console.WriteLine($"- {bookTitle1}");
                        hasBooks = true;
                    }
                    if (bookTitle2 != "")
                    {
                        Console.WriteLine($"- {bookTitle2}");
                        hasBooks = true;
                    }
                    if (bookTitle3 != "")
                    {
                        Console.WriteLine($"- {bookTitle3}");
                        hasBooks = true;
                    }
                    if (bookTitle4 != "")
                    {
                        Console.WriteLine($"- {bookTitle4}");
                        hasBooks = true;
                    }
                    if (bookTitle5 != "")
                    {
                        Console.WriteLine($"- {bookTitle5}");
                        hasBooks = true;
                    }
                    
                    if (!hasBooks)
                    {
                        Console.WriteLine("No books in the library!");
                    }
                    break;

                case "4":
                    Console.WriteLine("\nThank you for using the Book Management System!");
                    Console.WriteLine("Press any key to exit...");
                    Console.ReadKey();
                    return;

                default:
                    Console.WriteLine("\nInvalid choice. Please enter 1, 2, 3, or 4.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }

        // Final display of all books
        Console.WriteLine("\nFinal Book List:");
        Console.WriteLine($"1. {(bookTitle1 == "" ? "Empty" : bookTitle1)}");
        Console.WriteLine($"2. {(bookTitle2 == "" ? "Empty" : bookTitle2)}");
        Console.WriteLine($"3. {(bookTitle3 == "" ? "Empty" : bookTitle3)}");
        Console.WriteLine($"4. {(bookTitle4 == "" ? "Empty" : bookTitle4)}");
        Console.WriteLine($"5. {(bookTitle5 == "" ? "Empty" : bookTitle5)}");
    }
}
