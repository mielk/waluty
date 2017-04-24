using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Stock.Utils;

namespace Stock_UnitTest.Stock.Utils.HelperMethods
{
    [TestClass]
    public class ContainerHelperMethodsUnitTest
    {

        [TestMethod]
        public void ScrambledEquals_ReturnsTrue_ForEmptyCollections()
        {
            IEnumerable<string> a = new List<string>();
            IEnumerable<string> b = new List<string>();
            bool areEqual = a.ScrambledEquals(b);
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void ScrambledEquals_ReturnsTrue_ForExactSameCollectionsOfPrimitives()
        {
            int[] a = new int[] { 1, 2, 3 };
            int[] b = new int[] { 1, 2, 3 };
            bool areEqual = a.ScrambledEquals(b);
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void ScrambledEquals_ReturnsTrue_ForExactSameCollectionsOfObjects()
        {
            List<object> a = new List<object>();
            List<object> b = new List<object>();

            object objA = new { Id = 1, Value = 2 };
            object objB = new { Id = 2, Value = 4 };

            a.Add(objA);
            a.Add(objB);

            b.Add(objA);
            b.Add(objB);

            bool areEqual = a.ScrambledEquals(b);
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void ScrambledEquals_ReturnsTrue_ForCollectionsWithSameItemsInAnotherOrder()
        {
            int[] a = new int[] { 1, 2, 3 };
            int[] b = new int[] { 3, 1, 2 };
            bool areEqual = a.ScrambledEquals(b);
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void ScrambledEquals_ReturnsFalse_IfOneCollectionHasDuplicatedItem()
        {
            int[] a = new int[] { 1, 2, 3 };
            int[] b = new int[] { 3, 1, 2, 1 };
            bool areEqual = a.ScrambledEquals(b);
            Assert.IsFalse(areEqual);
        }


    }
}