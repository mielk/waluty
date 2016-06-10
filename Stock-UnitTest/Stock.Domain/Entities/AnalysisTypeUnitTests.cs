//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Stock.Domain.Enums;
//using System.Collections;
//using System.Linq;

//namespace Stock_UnitTest.Stock.Domain.Entities
//{
//    [TestClass]
//    public class AnalysisTypeUnitTests
//    {

//        [TestMethod]
//        public void GetAnalysisType_return_proper_enumeration()
//        {
//            AnalysisType type = AnalysisTypeHelper.GetAnalysisType("macd");
//            Assert.AreEqual(AnalysisType.MACD, type);
//        }


//        [TestMethod]
//        public void GetAnalysisType_return_Unknown_type_for_undefined_string()
//        {
//            AnalysisType type = AnalysisTypeHelper.GetAnalysisType(string.Empty);
//            Assert.AreEqual(AnalysisType.Unknown, type);
//        }


//        [TestMethod]
//        public void StringToTypesList_return_empty_list_for_empty_string()
//        {
//            AnalysisType[] types = AnalysisTypeHelper.StringToTypesList(string.Empty);
//            Assert.AreEqual(0, types.Length);
//        }


//        [TestMethod]
//        public void StringToTypesList_return_proper_number_of_types()
//        {
//            AnalysisType[] types = AnalysisTypeHelper.StringToTypesList("price,macd,adx", ',');
//            Assert.AreEqual(3, types.Length);
//        }


//        [TestMethod]
//        public void StringToTypesList_return_proper_enumerations()
//        {
//            AnalysisType[] types = AnalysisTypeHelper.StringToTypesList("price,macd,adx", ',');
//            Assert.IsTrue(types.Contains(AnalysisType.Price));
//            Assert.IsTrue(types.Contains(AnalysisType.MACD));
//            Assert.IsTrue(types.Contains(AnalysisType.ADX));
//        }


//        [TestMethod]
//        public void StringToTypesList_not_existing_string_is_ignored()
//        {
//            AnalysisType[] types = AnalysisTypeHelper.StringToTypesList("price,_##,zzz", ',');
//            Assert.AreEqual(1, types.Length);
//            Assert.IsTrue(types.Contains(AnalysisType.Price));
//        }

//    }
//}
