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

public class MessageService
{
    IMessageRepository messageRepository;
    IUserRepository userRepository;
    public MessageService()
    {
        messageRepository = new MessageRepository();
        userRepository = new UserRepository();
    }
    public void Send(MessageSendingData message)
    {
        UserEntity userEntity = userRepository.FindByEmail(message.RecipientEmail);
        if (userEntity is null) throw new UserNotFoundException();

        if ((message.content.Length > 5000) ||
            String.IsNullOrEmpty(message.content) ||
            String.IsNullOrEmpty(message.RecipientEmail) ||
            !new EmailAddressAttribute().IsValid(message.RecipientEmail)
            )                   throw new ArgumentNullException();

        MessageEntity messageEntity = new MessageEntity()
        {
            content = message.content,
            sender_id = message.sender_id,
            recipient_id = userEntity.id
        };

        if (messageRepository.Create(messageEntity) == 0)
                                throw new Exception();
    }
    public IEnumerable<Message> GetIncomingMessagesByUserId(int recipientId)
    {
        var messages = new List<Message>();

        messageRepository.FindByRecipientId(recipientId).ToList().ForEach(m =>
        {
            var senderUserEntity = userRepository.FindById(m.sender_id);
            var recipientUserEntity = userRepository.FindById(m.recipient_id);

            messages.Add(new Message(m.id, m.content, senderUserEntity.email, recipientUserEntity.email));
        });

        return messages;
    }

    public IEnumerable<Message> GetOutcomingMessagesByUserId(int senderId)
    {
        var messages = new List<Message>();

        messageRepository.FindBySenderId(senderId).ToList().ForEach(m =>
        {
            var senderUserEntity = userRepository.FindById(m.sender_id);
            var recipientUserEntity = userRepository.FindById(m.recipient_id);

            messages.Add(new Message(m.id, m.content, senderUserEntity.email, recipientUserEntity.email));
        });

        return messages;
    }
}
