using System;

namespace Antiban
{
    public class Application
    {
        public static void Main(string[] args)
        {
            int day = 1;
            int month = 3;
            int year = 2022;

            string[] phones = {
                "77070001101", "77070001102", "77070001103", "77070001104", "77070001105", "77070001106", "77070001107"
            };

            var antiban = new Antiban();

            antiban.PushEventMessage(new EventMessage(1, phones[0], new DateTime(year, month, day, 12, 0, 0), 0));
            antiban.PushEventMessage(new EventMessage(2, phones[0], new DateTime(year, month, day, 12, 0, 1), 1));
            antiban.PushEventMessage(new EventMessage(3, phones[0], new DateTime(year, month, day, 12, 0, 2), 1));
            antiban.PushEventMessage(new EventMessage(4, phones[2], new DateTime(year, month, day, 12, 0, 2, 1), 0));
            antiban.PushEventMessage(new EventMessage(5, phones[1], new DateTime(year, month, day, 12, 0, 3), 0));

            foreach (AntibanResult item in antiban.GetResult())
            {
                Console.WriteLine($"{item.EventMessageId}, {item.SentDateTime}");
            }
        }
    }
}