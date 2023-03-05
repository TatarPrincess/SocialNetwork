using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.PLL.Views;

public class UserIncomingMessageView
{
    public UserService userService;
    public UserIncomingMessageView(UserService userservice)
    { 
       this.userService = userservice;
    }
    public void Show(IEnumerable<Message> incomingMessages)
    {
        Console.WriteLine("Входящие сообщения");


        if (incomingMessages.Count() == 0)
        {
            Console.WriteLine("Входящих сообщения нет");
            return;
        }
        
        incomingMessages.ToList().ForEach(message =>
        {
            User sender = userService.FindByEmail(message.SenderEmail);
            Console.WriteLine("От кого: {0}. Текст сообщения: {1}", sender.FirstName + ' ' + sender.LastName, message.Content);
        });
    }
}
