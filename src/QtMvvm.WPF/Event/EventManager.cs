using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QtMvvm.WPF.Event
{
    public static class EventManager
    {
        private static Dictionary<Type, EventBase> m_EventList = new Dictionary<Type, EventBase>();

        public static TEvent GetEvent<TEvent>() where TEvent : EventBase, new()
        {
            Type eventType = typeof(TEvent);
            TEvent @event = new TEvent();
            if (!m_EventList.ContainsKey(eventType))
            {
                m_EventList.Add(eventType, @event);
            }

            return (TEvent)m_EventList[eventType];
        }
    }
}
