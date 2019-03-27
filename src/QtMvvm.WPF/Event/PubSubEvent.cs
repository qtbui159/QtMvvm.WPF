using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QtMvvm.WPF.Event
{
    public abstract class PubSubEvent : EventBase
    {
        private static Dictionary<Type, HashSet<Action>> m_TypeMapEvent = new Dictionary<Type, HashSet<Action>>();

        public void Subscribe(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            Type eventType = this.GetType();

            if (!m_TypeMapEvent.ContainsKey(eventType))
            {
                m_TypeMapEvent.Add(eventType, new HashSet<Action>());
            }
            m_TypeMapEvent[eventType].Add(action);
        }

        public void Unsubscribe(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            Type eventType = this.GetType();

            if (!m_TypeMapEvent.ContainsKey(eventType))
            {
                return;
            }
            m_TypeMapEvent[eventType].Remove(action);
            if (m_TypeMapEvent[eventType].Count == 0)
            {
                m_TypeMapEvent.Remove(eventType);
            }
        }

        public void Publish()
        {
            Type eventType = this.GetType();

            if (!m_TypeMapEvent.ContainsKey(eventType))
            {
                return;
            }
            foreach (Action action in m_TypeMapEvent[eventType])
            {
                action.BeginInvoke(null, null);
            }
        }
    }

    public abstract class PubSubEvent<TPayload> : EventBase
    {
        private static Dictionary<Type, HashSet<Action<TPayload>>> m_TypeMapEvent = new Dictionary<Type, HashSet<Action<TPayload>>>();
        
        public void Subscribe(Action<TPayload> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            Type eventType = this.GetType();

            if (!m_TypeMapEvent.ContainsKey(eventType))
            {
                m_TypeMapEvent.Add(eventType, new HashSet<Action<TPayload>>());
            }
            m_TypeMapEvent[eventType].Add(action);
        }

        public void Unsubscribe(Action<TPayload> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            Type eventType = this.GetType();

            if (!m_TypeMapEvent.ContainsKey(eventType))
            {
                return;
            }
            m_TypeMapEvent[eventType].Remove(action);
            if (m_TypeMapEvent[eventType].Count == 0)
            {
                m_TypeMapEvent.Remove(eventType);
            }
        }

        public void Publish(TPayload payload)
        {
            Type eventType = this.GetType();

            if (!m_TypeMapEvent.ContainsKey(eventType))
            {
                return;
            }

            foreach (var action in m_TypeMapEvent[eventType])
            {
                Action wrap = new Action(() =>
                {
                    action.Invoke(payload);
                });

                wrap.BeginInvoke(null, null);
            }
        }
    }
}
