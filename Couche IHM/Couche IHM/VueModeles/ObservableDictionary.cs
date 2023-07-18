using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;

namespace Couche_IHM.VueModeles
{

    /// <summary>
    /// Dictionaire pouvant être bind à une vue
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class ObservableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, INotifyCollectionChanged, INotifyPropertyChanged
    {
    
        private readonly IDictionary<TKey, TValue> baseDictionary;

        /// <summary>
        ///     Constructor
        /// </summary>
        public ObservableDictionary() 
        {
            this.baseDictionary = new Dictionary<TKey, TValue>();
        }

       
        #region base methods
     
        public void Clear()
        {
            baseDictionary.Clear();
            OnNotifyReset();
        }


        public void Add(TKey key, TValue value)
        {
            var item = new KeyValuePair<TKey, TValue>(key, value);
            baseDictionary.Add(item);
            OnNotifyAdd(item);
        }

        public bool ContainsKey(TKey key)
        {
            return baseDictionary.ContainsKey(key);
        }


        public bool Remove(TKey key)
        {
            TValue local = baseDictionary[key];
            bool flag = baseDictionary.Remove(key);
            OnNotifyReset();

            return flag;
        }

        public TValue this[TKey key]
        {
            get => baseDictionary[key];
            set
            {
                if (baseDictionary.ContainsKey(key))
                {
                    TValue originalValue = baseDictionary[key];
                    baseDictionary[key] = value;
                    OnNotifyReset();
                }
                else
                {
                    baseDictionary[key] = value;
                    OnNotifyAdd(new KeyValuePair<TKey, TValue>(key, value));
                }
            }
        }


        public ICollection<TKey> Keys
        {
            get => baseDictionary.Keys;
        }

        public ICollection<TValue> Values
        {
            get => baseDictionary.Values;
        }

        public int Count
        {
            get => baseDictionary.Count;
        }


        #endregion

        #region implementations

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            baseDictionary.Add(item);
            OnNotifyAdd(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            baseDictionary.CopyTo(array, arrayIndex);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            bool reussite = baseDictionary.Remove(item);
            if (reussite)
            {
                OnNotifyReset();
            }

            return reussite;
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            return baseDictionary.Contains(item);
        }

      
        public bool IsReadOnly
        {
            get => false;
        }


        public bool TryGetValue(TKey key, out TValue value)
        {
            return baseDictionary.TryGetValue(key, out value);
        }


     

        IEnumerator IEnumerable.GetEnumerator()
        {
            return baseDictionary.GetEnumerator();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return baseDictionary.GetEnumerator();
        }
        #endregion

        #region notify
        /// <summary>
        ///     Event raised for collection change notification
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <summary>
        ///     Event raise for property changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        ///     This is used to notify insertions into the dictionary.
        /// </summary>
        /// <param name="item">Item</param>
        protected void OnNotifyAdd(KeyValuePair<TKey, TValue> item)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(Count)));
            OnPropertyChanged(new PropertyChangedEventArgs(item.Key.ToString()));
        }



        /// <summary>
        ///     This is used to notify that the dictionary was completely reset.
        /// </summary>
        protected void OnNotifyReset()
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(Count)));
        }

        /// <summary>
        ///     Raises the <see cref="E:System.Collections.ObjectModel.ObservableCollection`1.CollectionChanged" /> event with the
        ///     provided arguments.
        /// </summary>
        /// <param name="e">Arguments of the event being raised.</param>
        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the property change notification
        /// </summary>
        /// <param name="e">Property event args.</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
        #endregion

    }
}