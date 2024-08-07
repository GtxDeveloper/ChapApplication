using Microsoft.EntityFrameworkCore;
using SimpleChatAppWithoutDesign.MVM.Model;

namespace SimpleChatAppWithoutDesign.MVM.Controllers;

public class ChatDbController
{
    public DbSet<UserModel> Users { get; set; }
    
    public DbSet<MessageModel> Messages { get; set; }
    
    public DbSet<ChatModel> Chats { get; set; }
}