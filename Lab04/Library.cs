using System.Collections.Generic;
using System.Linq;

public class Library
{
    private readonly Dictionary<int, Book> _books = new Dictionary<int, Book>();
    private readonly Dictionary<int, User> _users = new Dictionary<int, User>();

    public void AddBook(Book book)
    {
        if (_books.ContainsKey(book.Id))
            throw new InvalidOperationException($"Книга з ідентифікатором {book.Id} вже існує.");

        _books.Add(book.Id, book);
    }

    public void AddUser(User user)
    {
        if (_users.ContainsKey(user.Id))
            throw new InvalidOperationException($"Користувач з ідентифікатором {user.Id} вже існує.");

        _users.Add(user.Id, user);
    }

    public bool TryIssueBook(int userId, int bookId, out string message)
    {
        if (!_users.TryGetValue(userId, out User user))
        {
            message = $"Користувача з ідентифікатором {userId} не знайдено.";
            return false;
        }

        if (!_books.TryGetValue(bookId, out Book book))
        {
            message = $"Книгу з ідентифікатором {bookId} не знайдено.";
            return false;
        }

        if (!book.IsAvailable)
        {
            message = $"Книга \"{book.Title}\" вже в оренді користувача {book.Borrower.Name}.";
            return false;
        }

        book.Lend(user);
        user.AddBook(book);
        message = $"Книгу \"{book.Title}\" видано користувачу {user.Name}.";
        return true;
    }

    public bool TryReturnBook(int userId, int bookId, out string message)
    {
        if (!_users.TryGetValue(userId, out User user))
        {
            message = $"Користувача з ідентифікатором {userId} не знайдено.";
            return false;
        }

        if (!_books.TryGetValue(bookId, out Book book))
        {
            message = $"Книгу з ідентифікатором {bookId} не знайдено.";
            return false;
        }

        if (!user.HasBook(book))
        {
            message = $"Користувач {user.Name} не орендував книгу \"{book.Title}\".";
            return false;
        }

        user.RemoveBook(book);
        book.Receive();
        message = $"Книгу \"{book.Title}\" повернено користувачем {user.Name}.";
        return true;
    }

    public bool IsBookAvailable(int bookId)
    {
        return _books.TryGetValue(bookId, out Book book) && book.IsAvailable;
    }

    public void PrintAllBooks()
    {
        Console.WriteLine("Усі книги:");
        PrintBooks(_books.Values);
    }

    public void PrintAvailableBooks()
    {
        Console.WriteLine("Доступні книги:");
        PrintBooks(_books.Values.Where(b => b.IsAvailable));
    }

    public void PrintRentedBooks()
    {
        Console.WriteLine("Книги в оренді:");
        PrintBooks(_books.Values.Where(b => !b.IsAvailable));
    }

    public void PrintUsers()
    {
        Console.WriteLine("Користувачі:");
        foreach (User user in _users.Values)
            Console.WriteLine("  " + user);
    }

    private static void PrintBooks(IEnumerable<Book> books)
    {
        bool any = false;
        foreach (Book book in books)
        {
            Console.WriteLine("  " + book);
            any = true;
        }

        if (!any)
            Console.WriteLine("  (порожньо)");
    }
}