namespace CutMe.Communication;

public class SetRedirectRequest
{
    public string Shortcut { get; set; }
    public string FullUrl { get; set; }

    // To more simplify. It can be done with FluentValidation
    public bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(Shortcut) && !string.IsNullOrWhiteSpace(FullUrl);
    }
}