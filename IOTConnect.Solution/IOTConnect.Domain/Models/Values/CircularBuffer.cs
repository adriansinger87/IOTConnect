using System;
using System.Collections.Generic;
using System.Text;

namespace IOTConnect.Domain.Models.Values
{
    public class CircularBuffer<T>
    {
        // -- fields

        Queue<T> _queue;
        int _size;

        // -- constructor

        public CircularBuffer(int size)
        {
            _queue = new Queue<T>(size);
            _size = size;
        }

        // -- methods

        /// <summary>
        /// Fügt ein neues Element am Ende des Ringpuffers hinzu
        /// </summary>
        /// <param name="obj"></param>
        public void Add(T obj)
        {
            if (_queue.Count == _size)
            {
                _queue.Dequeue();
                _queue.Enqueue(obj);
            }
            else
            {
                _queue.Enqueue(obj);
            }
        }

        /// <summary>
        /// Liest das erste Element aus und entfernt es.
        /// </summary>
        /// <returns></returns>
        public T Read()
        {
            return _queue.Dequeue();
        }

        public override string ToString()
        {
            return $"{Count} items of type {typeof(T).Name}";
        }

        // -- properties

        /// <summary>
        /// Gibt die Anzahl gespeicherter Elemente aus. 
        /// </summary>
        public int Count { get { return _queue.Count; } }

        /// <summary>
        /// Liest das erste Element ohne es zu entfernen.
        /// </summary>
        /// <returns>das erste Element</returns>
        public T Peek { get { return _queue.Count > 0 ? _queue.Peek() : default(T); } }
    }
}
