using System.Collections.Generic;

public class User
{
    private readonly List<Book> _borrowedBooks = new List<Book>();

    public int Id { get; }
    public string Name { get; }
    public IReadOnlyList<Book> BorrowedBooks => _borrowedBooks.AsReadOnly();

    public User(int id, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Ім'я користувача не може бути порожнім.");

        Id = id;
        Name = name;
    }

    public bool HasBook(Book book)
    {
        return _borrowedBooks.Contains(book);
    }

    internal void AddBook(Book book)
    {
        _borrowedBooks.Add(book);
    }

    internal void RemoveBook(Book book)
    {
        _borrowedBooks.Remove(book);
    }

    public override string ToString()
    {
        if (_borrowedBooks.Count == 0)
            return $"[{Id}] {Name} - книг немає";

        string titles = string.Join(", ", _borrowedBooks.Select(b => $"\"{b.Title}\""));
        return $"[{Id}] {Name} - орендовано: {titles}";
    }
}