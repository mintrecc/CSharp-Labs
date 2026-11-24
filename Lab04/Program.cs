class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Library library = new Library();

        library.AddBook(new Book(1, "Кобзар", "Тарас Шевченко"));
        library.AddBook(new Book(2, "Лісова пісня", "Леся Українка"));
        library.AddBook(new Book(3, "Тіні забутих предків", "Михайло Коцюбинський"));
        library.AddBook(new Book(4, "Захар Беркут", "Іван Франко"));

        library.AddUser(new User(1, "Олена"));
        library.AddUser(new User(2, "Андрій"));
        library.AddUser(new User(3, "Марія"));

        library.PrintAllBooks();
        Console.WriteLine();
        library.PrintUsers();

        Console.WriteLine("\n--- Перегляд доступних книг ---");
        library.PrintAvailableBooks();

        Console.WriteLine("\n--- Видача книги 1 користувачу 1 ---");
        Execute(library.TryIssueBook(1, 1, out string message), message);

        Console.WriteLine("\n--- Спроба видати книгу 1 користувачу 2 ---");
        Execute(library.TryIssueBook(2, 1, out message), message);

        Console.WriteLine("\n--- Спроба видати неіснуючу книгу 99 ---");
        Execute(library.TryIssueBook(2, 99, out message), message);

        Console.WriteLine("\n--- Доступні книги та книги в оренді ---");
        library.PrintAvailableBooks();
        library.PrintRentedBooks();

        Console.WriteLine("\n--- Спроба повернути книгу 1 користувачем 2 ---");
        Execute(library.TryReturnBook(2, 1, out message), message);

        Console.WriteLine("\n--- Повернення книги 1 користувачем 1 ---");
        Execute(library.TryReturnBook(1, 1, out message), message);

        Console.WriteLine("\n--- Повторна перевірка доступності книги 1 ---");
        Console.WriteLine(library.IsBookAvailable(1)
            ? "Книга 1 знову доступна."
            : "Книга 1 недоступна.");

        Console.WriteLine();
        library.PrintAvailableBooks();
        Console.WriteLine();
        library.PrintUsers();
    }

    static void Execute(bool success, string message)
    {
        Console.WriteLine((success ? "Успіх: " : "Помилка: ") + message);
    }
}