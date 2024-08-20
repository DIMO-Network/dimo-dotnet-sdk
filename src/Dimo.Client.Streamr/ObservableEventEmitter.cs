using System;
using System.Collections.Generic;
using System.Reactive.Subjects;

namespace Dimo.Client.Streamr
{
    public interface IEvent {}
    
    public class ObservableEventEmitter<T> : IObservable<IEvent> where T : class 
    {
        private readonly Subject<IEvent> _subject = new Subject<IEvent>();
        private readonly Dictionary<string, List<Action<object>>> _eventHandlers = new Dictionary<string, List<Action<object>>>();
        
        public IDisposable Subscribe(IObserver<IEvent> observer)
        {
            return _subject.Subscribe(observer);
        }
        
        public void On(string eventName, Action<T> handler)
        {
            if (!_eventHandlers.ContainsKey(eventName))
            {
                _eventHandlers[eventName] = new List<Action<object>>();
            }
            
            _eventHandlers[eventName].Add(obj => handler(obj as T));
        }
        
        public void Once(string eventName, Action<T> handler)
        {
            Action<object> onceHandler = null;
            onceHandler = obj =>
            {
                handler(obj as T);
                _eventHandlers[eventName].Remove(onceHandler);
            };
            
            if (!_eventHandlers.ContainsKey(eventName))
            {
                _eventHandlers[eventName] = new List<Action<object>>();
            }
            
            _eventHandlers[eventName].Add(onceHandler);
        }
        
        public void Off(string eventName, Action<T> handler)
        {
            if (!_eventHandlers.TryGetValue(eventName, out var eventHandler))
            {
                return;
            }
            
            eventHandler.Remove(obj => handler(obj as T));
        }
        
        public void Emit(string eventName, T data)
        {
            if (!_eventHandlers.TryGetValue(eventName, out var eventHandler))
            {
                return;
            }
            
            foreach (var handler in eventHandler)
            {
                handler(data);
            }
        }
        
        public void RemoveAllHandlers(string eventName)
        {
            _eventHandlers.Remove(eventName);
        }
        
        public int GetHandlersCount(string eventName)
        {
            if (!_eventHandlers.TryGetValue(eventName, out var eventHandler))
            {
                return 0;
            }
            
            return eventHandler.Count;
        }
    }

 
}