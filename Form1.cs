using System;
using System.Drawing;
using System.Windows.Forms;

namespace ColorListBoxLab
{
    public partial class Form1 : Form
    {
        private Panel panelTop;
        private Panel panelBottom;
        private ComboBox comboBoxColors;
        private Button btnAdd;
        private Button btnDelete;
        private Button btnMoveUp;
        private Button btnMoveDown;
        private ListBox listBoxColors;
        private Label lblTop;
        private Label lblBottom;

        public Form1()
        {
            InitializeCustomComponents();
            InitializeColors();
            UpdateMoveButtonsState();
        }

        private void InitializeCustomComponents()
        {
            this.Text = "Лабораторная работа №1 - ListBox и ComboBox";
            this.Size = new Size(500, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = SystemColors.Control;

            panelTop = new Panel
            {
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(20, 20),
                Size = new Size(450, 50),
                BackColor = Color.White,
                Name = "panelTop"
            };

            lblTop = new Label
            {
                Text = "Цвет из ComboBox",
                Location = new Point(10, 15),
                Size = new Size(200, 20),
                Font = new Font("Arial", 9, FontStyle.Regular),
                ForeColor = Color.Gray
            };
            panelTop.Controls.Add(lblTop);

            comboBoxColors = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(20, 85),
                Size = new Size(200, 25),
                Font = new Font("Arial", 9, FontStyle.Regular),
                Name = "comboBoxColors"
            };
            comboBoxColors.SelectedIndexChanged += ComboBoxColors_SelectedIndexChanged;

            btnAdd = new Button
            {
                Text = "Add →",
                Location = new Point(240, 85),
                Size = new Size(100, 30),
                Font = new Font("Arial", 9, FontStyle.Bold),
                BackColor = Color.LightGreen,
                FlatStyle = FlatStyle.Flat,
                Name = "btnAdd"
            };
            btnAdd.Click += BtnAdd_Click;

            listBoxColors = new ListBox
            {
                Location = new Point(20, 130),
                Size = new Size(450, 150),
                Font = new Font("Arial", 9, FontStyle.Regular),
                BorderStyle = BorderStyle.FixedSingle,
                Name = "listBoxColors"
            };
            listBoxColors.SelectedIndexChanged += ListBoxColors_SelectedIndexChanged;

            panelBottom = new Panel
            {
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(20, 290),
                Size = new Size(450, 50),
                BackColor = Color.White,
                Name = "panelBottom"
            };

            lblBottom = new Label
            {
                Text = "Цвет из ListBox",
                Location = new Point(10, 15),
                Size = new Size(200, 20),
                Font = new Font("Arial", 9, FontStyle.Regular),
                ForeColor = Color.Gray
            };
            panelBottom.Controls.Add(lblBottom);

            btnDelete = new Button
            {
                Text = "Delete",
                Location = new Point(20, 360),
                Size = new Size(100, 30),
                Font = new Font("Arial", 9, FontStyle.Bold),
                BackColor = Color.LightCoral,
                FlatStyle = FlatStyle.Flat,
                Name = "btnDelete"
            };
            btnDelete.Click += BtnDelete_Click;

            btnMoveUp = new Button
            {
                Text = "↑ Move Up",
                Location = new Point(140, 360),
                Size = new Size(100, 30),
                Font = new Font("Arial", 9, FontStyle.Bold),
                BackColor = Color.LightBlue,
                FlatStyle = FlatStyle.Flat,
                Name = "btnMoveUp"
            };
            btnMoveUp.Click += BtnMoveUp_Click;

            btnMoveDown = new Button
            {
                Text = "Move Down ↓",
                Location = new Point(260, 360),
                Size = new Size(100, 30),
                Font = new Font("Arial", 9, FontStyle.Bold),
                BackColor = Color.LightBlue,
                FlatStyle = FlatStyle.Flat,
                Name = "btnMoveDown"
            };
            btnMoveDown.Click += BtnMoveDown_Click;

            Button btnClearAll = new Button
            {
                Text = "Clear All",
                Location = new Point(380, 360),
                Size = new Size(90, 30),
                Font = new Font("Arial", 9, FontStyle.Regular),
                BackColor = Color.LightYellow,
                FlatStyle = FlatStyle.Flat
            };
            btnClearAll.Click += BtnClearAll_Click;

            Controls.AddRange(new Control[]
            {
                panelTop,
                comboBoxColors,
                btnAdd,
                listBoxColors,
                panelBottom,
                btnDelete,
                btnMoveUp,
                btnMoveDown,
                btnClearAll
            });
        }

        private void InitializeColors()
        {
            string[] colors = Enum.GetNames(typeof(KnownColor));

            foreach (string color in colors)
            {
                if (!color.Contains("Active") && !color.Contains("Inactive") &&
                    !color.Contains("Control") && !color.Contains("Window") &&
                    !color.Contains("Menu") && !color.Contains("Highlight") &&
                    !color.Contains("Desktop"))
                {
                    comboBoxColors.Items.Add(color);
                }
            }

            comboBoxColors.Sorted = true;

            if (comboBoxColors.Items.Count > 0)
            {
                comboBoxColors.SelectedIndex = 0;
            }
        }

        private void ComboBoxColors_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxColors.SelectedItem != null)
            {
                string colorName = comboBoxColors.SelectedItem.ToString();
                try
                {
                    Color color = Color.FromName(colorName);
                    panelTop.BackColor = color;
                    lblTop.ForeColor = GetContrastColor(color);
                    lblTop.Text = $"Цвет: {colorName}";
                }
                catch
                {
                    panelTop.BackColor = Color.White;
                }
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (comboBoxColors.SelectedItem != null && comboBoxColors.SelectedItem.ToString() != "")
            {
                string selectedColor = comboBoxColors.SelectedItem.ToString();

                if (!listBoxColors.Items.Contains(selectedColor))
                {
                    listBoxColors.Items.Add(selectedColor);

                    listBoxColors.SelectedIndex = listBoxColors.Items.Count - 1;

                    comboBoxColors.Items.Remove(selectedColor);

                    UpdateMoveButtonsState();

                    if (comboBoxColors.Items.Count > 0)
                    {
                        comboBoxColors.SelectedIndex = 0;
                    }
                    else
                    {
                        panelTop.BackColor = Color.White;
                        lblTop.Text = "Цвет из ComboBox";
                        lblTop.ForeColor = Color.Gray;
                    }
                }
                else
                {
                    MessageBox.Show("Этот цвет уже добавлен в список!", "Предупреждение",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void ListBoxColors_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxColors.SelectedItem != null)
            {
                string colorName = listBoxColors.SelectedItem.ToString();
                try
                {
                    Color color = Color.FromName(colorName);
                    panelBottom.BackColor = color;
                    lblBottom.ForeColor = GetContrastColor(color);
                    lblBottom.Text = $"Цвет: {colorName}";
                }
                catch
                {
                    panelBottom.BackColor = Color.White;
                }
            }
            UpdateMoveButtonsState();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (listBoxColors.SelectedIndex != -1)
            {
                int deleteIndex = listBoxColors.SelectedIndex;
                string deletedColor = listBoxColors.SelectedItem.ToString();

                listBoxColors.Items.RemoveAt(deleteIndex);

                comboBoxColors.Items.Add(deletedColor);
                comboBoxColors.Sorted = true;

                if (listBoxColors.Items.Count > 0)
                {
                    if (deleteIndex < listBoxColors.Items.Count)
                    {
                        listBoxColors.SelectedIndex = deleteIndex;
                    }
                    else
                    {
                        listBoxColors.SelectedIndex = listBoxColors.Items.Count - 1;
                    }
                }
                else
                {
                    panelBottom.BackColor = Color.White;
                    lblBottom.Text = "Цвет из ListBox";
                    lblBottom.ForeColor = Color.Gray;
                }

                UpdateMoveButtonsState();
            }
        }

        private void BtnMoveUp_Click(object sender, EventArgs e)
        {
            if (listBoxColors.SelectedIndex > 0)
            {
                int selectedIndex = listBoxColors.SelectedIndex;
                object selectedItem = listBoxColors.SelectedItem;

                listBoxColors.Items.RemoveAt(selectedIndex);
                listBoxColors.Items.Insert(selectedIndex - 1, selectedItem);
                listBoxColors.SelectedIndex = selectedIndex - 1;

                UpdateMoveButtonsState();
            }
        }

        private void BtnMoveDown_Click(object sender, EventArgs e)
        {
            if (listBoxColors.SelectedIndex < listBoxColors.Items.Count - 1)
            {
                int selectedIndex = listBoxColors.SelectedIndex;
                object selectedItem = listBoxColors.SelectedItem;

                listBoxColors.Items.RemoveAt(selectedIndex);
                listBoxColors.Items.Insert(selectedIndex + 1, selectedItem);
                listBoxColors.SelectedIndex = selectedIndex + 1;

                UpdateMoveButtonsState();
            }
        }

        private void UpdateMoveButtonsState()
        {
            if (listBoxColors.SelectedIndex == -1)
            {
                btnMoveUp.Enabled = false;
                btnMoveDown.Enabled = false;
                btnDelete.Enabled = false;
                return;
            }

            btnDelete.Enabled = true;

            btnMoveUp.Enabled = (listBoxColors.SelectedIndex > 0);

            btnMoveDown.Enabled = (listBoxColors.SelectedIndex < listBoxColors.Items.Count - 1);

            btnMoveUp.Text = btnMoveUp.Enabled ? "↑ Move Up" : "↑ (First)";
            btnMoveDown.Text = btnMoveDown.Enabled ? "Move Down ↓" : "(Last) ↓";
        }

        private void BtnClearAll_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Вернуть все цвета обратно в ComboBox?",
                "Очистка списка",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                foreach (var item in listBoxColors.Items)
                {
                    comboBoxColors.Items.Add(item);
                }

                listBoxColors.Items.Clear();
                comboBoxColors.Sorted = true;

                panelTop.BackColor = Color.White;
                panelBottom.BackColor = Color.White;
                lblTop.Text = "Цвет из ComboBox";
                lblBottom.Text = "Цвет из ListBox";
                lblTop.ForeColor = Color.Gray;
                lblBottom.ForeColor = Color.Gray;

                if (comboBoxColors.Items.Count > 0)
                {
                    comboBoxColors.SelectedIndex = 0;
                }

                UpdateMoveButtonsState();
            }
        }

        private Color GetContrastColor(Color color)
        {
            double luminance = (0.299 * color.R + 0.587 * color.G + 0.114 * color.B) / 255;

            return luminance > 0.5 ? Color.Black : Color.White;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            listBoxColors.DoubleClick += (s, args) =>
            {
                if (listBoxColors.SelectedItem != null)
                {
                    string colorName = listBoxColors.SelectedItem.ToString();
                    MessageBox.Show($"Выбран цвет: {colorName}\n" +
                        $"RGB: {Color.FromName(colorName).R}, " +
                        $"{Color.FromName(colorName).G}, " +
                        $"{Color.FromName(colorName).B}",
                        "Информация о цвете",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            };
        }
    }
}