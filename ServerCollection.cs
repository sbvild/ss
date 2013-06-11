using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ss
{
    class ServerCollection : IList
    {
        protected ArrayList Items;

        protected  void SaveState(string path)
        {
            //
            //XMLファイルに保存する
            System.Xml.Serialization.XmlSerializer serializer1 =
                new System.Xml.Serialization.XmlSerializer(typeof(Server[]));
            System.IO.FileStream fs1 =
                new System.IO.FileStream(path, System.IO.FileMode.Create);
            serializer1.Serialize(fs1, Items.ToArray(typeof(Server)));
            fs1.Close();
        }

        protected void RestoreState(string path)
        {
            //
            //保存した内容を復元する
            System.Xml.Serialization.XmlSerializer serializer2 =
                new System.Xml.Serialization.XmlSerializer(typeof(Server[]));
            System.IO.FileStream fs2 =
                new System.IO.FileStream(path, System.IO.FileMode.Open);

            Items.AddRange((Server[])serializer2.Deserialize(fs2));
            fs2.Close();
        }

        public ServerCollection()
        {
            //
            this.Items = new ArrayList();
        }

        public object this[int i]
        {
            get { return Items[i]; }
            set { Items[i] = value; }
        }

        public void RemoveAt(int i)
        {
            Items.RemoveAt(i);
        }

        public void Remove(object o)
        {
            Items.Remove(o);
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool IsFixedSize
        {
            get { return false; }
        }

        public void Insert(int i, object o)
        {
            Items.Insert(i, o);
        }

        public int IndexOf(object o)
        {
            return Items.IndexOf(o);
        }

        public bool Contains(object o)
        {
            return Items.Contains(o);
        }

        public void Clear()
        {
            Items.Clear();
        }

        public IEnumerator GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        public int Add(object o)
        {
            return Items.Add(o);
        }

        public void AddRange(ICollection c)
        {
            Items.AddRange(c);
        }

        public object SyncRoot
        {
            get { return Items.SyncRoot; }
        }

        public bool IsSynchronized
        {
            get { return Items.IsSynchronized; }
        }

        public int Count
        {
            get { return Items.Count; }
        }

        public void CopyTo(Array a, int i)
        {
            Items.CopyTo(a, i);
        }

    }

    class BlueServerCollection : ServerCollection
    {
        //
        private readonly string path = @"serverList2.xml";

        public void SaveState()
        {
            //
            this.SaveState(path);
        }

        public void RestoreState()
        {
            //
            this.RestoreState(path);
        }

    }

    class WhiteServerCollection : ServerCollection
    {
        //
        private readonly string path = @"serverList1.xml";

        public void SaveState()
        {
            //
            this.SaveState(path);
        }
        public void RestoreState()
        {
            //
            this.RestoreState(path);
        }

    }
}
