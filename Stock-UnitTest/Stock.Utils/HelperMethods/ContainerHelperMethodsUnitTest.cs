using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Stock.Utils;

namespace Stock_UnitTest.Stock.Utils.HelperMethods
{

    [TestClass]
    public class ContainerHelperMethodsUnitTest
    {

        #region HAS_THE_SAME_ITEMS

        [TestMethod]
        public void HasTheSameItems_ReturnsTrue_ForEmptyCollections()
        {
            IEnumerable<string> a = new List<string>();
            IEnumerable<string> b = new List<string>();
            bool areEqual = a.HasTheSameItems(b);
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void HasTheSameItems_ReturnsTrue_ForExactSameCollectionsOfPrimitives()
        {
            int[] a = new int[] { 1, 2, 3 };
            int[] b = new int[] { 1, 2, 3 };
            bool areEqual = a.HasTheSameItems(b);
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void HasTheSameItems_ReturnsTrue_ForExactSameCollectionsOfObjects()
        {

            //Arrange.
            List<ContainerUnitTestEntity> a = new List<ContainerUnitTestEntity>();
            List<ContainerUnitTestEntity> b = new List<ContainerUnitTestEntity>();

            ContainerUnitTestEntity objA = new ContainerUnitTestEntity { Id = 1, Value = "a" };
            ContainerUnitTestEntity objB = new ContainerUnitTestEntity { Id = 2, Value = "b" };

            //Act.
            a.Add(objA);
            a.Add(objB);

            b.Add(objA);
            b.Add(objB);

            //Assert.
            bool areEqual = a.HasTheSameItems(b);
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void HasTheSameItems_ReturnsFalse_IfComparedCollectionHasClonesOfObjects()
        {

            //Arrange.
            List<ContainerUnitTestEntity> a = new List<ContainerUnitTestEntity>();
            List<ContainerUnitTestEntity> b = new List<ContainerUnitTestEntity>();

            //Act.
            a.Add(new ContainerUnitTestEntity { Id = 1, Value = "a" });
            a.Add(new ContainerUnitTestEntity { Id = 2, Value = "b" });
            b.Add(new ContainerUnitTestEntity { Id = 1, Value = "a" });
            b.Add(new ContainerUnitTestEntity { Id = 2, Value = "b" });

            //Assert.
            bool areEqual = a.HasTheSameItems(b);
            Assert.IsFalse(areEqual);

        }

        [TestMethod]
        public void HasTheSameItems_ReturnsTrue_ForCollectionsWithSameItemsInAnotherOrder()
        {
            int[] a = new int[] { 1, 2, 3 };
            int[] b = new int[] { 3, 1, 2 };
            bool areEqual = a.HasTheSameItems(b);
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void HasTheSameItems_ReturnsFalse_IfOneCollectionHasDuplicatedItem()
        {
            int[] a = new int[] { 1, 2, 3 };
            int[] b = new int[] { 3, 1, 2, 1 };
            bool areEqual = a.HasTheSameItems(b);
            Assert.IsFalse(areEqual);
        }

        #endregion HAS_THE_SAME_ITEMS



        #region HAS_EQUAL_ITEMS

        [TestMethod]
        public void HasEqualItems_ReturnsTrue_ForEmptyCollections()
        {

            //Arrange.
            IEnumerable<string> a = new List<string>();
            IEnumerable<string> b = new List<string>();

            //Act.
            bool areEqual = a.HasEqualItems(b);

            //Assert.
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void HasEqualItems_ReturnsTrue_ForExactSameCollectionsOfObjects()
        {

            //Arrange.
            List<ContainerUnitTestEntity> a = new List<ContainerUnitTestEntity>();
            List<ContainerUnitTestEntity> b = new List<ContainerUnitTestEntity>();

            ContainerUnitTestEntity objA = new ContainerUnitTestEntity { Id = 1, Value = "a" };
            ContainerUnitTestEntity objB = new ContainerUnitTestEntity { Id = 2, Value = "b" };

            //Act.
            a.Add(objA);
            a.Add(objB);

            b.Add(objA);
            b.Add(objB);

            //Assert.
            bool areEqual = a.HasEqualItems(b);
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void HasEqualItems_ReturnsTrue_IfComparedCollectionHasClonesOfObjects()
        {

            //Arrange.
            List<ContainerUnitTestEntity> a = new List<ContainerUnitTestEntity>();
            List<ContainerUnitTestEntity> b = new List<ContainerUnitTestEntity>();

            //Act.
            a.Add(new ContainerUnitTestEntity { Id = 1, Value = "a" });
            a.Add(new ContainerUnitTestEntity { Id = 2, Value = "b" });
            b.Add(new ContainerUnitTestEntity { Id = 1, Value = "a" });
            b.Add(new ContainerUnitTestEntity { Id = 2, Value = "b" });

            //Assert.
            bool areEqual = a.HasEqualItems(b);
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void HasEqualItems_ReturnsTrue_ForCollectionsWithSameItemsInAnotherOrder()
        {

            //Arrange.
            List<ContainerUnitTestEntity> a = new List<ContainerUnitTestEntity>();
            List<ContainerUnitTestEntity> b = new List<ContainerUnitTestEntity>();

            //Act.
            a.Add(new ContainerUnitTestEntity { Id = 1, Value = "a" });
            a.Add(new ContainerUnitTestEntity { Id = 2, Value = "b" });
            a.Add(new ContainerUnitTestEntity { Id = 3, Value = "c" });
            b.Add(new ContainerUnitTestEntity { Id = 3, Value = "c" });
            b.Add(new ContainerUnitTestEntity { Id = 2, Value = "b" });
            b.Add(new ContainerUnitTestEntity { Id = 1, Value = "a" });

            //Assert.
            bool areEqual = a.HasEqualItems(b);
            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void HasEqualItems_ReturnsFalse_IfOneCollectionHasDuplicatedItem()
        {

            //Arrange.
            List<ContainerUnitTestEntity> a = new List<ContainerUnitTestEntity>();
            List<ContainerUnitTestEntity> b = new List<ContainerUnitTestEntity>();

            //Act.
            a.Add(new ContainerUnitTestEntity { Id = 1, Value = "a" });
            a.Add(new ContainerUnitTestEntity { Id = 2, Value = "b" });
            a.Add(new ContainerUnitTestEntity { Id = 3, Value = "c" });

            b.Add(new ContainerUnitTestEntity { Id = 3, Value = "c" });
            b.Add(new ContainerUnitTestEntity { Id = 1, Value = "a" });
            b.Add(new ContainerUnitTestEntity { Id = 2, Value = "b" });
            b.Add(new ContainerUnitTestEntity { Id = 1, Value = "a" });

            //Assert.
            bool areEqual = a.HasEqualItems(b);
            Assert.IsFalse(areEqual);
        }

        #endregion HAS_EQUAL_ITEMS

    }

    class ContainerUnitTestEntity
    {
        public int Id { get; set; }
        public string Value { get; set; }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(ContainerUnitTestEntity)) return false;

            ContainerUnitTestEntity compared = (ContainerUnitTestEntity)obj;
            if ((compared.Id) != Id) return false;
            if (!compared.Value.Equals(Value)) return false;
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }

}