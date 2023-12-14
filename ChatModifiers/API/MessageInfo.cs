using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatModifiers.API
{
    /// <summary>
    /// Represents information about a chat message.
    /// </summary>
    public class MessageInfo
    {
        /// <summary>
        /// Gets the content of the chat message.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Gets the author of the chat message.
        /// </summary>
        public string Author { get; }

        /// <summary>
        /// Gets the channel in which the chat message was sent.
        /// </summary>
        public string Channel { get; }

        /// <summary>
        /// Gets the timestamp when the chat message was sent.
        /// </summary>
        public DateTime TimeStamp { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageInfo"/> class.
        /// </summary>
        /// <param name="message">The content of the chat message.</param>
        /// <param name="author">The author of the chat message.</param>
        /// <param name="channel">The channel in which the chat message was sent.</param>
        /// <param name="timeStamp">The timestamp when the chat message was sent.</param>
        public MessageInfo(string message, string author, string channel, DateTime timeStamp)
        {
            Message = message;
            Author = author;
            Channel = channel;
            TimeStamp = timeStamp;
        }
    }
}
