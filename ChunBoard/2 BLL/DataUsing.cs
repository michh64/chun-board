using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChunBoard
{
    public class DataUsing
    {
        // Kiểm tra từ có trong data root chưa
        public static bool CheckRoot(string strA)
        {
            if (Variable.rootarr.BinarySearch(strA) >= 0)
                return true;

            return false;
        }


        // Kiểm tra từ có trong data branch chưa
        public static bool CheckBranch(string strA, string strB)
        {
            if (Variable.rootarr.BinarySearch(strA) >= 0)
            {
                int secondword = Variable.brancharr[Variable.rootarr.BinarySearch(strA)].BinarySearch(strB);

                if (secondword >= 0)
                    return true;
            }

            return false;
        }

    }
}
