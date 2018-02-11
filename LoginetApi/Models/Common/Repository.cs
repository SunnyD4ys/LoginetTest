using System.Collections.Generic;
using System;
using System.Linq;
using System.Net;
using LoginetApi.Models.Common.Interfaces;

namespace LoginetApi.Models.Common
{
    //Словарик для кеширования, содержит дату добавления элемента
    public class Repository<Tkey, Tvalue> where Tvalue : IRepositoryItem<Tkey>
    {
        public class Entry
        {
            public DateTime AdditionDate { get; private set; }
            public Tvalue Value { get; private set; }

            public Entry(Tvalue value)
            {
                Value = value;
                AdditionDate = DateTime.Now;
            }
        }

        public static object syncRoot = new object();
        private Dictionary<Tkey, Entry> dictionary = new Dictionary<Tkey, Entry>();

        private DateTime lastUpdateDate;
        public DateTime LastUpdateDate
        {
            get { return lastUpdateDate; }
        }



        public Repository()
        {
            dictionary = new Dictionary<Tkey, Entry>();
        }

        public Entry GetEntry(Tkey key)
        {
            lock (syncRoot)
            {
                if (dictionary.ContainsKey(key))
                    return dictionary[key];
                return null;
            }
        }

        public int Count
        {
            get { return dictionary.Count; }
        }

        public IEnumerable<Tvalue> GetValues()
        {
            lock (syncRoot)
            {
                return dictionary.Values.Select(x => x.Value);
            }
        }

        public Tvalue this[Tkey key]
        {
            get
            {
                return GetEntry(key).Value;
            }
        }
        public void Add(IEnumerable<Tvalue> values)
        {
            lock (syncRoot)
            {
                foreach (var value in values)
                    add(value);
                lastUpdateDate = DateTime.Now;
            }
        }
        private void add(Tvalue value)
        {
            if (dictionary.ContainsKey(value.id))
                dictionary[value.id] = new Entry(value);
            else
                dictionary.Add(value.id, new Entry(value));
        }
        public void Add(Tvalue value) 
        {
            lock (syncRoot) 
            {
                add(value);
                lastUpdateDate = DateTime.Now;
            }
        }
    }
}