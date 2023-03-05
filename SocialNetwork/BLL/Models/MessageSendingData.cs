using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Models;

public class MessageSendingData
{
    public string RecipientEmail { get; set; }
    public string content { get; set; }
    public int sender_id { get; set; }
}
