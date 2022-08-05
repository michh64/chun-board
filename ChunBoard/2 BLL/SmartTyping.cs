using System;
using System.Collections;

namespace ChunBoard
{
    public class SmartTyping
    {
        // Cập nhật Độ phổ biến, Khi kiểm tra chính tả
        public static void UpdatePopul(string strA, string strB, int check)
        {
            if (DataUsing.CheckBranch(strA, strB) == true)
            {
                int x = Variable.rootarr.BinarySearch(strA);
                int y = Variable.brancharr[x].BinarySearch(strB);

                Variable.popularity[x][y] = int.Parse(Variable.popularity[x][y].ToString()) + 1 * check;
                Variable.popularity[x][0] = int.Parse(Variable.popularity[x][0].ToString()) + 1 * check;

            }
        }

        // kiểm tra chuỗi có ở trong mảng không
        private static bool Compare(char charA, char[] Arr)
        {
            for (int i = 0; i < Arr.Length; i++)
            {
                if (Arr[i] == charA)
                {
                    return true;
                }
            }
            return false;
        }

        // Quy đổi và lấy mã char những ký tự đặc biệt
        static int GetIndexChar(char strA)
        {
            char[] a = { 'a', 'á', 'à', 'ả', 'ạ', 'ã', 'ă', 'ắ', 'ằ', 'ẳ', 'ặ', 'ẵ', 'â', 'ấ', 'ầ', 'ẩ', 'ậ', 'ẫ' };
            char[] o = { 'ô', 'ố', 'ồ', 'ổ', 'ộ', 'ỗ', 'o', 'ó', 'ò', 'ỏ', 'ọ', 'õ', 'ơ', 'ớ', 'ờ', 'ở', 'ợ', 'ỡ' };
            char[] e = { 'e', 'é', 'è', 'ẻ', 'ẹ', 'ẽ', 'ê', 'ế', 'ề', 'ể', 'ệ', 'ễ' };
            char[] i = { 'i', 'í', 'ì', 'ỉ', 'ị', 'ĩ' };
            char[] u = { 'u', 'ú', 'ù', 'ủ', 'ụ', 'ũ', 'ư', 'ứ', 'ừ', 'ử', 'ự', 'ữ' };
            char[] d = { 'd', 'đ' };
            char[] y = { 'y', 'ý', 'ỳ', 'ỷ', 'ỵ', 'ỹ' };

            if (Compare(strA, a))
                return (int)'a';
            if (Compare(strA, o))
                return (int)'o';
            if (Compare(strA, i))
                return (int)'i';
            if (Compare(strA, e))
                return (int)'e';
            if (Compare(strA, u))
                return (int)'u';
            if (Compare(strA, d))
                return (int)'d';
            if (Compare(strA, y))
                return (int)'y';

            return (int)strA;
        }

        // chuỗi search có chứa trong chuỗi data không - true false
        public static bool IsConclude(string search, string data)
        {
            bool check = true;
            if (search.Length > data.Length)
            {
                return false;
            }

            for (int i = 0; i < search.Length; i++)
            {
                if (GetIndexChar(search[i]) != GetIndexChar(data[i]))
                    check = false;
            }
            return check;
        }

        static int BinSearchStart(ArrayList a, string value)
        {
            int left, right, middle;
            left = 0;
            right = a.Count - 1;


            while (left <= right)
            {
                if (left == right)
                {
                    if (IsConclude(value, a[left].ToString()) == true)
                    {
                        return left;
                    }
                    else
                        return -1;
                }

                middle = (left + right) / 2;
                //middle = (int) Math.Floor((left + right) / 2.0);
                if (middle == 0)
                {
                    if (IsConclude(value, a[left].ToString()) == true)
                        return left;
                    else
                        if (IsConclude(value, a[right].ToString()) == true)
                    {
                        return right;
                    }
                    else
                        return -1;

                }
                string strPASS = a[middle - 1].ToString();
                string strPRE = a[middle].ToString();

                if (IsConclude(value, strPRE) == true)
                {
                    if (IsConclude(value, strPASS) == false || middle - 1 < 0)
                        return middle;
                }

                if (string.Compare(value, strPRE) == 1) // Ưu tiên right -
                //if (StringCompare(value, strPRE) == 1) // Ưu tiên right -
                    left = middle + 1;
                else
                    right = middle - 1;

            }

            return -1;
        }

        static int BinSearchEnd(ArrayList a, string value)
        {
            int left, right, middle;
            left = BinSearchStart(a, value);
            right = a.Count - 1;

            if (left == -1)
            {
                return left;
            }

            while (left <= right)
            {

                if (left == right)
                {
                    if (IsConclude(value, a[left].ToString()) == true)
                    {
                        return left;
                    }
                    else
                        return -1;
                }

                middle = ((left + right) / 2);

                string strPRE = a[middle].ToString();
                string strFU = a[middle + 1].ToString();

                if (IsConclude(value, strPRE) == true)
                {
                    if (IsConclude(value, strFU) == false || middle + 1 > a.Count - 1)
                        return middle;
                    left = middle + 1;
                }
                else

                if (string.Compare(value, strPRE) == -1) // Ưu tiên left +
                    right = middle - 1;
                else
                    left = middle + 1;

            }

            return BinSearchStart(a, value);
        }


        static int[] GetIndexRangePosition (int pull, string[] str)
        {
            int[] output = new int[3];

            string end = str[0];
            string semiend = str[1];

            int indexS = -1;
            int indexE = -1;

            /*3 trường hợp: 
             *      + Hiện đầu cuối 
             *      + Không tìm thấy đầu cuối (-1) (Mặc định BinSearch)
             *      + End = "" hiện toàn bộ root
             */
            if (pull == 0)
            {

                if (end != "")
                {
                    indexS = BinSearchStart(Variable.rootarr, end);
                    indexE = BinSearchEnd(Variable.rootarr, end);
                }
                else
                {
                    indexS = 0;
                    indexE = Variable.rootarr.Count - 1;
                }
            }

            /* 2 trường hợp: 
             * Có semiend != ""
             *      + tìm thấy semiend:
             *              - tìm thấy end          = Hiện đầu cuối
             *              - không tìm thấy end    = Hiện toàn bộ branch
             *      + không tìm thấy semiend
             *          = (-1)
             *      
             * Không có semiend ""
             *      = (-1)
             * 
             */
            if (pull == 1)
            {
                if (semiend != "")
                {
                    int search = Variable.rootarr.BinarySearch(semiend);
                    if (search >= 0) // Tìm thấy semiend
                    {
                        if (end != "")
                        {
                            indexS = BinSearchStart(Variable.brancharr[search], end);
                            indexE = BinSearchEnd(Variable.brancharr[search], end);
                        }
                        else
                        {
                            indexS = 1;
                            indexE = Variable.brancharr[search].Count - 1;
                        }

                    }
                }
            }

            output[0] = indexS;
            output[1] = indexE;
            if (semiend != "")
                output[2] = Variable.rootarr.BinarySearch(semiend);

            return output;
        }


        static string[] GetIndexTop(int pull, string[] str)
        {
            string[] indextop = { "", "", "", "", "", "" };
            int[] RangePosition = GetIndexRangePosition(pull, str);

            int tempp;
            string tempd;

            // Trường hợp ra -1
            if (RangePosition[1] == -1 && RangePosition[0] == -1)
            {
                indextop[0] = "";
                indextop[1] = "";
                indextop[2] = "";
                indextop[3] = "0";
                indextop[4] = "0";
                indextop[5] = "0";

                return indextop;
            }

            string[] tempdata = new string[RangePosition[1] - RangePosition[0] + 1];
            int[] temppopu = new int[RangePosition[1] - RangePosition[0] + 1];

            if (pull == 0)
            {
                // Lấy dữ liệu và độ phổ biến trong khoảng cho phép ra
                for (int i = 0; i < tempdata.Length; i++)
                {
                    tempdata[i] = Variable.rootarr[i + RangePosition[0]].ToString();
                    temppopu[i] = int.Parse(Variable.popularity[i + RangePosition[0]][0].ToString());
                }

                for (int i = 0; i < 3 && i < tempdata.Length; i++)
                {
                    for (int j = i + 1; j < tempdata.Length; j++)
                    {
                        if (int.Parse(temppopu[i].ToString()) < int.Parse(temppopu[j].ToString()))
                        {
                            tempp = temppopu[i];
                            temppopu[i] = temppopu[j];
                            temppopu[j] = tempp;

                            tempd = tempdata[i];
                            tempdata[i] = tempdata[j];
                            tempdata[j] = tempd;
                        }
                    }
                }
            }

            if (pull == 1)
            {
                for (int i = 0; i < tempdata.Length; i++)
                {
                    tempdata[i] = Variable.brancharr[RangePosition[2]][i + RangePosition[0]].ToString();
                    temppopu[i] = int.Parse(Variable.popularity[RangePosition[2]][i + RangePosition[0]].ToString());
                }

                for (int i = 0; i < 3 && i < tempdata.Length; i++)
                {
                    for (int j = i + 1; j < tempdata.Length; j++)
                    {
                        if ((int)temppopu[i] < (int)temppopu[j])
                        {
                            tempp = temppopu[i];
                            temppopu[i] = temppopu[j];
                            temppopu[j] = tempp;

                            tempd = tempdata[i];
                            tempdata[i] = tempdata[j];
                            tempdata[j] = tempd;
                        }
                    }
                }

            }

            // lấy dữ liệu nếu tempdata không đủ 3
            for (int i = 0; i < temppopu.Length && i < 3; i++)
            {
                indextop[i] = tempdata[i];
                indextop[i + 3] = temppopu[i].ToString();
            }


            return indextop;

        }

        public static string[] SearchPopular (string str)
        {
            // Xử lý dữ liệu nhập có nhiều dòng
            str = StringHandling.RemoveLineDown(str);

            // Lấy dữ liệu 2 từ cuối
            string[] input = StringHandling.GetDataFormString(str, false);
            string[] data = new string[2];

            data[0] = input[input.Length - 1];
            if (input.Length > 1)
                data[1] = input[input.Length - 2];
            else
                data[1] = "";

            string[] output = new string[6];
            // data[0]: từ cuối
            // data[1]: từ kế cuối

            // ----------------------------------

            // I
            if (data[0] == "" && data[1] == "")
            {
                //output = GetIndexTop(0, data);
                output[0] = "";
                output[1] = "";
                output[2] = "";
                output[3] = "";
                output[4] = "";
                output[5] = "";


            }

            // TruI hoặc Trung nè. ĐâI
            if ( (data[0] != "" && data[1] == "" && StringHandling.GetTypeData(data[0]) == 0) ||
                 (data[0] != "" && StringHandling.GetTypeData(data[1]) != 0 && StringHandling.GetTypeData(data[0]) == 0) )
            {
                output = GetIndexTop(0, data);
            }

            // Trung I
            if (data[0] == "" && data[1] != "" && StringHandling.GetTypeData(data[1]) == 0)
            {
                output = GetIndexTop(1, data);
                //output[2] = GetIndexTop(0, data)[0];
            }

            // Trung nI
            if (data[0] != "" && data[1] != "" && StringHandling.GetTypeData(data[1]) == 0 && StringHandling.GetTypeData(data[0]) == 0)
            {
                output = GetIndexTop(1, data);
            }

            // Trung nè.I
            if (data[0] != "" && StringHandling.GetTypeData(data[0]) != 0 || 
                data[0] == "" && StringHandling.GetTypeData(data[1]) != 0)
            {
                //output = GetIndexTop(0, new string[] {"", "" });
                output[0] = "";
                output[1] = "";
                output[2] = "";
                output[3] = "";
                output[4] = "";
                output[5] = "";

            }


            return output;
        }

    }
}
