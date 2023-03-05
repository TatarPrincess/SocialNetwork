using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.PLL.Views;

public class MessageSendingView
{
    UserService userService;
    MessageService messageService;
    public MessageSendingView(MessageService messageService, UserService userService)
    {
        this.messageService = messageService;
        this.userService = userService;
    }
    public void Show(User user)
    {
        MessageSendingData message = new MessageSendingData();
        message.sender_id = user.Id;

        Console.WriteLine("Введите email получателя");
        message.RecipientEmail = Console.ReadLine();

        Console.WriteLine("Введите текст сообщения (не больше 5000 символов)");
        message.content = Console.ReadLine();

        try
        {
            messageService.Send(message);
            SuccessMessage.Show("Сообщение успешно отправлено!");
        }
        catch (UserNotFoundException)
        {
            AlertMessage.Show("Пользователь не найден!");
        }

        catch (ArgumentNullException)
        {
            AlertMessage.Show("Введите корректное значение!");
        }

        catch (Exception)
        {
            AlertMessage.Show("Произошла ошибка при отправке сообщения!");
        }

    }
}
