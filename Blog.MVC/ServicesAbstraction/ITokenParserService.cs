namespace Blog.MVC.ServicesAbstraction
{
    public interface ITokenParserService
    {
        string? GetUserName();
        string? GetRole();
        bool IsLoggedIn();
    }
}
