using Microsoft.VisualStudio.TestTools.UnitTesting;
using z01_LINQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using z99.Database.Models;
using ExpectedObjects;
using FluentAssertions;

namespace z01_LINQ.Tests
{
    [TestClass()]
    public class ExpressionTreeTests
    {
        [TestMethod()]
        public void ExpressionTreeQueryTest()
        {
            //arrange
            var target = new ExpressionTree();

            IQueryable<Products> expected1;
            IQueryable<Products> expected2;
            IQueryable<Products> actual;

            //act
            target.ExpressionTreeQuery(
                    out expected1, out expected2, out actual);

            //assert
            expected1.ToList().ToExpectedObject().ShouldEqual(actual.ToList());
            expected2.ToList().ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod()]
        public void ExpressionTreeReflectionTest()
        {
            //arrange
            var target = new ExpressionTree();

            long? reflection;
            long? dynamic;
            long? expression;

            //act
            target.ExpressionTreeReflection(
                    out reflection, out dynamic, out expression);

            Console.WriteLine(string.Format("reflection => {0}", reflection.Value));
            Console.WriteLine(string.Format("dynamic => {0}", dynamic.Value));
            Console.WriteLine(string.Format("expression => {0}", expression.Value));
            //assert
            reflection.Value.Should().BeGreaterThan(expression.Value);
            dynamic.Value.Should().BeGreaterThan(expression.Value);
        }
    }
}