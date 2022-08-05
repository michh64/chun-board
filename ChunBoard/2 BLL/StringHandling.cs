using System;
using System.Collections;
using System.Text;

namespace ChunBoard
{
    // class xử lý chuỗi
    class StringHandling
    {
        // =============<> Kiểm tra kiểu dữ liệu của chuỗi <>==============

        // kiểm tra chuỗi có ở trong mảng không
        private static bool Compare(string strA, string[] Arr)
        {
            for (int i = 0; i < Arr.Length; i++)
            {
                string temp = Arr[i];
                if (strA.Contains(Arr[i]) )
                {
                    return true;
                }
            }
            return false;
        }

        // kiểm tra xem là chữ hay số hay dấu câu
        public static int GetTypeData(string strA)
        {
            if (Compare(strA, Variable.punctuation))
                return 1;
            if (Compare(strA, Variable.numb))
                return 2;

            return 0;

        }

        // =============<> Hàm căn giữa <>==============

        // Căn giữa
        public static string TextCenter(string str)
        {
            int i = str.Length;
            if (i != 0)
            {
                if (i % 2 == 1)
                {
                    i++;
                    str = str + " ";
                }

                string space = "";
                space = space.PadRight((34 - i) / 2);

                return space + str;
            }
            else
                return str;
        }


        // =============<> Tạo mảng dữ liệu đầu vào <>==============

        // chèn dấu cách ngăn giữa các dấu
        private static void InsertSpace(ref string strA, string[] Arr)
        {
            for (int i = 0; i < Arr.Length; i++)
            {
                int temp = 0;
                while (strA.IndexOf(Arr[i], temp) != -1)
                {
                    temp = strA.IndexOf(Arr[i], temp) + 3;
                    strA = strA.Insert(temp - 3, " ");
                    strA = strA.Insert(temp - 1, " ");
                }
            }
        }

        // Tách dấu, xóa khoảng trắng thừa, lưu vào mảng
        public static string[] GetDataFormString(string strA, bool IsTrim)
        {
            strA = strA.ToLower();

            // Phân tách các ký tự
            InsertSpace(ref strA, Variable.punctuation);
            InsertSpace(ref strA, Variable.numb);


            // Xóa khoảng trắng thừa
            while (strA.Contains("  "))
            {
                strA = strA.Replace("  ", " ");
            }

            if (IsTrim == true)
            {
                strA = strA.Trim();
            }

            // Tạo mảng & lưu
            return strA.Split(' ');

        }

        public static string RemoveLineDown(string strA)
        {
            while (strA.Contains("\n"))
            {
                strA = strA.Replace("\n", ". ");
            }

            while (strA.Contains("\r\n"))
            {
                strA = strA.Replace("\r\n", ". ");
            }

            return strA;

        }


    }


}
