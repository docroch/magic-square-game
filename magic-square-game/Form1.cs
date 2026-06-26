using System;
using System.Drawing;
using System.Windows.Forms;

namespace magic_square_game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // دکمه بررسی نتیجه
        private void btnCheck_Click(object sender, EventArgs e)
        {
            // بررسی خالی نبودن خانه‌ها
            if (txt1.Text == "" || txt2.Text == "" || txt3.Text == "" ||
                txt4.Text == "" || txt5.Text == "" || txt6.Text == "" ||
                txt7.Text == "" || txt8.Text == "" || txt9.Text == "")
            {
                lblStatus.Text = "لطفاً تمام خانه‌ها را با عدد پر کنید.";
                return;
            }

            int[,] grid = new int[3, 3];
            grid[0, 0] = int.Parse(txt1.Text); grid[0, 1] = int.Parse(txt2.Text); grid[0, 2] = int.Parse(txt3.Text);
            grid[1, 0] = int.Parse(txt4.Text); grid[1, 1] = int.Parse(txt5.Text); grid[1, 2] = int.Parse(txt6.Text);
            grid[2, 0] = int.Parse(txt7.Text); grid[2, 1] = int.Parse(txt8.Text); grid[2, 2] = int.Parse(txt9.Text);

            // بررسی بازه اعداد ۱ تا ۹
            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    if (grid[r, c] < 1 || grid[r, c] > 9)
                    {
                        lblStatus.ForeColor = Color.Red;
                        lblStatus.Text = "اعداد باید بین ۱ تا ۹ باشند!";
                        return;
                    }
                }
            }

            // بررسی تکراری نبودن اعداد
            for (int i = 0; i < 9; i++)
            {
                for (int j = i + 1; j < 9; j++)
                {
                    int r1 = i / 3; int c1 = i % 3;
                    int r2 = j / 3; int c2 = j % 3;

                    if (grid[r1, c1] == grid[r2, c2])
                    {
                        lblStatus.ForeColor = Color.Red;
                        lblStatus.Text = "اعداد وارد شده نباید تکراری باشند!";
                        return;
                    }
                }
            }

            // بررسی مجموع سطرها، ستون‌ها و قطرها
            bool isMagic = true;

            if ((grid[0, 0] + grid[0, 1] + grid[0, 2] != 15) || // سطر 1
                (grid[1, 0] + grid[1, 1] + grid[1, 2] != 15) || // سطر 2
                (grid[2, 0] + grid[2, 1] + grid[2, 2] != 15) || // سطر 3
                (grid[0, 0] + grid[1, 0] + grid[2, 0] != 15) || // ستون 1
                (grid[0, 1] + grid[1, 1] + grid[2, 1] != 15) || // ستون 2
                (grid[0, 2] + grid[1, 2] + grid[2, 2] != 15) || // ستون 3
                (grid[0, 0] + grid[1, 1] + grid[2, 2] != 15) || // قطر اصلی
                (grid[0, 2] + grid[1, 1] + grid[2, 0] != 15))   // قطر فرعی
            {
                isMagic = false;
            }

            if (isMagic)
            {
                lblStatus.ForeColor = Color.Green;
                lblStatus.Text = "تبریک! این یک مربع جادویی است.";
            }
            else
            {
                lblStatus.ForeColor = Color.Red;
                lblStatus.Text = "جمع سطرها یا ستون‌ها ۱۵ نیست. دوباره تلاش کنید.";
            }
        }

        // کنترل ورودی تکست‌باکس‌ها
        private void txt1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && (!char.IsDigit(e.KeyChar) || e.KeyChar == '0'))
            {
                e.Handled = true;
            }
        }

        // دکمه حل خودکار هوشمند
        private void btnSolve_Click(object sender, EventArgs e)
        {
            int[,] grid = new int[3, 3];
            bool[,] isUserDefined = new bool[3, 3];

            // ماتریس کمکی برای بررسی تکراری‌ها
            bool[] usedNumbers = new bool[10];

            // پر کردن اطلاعات از تکست‌باکس‌ها
            string[] texts = { txt1.Text, txt2.Text, txt3.Text, txt4.Text, txt5.Text, txt6.Text, txt7.Text, txt8.Text, txt9.Text };

            for (int i = 0; i < 9; i++)
            {
                int r = i / 3;
                int c = i % 3;

                if (texts[i] != "")
                {
                    int val = int.Parse(texts[i]);

                    // چک کردن تکراری بودن ورودی کاربر
                    if (usedNumbers[val] == true)
                    {
                        lblStatus.ForeColor = Color.Red;
                        lblStatus.Text = "اعداد وارد شده توسط شما تکراری هستند!";
                        return;
                    }
                    grid[r, c] = val;
                    isUserDefined[r, c] = true;
                    usedNumbers[val] = true; // علامت زدن عدد به عنوان استفاده شده
                }
                else
                {
                    grid[r, c] = 0;
                    isUserDefined[r, c] = false;
                }
            }

            // اجرای تابع حل جدول
            if (SolveMagicSquare(grid, isUserDefined, usedNumbers, 0, 0))
            {
                // نمایش نتایج و رنگ‌بندی ساده
                txt1.Text = grid[0, 0].ToString(); txt1.ForeColor = isUserDefined[0, 0] ? Color.Black : Color.Blue;
                txt2.Text = grid[0, 1].ToString(); txt2.ForeColor = isUserDefined[0, 1] ? Color.Black : Color.Blue;
                txt3.Text = grid[0, 2].ToString(); txt3.ForeColor = isUserDefined[0, 2] ? Color.Black : Color.Blue;
                txt4.Text = grid[1, 0].ToString(); txt4.ForeColor = isUserDefined[1, 0] ? Color.Black : Color.Blue;
                txt5.Text = grid[1, 1].ToString(); txt5.ForeColor = isUserDefined[1, 1] ? Color.Black : Color.Blue;
                txt6.Text = grid[1, 2].ToString(); txt6.ForeColor = isUserDefined[1, 2] ? Color.Black : Color.Blue;
                txt7.Text = grid[2, 0].ToString(); txt7.ForeColor = isUserDefined[2, 0] ? Color.Black : Color.Blue;
                txt8.Text = grid[2, 1].ToString(); txt8.ForeColor = isUserDefined[2, 1] ? Color.Black : Color.Blue;
                txt9.Text = grid[2, 2].ToString(); txt9.ForeColor = isUserDefined[2, 2] ? Color.Black : Color.Blue;

                lblStatus.ForeColor = Color.Green;
                lblStatus.Text = "جدول با موفقیت تکمیل شد!\n(برای استفاده مجدد ابتدا دکمه 'پاک کردن' را بزنید)";
            }
            else
            {
                lblStatus.ForeColor = Color.Red;
                lblStatus.Text = "با اعداد فعلی شما، هیچ راه حلی وجود ندارد.\n(برای شروع مجدد 'پاک کردن' را بزنید)";
            }
        }

        // تابع بازگشتی حل جدول
        private bool SolveMagicSquare(int[,] grid, bool[,] isUserDefined, bool[] usedNumbers, int row, int col)
        {
            if (row == 3)
            {
                return IsSquareValid(grid);
            }

            int nextRow = (col == 2) ? row + 1 : row;
            int nextCol = (col == 2) ? 0 : col + 1;

            if (isUserDefined[row, col])
            {
                return SolveMagicSquare(grid, isUserDefined, usedNumbers, nextRow, nextCol);
            }

            for (int num = 1; num <= 9; num++)
            {
                if (usedNumbers[num] == false)
                {
                    grid[row, col] = num;
                    usedNumbers[num] = true;

                    if (SolveMagicSquare(grid, isUserDefined, usedNumbers, nextRow, nextCol))
                        return true;

                    grid[row, col] = 0;
                    usedNumbers[num] = false;
                }
            }

            return false;
        }

        // تابع بررسی معتبر بودن جمع‌ها
        private bool IsSquareValid(int[,] mat)
        {
            for (int i = 0; i < 3; i++)
                if (mat[i, 0] + mat[i, 1] + mat[i, 2] != 15) return false;

            for (int i = 0; i < 3; i++)
                if (mat[0, i] + mat[1, i] + mat[2, i] != 15) return false;

            if (mat[0, 0] + mat[1, 1] + mat[2, 2] != 15) return false;
            if (mat[0, 2] + mat[1, 1] + mat[2, 0] != 15) return false;

            return true;
        }

        // دکمه پاک کردن و ریست جدول
        private void btnReset_Click(object sender, EventArgs e)
        {
            txt1.Clear(); txt2.Clear(); txt3.Clear();
            txt4.Clear(); txt5.Clear(); txt6.Clear();
            txt7.Clear(); txt8.Clear(); txt9.Clear();

            txt1.ForeColor = Color.Black; txt2.ForeColor = Color.Black; txt3.ForeColor = Color.Black;
            txt4.ForeColor = Color.Black; txt5.ForeColor = Color.Black; txt6.ForeColor = Color.Black;
            txt7.ForeColor = Color.Black; txt8.ForeColor = Color.Black; txt9.ForeColor = Color.Black;
        }
    }
}