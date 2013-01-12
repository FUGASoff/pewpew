﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PEWPEW
{
    public partial class MainScreen : Form
    {
        List<Shape> Shapes = new List<Shape>();
        Point ShapeStart;
        bool IsShapeStart = true;
        string curFile;
        public MainScreen()
        {
            InitializeComponent();
        }
        private void rb_CheckedChanged(object sender, EventArgs e)
        {
            IsShapeStart = !IsShapeStart;
        }
        private void MainScreen_MouseMove(object sender, MouseEventArgs e)
        {
            this.Text = Convert.ToString(e.X) + " . " + (e.Y);
            this.Text = Convert.ToString(e.Location);
        }
        private void MainScreen_Paint(object sender, PaintEventArgs e)
        {
            foreach (Shape p in this.Shapes)
            {
                p.DrawWith(e.Graphics);
            }
        }
        private void MainScreen_MouseDown(object sender, MouseEventArgs e)
        {
            if (rb_Cross.Checked) Shapes.Add(new Cross(e.X,e.Y));
            if (rb_Line.Checked)
            {
                if (!IsShapeStart) ShapeStart = e.Location;
                else Shapes.Add(new Line(ShapeStart, e.Location));
                IsShapeStart = !IsShapeStart;
            }
            if (rb_circle.Checked)
            {
                if (IsShapeStart) ShapeStart = e.Location;
                else Shapes.Add(new Circle(ShapeStart, e.Location));
                IsShapeStart = !IsShapeStart;
            }
            this.Refresh();
        }
        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                curFile = saveFileDialog1.FileName;
                StreamWriter sw = new StreamWriter(curFile);
                foreach (Shape p in this.Shapes)
                {
                    p.SaveTo(sw);
                }
                sw.Close();
            }
        }

        private void MainScreen_Load(object sender, EventArgs e)
        {

        }
    }
}
