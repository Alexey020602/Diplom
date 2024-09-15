namespace Model;

public interface ISelectionWithTextElement
{
    string Text { get; set; }
    bool CanSelect => string.IsNullOrEmpty(Text);
    string NonInputDescription { get; }

    void Reset()
    {
        Text = string.Empty;
    }
}