namespace DTOWithSRM.Filters;

public class PersonFilter : BaseFilter
{
    public int Age { get; set; }

    public string Email { get; set; } = null!;
    
}