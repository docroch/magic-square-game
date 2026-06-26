using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace magic_square_game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            try
            {
                // ۱. تعریف آرایه و گرفتن مقادیر
                int[,] grid = new int[3, 3];
                grid[0, 0] = int.Parse(txt1.Text); grid[0, 1] = int.Parse(txt2.Text); grid[0, 2] = int.Parse(txt3.Text);
                grid[1, 0] = int.Parse(txt4.Text); grid[1, 1] = int.Parse(txt5.Text); grid[1, 2] = int.Parse(txt6.Text);
                grid[2, 0] = int.Parse(txt7.Text); grid[2, 1] = int.Parse(txt8.Text); grid[2, 2] = int.Parse(txt9.Text);

                // ۲. بررسی تکراری نبودن (استفاده از HashSet)
                HashSet<int> numbers = new HashSet<int>();
                for (int r = 0; r < 3; r++)
                    for (int c = 0; c < 3; c++)
                        if (grid[r, c] < 1 || grid[r, c] > 9 || !numbers.Add(grid[r, c]))
                        {
                            lblStatus.Text = "اعداد باید بین ۱ تا ۹ و غیرتکراری باشند!";
                            return;
                        }

                // ۳. بررسی جمع‌ها
                bool isMagic = true;
                // جمع سطرها، ستون‌ها و قطرها رو چک می‌کنیم
                if ((grid[0, 0] + grid[0, 1] + grid[0, 2] != 15) || // سطر ۱
                    (grid[1, 0] + grid[1, 1] + grid[1, 2] != 15) || // سطر ۲
                    (grid[2, 0] + grid[2, 1] + grid[2, 2] != 15) || // سطر ۳
                    (grid[0, 0] + grid[1, 0] + grid[2, 0] != 15) || // ستون ۱
                    (grid[0, 1] + grid[1, 1] + grid[2, 1] != 15) || // ستون ۲
                    (grid[0, 2] + grid[1, 2] + grid[2, 2] != 15) || // ستون ۳
                    (grid[0, 0] + grid[1, 1] + grid[2, 2] != 15) || // قطر اصلی
                    (grid[0, 2] + grid[1, 1] + grid[2, 0] != 15))   // قطر فرعی
                {
                    isMagic = false;
                }

                if (isMagic)
                {
                    lblStatus.ForeColor = Color.Green;
                    lblStatus.Text = "تبریک! این یک مربع جادویی است.";
                    MessageBox.Show("شما برنده شدید!");
                }
                else
                {
                    lblStatus.ForeColor = Color.Red;
                    lblStatus.Text = "جمع سطرها یا ستون‌ها ۱۵ نیست. دوباره تلاش کنید.";
                }
            }
            catch
            {
                MessageBox.Show("لطفاً تمام خانه‌ها را با عدد پر کنید.");
            }
        }

        private void txt1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
            // پاسخ استاندارد مربع جادویی ۳در۳ طبق الگوریتم سیامی
            txt1.Text = "8"; txt2.Text = "1"; txt3.Text = "6";
            txt4.Text = "3"; txt5.Text = "5"; txt6.Text = "7";
            txt7.Text = "4"; txt8.Text = "9"; txt9.Text = "2";

            lblStatus.ForeColor = Color.Blue;
            lblStatus.Text = "مربع توسط هوش کامپیوتر حل شد.";
            this.BackColor = SystemColors.Control;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // پاک کردن متن تمام تکست‌باکس‌ها
            txt1.Clear(); txt2.Clear(); txt3.Clear();
            txt4.Clear(); txt5.Clear(); txt6.Clear();
            txt7.Clear(); txt8.Clear(); txt9.Clear();

            // برگرداندن وضعیت فرم به حالت اولیه
            lblStatus.Text = "";
            this.BackColor = SystemColors.Control;
        }
    }
}
