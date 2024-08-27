using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace L1_conccurent_programming
{
    class Rmonitor
    {
        private static readonly object lockObject = new object();
        private int n;
        private field[] store;

        public Rmonitor(int size)
        {
            n = 0;
            store = new field[size];
        }


        public field Get(int i) { return store[i]; }
        /// <summary>
       
        public int GetSize() { return n; }
       



        public void Add(field a)
        {
            lock (lockObject)
            {
                int insertionIndex = n;

                while (insertionIndex > 0 && a.CompareTo(store[insertionIndex - 1]) > 0)
                {
                    store[insertionIndex] = store[insertionIndex - 1];
                    insertionIndex--;
                }
 
                store[insertionIndex] = a;
                n++;
            }
        }



    }

}