using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using z02_Reflection.Infrastructure;

namespace z02_Reflection
{
    public class Class1
    {
        private int _iteration = 10000;
        public void Reflection(
           out long? data1,
           out long? data2)
        {
            data1 = ReflectionGet();
            data2 = ListGet();
        }
        private long? ReflectionGet()
        {
         
            Stopwatch watch = new Stopwatch();
            watch.Start();

            for (int i = 0; i < this._iteration; i++)
            {
                //調用方法
                var result = ReturnStatusEnum.ReflectionGet();
            }
            watch.Stop();
            return watch.ElapsedTicks;
        }

        private long? ListGet()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < this._iteration; i++)
            {
                var result = ReturnStatusEnum.ListGet();
            }
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }
    }
}
