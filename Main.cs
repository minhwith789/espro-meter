using ConstructionApp.DAL;
using ConstructionApp.Properties;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConstructionApp
{
    public partial class Main : Form
    {
        private Form activeForm = null;
        int dragRow = -1;
        Label dragLabel = null;
        private int childFormNumber = 0;
        private Rectangle dragBoxFromMouseDown;
        private int rowIndexFromMouseDown;
        private int rowIndexOfItemUnderMouseToDrop;


        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd,int wMsg,int wParam,int lParam);
        private void OpenChildForm(Form childForm,DockStyle style=DockStyle.None)
        {
            if(activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            activeForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock=style;
            panel2.Controls.Add(childForm);
            panelMenu.Tag = childForm;
            childForm.BringToFront();
            childForm.StartPosition = FormStartPosition.CenterScreen;
            childForm.Show();

        }



        //private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        //{
        //    if (e.ColumnIndex < 0 || e.RowIndex < 0) return;
        //    dragRow = e.RowIndex;
        //    if (dragLabel == null) dragLabel = new Label();
        //    dragLabel.Text = dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString();
        //    dragLabel.Parent = dataGridView1;
        //    dragLabel.Location = e.Location;
        //}
        //private void dataGridView1_MouseMove(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Left && dragLabel != null)
        //    {
        //        dragLabel.Location = e.Location;
        //        dataGridView1.ClearSelection();
        //    }
        //}

        //private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        //{
        //    var hit = dataGridView1.HitTest(e.X, e.Y);
        //    int dropRow = -1;
        //    if (hit.Type != DataGridViewHitTestType.None)
        //    {
        //        dropRow = hit.RowIndex;
        //        if (dragRow >= 0)
        //        {
        //            int tgtRow = dropRow + (dragRow > dropRow ? 1 : 0);
        //            if (tgtRow != dragRow)
        //            {
        //                DataGridViewRow row = dataGridView1.Rows[dragRow];
        //                dataGridView1.Rows.Remove(row);
        //                dataGridView1.Rows.Insert(tgtRow, row);

        //                dataGridView1.ClearSelection();
        //                row.Selected = true;
        //            }
        //        }
        //    }
        //    else { dataGridView1.Rows[dragRow].Selected = true; }

        //    if (dragLabel != null)
        //    {
        //        dragLabel.Dispose();
        //        dragLabel = null;
        //    }
        //}
        //        if (tgtRow != dragRow)
        //{
        //    DataRow dtRow = DT.Rows[dragRow];
        //        DataRow newRow = DT.NewRow();
        //        newRow.ItemArray = DT.Rows[dragRow].ItemArray; // we need to clone the values

        //    DT.Rows.Remove(dtRow);
        //    DT.Rows.InsertAt(newRow, tgtRow);
        //    dataGridView1.Refresh();
        //    dataGridView1.Rows[tgtRow].Selected = true;
        //}
        public Main()
        {
            InitializeComponent();
            panelMenu.Width = Properties.Settings.Default.menuSide;
            if (panelMenu.Width >= 200)
            {
                foreach (Button menuItem in panelMenu.Controls.OfType<Button>())
                {
                    btnMenu.IconChar = IconChar.Times;
                    btnMenu.IconColor = Color.White;
                    menuItem.Text = menuItem.Tag.ToString();
                    menuItem.ImageAlign = ContentAlignment.MiddleLeft;
                    menuItem.TextAlign = ContentAlignment.MiddleCenter;
                    menuItem.Padding = new Padding(10, 0, 0, 0);
                }
            }
            else
            {
                btnMenu.IconChar = IconChar.Bars;
                btnMenu.IconColor = Color.White;
                btnMenu.FlatStyle = FlatStyle.Flat;
                foreach (Button menuItem in panelMenu.Controls.OfType<Button>())
                {
                    menuItem.Text = "";
                    menuItem.ImageAlign = ContentAlignment.MiddleCenter;
                    menuItem.Padding = new Padding(0);
                }
            }

        }


        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*statusStrip.Visible = statusBarToolStripMenuItem.Checked;*/
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void dataGridView1_MouseMove(object sender, MouseEventArgs e)
        {

            //if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            //{
            //    // If the mouse moves outside the rectangle, start the drag.
            //    if (dragBoxFromMouseDown != Rectangle.Empty &&
            //    !dragBoxFromMouseDown.Contains(e.X, e.Y))
            //    {
            //        // Proceed with the drag and drop, passing in the list item.                    
            //        DragDropEffects dropEffect = dataGridView1.DoDragDrop(
            //              dataGridView1.Rows[rowIndexFromMouseDown],
            //              DragDropEffects.Move);
            //    }
            //}
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            //Get the index of the item the mouse is below.
           //rowIndexFromMouseDown = dataGridView1.HitTest(e.X, e.Y).RowIndex;

           // if (rowIndexFromMouseDown != -1)
           // {
           //     Size dragSize = SystemInformation.DragSize;

           //     dragBoxFromMouseDown = new Rectangle(
           //               new Point(
           //                 e.X - (dragSize.Width / 2),
           //                 e.Y - (dragSize.Height / 2)),
           //           dragSize);
           // }
           // else
           //     // Reset the rectangle if the mouse is not over an item in the ListBox.
           //     dragBoxFromMouseDown = Rectangle.Empty;
        }

        private void dataGridView1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void dataGridView1_DragDrop(object sender, DragEventArgs e)
        {


            //Point clientPoint = dataGridView1.PointToClient(new Point(e.X, e.Y));

            //// Get the row index of the item the mouse is below. 
            //rowIndexOfItemUnderMouseToDrop = dataGridView1.HitTest(clientPoint.X, clientPoint.Y).RowIndex;
            //// If the drag operation was a move then remove and insert the row.

            //if (e.Effect == DragDropEffects.Move)
            //{
            //    try
            //    {
            //        DataGridViewRow rowToMove = e.Data.GetData(typeof(DataGridViewRow)) as DataGridViewRow;
            //        if(rowIndexOfItemUnderMouseToDrop >= dataGridView1.Rows.Count)
            //        {
            //            return;
            //        }
            //        dataGridView1.Rows.RemoveAt(rowIndexFromMouseDown);
            //        dataGridView1.Rows.Insert(rowIndexOfItemUnderMouseToDrop, rowToMove);
            //    }
            //    catch 
            //    {

            //    }
               
            //}
        }
        private DataGridViewComboBoxColumn CreateComboCell()
        {
            DataGridViewComboBoxColumn combo = new DataGridViewComboBoxColumn();
            combo.FlatStyle = FlatStyle.Flat;
            combo.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
            combo.DataSource = DAL.Item.getAllItems("");
            combo.DisplayMember = "ItemName";
            combo.ValueMember = "Id";

            return combo;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            try
            {
                //    Configure config = new Configure("localhost", "mydb", "sa", "123");
                //    config.RegisterServer();
                ////    MessageBox.Show(config.Connection());
                //dbComboBox1.DataSource = DAL.Item.getAllItems("");
                //dbComboBox1.DisplayMember = "ItemName";
                //dbComboBox1.ValueMember = "Id";

                //((DataGridViewComboBoxColumn)dataGridView1.Columns["column2"]).DataSource= DAL.Item.getAllItems("");
                //((DataGridViewComboBoxColumn)dataGridView1.Columns["column2"]).ValueMember = "Id";
                //((DataGridViewComboBoxColumn)dataGridView1.Columns["column2"]).DisplayMember = "ItemName";
            }
            catch
            {


            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex > -1 && e.ColumnIndex==0)
            //{
            //    DataGridViewComboBoxCell cell = new DataGridViewComboBoxCell();
            //    dataGridView1[e.ColumnIndex, e.RowIndex] = cell;
            //    cell.DataSource = DAL.Item.getAllItems("");
            //    cell.DisplayMember = "ItemName";
            //    cell.ValueMember = "Id";
               
            //}
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //try
            //{ 
            //    var columnIndex = dataGridView1.CurrentCell.ColumnIndex;
            //    var rowIndex=dataGridView1.CurrentCell.RowIndex;

            //    if (dataGridView1.CurrentCell.ColumnIndex == 0)
            //    {
            //        ComboBox com = e.Control as ComboBox;
            //        com.DropDownStyle = ComboBoxStyle.DropDown;
            //        com.SelectedValueChanged += (s, o)=>
            //        {
            //            //if (com.SelectedValue == null) return;
            //            //ComboBox c = (ComboBox)s;
            //            //MessageBox.Show(c.SelectedValue.ToString());
            //        };

            //    }
            //    if (dataGridView1.CurrentCell.ColumnIndex == 1)
            //    {
            //        TextBox txtbox=e.Control as TextBox;

            //        //txtbox.KeyUp += Txtbox_KeyUp;
            //        txtbox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //        txtbox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //        AutoCompleteStringCollection col = new AutoCompleteStringCollection();
            //        AddItem(col);
            //        txtbox.AutoCompleteCustomSource = col;


            //    }

            //}
            //catch
            //{

              
            //}
           
        }

        //private void Txtbox_KeyUp(object sender, KeyEventArgs e)
        //{
        //        ((TextBox)sender).AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        //        ((TextBox)sender).AutoCompleteSource = AutoCompleteSource.CustomSource;
        //        ((TextBox)sender).AutoCompleteCustomSource = col;
        //}
        private void AddItem(AutoCompleteStringCollection collection)
        {
            DataTable data = DAL.Item.getAllItems("");
            foreach (DataRow item in data.Rows)
            {
                collection.Add(item.ItemArray[1].ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Views.FormLogin());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Views.FormList(), DockStyle.Fill);
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle,0x112,0xf012,0);
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            if(panelMenu.Width <= 40)
            {
                btnMenu.IconChar = IconChar.Times;
                btnMenu.IconColor= Color.White;
                btnMenu.FlatStyle= FlatStyle.Flat;
                btnMenu.FlatAppearance.BorderSize = 0;
                panelMenu.Width = 200;
                Settings.Default.menuSide=200;
                Settings.Default.Save();
                foreach (Button  menuItem in panelMenu.Controls.OfType<Button>())
                {
                    menuItem.Text = menuItem.Tag.ToString();
                    menuItem.ImageAlign = ContentAlignment.MiddleLeft;
                    menuItem.TextAlign = ContentAlignment.MiddleCenter;
                    menuItem.Padding = new Padding(10, 0, 0, 0);
                  
                }
            }
            else{
                panelMenu.Width = 40;
                Settings.Default.menuSide = 40;
                Settings.Default.Save();
                btnMenu.IconChar = IconChar.Bars;
                btnMenu.IconColor = Color.White;
                btnMenu.FlatStyle = FlatStyle.Flat;
                btnMenu.FlatAppearance.BorderSize = 0;
                foreach (Button menuItem in panelMenu.Controls.OfType<Button>())
                {
                    menuItem.Text = "";
                    menuItem.ImageAlign = ContentAlignment.MiddleCenter;
                    menuItem.Padding = new Padding(0);
                }
            }
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {

        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            var x = ((Button)sender).Size.Width;
            contextEmployee.Show(x, Cursor.Position.Y);
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //OpenChildForm(new Views.Employees.Create());
            //TabPage tabPage = new TabPage();
            //tabPage.Text = $"{tabControl1.TabPages.Count}";
            //tabControl1.TabPages.Add(tabPage);
            //Form login = new Views.FormLogin();
            //login.Dock=DockStyle.Fill;
            //login.TopLevel = false;
            //tabPage.Controls.Add(login);
            //login.Show();
            //tabPage.Show();
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Views.FormLogin());
        }
    }
}
