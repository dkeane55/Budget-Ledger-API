namespace BudgetOnline.Domain.Entities;

public class Transaction
{
    public Guid Id { get; private set; }
    public decimal Amount { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public DateTime Date { get; private set; }
    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; } = null!;
    private Transaction() { }
    public void UpdateDetails(string description, decimal amount, DateTime date, Guid categoryId)
    {
        if (amount <= 0) throw new ArgumentException("Amount must be positive");
        if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Description required");
        if (categoryId == Guid.Empty) throw new ArgumentException("Valid Category required");

        Description = description;
        Amount = amount;
        Date = date;
        CategoryId = categoryId;
    }
    public Transaction(Guid id, decimal amount, string description, DateTime date, Guid categoryId)
    {
        if(amount <= 0)
            throw new ArgumentException("Amount must be greater than zero.", nameof(amount));

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty.", nameof(description));
        
        if (categoryId == Guid.Empty)
            throw new ArgumentException("Transaction must belong to a valid category.", nameof(categoryId));
        Id = id;
        Amount = amount;
        Description = description;
        Date = date;
        CategoryId = categoryId;
    }
}
