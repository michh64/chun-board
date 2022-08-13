using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ChunBoard
{
    public partial class GUI : Form
    {
        public GUI()
        {
            InitializeComponent();

            KeyPreview = true;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Storage.ReadFile();
            Storage.WriteFile();

            textBox1.Text = "\r\n" + SelfChecking.Version(); // khởi tạo version màn hình chính
            
            // Khởi tạp màn hình home
            button6.Visible = false;
            tabControl1.SelectTab(0);

            // Khởi tạo Nhập liệu thông minh
            string[] str = SmartTyping.SearchPopular(textBox4.Text);

            label31.Text = StringHandling.TextCenter(str[0]);
            label32.Text = StringHandling.TextCenter(str[1]);
            label33.Text = StringHandling.TextCenter(str[2]);

            label34.Visible = false;
            label35.Visible = false;
            label36.Visible = false;

            label28.Visible = false;
            label29.Visible = false;
            label30.Visible = false;
            label20.Visible = false;

            //label34.Text = StringHandling.TextCenter(str[3]);
            //label35.Text = StringHandling.TextCenter(str[4]);
            //label36.Text = StringHandling.TextCenter(str[5]);

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                SelfChecking.RestoreBackup("Contribute");
                Contribute.ContributeData(textBox3.Text.ToLower());
                MessageBox.Show("Đóng góp dữ liệu thành công");
            }
            else
                MessageBox.Show("Không có dữ liệu");
        }


        private void button8_Click(object sender, EventArgs e)
        {
            textBox3.Text = "...LOADING...";
            textBox3.Enabled = false;
            button7.Enabled = false;

            SelfChecking.RestoreBackup("Contribute");

            if (Storage.GetDataAndContribute(5000) == true)
                MessageBox.Show("Đóng góp dữ liệu thành công");
            else
            {
                MessageBox.Show("Không có dữ liệu");
                File.Delete(SelfChecking.GetNewPathFileBackup());
            }


            textBox3.Enabled = true;
            textBox3.Text = "";
            button7.Enabled = true;
        }

        // Hướng dẫn sử dụng
        private void button13_Click(object sender, EventArgs e)
        {
            // Hướng dẫn sử dụng
            MessageBox.Show("Chức năng bảo trì");
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

            string[] str = SmartTyping.SearchPopular(textBox4.Text);

            label31.Text = StringHandling.TextCenter(str[0]);
            label32.Text = StringHandling.TextCenter(str[1]);
            label33.Text = StringHandling.TextCenter(str[2]);

            label34.Text = StringHandling.TextCenter(str[3]);
            label35.Text = StringHandling.TextCenter(str[4]);
            label36.Text = StringHandling.TextCenter(str[5]);
        }

        private void ChoosingTyping (string text)
        {
            text = text.Trim();
            if (text != "")
            {
                string[] data = textBox4.Text.Split(' ');
                data[data.Length - 1] = text;
                textBox4.Text = string.Join(" ", data) + " ";
                textBox4.SelectionStart = textBox4.Text.Length;
            }

        }

        #region Chuyển tab menu
        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1); // hệ đếm từ 0
            button6.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(2);
            button6.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(3);
            button6.Visible = true;
        }

        // Thông tin hệ thống
        private void button5_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(4);
            button6.Visible = true;

            // tab Info
            label10.Text = SelfChecking.Version();
            label11.Text = SelfChecking.DataSize(1);
            label12.Text = SelfChecking.DataSize(2);
            label13.Text = SelfChecking.DataSize(3);
            label24.Text = SelfChecking.DataSize(4);
            label26.Text = SelfChecking.SelfCheckData();
        }

        // Nút back
        private void button6_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(0);
            textBox1.Text = "\r\n" + SelfChecking.Version(); // version màn hình chính

            button6.Visible = false;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(5);
            button6.Visible = true;
        }

        #endregion

        #region chức năng chọn từ của SmartTyping
        private void label31_Click(object sender, EventArgs e)
        {
            ChoosingTyping(label31.Text);
        }

        private void label32_Click(object sender, EventArgs e)
        {
            ChoosingTyping(label32.Text);
        }

        private void label33_Click(object sender, EventArgs e)
        {
            ChoosingTyping(label33.Text);
        }
        #endregion

        // Phím tắt
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            // Tạo phím tắt SmartTyping
            if (e.Control)
            {
                if (e.KeyCode.Equals(Keys.NumPad1) || e.KeyCode.Equals(Keys.D1))
                {
                    ChoosingTyping(label31.Text);
                }

                if (e.KeyCode.Equals(Keys.NumPad2) || e.KeyCode.Equals(Keys.D2))
                {
                    ChoosingTyping(label32.Text);
                }

                if (e.KeyCode.Equals(Keys.NumPad3) || e.KeyCode.Equals(Keys.D3))
                {
                    ChoosingTyping(label33.Text);
                }

            }

            // Phím tắt cho nút Back
            if (e.Alt)
            {
                if (e.KeyCode.Equals(Keys.Left))
                {
                    button6_Click(sender, e);
                }
            }
        }

        #region Nút của SelfChecking
        private void button15_Click(object sender, EventArgs e)
        {
            SelfChecking.SelfRepairDuplicate(0);
            MessageBox.Show("Sửa lỗi root thành công");
        }

        private void button16_Click(object sender, EventArgs e)
        {
            SelfChecking.SelfRepairDuplicate(1);
            MessageBox.Show("Sửa lỗi branch thành công");
        }

        private void button17_Click(object sender, EventArgs e)
        {
            SelfChecking.SortAllData();
            MessageBox.Show("Sắp xếp thành công");
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Điều này sẽ xóa toàn bộ dữ liệu kí tự lỗi bị bỏ qua. File backup sẽ được lưu. Bạn vẫn muốn tiếp tục ?", "Cảnh báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                SelfChecking.RestoreBackup("FixSpecError");

                SelfChecking.SpecError();
                MessageBox.Show("Xóa kí tự lỗi bỏ qua thành công");
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (textBox6.Visible == false)
                textBox6.Visible = true;
            else
            {
                if (textBox6.Text != "")
                    if (MessageBox.Show("Điều này sẽ xóa toàn bộ dữ liệu chứa từ đặc biệt. File backup sẽ được lưu. Bạn vẫn muốn tiếp tục ?", "Cảnh báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        SelfChecking.RestoreBackup("DeleteSpecKey");

                        SelfChecking.DeleteSpecKey(textBox6.Text);
                        MessageBox.Show("Xóa kí tự đặc biệt thành công");
                    }

                textBox6.Visible = false;
                textBox6.Text = "";

            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            SelfChecking.RestoreBackup("RestoreBackup");
            MessageBox.Show("Tạo file backup thành công");

        }

        private void button21_Click(object sender, EventArgs e)
        {
            //SelfChecking.RestoreBackup("DeleteErrorWord");

            //SelfChecking.DeleteErrorWord();
            //MessageBox.Show("Xóa dữ liệu lỗi thành công");
            MessageBox.Show("Chức năng bảo trì");
        }

        #endregion

    }
}
