using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stock.DAL.Helpers;

namespace Stock_UnitTest
{
    [TestClass]
    public class HelperMethod
    {

        [TestMethod]
        public void toDbString_ReturnsStringNull_ForNullDoubleValue()
        {
            double? value = null;
            Assert.AreEqual("NULL", value.ToDbString());
        }

        [TestMethod]
        public void toDbString_ReturnsStringRepresentationOfNumberWithDotAsDecimalSeparator_ForNumericValue()
        {
            double? value = 5.342137;
            Assert.AreEqual("5.34214", value.ToDbString());
        }

        [TestMethod]
        public void toDbString_ReturnsStringNull_ForNullIntValue()
        {
            int? value = null;
            Assert.AreEqual("NULL", value.ToDbString());
        }

        [TestMethod]
        public void toDbString_ReturnsStringRepresentationOfNumber_ForInt()
        {
            int? value = 5;
            Assert.AreEqual("5", value.ToDbString());
        }

        [TestMethod]
        public void toDbString_Returns1_ForTrue()
        {
            bool value = true;
            Assert.AreEqual("1", value.ToDbString());
        }

        [TestMethod]
        public void toDbString_Returns0_ForFalse()
        {
            bool value = false;
            Assert.AreEqual("0", value.ToDbString());
        }

        [TestMethod]
        public void toDbString_ReturnsNullString_ForNullBoolean()
        {
            bool? value = null;
            Assert.AreEqual("NULL", value.ToDbString());
        }

        [TestMethod]
        public void toDbString_ReturnsNullString_ForNullDate()
        {
            DateTime? date = null;
            Assert.AreEqual("NULL", date.ToDbString());
        }

        [TestMethod]
        public void toDbString_ReturnsProperString_ForOnlyDate()
        {
            DateTime date = new DateTime(2017, 12, 4);
            Assert.AreEqual("'2017-12-04 00:00:00'", date.ToDbString());
        }

        [TestMethod]
        public void toDbString_ReturnsProperString_ForDateWithTime()
        {
            DateTime date = new DateTime(2017, 12, 4, 11, 31, 14);
            Assert.AreEqual("'2017-12-04 11:31:14'", date.ToDbString());
        }
    }
}
