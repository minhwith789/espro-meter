using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace ConstructionApp.custom
{
    public partial class MultiColumnCombobox:UserControl
    {
        //private fields
        private TextBox _textbox;
        private DataGridView gridView;
        private object _selectValue;
        private object _selectText;
        private int _selectedIndex;
        public MultiColumnCombobox()
        {
            InitializeControls();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
           
        }
        private void InitializeControls()
        {
            this._textbox = new TextBox();
            this._textbox.Width = this.Width;
            this._textbox.Height = 40;
            this._textbox.Dock = DockStyle.Fill;
            this.gridView = new DataGridView();
            this.gridView.Location = new Point
            {
                X = 0,
                Y = 0
            };
            this.SuspendLayout();
            this.Controls.Add(_textbox);
            this.Controls.Add(gridView);
            //this.MaximumSize = new Size(300, 30);
            //this.Size = new Size(300, 30);
            //event 

            this._textbox.KeyUp += (s, e) =>
            {
                // this.Controls.Add(gridView);
                if(this._textbox.Text == "")
                {
                    this.gridView.Visible = false;
                }
                else
                {
                    this.gridView.Visible = true;
                }
               
            };
            this.ResumeLayout();
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //Graphics graph=e.Graphics;

            //graph.DrawRectangle(new Pen(Color.Black),new Rectangle(0,0,Width,Height));


        }
    }
}
