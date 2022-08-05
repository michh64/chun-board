using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace ChunBoard
{
    class Spelling
    {

        public static ArrayList SpellCheck(string[] ArrStr, int pull)
        {
            ArrayList Error = new ArrayList();

            if (pull == 1)
            {
                for (int i = 0; i < ArrStr.Length - 1; i++)
                {
                    if (/* DuplicateCheck2(Error, ArrStr, pull, i) != true && */
                        StringHandling.GetTypeData(ArrStr[i]) == 0
                        && StringHandling.GetTypeData(ArrStr[i + 1]) == 0)
                        
                        if (DataUsing.CheckBranch(ArrStr[i], ArrStr[i + 1]) == false)
                            Error.Add(i);
                        else
                        {

                        }
                }
                if (Error.Count > 0)
                {
                    DuplicateCheck(Error, ArrStr, pull);
                }

            }
            else
            if (pull == 0)
            {
                for (int i = 0; i < ArrStr.Length; i++)
                {
                    if (/* DuplicateCheck2(Error, ArrStr, pull, i) != true && */
                        DataUsing.CheckRoot(ArrStr[i]) == false
                        && StringHandling.GetTypeData(ArrStr[i]) == 0
                        )
                        Error.Add(i);
                }

                // kiểm tra trùng lặp
                if (Error.Count > 0)
                {
                    DuplicateCheck(Error, ArrStr, pull);
                }
            }

            return Error;

        }

        static void DuplicateCheck(ArrayList Error, string[] ArrStr, int pull)
        {
            ArrayList words = new ArrayList();
            ArrayList dup = new ArrayList();

            // chuyển mã vị trí thành từ kiểm tra
            if (pull == 0)
            {
                for (int i = 0; i < Error.Count; i++)
                {
                    words.Add(ArrStr[(int)Error[i]].ToLower());
                }
            }
            if (pull == 1)
            {
                for (int i = 0; i < Error.Count; i++)
                {
                    words.Add(ArrStr[(int)Error[i]].ToLower() + ArrStr[(int)Error[i] + 1].ToLower());
                }

            }

            for (int i = 0; i < words.Count - 1; i++)
                for (int j = i + 1; j < words.Count; j++)
                    if (
                        (string)words[i] == (string)words[j]
                        // || IO.SkipwordCheck(ArrStr[(int) Error[i]]) == true
                        // || (pull == 1 && IO.SkipwordCheck(ArrStr[(int) Error[i+1]]) == true)
                        )
                    {
                        dup.Add(i);
                        break;
                    }

            dup.Sort();
            for (int i = dup.Count - 1; i >= 0; i--)
            {
                Error.RemoveAt((int)dup[i]);
            }

        }

/*        static bool DuplicateCheck2(ArrayList Error, string[] ArrStr, int pull, int n)
        {
            ArrayList words = new ArrayList();
            string check = "";

            // chuyển mã vị trí thành từ kiểm tra
            if (pull == 0)
            {
                check = ArrStr[n];
                for (int i = 0; i < Error.Count; i++)
                {
                    words.Add(ArrStr[(int)Error[i]]);
                }
            }
            if (pull == 1)
            {
                check = ArrStr[n] + ArrStr[n + 1];
                for (int i = 0; i < Error.Count; i++)
                {
                    words.Add(ArrStr[(int)Error[i]] + ArrStr[(int)Error[i] + 1]);
                }

            }

            for (int i = 0; i < words.Count; i++)
                if (check == (string)words[i])
                {
                    return true;
                }

            return false;
        }
*/
    }

}
