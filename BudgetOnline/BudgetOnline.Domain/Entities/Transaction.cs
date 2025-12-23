namespace BudgetOnline.Domain.Entities;

public class Transaction
{
    public Guid Id { get; private set; }
    public decimal Amount { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public DateTime Date { get; private set; }
    public Guid CategoryId { get; private set; }

    private Transaction() { }

    public Transaction(Guid id, decimal amount, string description, DateTime date, Guid categoryId)
    {
        if(amount <= 0)
            throw new ArgumentException("Amount must be greater than zero.", nameof(amount));

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty.", nameof(description));

        Id = id;
        Amount = amount;
        Description = description;
        Date = date;
        CategoryId = categoryId;
    }
}
