using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace L1_conccurent_programming
{
    class Dmonitor
    {
        private static readonly object signal = new object();
        
        const int MaxSize = 13;
        private  field[] store;
        private int counter = 0;
        private int count;



        public Dmonitor()
        {
            store = new field[MaxSize];
            count = 0;
        }

        public int Getcount()
        {
             return count;   
        }

        public field Removeitem()
        {

            lock (signal)
            {
                while (count == 0 && counter < 25)
                {
                    Monitor.Wait(signal);
                }
                if (count == 0 && counter >= 25)
                {
                    return null;                 // 25 terminate the process
                }
                field item = store[--count];
                Monitor.Pulse(signal);
                return item;
            }
        }
        

         public void AddItem(field r)
        {

            lock (signal)
            {
                while (count >= MaxSize)
                {
                    Monitor.Wait(signal);
                }
                store[count++] = r;
                counter++;
                Monitor.Pulse(signal);
            }


        }







    }







}

