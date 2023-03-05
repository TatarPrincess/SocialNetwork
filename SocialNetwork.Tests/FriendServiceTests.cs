using NUnit.Framework;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;


namespace SocialNetwork.Tests;

[TestFixture]
public class FriendServiceTests
{
    [Test]
    public void NewFriendMustBeSuccessfullyAdded()
    {
        var friendService = new FriendService();

        FriendAdditionData friendAdditionData = new FriendAdditionData
        {
            FriendEmail = "antony@gmail.com",
            UserId = 3
        };

        Assert.DoesNotThrow(() => friendService.AddNewFriend(friendAdditionData));
    }
}