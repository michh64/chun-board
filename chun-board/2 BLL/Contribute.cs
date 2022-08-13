using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace ChunBoard
{
    class Contribute
    {
        public static void ContributeData(string str)
        {
            // Lấy dữ liệu
            Variable.ArrStr = StringHandling.GetDataFormString(str, true);

            ControlContribute(Variable.ArrStr);

            Storage.WriteFile();

        }

        // Kiểm soát thêm dữ liệu
        public static void ControlContribute(string[] ArrStr)
        {
            if (StringHandling.GetTypeData(ArrStr[0]) == 0)
                AddRoot((string)ArrStr[0]);

            for (int i = 1; i < ArrStr.Length; i++)
            {
                if (StringHandling.GetTypeData(ArrStr[i]) == 0)
                {
                    AddRoot((string)ArrStr[i]);
                }

                if (StringHandling.GetTypeData(ArrStr[i - 1]) == 0
                && StringHandling.GetTypeData(ArrStr[i]) == 0)
                {
                    AddBranch(ArrStr[i - 1], ArrStr[i]);
                }
            }
        }

        // Thêm dữ liệu root
        public static void AddRoot(string strA)
        {
            if (DataUsing.CheckRoot(strA) == false)
            {
                Variable.rootarr.Add(strA);
                Variable.rootarr.Sort();

                // Khởi tạo branch data
                ArrayList temp = new ArrayList();
                temp.Add(" ");
                Variable.brancharr.Insert(Variable.rootarr.BinarySearch(strA), temp);

                ArrayList num = new ArrayList();
                num.Add(0);
                Variable.popularity.Insert(Variable.rootarr.BinarySearch(strA), num);
            }
        }

        // Thêm dữ liệu branch
        public static void AddBranch(string strA, string strB)
        {
            int i = Variable.rootarr.BinarySearch(strA);

            if (DataUsing.CheckBranch(strA, strB) == false)
            {
                Variable.brancharr[i].Add(strB);
                Variable.brancharr[i].Sort();

                Variable.popularity[i].Insert(Variable.brancharr[i].BinarySearch(strB), 1);
                Variable.popularity[i][0] = int.Parse(Variable.popularity[i][0].ToString()) + 1;
            }
            else
            {
                SmartTyping.UpdatePopul(strA, strB, 1);
            }
        }


    }
}
