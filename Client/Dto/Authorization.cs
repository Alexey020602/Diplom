namespace Client.Dto;

public class Authorization
{
    public string Token { get; set; }
    public List<string> Roles { get; set; }
}