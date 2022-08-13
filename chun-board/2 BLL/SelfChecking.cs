using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;

namespace ChunBoard
{
    // Class thông tin hệ thống

    class SelfChecking
    {
        #region Check
        // Kiểm tra phiên bản
        public static string Version()
        {
            return Variable.ver;
        }


        // Kiểm tra dữ liệu trùng lặp
        public static string SelfCheckData()
        {
            bool check = false;
            string str = "";
            int rootcount = Variable.rootarr.Count;
            int popcount = Variable.popularity.Count;
            int branchcount = Variable.brancharr.Count;

            int info = 5;

            // Check dữ liệu đồng nhất
            if (rootcount != popcount || branchcount != popcount || rootcount != branchcount)
            {
                str += "Dữ liệu không đồng nhất\n";
            }
            else
                for (int i = 0; i < rootcount; i++)
                {
                    if (Variable.popularity[i].Count != Variable.brancharr[i].Count)
                    {
                        str += string.Format("Dữ liệu không đồng nhất dòng {0} ({1}) - {2} {3}\n", i + 1 + info, i + 1 + info + Variable.rootarr.Count + 2, Variable.brancharr[i].Count, Variable.popularity[i].Count);
                    }
                }
                //str += "Dữ liệu đồng nhất\n";


            for (int i = 0; i < Variable.rootarr.Count - 1; i++)
                if (string.Compare((string)Variable.rootarr[i], (string)Variable.rootarr[i + 1]) == 0)
                {
                    str += string.Format("Lặp root dòng {0} ({1})\n", i + 2 + info, i + 2 + info + Variable.rootarr.Count + 2);
                    check = true;
                }

            for (int i = 0; i < Variable.rootarr.Count; i++)
                for (int j = 0; j < Variable.brancharr[i].Count - 1; j++)
                {
                    if (string.Compare((string)Variable.brancharr[i][j], (string)Variable.brancharr[i][j + 1]) == 0)
                    {
                        str += string.Format("Lặp root dòng {0} ({1}) branch {2}\n", i + 1 + info, i + 1 + info + Variable.rootarr.Count + 2, j + 2);
                        check = true;
                    }
                }

            if (check == false)
            {
                str += string.Format("Dữ liệu ổn định, không trùng lặp.\n");
            }

            return str;
        }


        // Kiểm tra độ lớn dữ liệu
        public static string DataSize(int a)
        {

            int size = 0;
            int max = 0;
            int relation = 0;
            for (int i = 0; i < Variable.rootarr.Count; i++)
            {
                relation += int.Parse(Variable.popularity[i][0].ToString());
                size += Variable.brancharr[i].Count;
                if (Variable.brancharr[i].Count > Variable.brancharr[max].Count)
                    max = i;
            }

            if (a == 1) return Variable.rootarr.Count.ToString("0,0"); // Số từ vựng đã có
            if (a == 2) return size.ToString("0,0"); // Độ lớn dữ liệu
            //if (a == 3) return ((double) size / Variable.rootarr.Count).ToString("0,0.0");// Độ lớn trường dữ liệu trung bình
            if (a == 3) return ((double)size / Variable.rootarr.Count).ToString("0,0");// Độ lớn trường dữ liệu trung bình
            if (a == 4) return relation.ToString("0,0"); // Độ lớn mối kết hợp

            //("Trường dữ liệu lớn nhất: \"{0}\" - root {1} - độ lớn {2}", Variable.rootarr[max], max + 1 + 8, Variable.brancharr[max].Count);

            return "Lỗi tham số truyền";
        }
        #endregion

        #region SelfRepairDuplicate

        // xóa root theo chỉ số
        public static bool DeleteRoot(int index)
        {
            if (Variable.rootarr.Count <= index)
            {
                return false;
            }

            int posA = index;
            // **
            if (Variable.brancharr[posA].Count > 1)
            {
                return false;
            }

            Variable.rootarr.RemoveAt(posA);
            Variable.popularity.RemoveAt(posA);
            Variable.brancharr.RemoveAt(posA);

            return true;

        }


        // Xóa branch theo chỉ số strA, chỉ số strB
        public static bool DeleteBranch(int indexA, int indexB)
        {
            if (Variable.rootarr.Count <= indexA || Variable.brancharr[indexA].Count <= indexB)
            {
                return false;
            }

            int posA = indexA;
            int posB = indexB;

            Variable.popularity[posA][0] = int.Parse(Variable.popularity[posA][0].ToString()) - int.Parse(Variable.popularity[posA][posB].ToString());

            Variable.brancharr[posA].RemoveAt(posB);
            Variable.popularity[posA].RemoveAt(posB);

            return true;
        }

        // [Gộp branch] Chuyển branch chứa strB sang branch[indexA]
        public static void AddBranch(int indexA, string strB)
        {
            int i = indexA;

            if (i < Variable.rootarr.Count)
                if (CheckBranch(indexA, strB) == false)
                {
                    Variable.brancharr[i].Add(strB);
                    Variable.brancharr[i].Sort();

                    Variable.popularity[i].Insert(Variable.brancharr[i].BinarySearch(strB), 1);
                    Variable.popularity[i][0] = int.Parse(Variable.popularity[i][0].ToString()) + 1;
                }
                else
                {
                    UpdatePopul(indexA, strB, 1);
                }

        }

        // Lấy từ khỏi \r.(chức năng đã được bổ sung ở readFile)
        //public static string Getstring(string str)
        //{
        //    string[] temp = str.Split('\r');
        //    return temp[0];
        //}
        /*//*/

        // Cập nhật popularity, Chức năng dành riêng cho class này
        public static void UpdatePopul(int indexA, string strB, int check)
        {
            if (indexA < Variable.rootarr.Count && Variable.brancharr[indexA].BinarySearch(strB) > -1)
            {
                int x = indexA;
                int y = Variable.brancharr[x].BinarySearch(strB);

                Variable.popularity[x][y] = int.Parse(Variable.popularity[x][y].ToString()) + 1 * check;
                Variable.popularity[x][0] = int.Parse(Variable.popularity[x][0].ToString()) + 1 * check;

            }
        }

        // Check branch dành riêng cho class này
        // Kiểm tra từ có trong data branch chưa
        public static bool CheckBranch(int indexA, string strB)
        {
            if (indexA < Variable.rootarr.Count)
            {
                int secondword = Variable.brancharr[indexA].BinarySearch(strB);

                if (secondword >= 0)
                    return true;
            }

            return false;
        }

        // Hàm Control chức năng
        public static void SelfRepairDuplicate(int pull)
        {
            // root
            if (pull == 0)
            {
                for (int i = Variable.rootarr.Count - 2; i >= 0; i--)
                    if (string.Compare((string)Variable.rootarr[i], (string)Variable.rootarr[i + 1]) == 0 )
                    {
                        for (int j = Variable.brancharr[i + 1].Count - 1; j > 0; j--)
                        {
                            int a = int.Parse(Variable.popularity[i + 1][j].ToString());
                            for (int k = 0; k < a; k++)
                            {
                                AddBranch(i, Variable.brancharr[i + 1][j].ToString());
                            }
                            DeleteBranch(i + 1, j);
                        }
                    }

                for (int i = Variable.rootarr.Count - 2; i >= 0; i--)
                    if (string.Compare((string)Variable.rootarr[i], (string)Variable.rootarr[i + 1]) == 0)
                        if (Variable.popularity[i + 1].Count == 1)
                        {
                            DeleteRoot(i + 1);
                        }
            }

            // branch
            if (pull == 1)
            {
                if (pull == 1)
                    for (int i = 0; i < Variable.rootarr.Count; i++)
                        for (int j = 0; j < Variable.brancharr[i].Count - 1; j++)
                            if (string.Compare((string)Variable.brancharr[i][j], (string)Variable.brancharr[i][j + 1]) == 0)
                            {
                                while (int.Parse(Variable.popularity[i][j + 1].ToString()) > 0)
                                {
                                    Variable.popularity[i][j] = int.Parse(Variable.popularity[i][j].ToString()) + 1;
                                    Variable.popularity[i][j + 1] = int.Parse(Variable.popularity[i][j + 1].ToString()) - 1;
                                }
                                DeleteBranch(i, j + 1);
                            }

            }

            Storage.WriteFile();
        }

        #endregion

        public static bool SpecError()
        {
            // Xóa từ là kí tự bỏ qua
            for (int i = 0; i < Variable.rootarr.Count; i++)
            {
                // Nếu root là dấu câu hoặc số
                if (Variable.rootarr[i].ToString().Length == 1 && StringHandling.GetTypeData(Variable.rootarr[i].ToString()) != 0)
                {
                    for (int j = 0; j < Variable.brancharr[i].Count; j++)
                    {
                        DeleteBranch(i, j);
                    }
                    DeleteRoot(i);
                }

                // Kiểm tra từng branch xem có cái nào là dấu câu hoặc số ?
                else
                    for (int j = 0; j < Variable.brancharr[i].Count; j++)
                    {
                        if (Variable.brancharr[i][j].ToString().Length == 1 && StringHandling.GetTypeData(Variable.brancharr[i][j].ToString()) != 0)
                        {
                            DeleteBranch(i, j);
                        }
                    }
            }

            // Xóa từ bị kèm kí tự bỏ qua
            for (int i = 0; i < Variable.rootarr.Count; i++)
            {
                // Root bị kèm dấu câu hoặc số
                if (Variable.rootarr[i].ToString().Length > 1 && StringHandling.GetTypeData(Variable.rootarr[i].ToString()) != 0)
                {
                    for (int j = 0; j < Variable.brancharr[i].Count; j++)
                    {
                        string strA = Variable.rootarr[i].ToString();
                        string strB = Variable.brancharr[i][j].ToString();
                        string data = string.Join(" ", StringHandling.GetDataFormString(strA, true)) + string.Join(" ", StringHandling.GetDataFormString(strB, true));
                        for (int k = 0; k < int.Parse(Variable.popularity[i][j].ToString()); k++)
                        {
                            Contribute.ContributeData(data);
                        }
                        DeleteBranch(i, j);
                    }
                    DeleteRoot(i);
                }


                // Branch bị kèm dấu câu hoặc số
                else
                    for (int j = 0; j < Variable.brancharr[i].Count; j++)
                    {
                        if (Variable.brancharr[i][j].ToString().Length > 1 && StringHandling.GetTypeData(Variable.brancharr[i][j].ToString()) != 0)
                        {
                            string strA = Variable.rootarr[i].ToString();
                            string strB = Variable.brancharr[i][j].ToString();
                            string data = string.Join(" ", StringHandling.GetDataFormString(strA, true)) + " " + string.Join(" ", StringHandling.GetDataFormString(strB, true));
                            for (int k = 0; k < int.Parse(Variable.popularity[i][j].ToString()); k++)
                            {
                                Contribute.ContributeData(data);
                            }
                            DeleteBranch(i, j);
                        }
                    }

            }
            
            Storage.WriteFile();

            return true;
        }

        // Xóa những đối tượng đặc biệt
        public static bool DeleteSpecKey(string str)
        {
            for (int i = 0; i < Variable.rootarr.Count; i++)
            {
                // Nếu root là đối tượng đặc biệt
                if (string.Compare(Variable.rootarr[i].ToString(), str) == 0)
                {
                    for (int j = 0; j < Variable.brancharr[i].Count; j++)
                    {
                        DeleteBranch(i, j);
                    }
                    DeleteRoot(i);
                }

                // Kiểm tra từng branch xem có cái nào là đối tượng đặc biệt ?
                else
                    for (int j = 0; j < Variable.brancharr[i].Count; j++)
                    {
                        if ( string.Compare(Variable.brancharr[i][j].ToString(), str) == 0)
                        {
                            DeleteBranch(i, j);
                        }
                    }
            }

            Storage.WriteFile();
            return true;
        }


        // Sắp xếp root và branch
        public static bool SortAllData()
        {
            int length = Variable.rootarr.Count;

            // sắp xếp root
            for (int i = 0; i < length; i++)
            {
                for (int j = i + 1; j < length; j++)
                {
                    if (string.Compare(Variable.rootarr[i].ToString(), Variable.rootarr[j].ToString()) == 1)
                    {
                        object tempobj = Variable.rootarr[i];
                        Variable.rootarr[i] = Variable.rootarr[j];
                        Variable.rootarr[j] = tempobj;

                        ArrayList temparl = Variable.brancharr[i];
                        Variable.brancharr[i] = Variable.brancharr[j];
                        Variable.brancharr[j] = temparl;

                        temparl = Variable.popularity[i];
                        Variable.popularity[i] = Variable.popularity[j];
                        Variable.popularity[j] = temparl;
                    }
                }
            }

            //sắp xếp branch
            for (int i = 0; i < length; i++)
                for (int j = 1; j < Variable.brancharr[i].Count - 1; j++)
                    for (int k = j + 1; k < Variable.brancharr[i].Count; k++)

                        if (string.Compare(Variable.brancharr[i][j].ToString(), Variable.brancharr[i][k].ToString()) == 1)
                        {
                            object tempobj = Variable.brancharr[i][j];
                            Variable.brancharr[i][j] = Variable.brancharr[i][k];
                            Variable.brancharr[i][k] = tempobj;

                            tempobj = Variable.popularity[i][j];
                            Variable.popularity[i][j] = Variable.popularity[i][k];
                            Variable.popularity[i][k] = tempobj;
                        }
            
            Storage.WriteFile();
            return true;
        }

        // Tạo file backup
        public static void RestoreBackup(string str)
        {
            DateTime now = DateTime.Now;

            var filename = $"{str}_{now.Year.ToString("0000")}{now.Month.ToString("00")}{now.Day.ToString("00")}_{now.Hour.ToString("00")}{now.Minute.ToString("00")}{now.Second.ToString("00")}.txt";
            string contentfile = File.ReadAllText(Variable.storageFile);

            string storageFilePath = $@"..\..\Restore Backup\{filename}";

            File.WriteAllText(storageFilePath, contentfile);
            
        }

        public static string GetNewPathFileBackup()
        {
            string[] directories = Directory.GetFiles($@"..\..\Restore Backup");
            string lastFile = directories[0];
            foreach (var file in directories)
            {
                if (Directory.GetLastWriteTime(file) > Directory.GetLastWriteTime(lastFile))
                {
                    lastFile = file;
                }
            }

            return lastFile;
        }

        // Chức năng này sẽ được xử lý bởi người dùng
        /*public static void DeleteErrorWord ()
        {
            for (int i = Variable.popularity.Count - 1; i >= 0; i--)
            {
                if (Convert.ToInt16(Variable.popularity[i][0]) == 0 )
                {
                    DeleteRoot(i);
                }
            }

            Storage.WriteFile();

        }*/
    }

}
