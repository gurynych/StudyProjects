using System;
using System.Collections.Generic;

namespace DB_Lab1.MVVM
{
    public class Messenger
    {
        private Dictionary<Type, Action<object>> handlers =
                new Dictionary<Type, Action<object>>();

        public void Receive(Type type, Action<object> message)
        {            
            handlers.Add(type, message);
        }

        public void Send<T>(object message)
        {
            handlers[typeof(T)].Invoke(message);
        }
    }
}
