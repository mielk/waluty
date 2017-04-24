using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.DAL.TransferObjects;

namespace Stock.DAL.Helpers
{
    public class ExtremumDALHelper
    {

        public static string RemoveSql(ExtremumDto dto)
        {
            return "DELETE FROM fx.{0} " +
                            " WHERE " +
                                " Type = " + dto.Type + " AND " +
                                " Symbol = '" + dto.Symbol + "' AND " +
                                " PriceDate = '" + dto.PriceDate + "';";
        }

        private string getSqlInsertIntoString(string[][] values)
        {
            return string.Empty;
        }

        public static string InsertSql(ExtremumDto dto)
        {
            var dbStringBuilder = new DbStringBuilder();
            dbStringBuilder.DbAppendix = "fx";
            dbStringBuilder.Add("Symbol", dto.Symbol);
            dbStringBuilder.Add("Type", dto.Type.ToString());
            dbStringBuilder.Add("PriceDate", dto.PriceDate.ToString());
            dbStringBuilder.Add("EarlierCounter", dto.EarlierCounter);
            dbStringBuilder.Add("LaterCounter", dto.LaterCounter);
            dbStringBuilder.Add("EarlierAmplitude", dto.EarlierAmplitude);
            dbStringBuilder.Add("LaterAmplitude", dto.LaterAmplitude);
            dbStringBuilder.Add("Volatility", dto.Volatility);
            dbStringBuilder.Add("EarlierChange1", dto.EarlierChange1);
            dbStringBuilder.Add("EarlierChange2", dto.EarlierChange2);
            dbStringBuilder.Add("EarlierChange3", dto.EarlierChange3);
            dbStringBuilder.Add("EarlierChange5", dto.EarlierChange5);
            dbStringBuilder.Add("EarlierChange10", dto.EarlierChange10);
            dbStringBuilder.Add("LaterChange1", dto.LaterChange1);
            dbStringBuilder.Add("LaterChange2", dto.LaterChange2);
            dbStringBuilder.Add("LaterChange3", dto.LaterChange3);
            dbStringBuilder.Add("LaterChange5", dto.LaterChange5);
            dbStringBuilder.Add("LaterChange10", dto.LaterChange10);
            dbStringBuilder.Add("IsOpen", dto.IsOpen);
            dbStringBuilder.AddTimestamp();

            return dbStringBuilder.GenerateInsertSqlString();

        }

        public static string UpdateSql(ExtremumDto dto)
        {
            var dbStringBuilder = new DbStringBuilder();
            dbStringBuilder.DbAppendix = "fx";
            dbStringBuilder.Add("Symbol", dto.Symbol);
            dbStringBuilder.Add("Type", dto.Type.ToString());
            dbStringBuilder.Add("PriceDate", dto.PriceDate.ToString());
            dbStringBuilder.Add("EarlierCounter", dto.EarlierCounter);
            dbStringBuilder.Add("LaterCounter", dto.LaterCounter);
            dbStringBuilder.Add("EarlierAmplitude", dto.EarlierAmplitude);
            dbStringBuilder.Add("LaterAmplitude", dto.LaterAmplitude);
            dbStringBuilder.Add("Volatility", dto.Volatility);
            dbStringBuilder.Add("EarlierChange1", dto.EarlierChange1);
            dbStringBuilder.Add("EarlierChange2", dto.EarlierChange2);
            dbStringBuilder.Add("EarlierChange3", dto.EarlierChange3);
            dbStringBuilder.Add("EarlierChange5", dto.EarlierChange5);
            dbStringBuilder.Add("EarlierChange10", dto.EarlierChange10);
            dbStringBuilder.Add("LaterChange1", dto.LaterChange1);
            dbStringBuilder.Add("LaterChange2", dto.LaterChange2);
            dbStringBuilder.Add("LaterChange3", dto.LaterChange3);
            dbStringBuilder.Add("LaterChange5", dto.LaterChange5);
            dbStringBuilder.Add("LaterChange10", dto.LaterChange10);
            dbStringBuilder.Add("IsOpen", dto.IsOpen);
            dbStringBuilder.AddWhere("Id", dto.ExtremumId);
            dbStringBuilder.AddTimestamp();

            var sql = "";
            //var sql = " UPDATE fx.{0}" +
            //          " SET" +
            //                "  EarlierCounter = " + EarlierCounter +
            //                ", LaterCounter = " + LaterCounter +
            //                ", EarlierAmplitude = " + EarlierAmplitude.ToDbString() +
            //                ", LaterAmplitude = " + LaterAmplitude.ToDbString() +
            //                ", Volatility = " + Volatility.ToDbString() +
            //                ", EarlierChange1 = " + EarlierChange1.ToDbString() +
            //                ", EarlierChange2 = " + EarlierChange2.ToDbString() +
            //                ", EarlierChange3 = " + EarlierChange3.ToDbString() +
            //                ", EarlierChange5 = " + EarlierChange5.ToDbString() +
            //                ", EarlierChange10 = " + EarlierChange10.ToDbString() +
            //                ", LaterChange1 = " + LaterChange1.ToDbString() +
            //                ", LaterChange2 = " + LaterChange2.ToDbString() +
            //                ", LaterChange3 = " + LaterChange3.ToDbString() +
            //                ", LaterChange5 = " + LaterChange5.ToDbString() +
            //                ", LaterChange10 = " + LaterChange10.ToDbString() +
            //                ", IsOpen = " + (IsOpen ? 1 : 0) +
            //                ", Timestamp = NOW()" +
            //          " WHERE" +
            //                " ExtremumId = " + ExtremumId + ";";

            return sql;

        }


    }
}
