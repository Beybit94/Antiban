using System;
using System.Collections.Generic;

namespace Antiban
{
    public static class ListExt
    {
        public static void AddSorted<T>(this List<T> @this, T item) where T : IComparable<T>
        {
            if (@this.Count == 0)
            {
                @this.Add(item);
                return;
            }
            if (@this[@this.Count - 1].CompareTo(item) <= 0)
            {
                @this.Add(item);
                return;
            }
            if (@this[0].CompareTo(item) >= 0)
            {
                @this.Insert(0, item);
                return;
            }
            int index = @this.BinarySearch(item);
            if (index < 0)
                index = ~index;
            @this.Insert(index, item);
        }
    }

    public class EventMessage : IComparable<EventMessage>
    {
        public int Id { get; set; }
        /// <summary>
        /// Номер телефона
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// Время возникновения события
        /// </summary>
        public DateTime DateTime { get; set; }
        /// <summary>
        /// Приоритет сообщения
        /// 0 - сервисные
        /// 1 - рассылки
        /// </summary>
        public int Priority { get; set; }
        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Срок жизни сообщения
        /// </summary>
        public DateTime ExpireDateTime { get; set; }

        public EventMessage(int id, string phone, DateTime dateTime, int priority)
        {
            Id = id;
            Phone = phone;
            DateTime = dateTime;
            Priority = priority;
            Text = "Something";
            ExpireDateTime = priority == 0 ? dateTime.AddHours(1) : dateTime.AddDays(1);
        }

        public int CompareTo(EventMessage? other)
        {
            return this.DateTime.CompareTo(other.DateTime);
        }
    }
}
