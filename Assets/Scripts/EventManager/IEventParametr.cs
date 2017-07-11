namespace EventsManager
{
    interface IEventMessage
    {
        void Set(object item);
        void Invoke(object item);
        bool Compare(object item);
        bool CompareParametrType(object item);
    }
}