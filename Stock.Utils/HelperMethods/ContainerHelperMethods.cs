using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Utils
{
    public static class ContainerHelperMethods
    {

        public static bool HasTheSameItems<T>(this IEnumerable<T> list1, IEnumerable<T> list2)
        {
            List<T> removableList2 = list2.ToList();
            foreach (T baseObject in list1)
            {
                bool isFound = false;
                foreach (T comparedObject in removableList2)
                {
                    Type comparedObjectType = comparedObject.GetType();
                    if (comparedObjectType.IsPrimitive || comparedObjectType == typeof(Decimal) || comparedObjectType == typeof(String))
                    {
                        isFound = (baseObject.Equals(comparedObject));
                    }
                    else
                    {
                        isFound = (Object.ReferenceEquals(baseObject, comparedObject));
                    }

                    if (isFound)
                    {
                        removableList2.Remove(comparedObject);
                        break;
                    }

                }

                if (!isFound)
                {
                    return false;
                }

            }

            return (removableList2.Count == 0);

        }

        public static bool HasEqualItems<T>(this IEnumerable<T> list1, IEnumerable<T> list2)
        {
            List<T> removableList2 = list2.ToList();
            foreach (T baseObject in list1)
            {
                bool isFound = false;
                foreach (T comparedObject in removableList2)
                {
                    if (baseObject.Equals(comparedObject))
                    {
                        removableList2.Remove(comparedObject);
                        isFound = true;
                        break;
                    }
                }

                if (!isFound)
                {
                    return false;
                }

            }

            return (removableList2.Count == 0);

        }

    }
}
