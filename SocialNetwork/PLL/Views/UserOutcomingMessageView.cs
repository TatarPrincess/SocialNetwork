using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.PLL.Views;

public class UserOutcomingMessageView
{
    public UserService userService;
    public UserOutcomingMessageView(UserService userservice)
    {
        this.userService = userservice;
    }
    public void Show(IEnumerable<Message> outcomingMessages)
    {
        Console.WriteLine("Исходящие сообщения");

        if (outcomingMessages.Count() == 0)
        {
            Console.WriteLine("Исходящих сообщений нет");
            return;
        }

        outcomingMessages.ToList().ForEach(message =>
        {
            User recipient = userService.FindByEmail(message.RecipientEmail);
            Console.WriteLine("Кому: {0}. Текст сообщения: {1}", recipient.FirstName + ' ' + recipient.LastName, message.Content);
        });
    }
}
