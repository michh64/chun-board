using System;
using System.Collections;
using System.IO;

namespace ChunBoard
{
    // class đọc ghi file
    class Storage
    {
        //string output = File.ReadAllText(Variable.storageFile);
        //File.WriteAllText(Variable.storageFile, input);

        // Ghi file text
        public static void WriteFile()
        {
            // Dòng info dữ liệu có 3 dòng
            int info = 3;
            int length = Variable.rootarr.Count;
            string[] inputarr = new string[info + 2 + length + 2 + length];

            string[] vArr = Variable.ver.Split('.');
            int num = int.Parse(vArr[2]);
            num++;
            vArr[2] = num.ToString();
            Variable.ver = string.Join(".", vArr);
            
            inputarr[0] = Variable.ver;
            inputarr[1] = string.Join(" ", Variable.punctuation);
            inputarr[2] = string.Join(" ", Variable.numb);
            inputarr[3] = "----------------------***----------------------";
            inputarr[4] = "----------------------***----------------------";


            for (int i = 0; i < length; i++)
            {
                inputarr[i + info + 2] = Variable.rootarr[i].ToString();
                // Console.WriteLine("~" + Variable.rootarr[i]);
                for (int j = 0; j < Variable.brancharr[i].Count; j++)
                {
                    inputarr[i + info + 2] += "~" + Variable.brancharr[i][j].ToString();
                    // Console.WriteLine(Variable.brancharr[i][j]);
                }

            }

            inputarr[info + 2 + length + 0] = "----------------------***----------------------";
            inputarr[info + 2 + length + 1] = "----------------------***----------------------";

            for (int i = 0; i < length; i++)
            {
                inputarr[i + (info + 2 + length + 2)] = Variable.popularity[i][0].ToString();

                for (int j = 1; j < Variable.popularity[i].Count; j++)
                {
                    inputarr[i + (info + 2 + length + 2)] += "~" + Variable.popularity[i][j].ToString();
                }

            }


            string input = string.Join("\n", inputarr);
            
            File.WriteAllText(Variable.storageFile, input);
        }

        // Đọc file text
        public static bool ReadFile()
        {
            string output = File.ReadAllText(Variable.storageFile);

            if (output == "")
                return false;

            string[] separatingStrings = { "\n", "\r\n" };
            string[] outputarr = output.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);

            // Dòng info dữ liệu có 3 dòng
            int info = 3;

            Variable.ver = outputarr[0];
            Variable.punctuation = outputarr[1].Split(' ');
            Variable.numb = outputarr[2].Split(' ');
            // outputarr[3 và 4] là để làm mốc chia tách 2 phần

            Variable.rootarr = new ArrayList();
            int length = (outputarr.Length - 2 - 2 - info) / 2;

            for (int i = 0; i < length; i++)
            {
                string a = outputarr[i + info + 2];
                string[] temp = a.Split('~');
                Variable.rootarr.Add(temp[0]);

                ArrayList TempArraylist = new ArrayList();
                for (int j = 1; j < temp.Length; j++)
                {
                    TempArraylist.Add(temp[j]);
                }
                Variable.brancharr.Add(TempArraylist);
            }

            // outputarr[length + 5 và length + 6 ] là để làm mốc chia tách 2 phần
            for (int i = 0; i < length; i++)
            {
                string a = outputarr[i + length + info + 2 + 2];
                string[] temp = a.Split('~');

                ArrayList TempArraylist = new ArrayList();
                for (int j = 0; j < temp.Length; j++)
                {
                    TempArraylist.Add(temp[j]);
                }
                Variable.popularity.Add(TempArraylist);
            }
            return true;
        }

        // Lấy dữ liệu đóng góp từ file và xử lý lấy chuỗi
        public static bool GetDataAndContribute(int max)
        {
            // read file
            string output = File.ReadAllText(Variable.contributeFile);

            if (output == "")
                return false;
            else
            {
                string[] separatingStrings = { "\n", "\r\n" };
                string[] ContributeArr = output.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);

                int bound = ContributeArr.Length > max ? max : ContributeArr.Length;

                for (int i = 0; i < bound; i++)
                {
                    Variable.ArrStr = StringHandling.GetDataFormString(ContributeArr[i], true);
                    Contribute.ControlContribute(Variable.ArrStr);
                }

                // Ghi dữ liệu
                WriteFile();

                //i < outputarr.Length & i < max

                // write lại file contribute
                string input = "";
                if (ContributeArr.Length > max)
                {
                    input = ContributeArr[bound];

                    for (int i = bound; i < ContributeArr.Length; i++)
                        input += "\n" + ContributeArr[i];
                }

                File.WriteAllText(Variable.contributeFile, input);


                return true;
            }
        }


    }

}
