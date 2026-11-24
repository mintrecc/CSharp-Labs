public class Book
{
    public int Id { get; }
    public string Title { get; }
    public string Author { get; }
    public User Borrower { get; private set; }

    public bool IsAvailable => Borrower == null;

    public Book(int id, string title, string author)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Назва книги не може бути порожньою.");
        if (string.IsNullOrWhiteSpace(author))
            throw new ArgumentException("Автор не може бути порожнім.");

        Id = id;
        Title = title;
        Author = author;
    }

    internal void Lend(User user)
    {
        Borrower = user;
    }

    internal void Receive()
    {
        Borrower = null;
    }

    public override string ToString()
    {
        string status = IsAvailable ? "доступна" : $"в оренді ({Borrower.Name})";
        return $"[{Id}] \"{Title}\", {Author} - {status}";
    }
}