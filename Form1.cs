/*
 * Recurse, an ultra-lightweight and simplistic text editor
 * Copyright (C) 2022 REMCodes
 * 
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 * 
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.

 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301
 * USA
 * 
 * To contact REMCodes, please email "ContactREMCodes@gmail.com"
*/

namespace Recurse
{
    public partial class Recurse : Form
    {
        private string FileContent = String.Empty;
        public Recurse()
        {
            InitializeComponent();
            OpenFileDialog.RestoreDirectory = true;
            SaveFileDialog.RestoreDirectory = true;
            FileName.Focus();
            KeyPreview = true;
        }

        private void TextArea_TextChanged(object sender, EventArgs e)
        {
            FileContent = TextArea.Text;
        }

        private void Open()
        {
            OpenFileDialog.ShowDialog(this);
        }

        private void Save()
        {
            SaveFileDialog.ShowDialog(this);
        }

        private void UpdateFileName(string path)
        {
            Text = "Recurse | " + path;
            string[] name = path.Split(new char[] { '\\', '/' });
            FileName.Text = name[name.Length-1];
        }

        private void Recurse_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Control && e.KeyCode == Keys.O)
            {
                Open();
            } else if(e.Control && e.KeyCode == Keys.S)
            {
                Save();
            } else if(e.Control && e.KeyCode == Keys.Q)
            {
                if (MessageBox.Show("Are you sure you want to exit?", "Confirm Exit", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Environment.Exit(0);
                }
            }
        }

        private void OpenFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var filePath = OpenFileDialog.FileName;

            var fileStream = OpenFileDialog.OpenFile();

            using (StreamReader reader = new StreamReader(fileStream))
            {
                FileContent = reader.ReadToEnd();
                UpdateFileName(filePath);
                TextArea.Text = FileContent;
            }
        }

        private void SaveFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            File.WriteAllText(SaveFileDialog.FileName, FileContent);
            UpdateFileName(SaveFileDialog.FileName);
        }
    }
}