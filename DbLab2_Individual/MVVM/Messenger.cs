using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLab2_Individual.MVVM
{
    public class Messenger
    {
        private Dictionary<Type, Action<object>> handlers = new();

        public void Receive(Type type, Action<object> message)
        {
            if (handlers.ContainsKey(type))
            {
                handlers.Remove(type);
            }

            handlers.Add(type, message);
        }

        public void Send<T>(object message)
        {
            handlers[typeof(T)].Invoke(message);
        }
    }
}
