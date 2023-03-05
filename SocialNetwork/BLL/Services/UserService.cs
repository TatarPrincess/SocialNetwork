﻿using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Services;

public class UserService
{
    MessageService messageService;
    IUserRepository userRepository;
    public UserService()
    {
        userRepository = new UserRepository();
        messageService = new MessageService();
    }
    public void Register(UserRegistrationData userRegistrationData)
    {
        if (!new EmailAddressAttribute().IsValid(userRegistrationData.Email))
            throw new ArgumentNullException();

        if (String.IsNullOrEmpty(userRegistrationData.FirstName) ||
            String.IsNullOrEmpty(userRegistrationData.LastName) ||
            String.IsNullOrEmpty(userRegistrationData.Email) ||
            String.IsNullOrEmpty(userRegistrationData.Password) ||
            userRegistrationData.Password.Length < 8 ||
            !new EmailAddressAttribute().IsValid(userRegistrationData.Email) ||
            userRepository.FindByEmail(userRegistrationData.Email) is not null
            )
            throw new ArgumentNullException();

        UserEntity userEntity = new UserEntity()
        {
            firstname = userRegistrationData.FirstName,
            lastname = userRegistrationData.LastName,
            password = userRegistrationData.Password,
            email = userRegistrationData.Email
        };
        
        if (this.userRepository.Create(userEntity) == 0)
            throw new Exception();
    }
    public User Authenticate(UserAuthenticationData userAuthenticationData)
    {
        var findUserEntity = userRepository.FindByEmail(userAuthenticationData.Email);
        if (findUserEntity is null) throw new UserNotFoundException();

        if (findUserEntity.password != userAuthenticationData.Password)
            throw new WrongPasswordException();

        return ConstructUserModel(findUserEntity);
    }
    public User FindById(int userId)
    {
        var findUserEntity = userRepository.FindById(userId);
        if (findUserEntity is null) throw new UserNotFoundException();

        return ConstructUserModel(findUserEntity);
    }

    public User FindByEmail(string email)
    {
        var findUserEntity = userRepository.FindByEmail(email);
        if (findUserEntity is null) throw new UserNotFoundException();

        return ConstructUserModel(findUserEntity);
    }

    public void Update(User user)
    {
        var updatableUserEntity = new UserEntity()
        {
            id = user.Id,
            firstname = user.FirstName,
            lastname = user.LastName,
            password = user.Password,
            email = user.Email,
            photo = user.Photo,
            favorite_movie = user.FavoriteMovie,
            favorite_book = user.FavoriteBook
        };

        if (this.userRepository.Update(updatableUserEntity) == 0)
            throw new Exception();
    }

    private User ConstructUserModel(UserEntity userEntity)
    {
        var incomingMessages = messageService.GetIncomingMessagesByUserId(userEntity.id);

        var outgoingMessages = messageService.GetOutcomingMessagesByUserId(userEntity.id);

        return new User(userEntity.id,
                      userEntity.firstname,
                      userEntity.lastname,
                      userEntity.password,
                      userEntity.email,
                      userEntity.photo,
                      userEntity.favorite_movie,
                      userEntity.favorite_book,
                      incomingMessages,
                      outgoingMessages);
    }
}
