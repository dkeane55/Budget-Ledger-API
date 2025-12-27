namespace BudgetOnline.Domain.Entities;

public class Category
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public decimal Budget { get; private set; }

    private Category() { }

    public Category(Guid id, string name, decimal budget = 0)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Category name cannot be empty", nameof(name));

        Id = id;
        Name = name;
        Budget = budget;
    }
}
