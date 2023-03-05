using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.PLL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.PLL.Views;

public class FriendAdditionView
{
    FriendService friendService;
    public FriendAdditionView(FriendService friendService)
    {
        this.friendService = friendService;
    }

    public void Show(User user)
    {
        var friendAdditionDate = new FriendAdditionData();
        friendAdditionDate.UserId = user.Id; 

        Console.WriteLine("Для добавления в друзья введите email друга:");
        friendAdditionDate.FriendEmail = Console.ReadLine();    
        

        try
        {
            friendService.AddNewFriend(friendAdditionDate);

            SuccessMessage.Show("Ваш новый друг успешно добавлен.");
        }

        catch (UserNotFoundException)
        {
            AlertMessage.Show("Вы пытаетесь добавить друга, который не зарегистрирован в социальной сети.");
        }

        catch (Exception)
        {
            AlertMessage.Show("Произошла ошибка при добавлении в друзья.");
        }
    }
}
