﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Entities;
using Stock.Domain.Enums;

namespace Stock.Domain.Services
{

    public static class HelperMethods
    {

        public static void AppendIndexNumbers(this DataItem[] items)
        {

            for (var i = 0; i < items.Length; i++)
            {
                items[i].Index = i;
            }

        }

        public static int GetFactor(this TrendlineType type)
        {
            return (type == TrendlineType.Resistance ? 1 : -1);
        }

        public static bool isInRange(this double value, double low, double up)
        {
            return false;
        }

        public static bool isInRange(this int value, int low, int up)
        {

            int min;
            int max;

            if (low < up)
            {
                min = low;
                max = up;
            }
            else
            {
                min = low;
                max = up;
            }


            return (value >= min && value <= max);

        }

    }
}