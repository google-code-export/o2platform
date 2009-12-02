using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Evaluant.NLinq.Memory
{
    /// <summary>
    /// DC Change , made this an IEnumerable so that we can access the data stored in here
    /// </summary>
    public class Variant : IDictionary<string, object>
    {
        public Dictionary<string, object> members = new Dictionary<string, object>();

        public object this[string propertyName]
        {
            get { return members[propertyName]; }
            set { members[propertyName] = value; }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("{ ");
            foreach (KeyValuePair<string, object> entry in members)
            {
                sb.Append(entry.Key).Append(" = ").Append(entry.Value.ToString()).Append("; ");
            }
            sb.Append(" }");

            return sb.ToString();
        }


        #region IDictionary<string,object> Members

        public void Add(string key, object value)
        {
            members.Add(key,value);
        }

        public bool ContainsKey(string key)
        {
            return members.ContainsKey(key);
        }

        public ICollection<string> Keys
        {
            get { return members.Keys; }
        }

        public bool Remove(string key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(string key, out object value)
        {
            throw new NotImplementedException();
        }

        public ICollection<object> Values
        {
            get { return members.Values; }
        }

        #endregion

        #region ICollection<KeyValuePair<string,object>> Members

        public void Add(KeyValuePair<string, object> item)
        {
            members.Add(item.Key,item.Value);
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return members.Count; }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable<KeyValuePair<string,object>> Members

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return members.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
           return members.GetEnumerator();
        }

        #endregion
    }
}
