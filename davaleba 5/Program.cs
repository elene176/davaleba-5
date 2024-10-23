using System;
using System.Collections.Generic;

namespace davaleba_5
{
    class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();
            bool running = true;

            while (running)
            {
                // Display menu options
                Console.WriteLine("Library Menu:");
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. Remove Book");
                Console.WriteLine("3. Display All Books");
                Console.WriteLine("4. Search for a Book");
                Console.WriteLine("5. Borrow a Book");
                Console.WriteLine("6. Return a Book");
                Console.WriteLine("0. Exit");
                Console.Write("Select an option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1": // Add Book
                        Console.Write("Enter title: ");
                        string title = Console.ReadLine();
                        Console.Write("Enter author: ");
                        string author = Console.ReadLine();
                        Console.Write("Enter ISBN (13 characters): ");
                        string isbn = Console.ReadLine();
                        Console.Write("Enter copies available: ");
                        int copies = int.Parse(Console.ReadLine());

                        try
                        {
                            Book book = new Book(title, author, isbn, copies);
                            library.AddBook(book);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;

                    case "2": // Remove Book
                        Console.Write("Enter ISBN of the book to remove: ");
                        library.RemoveBook(Console.ReadLine());
                        break;

                    case "3": // Display All Books
                        library.DisplayAllBooks();
                        break;

                    case "4": // Search for a Book
                        Console.Write("Enter title to search for: ");
                        library.SearchBook(Console.ReadLine());
                        break;

                    case "5": // Borrow a Book
                        Console.Write("Enter ISBN of the book to borrow: ");
                        string borrowIsbn = Console.ReadLine();
                        library.BorrowBook(borrowIsbn);
                        break;

                    case "6": // Return a Book
                        Console.Write("Enter ISBN of the book to return: ");
                        string returnIsbn = Console.ReadLine();
                        library.ReturnBook(returnIsbn);
                        break;

                    case "0": // Exit
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

                Console.WriteLine();
            }
        }
    }

    public class Library
    {
        private List<Book> books = new List<Book>();

        public void AddBook(Book book)
        {
            books.Add(book);
            Console.WriteLine($"Book '{book.Title}' added to the library.");
        }

        public void RemoveBook(string isbn)
        {
            var bookToRemove = books.Find(b => b.ISBN == isbn);
            if (bookToRemove != null)
            {
                books.Remove(bookToRemove);
                Console.WriteLine($"Book '{bookToRemove.Title}' removed from the library.");
            }
            else
            {
                Console.WriteLine("Book not found.");
            }
        }

        public void DisplayAllBooks()
        {
            Console.WriteLine("Books in the library:");
            foreach (var book in books)
            {
                book.DisplayInfo();
            }
        }

        public void SearchBook(string title)
        {
            var foundBooks = books.FindAll(b => b.Title.IndexOf(title, StringComparison.OrdinalIgnoreCase) >= 0);
            if (foundBooks.Count > 0)
            {
                Console.WriteLine("Found books:");
                foreach (var book in foundBooks)
                {
                    book.DisplayInfo();
                }
            }
            else
            {
                Console.WriteLine("No books found.");
            }
        }

        public void BorrowBook(string isbn)
        {
            var bookToBorrow = books.Find(b => b.ISBN == isbn);
            if (bookToBorrow != null)
            {
                bookToBorrow.BorrowBook();
            }
            else
            {
                Console.WriteLine("Book not found.");
            }
        }

        public void ReturnBook(string isbn)
        {
            var bookToReturn = books.Find(b => b.ISBN == isbn);
            if (bookToReturn != null)
            {
                bookToReturn.ReturnBook();
            }
            else
            {
                Console.WriteLine("Book not found.");
            }
        }
    }

    public class Book
    {
        private string title;
        private string author;
        private string isbn;
        private int copiesAvailable;

        public string Title
        {
            get => title;
            set => title = value;
        }

        public string Author
        {
            get => author;
            set => author = value;
        }

        public string ISBN
        {
            get => isbn;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("ISBN cannot be empty.");
                if (value.Length != 13)
                    throw new ArgumentException("ISBN must be 13 characters long.");
                isbn = value;
            }
        }

        public int CopiesAvailable
        {
            get => copiesAvailable;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Copies available cannot be negative.");
                copiesAvailable = value;
            }
        }

        public Book(string title, string author, string isbn, int copiesAvailable)
        {
            Title = title;
            Author = author;
            ISBN = isbn;
            CopiesAvailable = copiesAvailable;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Title: {Title}, Author: {Author}, ISBN: {ISBN}, Copies Available: {CopiesAvailable}");
        }

        public void BorrowBook()
        {
            if (CopiesAvailable > 0)
            {
                CopiesAvailable--;
                Console.WriteLine($"You have borrowed '{Title}'.");
            }
            else
            {
                Console.WriteLine($"Sorry, '{Title}' is not available.");
            }
        }


        public void ReturnBook()
        {
            CopiesAvailable++;
            Console.WriteLine($"You have returned '{Title}'.");
        }
    }
}

        


       
         
            
           
        
    

