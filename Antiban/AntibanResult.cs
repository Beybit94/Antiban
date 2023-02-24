using System;

namespace Antiban
{
    public class AntibanResult : IComparable<AntibanResult>
    {
        /// <summary>
        /// Предполагаемое время отправки сообщения
        /// </summary>
        public DateTime SentDateTime { get; set; }
        public int EventMessageId { get; set; }

        public AntibanResult(int eventId, DateTime sentDateTime)
        {
            this.EventMessageId = eventId;
            this.SentDateTime = sentDateTime;
        }

        public int CompareTo(AntibanResult? other)
        {
            return this.SentDateTime.CompareTo(other.SentDateTime);
        }
    }
}
