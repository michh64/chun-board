using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChunBoard
{
    internal class Restore_Code
    {
        #region Hiển thị danh sách checkedListBox1
        /*        private void DisplayErr(ArrayList Error, string[] ArrStr, int display, int start)
                {
                    if (Variable.index == 0)
                        button11.Enabled = false;
                    else
                        button11.Enabled = true;

                    if ((Variable.index + 1) * Variable.display * 3 < Variable.Error.Count)
                        button12.Enabled = true;
                    else
                        button12.Enabled = false;


                    int i = start;

                    checkedListBox1.Items.Clear();
                    checkedListBox2.Items.Clear();
                    checkedListBox3.Items.Clear();
                    for (int j = 0; j < display && i + j < Error.Count; j++)
                    {
                        checkedListBox1.Items.Add(ArrStr[(int)Error[i + j]]);
                        checkedListBox1.SetItemChecked(j, Variable.CheckList[i + j]);

                        if (Error.Count > i + j + display)
                        {
                            checkedListBox2.Items.Add(ArrStr[(int)Error[i + j + display]]);
                            checkedListBox2.SetItemChecked(j, Variable.CheckList[i + j + display]);
                        }

                        if (Error.Count > i + j + display * 2)
                        {
                            checkedListBox3.Items.Add(ArrStr[(int)Error[i + j + display * 2]]);
                            checkedListBox3.SetItemChecked(j, Variable.CheckList[i + j + display * 2]);
                        }

                    }
                }
        */
        #endregion

        #region Lưu trữ check của các checkedListBox
        /*        private void SaveChecked()
                {
                    int display = Variable.display;
                    int index = Variable.index;

                    for (int i = 0; i < Variable.display; i++)
                    {
                        Variable.CheckList[index * display * 3 + i] = checkedListBox1.GetItemChecked(i);

                        if (checkedListBox2.Items.Count > i)
                            Variable.CheckList[index * display * 3 + display + i] = checkedListBox2.GetItemChecked(i);

                        if (checkedListBox3.Items.Count > i)
                            Variable.CheckList[index * display * 3 + display * 2 + i] = checkedListBox3.GetItemChecked(i);
                    }

                }
        */
        #endregion

        #region Control: lưu check, Ẩn GroupBox, bắt đầu thực hiện việc với các từ được check
        /*        private void button10_Click(object sender, EventArgs e)
                {
                    SaveChecked();
                    groupBox1.Visible = false;

                    for (int i = Variable.Error.Count - 1; i >= 0; i--)
                    {
                        if (Variable.CheckList[i] == false)
                        {
                            Variable.Error.RemoveAt(i);
                        }
                    }

                    AddControl();
                }
        */

        // Chuyển trước sau của các view trong các checkedListBox
        /*        private void button11_Click(object sender, EventArgs e)
                {
                    SaveChecked();
                    Variable.index--;

                    DisplayErr(Variable.Error, Variable.ArrStr, Variable.display, Variable.index * Variable.display * 3);
                }

                private void button12_Click(object sender, EventArgs e)
                {
                    SaveChecked();
                    Variable.index++;

                    if (Variable.index == 0)
                        button11.Enabled = false;
                    else
                        button11.Enabled = true;

                    if ((Variable.index + 1) * Variable.display * 3 < Variable.Error.Count)
                        button12.Enabled = true;
                    else
                        button12.Enabled = false;

                    DisplayErr(Variable.Error, Variable.ArrStr, Variable.display, Variable.index * Variable.display * 3);
                }
        */
        #endregion


        #region In Danh sách từ kiểm tra và thêm - Chả hiểu lắm. Xưa m code cái đ gì đấy
        /*            if (Variable.Error.Count != 0)
                    {
                        for (int i = 0; i < Variable.Error.Count; i++)
                        {
                            Variable.CheckList[i] = true;
                        }

                        groupBox1.Visible = Enabled;
                        Variable.index = 0;
                        DisplayErr(Variable.Error, Variable.ArrStr, Variable.display, Variable.index * Variable.display * 3);
                    }
                    else
                        AddControl();
        */
        #endregion

        #region Kiểm tra chính tả lại, sau khi được đóng góp các từ đúng chính tả
        //Error = new ArrayList();
        //Internal.SpellCheck(Error, ArrStr, 0);
        //Internal.SpellCheck(Error, ArrStr, 1);

        //Error.Sort();

        #endregion

        #region In dữ liệu nhập vào và định dạng theo danh sách từ sai chính tả
        /*            public static void Print(string[] ArrStrData, ArrayList ArrFault)
                    {
                        int j = 0;
                        for (int i = 0; i < ArrStrData.Length; i++)
                        {
                            if (CompareArr(ArrStrData[i]) != 0 && CompareArr(ArrStrData[i]) != 2)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                switch (CompareArr(ArrStrData[i]))
                                {
                                    case 1: Console.Write(ArrStrData[i]); if (i + 1 < ArrStrData.Length && ArrStrData[i + 1] != ".") Console.Write(" "); break;
                                    case 3: if (!(i == 0 || CompareArr(ArrStrData[i - 1]) == 1)) Console.Write(" "); Console.Write(ArrStrData[i]); break;
                                    case 4: Console.Write(ArrStrData[i]); if (i + 1 >= ArrStrData.Length || CompareArr(ArrStrData[i + 1]) != 1) Console.Write(" "); break;
                                    case 5:
                                        if (i != 0 && (CompareArr(ArrStrData[i - 1]) != 1 && CompareArr(ArrStrData[i - 1]) != 5)) Console.Write(" ");
                                        Console.Write(ArrStrData[i]);
                                        if (i + 1 < ArrStrData.Length && CompareArr(ArrStrData[i + 1]) == 0) Console.Write(" ");
                                        break;
                                }
                            }
                            else
                            {
                                if (ArrFault.Count <= j || i != (int)ArrFault[j])
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                                    Console.Write(ArrStrData[i]);
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    Console.Write(ArrStrData[i] + " " + ArrStrData[i + 1]);
                                    j++;
                                    i++;
                                }

                                if (i + 1 < ArrStrData.Length)
                                    if ((CompareArr(ArrStrData[i]) == 0 && CompareArr(ArrStrData[i + 1]) == 0) ||
                                        (CompareArr(ArrStrData[i]) == 0 && CompareArr(ArrStrData[i + 1]) == 2) ||
                                        (CompareArr(ArrStrData[i]) == 2 && CompareArr(ArrStrData[i + 1]) == 0))
                                    {
                                        Console.Write(" ");
                                    }
                            }
                        }

                        Console.ResetColor();
                        Console.WriteLine();

                    }
        */
        #endregion

    }
}
