using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ektiposeis.MyClass
{
    class ConvertFormToHTML
    {
        public void convertToHTML(Control ektiposiForm) 
        {
            String descktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            String ektyposeisPath = descktopPath + @"\" + "" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
            String htmlFileName = "" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Hour+"_"+DateTime.Now.Minute+"_"+DateTime.Now.Second+".html";
            Directory.CreateDirectory(descktopPath + @"\" + "" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day);
            StreamWriter htmlSW = new StreamWriter(ektyposeisPath + @"\" + htmlFileName);
            htmlSW.Write("<head> <meta charset=\"UTF-8\"></head>\n<body>\n");
            for (int i = 0; i < ektiposiForm.Controls.Count; i++)
            {
                Control currentControl = ektiposiForm.Controls[i];
                if (currentControl.GetType().ToString() == "System.Windows.Forms.PictureBox")
                {
                    PictureBox currentPictureBox = (PictureBox)currentControl;
                    int locationTop = currentPictureBox.Location.Y;
                    int locationLeft = currentPictureBox.Location.X;
                    int imageWidth = currentPictureBox.Width;
                    int imageHeight = currentPictureBox.Height;
                    String filePath = Directory.GetCurrentDirectory() + "\\computerlifeLogo.jpg";
                    String fileName = Path.GetFileName(filePath);
                    if (!File.Exists(ektyposeisPath + @"\" + fileName))
                    {
                        File.Copy(filePath, ektyposeisPath + @"\" + fileName);
                    }
                    htmlSW.Write("\n<img STYLE=\"position:absolute; TOP:" + locationTop + "px; LEFT:" + locationLeft + "px; WIDTH:" + imageWidth + "px; HEIGHT:" + imageHeight + "px\" src=\"" + fileName + "\" alt=\"Mountain View\">");
                }
                if (currentControl.GetType().ToString() == "System.Windows.Forms.RichTextBox")
                {
                    RichTextBox currentRichTextBox = (RichTextBox)currentControl;
                    int richTextBoxWidth = currentRichTextBox.Width;
                    int richTextBoxHeight = currentRichTextBox.Height;
                    int richTextBoxTOP = currentRichTextBox.Location.Y;
                    int richTextBoxLEFT = currentRichTextBox.Location.X;
                    String richTextBoxText = currentRichTextBox.Text;
                    htmlSW.Write("\n<textarea style=\"position:absolute; TOP:" + richTextBoxTOP + "px; LEFT:" + richTextBoxLEFT + "px; width: " + richTextBoxWidth + "px; height: " + richTextBoxHeight + "px;\"\">" + richTextBoxText + "</textarea>");
                }
                if (currentControl.GetType().ToString() == "System.Windows.Forms.Label")
                {
                    Label currentLabel = (Label)currentControl;
                    String labelText = currentLabel.Text;
                    int labelTop = currentLabel.Location.Y;
                    int labelLeft = currentLabel.Location.X;
                    int labelWidth = currentLabel.Width;
                    int labelHeight = currentLabel.Height;
                    float fontSize = currentLabel.Font.Size/4.1F;
                    htmlSW.Write("<font size=\"" + fontSize + "\">");
                    htmlSW.Write("<label  style=\"position:absolute; TOP:" + labelTop + "px; LEFT:" + labelLeft + "px; width: " + labelWidth + "px; height: " + labelHeight + "px;\"\" for=\"male\">" + labelText + "</label>");
                    htmlSW.Write("</font>");
                }
                if (currentControl.GetType().ToString() == "System.Windows.Forms.DataGridView")
                {
                    DataGridView currentDataGridView = (DataGridView)currentControl;
                    int currentDataGridViewWidth = currentDataGridView.Width;
                    int gridTop = currentDataGridView.Location.Y;
                    int gridLeft = currentDataGridView.Location.X;
                    htmlSW.Write("\n<table style=\"position:absolute; TOP:" + gridTop + "px; LEFT:" + gridLeft + "px; width=\"" + currentDataGridViewWidth + "\">");
                    for (int c = 0; c < currentDataGridView.Columns.Count; c++)
                    {
                        String columnName = currentDataGridView.Columns[c].Name;
                        htmlSW.Write("\n<th bgcolor=\"#C0C0C0\" style = \"border: 1px solid black;\">" + columnName + "\n</th>");
                    }
                    for (int r = 0; r < currentDataGridView.Rows.Count; r++) 
                    {
                        htmlSW.Write("\n<tr>");
                        for (int c = 0; c < currentDataGridView.Columns.Count; c++) 
                        {
                            if (currentDataGridView.Rows[r].Cells[c].Value == null) 
                            {
                                break;
                            }
                            String cellValue = currentDataGridView.Rows[r].Cells[c].Value.ToString();
                            htmlSW.Write("\n<td style = \"border: 1px solid black;\">" + cellValue + "\n</td>");
                        }
                        htmlSW.Write("\n</tr>");
                    }
                    htmlSW.Write("\n</table>");
                }
            }
            htmlSW.Write("\n</body>");
            htmlSW.Close();
            MessageBox.Show("Η μετατροπή ολοκληρώθηκε με επιτυχία.");
            System.Diagnostics.Process.Start(ektyposeisPath + @"\" + htmlFileName);
        }
    }
}
