using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.DAL.Infrastructure;

namespace Stock.DAL.TransferObjects
{
    public class ExtremumDto
    {

        [Key]
        public int ExtremumId { get; set; }
        public string Symbol { get; set; }
        public int Type { get; set; }
        public DateTime PriceDate { get; set; }
        public int EarlierCounter { get; set; }
        public int LaterCounter { get; set; }
        public double EarlierAmplitude { get; set; }
        public double LaterAmplitude { get; set; }
        public double Volatility { get; set; }
        public double EarlierChange1 { get; set; }
        public double EarlierChange2 { get; set; }
        public double EarlierChange3 { get; set; }
        public double EarlierChange5 { get; set; }
        public double EarlierChange10 { get; set; }
        public double LaterChange1 { get; set; }
        public double LaterChange2 { get; set; }
        public double LaterChange3 { get; set; }
        public double LaterChange5 { get; set; }
        public double LaterChange10 { get; set; }
        public bool IsOpen { get; set; }
        public bool Cancelled { get; set; }


        public DateTime GetDate()
        {
            return PriceDate;
        }

        public string RemoveSql()
        {
            return "DELETE FROM fx.{0} " +
                            " WHERE " +
                                " Type = " + Type + " AND " +
                                " Symbol = '" + Symbol + "' AND " +
                                " PriceDate = '" + PriceDate + "';";
        }

        public string InsertSql()
        {
            var sqlInsert = "INSERT INTO fx.{0}" +
                "(Symbol, Type, PriceDate, EarlierCounter, LaterCounter, EarlierAmplitude, " +
                    "LaterAmplitude, Volatility, EarlierChange1, EarlierChange2, EarlierChange3, " +
                    "EarlierChange5, EarlierChange10, LaterChange1, LaterChange2, LaterChange3, " +
                    "LaterChange5, LaterChange10, IsOpen, Timestamp) " +
                "VALUES ('" + Symbol + "'" +
                    ", " + Type +
                    ", '" + PriceDate + "'" +
                    ", " + EarlierCounter +
                    ", " + LaterCounter +
                    ", " + EarlierAmplitude.ToDbString() +
                    ", " + LaterAmplitude.ToDbString() +
                    ", " + Volatility.ToDbString() +
                    ", " + EarlierChange1.ToDbString() +
                    ", " + EarlierChange2.ToDbString() +
                    ", " + EarlierChange3.ToDbString() +
                    ", " + EarlierChange5.ToDbString() +
                    ", " + EarlierChange10.ToDbString() +
                    ", " + LaterChange1.ToDbString() +
                    ", " + LaterChange2.ToDbString() +
                    ", " + LaterChange3.ToDbString() +
                    ", " + LaterChange5.ToDbString() +
                    ", " + LaterChange10.ToDbString() +
                    ", " + (IsOpen ? 1 : 0) +
                    ", NOW());";

            return sqlInsert;

        }

        public string UpdateSql()
        {

            var sql = " UPDATE fx.{0}" +
                      " SET" +
                            "  EarlierCounter = " + EarlierCounter +
                            ", LaterCounter = " + LaterCounter +
                            ", EarlierAmplitude = " + EarlierAmplitude.ToDbString() +
                            ", LaterAmplitude = " + LaterAmplitude.ToDbString() +
                            ", Volatility = " + Volatility.ToDbString() +
                            ", EarlierChange1 = " + EarlierChange1.ToDbString() +
                            ", EarlierChange2 = " + EarlierChange2.ToDbString() +
                            ", EarlierChange3 = " + EarlierChange3.ToDbString() +
                            ", EarlierChange5 = " + EarlierChange5.ToDbString() +
                            ", EarlierChange10 = " + EarlierChange10.ToDbString() +
                            ", LaterChange1 = " + LaterChange1.ToDbString() +
                            ", LaterChange2 = " + LaterChange2.ToDbString() +
                            ", LaterChange3 = " + LaterChange3.ToDbString() +
                            ", LaterChange5 = " + LaterChange5.ToDbString() +
                            ", LaterChange10 = " + LaterChange10.ToDbString() +
                            ", IsOpen = " + (IsOpen ? 1 : 0) + 
                            ", Timestamp = NOW()" +
                      " WHERE" +
                            " ExtremumId = " + ExtremumId + ";";

            return sql;

        }

    }
}
