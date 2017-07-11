using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace EventsManager
{
    internal class ParametrEvent<T> : IEventMessage
    {
        UnityAction<T> uEvent;
        public void Set(object item)
        {
            if (item is UnityAction<T>) { uEvent = (UnityAction<T>)item; }
        }

        public void Invoke(object item)
        {
            if (item is T) { uEvent.Invoke((T)item); }
        }

        public bool Compare(object item)
        {
            if (!(item is UnityAction<T>)) return false;
            if (uEvent == null) return false;
            return ((UnityAction<T>)item).Equals(uEvent);
        }

        public bool CompareParametrType(object item)
        {
            if (!(item is T)) return false;
            return true;
        }
    }
}
