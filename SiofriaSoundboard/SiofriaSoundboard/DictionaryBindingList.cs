using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiofriaSoundboard
{
    //Credit to S. Bleier
    //https://stackoverflow.com/questions/854953/datagridview-bound-to-a-dictionary

    public class DictionaryBindingList<TKey, TValue> : BindingList<KeyValuePair<TKey, TValue>>
    {
        public readonly IDictionary<TKey, TValue> Dictionary;

        public DictionaryBindingList()
        {
            Dictionary = new Dictionary<TKey, TValue>();
        }

        //Credit to David Rath from the same SO thread
        public void Add(TKey key, TValue value)
        {
            if (Dictionary.ContainsKey(key))
            {
                int position = IndexOf(key);
                Dictionary.Remove(key);
                Remove(key);
                InsertItem(position, new KeyValuePair<TKey, TValue>(key, value));
                return;
            }
            base.Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        public void Remove(TKey key)
        {
            var item = this.First(x => x.Key.Equals(key));
            base.Remove(item);
        }

        protected override void InsertItem(int index, KeyValuePair<TKey, TValue> item)
        {
            Dictionary.Add(item.Key, item.Value);
            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            Dictionary.Remove(this[index].Key);
            base.RemoveItem(index);
        }

        public int IndexOf(TKey key)
        {
            var item = this.FirstOrDefault(x => x.Key.Equals(key));
            return item.Equals(null) ? -1 : base.IndexOf(item);
        }

        public bool ContainsKey(TKey key)
        {
            return IndexOf(key) != -1;
        }

        public TValue GetValue(TKey key)
        {
            return base[IndexOf(key)].Value;
        }

        public Dictionary<TKey, TValue> GetInternalData()
        {
            return (Dictionary<TKey, TValue>)Dictionary;
        }
    }
}
