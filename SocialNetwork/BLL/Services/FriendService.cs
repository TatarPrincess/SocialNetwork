using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SocialNetwork.BLL.Services;

public class FriendService
{
    IFriendRepository friendRepository;  
    IUserRepository userRepository;
    public FriendService()
    {
        friendRepository = new FriendRepository();
        userRepository = new UserRepository();
    }
    public void AddNewFriend(FriendAdditionData newFriendData)
    {
        if (String.IsNullOrEmpty(newFriendData.FriendEmail) ||
            !new EmailAddressAttribute().IsValid(newFriendData.FriendEmail)
            ) throw new ArgumentNullException();

        UserEntity userEntity = userRepository.FindByEmail(newFriendData.FriendEmail);
        if (userEntity is null) throw new UserNotFoundException();


        FriendEntity friendEntity = new FriendEntity()
        {
            user_id = newFriendData.UserId,
            friend_id = userEntity.id
        };

        if (friendRepository.Create(friendEntity) == 0)
            throw new Exception();
    }    
}
