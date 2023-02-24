using System;
using System.Collections.Generic;
using System.Linq;

namespace Antiban
{
    public class Antiban
    {
        private readonly int diffSameByPhone = 60;
        private readonly int diffNotSameByPhone = 10;
        private readonly int diffSamePriority = 86400;


        List<EventMessage> _events = new List<EventMessage>();

        /// <summary>
        /// Добавление сообщений в систему, для обработки порядка сообщений
        /// </summary>
        /// <param name="eventMessage"></param>
        public void PushEventMessage(EventMessage eventMessage)
        {
            if (_events.Count > 0)
            {
                var lastEventByPhone = _events.Where(e => e.Phone == eventMessage.Phone);
                if (!lastEventByPhone.Any())
                {
                    var lasteventNotSame = _events.Where(e => e.Phone != eventMessage.Phone);
                    EventMessage last;
                    if (lasteventNotSame.GroupBy(e => e.Phone).Count() > 1)
                    {
                        last = lasteventNotSame.GroupBy(e => e.Phone).Last().Last();
                    }
                    else
                    {
                        last = lasteventNotSame.First();
                    }
                    eventMessage.DateTime = diff(last.DateTime, eventMessage.DateTime) >= diffNotSameByPhone ?
                                            eventMessage.DateTime : last.DateTime.AddSeconds(diffNotSameByPhone);
                }
                else
                {
                    if (eventMessage.Priority == 1 && lastEventByPhone.Any(e => e.Priority == eventMessage.Priority))
                    {
                        var last = lastEventByPhone.Last(e => e.Priority == eventMessage.Priority);
                        eventMessage.DateTime = diff(last.DateTime, eventMessage.DateTime) >= diffSamePriority ?
                                                eventMessage.DateTime : last.DateTime.AddSeconds(diffSamePriority);
                    }
                    else
                    {
                        var last = lastEventByPhone.Where(e => e.DateTime <= eventMessage.DateTime).Last();
                        eventMessage.DateTime = diff(last.DateTime, eventMessage.DateTime) >= diffSameByPhone ?
                                                eventMessage.DateTime : last.DateTime.AddSeconds(diffSameByPhone);
                    }
                }
            }

            _events.AddSorted(eventMessage);
        }

        /// <summary>
        /// Вовзращает порядок отправок сообщений
        /// </summary>
        /// <returns></returns>
        public List<AntibanResult> GetResult()
        {
            return _events.Select(e => new AntibanResult(e.Id, e.DateTime)).ToList();
        }

        private int diff(DateTime prev, DateTime next)
        {
            return (int)(next - prev).TotalSeconds;
        }
    }
}
