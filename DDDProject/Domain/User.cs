namespace DDDProject.Domain;

public class User
{
    private User(string userName, string password)
    {
        Id = Guid.NewGuid();
        UserName = userName;
        Password = password;
    }
    public Guid Id {get; private set;}
    public string UserName {get; private set;}
    public string Password {get; private set;}

    public static User Create(string userName, string password) => new User(userName, password);
}