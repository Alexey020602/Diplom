namespace Model;

public interface ISelectionWithTextElement
{
    string Text { get; set; }
    void Reset() => Text = string.Empty;
    bool CanSelect => string.IsNullOrEmpty(Text);
    string NonInputDescription { get; }
}
