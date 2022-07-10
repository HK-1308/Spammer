using MailKit.Net.Smtp;
using MimeKit;

namespace TestTask.Services.Data
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public string CsvString { get; set; }

        public Message(IEnumerable<string> to, string subject, string content, string csvString)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress("SpaMMMer",x)));
            Subject = subject;
            Content = content;
            CsvString = csvString;
        }
    }
}
