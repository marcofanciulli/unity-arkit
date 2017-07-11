using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System;

namespace EventsManager {  
    internal class NullParametr { };
    public class EventManager<T> where T : struct, IComparable, IConvertible, IFormattable
    {

        private Dictionary<string, List<IEventMessage>> eventDictionary;

        private static EventManager<T> eventManager;

        public static EventManager<T> Instance
        {
            get
            {
                if (eventManager == null)
                {
                    eventManager = new EventManager<T>();
                    eventManager.Init();
                }
                return eventManager;
            }
        }

        private void Init()
        {
            if (eventDictionary == null)
            {
                eventDictionary = new Dictionary<string, List<IEventMessage>>();
            }
        }

        public void StartListening(T eventName, UnityAction listener, int? id = null)
        {
            SimpleEvent simpleEvent = new SimpleEvent();
            simpleEvent.Set(listener);
            AddEventMessage(eventName, simpleEvent, id);
        }

        public void StartListening<U>(T eventName, UnityAction<U> listener, int? id = null)
        {
            ParametrEvent<U> parametrEvent = new ParametrEvent<U>();
            parametrEvent.Set(listener);
            AddEventMessage(eventName, parametrEvent, id);
        }


        private void AddEventMessage(T eventName, IEventMessage parametrEvent, int? id = null)
        {
            List<IEventMessage> eventList = null;
            string value = eventName.ToString();
            value = id != null ? value + id.ToString() : value;
            if (Instance.eventDictionary.TryGetValue(value, out eventList))
            {
                eventList.Add(parametrEvent);
            }
            else
            {
                eventList = new List<IEventMessage>();
                eventList.Add(parametrEvent);
                Instance.eventDictionary.Add(value, eventList);
            }
        }


        public void StopListening(T eventName, UnityAction listener, int? id = null)
        {
            RemoveEventMessage(eventName, listener, id);
        }

        public void StopListening<U>(T eventName, UnityAction<U> listener, int? id = null)
        {
            RemoveEventMessage(eventName, listener, id);
        }

        private void RemoveEventMessage(T eventName, object listener, int? id = null)
        {
            if (eventManager == null) return;
            List<IEventMessage> eventList = null;
            string value = eventName.ToString();
            value = id != null ? value + id.ToString() : value;
            if (Instance.eventDictionary.TryGetValue(value, out eventList))
            {
                for (int i = eventList.Count - 1; i >= 0; i--)
                {

                    if (eventList[i].Compare(listener))
                        eventList.RemoveAt(i);
                }
            }
        }

        public void TriggerEvent(T eventName)
        {
            TriggerEvent<object>(eventName, new NullParametr(), null);
        }

        public void TriggerEvent(T eventName, int id)
        {
            TriggerEvent<object>(eventName, new NullParametr(), id);
        }

        public void TriggerEvent<U>(T eventName, U parametr, int? id = null)
        {
            List<IEventMessage> eventList = null;
            string value = eventName.ToString();
            value = id != null ? value + id.ToString() : value;
            if (Instance.eventDictionary.TryGetValue(value, out eventList))
            {
                for (int i = eventList.Count - 1; i >= 0; i--)
                {
                    if (eventList[i].CompareParametrType(parametr)) eventList[i].Invoke(parametr);
                }
            }
        }
    }
}