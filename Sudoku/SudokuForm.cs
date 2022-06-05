using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku
{
    public partial class SudokuForm : Form
    {
        private Sudoku game = new Sudoku();
        private Random random = new Random();

        public SudokuForm()
        {
            InitializeComponent();
            Load += Form1_Load;
            btnNew.Click += btnNew_Click;
            btnSolution.Click += btnSolution_Click;
            DataGridView1.Paint += DataGridView1_Paint;
            ComboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            game.ShowClues += game_ShowClues;
            game.ShowSolution += game_ShowSolution;
            game.ClearInputs += game_ClearInputs;
            game.VerifyInputs += game_VerifyInputs;
        }

        private void Form1_Load(System.Object sender, System.EventArgs e)
        {
            DataGridView1.Rows.Add(9);
            ComboBox1.SelectedIndex = 0;
            btnNew.PerformClick();
        }

        private void btnNew_Click(System.Object sender, System.EventArgs e)
        {
            game.NewGame(random);
        }

        private void DataGridView1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(Color.Black, 1), 120, 0, 120, 450);
            e.Graphics.DrawLine(new Pen(Color.Black, 1), 240, 0, 240, 450);
            e.Graphics.DrawLine(new Pen(Color.Black, 1), 0, 122, 485, 122);
            e.Graphics.DrawLine(new Pen(Color.Black, 1), 0, 244, 485, 244);
        }

        private void btnSolution_Click(System.Object sender, System.EventArgs e)
        {
            game.showGridSolution();
        }

        private void ComboBox1_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            btnNew.PerformClick();
        }

        public void game_ShowClues(int[][] grid)
        {
            for (int y = 0; y <= 8; y++)
            {
                List<int> cells = new List<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
                for (int c = 1; c <= 9 - (5 - ComboBox1.SelectedIndex); c++)
                {
                    int randomNumber = cells[random.Next(0, cells.Count())];
                    cells.Remove(randomNumber);
                }
                for (int x = 0; x <= 8; x++)
                {
                    if (cells.Contains(x + 1))
                    {
                        DataGridView1.Rows[y].Cells[x].Value = grid[y][x];
                        DataGridView1.Rows[y].Cells[x].Style.ForeColor = Color.Black;
                        DataGridView1.Rows[y].Cells[x].ReadOnly = true;
                    }
                    else
                    {
                        DataGridView1.Rows[y].Cells[x].Value = "";
                        DataGridView1.Rows[y].Cells[x].Style.ForeColor = Color.Blue;
                        DataGridView1.Rows[y].Cells[x].ReadOnly = false;
                    }
                }
            }
        }

        public void game_ShowSolution(int[][] grid)
        {
            for (int y = 0; y <= 8; y++)
            {
                for (int x = 0; x <= 8; x++)
                {
                    if (DataGridView1.Rows[y].Cells[x].Style.ForeColor != Color.Black)
                    {
                        if (string.IsNullOrEmpty(DataGridView1.Rows[y].Cells[x].Value.ToString()))
                        {
                            DataGridView1.Rows[y].Cells[x].Style.ForeColor = Color.Green;
                            DataGridView1.Rows[y].Cells[x].Value = grid[y][x];
                        }
                        else
                        {
                            if (grid[y][x].ToString() != DataGridView1.Rows[y].Cells[x].Value.ToString())
                            {
                                DataGridView1.Rows[y].Cells[x].Style.ForeColor = Color.Green;
                                DataGridView1.Rows[y].Cells[x].Value = grid[y][x];
                            }
                        }
                    }
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            game.clearInputs();
        }

        public void game_ClearInputs(int[][] grid)
        {
            for (int y = 0; y <= 8; y++)
            {
                for (int x = 0; x <= 8; x++)
                {
                    if (DataGridView1.Rows[y].Cells[x].Style.ForeColor != Color.Black)
                    {
                        if (!string.IsNullOrEmpty(DataGridView1.Rows[y].Cells[x].Value.ToString()))
                        {
                            DataGridView1.Rows[y].Cells[x].Value = string.Empty;
                            DataGridView1.Rows[y].Cells[x].Style.ForeColor = Color.Blue;
                        }
                    }
                }
            }
        }

        public void game_VerifyInputs(int[][] grid)
        {
            for (int y = 0; y <= 8; y++)
            {
                for (int x = 0; x <= 8; x++)
                {
                    if (DataGridView1.Rows[y].Cells[x].Style.ForeColor != Color.Black)
                    {
                        if (grid[y][x].ToString() == DataGridView1.Rows[y].Cells[x].Value.ToString())
                        {
                            DataGridView1.Rows[y].Cells[x].Style.ForeColor = Color.Green;
                        }
                        else if (!string.IsNullOrEmpty(DataGridView1.Rows[y].Cells[x].Value.ToString()))
                        {
                            DataGridView1.Rows[y].Cells[x].Style.ForeColor = Color.Red;
                        }
                    }
                }
            }
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            game.verifyInputs();
        }
    }
}
