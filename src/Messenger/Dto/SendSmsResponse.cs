using System.Collections.Generic;

namespace Messenger.Dto;

public class SendSmsResponse
{
    public int Status;
    
    public Dictionary<string, string> Headers;
    
    public string Content;
}