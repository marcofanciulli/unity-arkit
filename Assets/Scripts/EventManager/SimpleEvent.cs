using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace EventsManager
{
    internal class SimpleEvent : IEventMessage
    {
        internal UnityAction uEvent;
        public void Set(object item)
        {
            if (item is UnityAction) { uEvent = (UnityAction)item; }
        }

        public void Invoke(object item)
        {
            uEvent.Invoke();
        }

        public bool Compare(object item)
        {
            if (!(item is UnityAction)) return false;
            if (uEvent == null) return false;
            return ((UnityAction)item).Equals(uEvent);
        }

        public bool CompareParametrType(object item)
        {
            if (item is NullParametr) return true;
            return false;
        }
    }
}