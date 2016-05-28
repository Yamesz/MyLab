using Microsoft.VisualStudio.TestTools.UnitTesting;
using z02_Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace z02_Reflection.Tests
{
    [TestClass()]
    public class Class1Tests
    {
        [TestMethod()]
        public void ReflectionTest()
        {
            // arrange
            var target = new Class1();

            long? reflection;
            long? list;

            //act
            target.Reflection(
                    out reflection, out list);

            Console.WriteLine(string.Format("reflection => {0}", reflection.Value));
            Console.WriteLine(string.Format("list => {0}", list.Value));
            //assert
            
        }
    }
}