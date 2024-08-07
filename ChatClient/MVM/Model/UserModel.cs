namespace SimpleChatAppWithoutDesign.MVM.Model;

public class UserModel
{
    public string Id { get; set; }
    
    public string UserName { get; set; }

    public string IUD { get; set; } = Guid.NewGuid().ToString();
}