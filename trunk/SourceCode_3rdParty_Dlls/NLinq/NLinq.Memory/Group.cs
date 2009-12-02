using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Evaluant.NLinq.Memory
{
    public class NLinqGroup : IEnumerable
    {
        public NLinqGroup(object key)
        {
            this.key = key;
        }

        private object key;

        public object Key
        {
            get { return key; }
            set { key = value; }
        }

        private List<object> group = new List<object>();

        public List<object> Group
        {
            get { return group; }
            set { group = value; }
        }

        public int Count()
        {
            return group.Count;
        }

        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            return group.GetEnumerator();
        }

        #endregion
    }
}
