using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chapter_10._1
{
    public partial class Form1 : Form
    {
        public Flooring selectedFlooring;
        private List<Flooring> allFloorings = new List<Flooring>();
        private Dictionary<string, AddOns> addOns = new Dictionary<string, AddOns>();
        private List<AddOns> selectedAddOns = new List<AddOns>();
        public Form1()
        {
            InitializeComponent();


            Flooring bruce = new Flooring();
            bruce.Name = "Bruce";
            bruce.PricePerSquareFoot = 2.49;
            bruce.Description = "Oak Gunstock Hardwood";
            bruce.ItemNumber = "1051367";
            allFloorings.Add(bruce);

            Flooring smartCore = new Flooring();
            smartCore.Name = "Smartcore";
            smartCore.PricePerSquareFoot = 3.79;
            smartCore.Description = "Maple Bluegrass Hardwood";
            smartCore.ItemNumber = "1051339";
            allFloorings.Add(smartCore);

            Flooring flexco = new Flooring();
            flexco.Name = "Flexco";
            flexco.PricePerSquareFoot = 4.39;
            flexco.Description = "Walnut Light Hardwood";
            flexco.ItemNumber = "1077786";
            allFloorings.Add(flexco);

            Flooring traffic = new Flooring();
            traffic.Name = "Traffic";
            traffic.PricePerSquareFoot = 4.39;
            traffic.Description = "Walnut Coffee Carpet";
            traffic.ItemNumber = "2361367";
            allFloorings.Add(traffic);

            Flooring versatile = new Flooring();
            versatile.Name = "Versatile";
            versatile.PricePerSquareFoot = 0.97;
            versatile.Description = "Field Day Rolling Carpet";
            versatile.ItemNumber = "2361367";
            allFloorings.Add(versatile);

            Flooring foss = new Flooring();
            foss.Name = "Foss";
            foss.PricePerSquareFoot = 2.75;
            foss.Description = "Peel & Stick Carpet";
            foss.ItemNumber = "2361367";
            allFloorings.Add(foss);

            Flooring pergo = new Flooring();
            pergo.Name = "Pergo";
            pergo.PricePerSquareFoot = 2.79;
            pergo.Description = "Oak Glazed Laminate";
            pergo.ItemNumber = "4055667";
            allFloorings.Add(pergo);

            Flooring master = new Flooring();
            master.Name = "Master";
            master.PricePerSquareFoot = 1.49;
            master.Description = "Paradise Jatoba Laminate";
            master.ItemNumber = "4365886";
            allFloorings.Add(master);

            FloorInstaller floorInstallation = new FloorInstaller();
            floorInstallation.Name = "Floor Installer";
            floorInstallation.Fee = 200;
            floorInstallation.Description = "";
            addOns.Add("Floor Installer", floorInstallation);

            AddOns manualInstallation = new AddOns();
            manualInstallation.Name = "Installation Manual";
            manualInstallation.Fee = 49.99;
            manualInstallation.Description = "";
            addOns.Add("Installation Manual", manualInstallation);

            AddOns truckDelivery = new AddOns();
            truckDelivery.Name = "Truck Delivery";
            truckDelivery.Fee = 399.99;
            truckDelivery.Description = "Flat Delivery Fee";
            addOns.Add("Truck Delivery", truckDelivery);

        }


        private double input_check(TextBox txt)
        {
            double retval = 0;
            while (double.TryParse(txt.Text, out retval) == false)
            {
                txt.Text = "0.0";
                txt.Focus();
            }

            return retval;
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            
            double totalSquareFeet = 0;
            double lenght = input_check(textBoxLength);
            double width = input_check(textBoxWidth);
            totalSquareFeet = lenght * width;

            if(selectedFlooring == null)
            {
                MessageBox.Show("You must first select a flooring!", "Select Flooring", MessageBoxButtons.OK);
                return;
            }

            double floorPrice = selectedFlooring.CalculatePrice(totalSquareFeet);

            if(totalSquareFeet >= 1)
            {
                var rand = new Random();
                richTextBoxSubtotal.Text = "Floor + Bulbs".PadLeft(40) + "\n" + "5550 Hwy 153 Suite #101".PadLeft(40) + 
                    "\n" + "Hixson, TN 37343".PadLeft(40) + "\n" + "(423) 874-1050".PadLeft(40);
                richTextBoxSubtotal.Text += "\n" + "\n" + "Original Order:" + rand.Next(589632, 789689).ToString().PadLeft(27) + "\n" +(selectedFlooring.Name + "\n" + selectedFlooring.Description +
                    "$".PadLeft(9) + selectedFlooring.PricePerSquareFoot.ToString() + " /sq ft" + "\n" + "@".PadLeft(21) + totalSquareFeet.ToString() + "/sq ft" +"$".PadLeft(10) + floorPrice.ToString() +
                      "\nItem" + "#".PadLeft(37) + selectedFlooring.ItemNumber);
                

                double addOnTotal = 0.0;
                foreach (var addOn in selectedAddOns)
                {
                    addOnTotal += addOn.Fee;

                    if (addOn.GetType() == typeof(FloorInstaller))
                    {
                        double floorInstallationPrice = ((FloorInstaller)addOn).CalculateAddOnWorkCost(totalSquareFeet);
                        addOnTotal += floorInstallationPrice;

                        richTextBoxSubtotal.Text += "\nFloor Installer Fee:" + "$".PadLeft(18) + addOns["Floor Installer"].Fee;
                        richTextBoxSubtotal.Text += "\nFloor Installer Cost:" + "$".PadLeft(17) + floorInstallationPrice + " ($30/sq ft)";
                    }
                }

                if (checkBoxInstallationManual.Checked)
                {
                    richTextBoxSubtotal.Text += "\nInstallation Manual" + "$".PadLeft(18) + addOns["Installation Manual"].Fee;
                }

                if (radioButtonTruckDelivery.Checked)
                {
                    richTextBoxSubtotal.Text += "\nTruck Delivery Fee" + "$".PadLeft(19) + addOns["Truck Delivery"].Fee;
                }

                richTextBoxSubtotal.Text += "\nTotal" + "$".PadLeft(38) + Math.Round(floorPrice + addOnTotal, 2, MidpointRounding.AwayFromZero);
            }
            else
            {
                richTextBoxSubtotal.Text = "";
                MessageBox.Show("Enter flooring size.","Measures", MessageBoxButtons.OK);

            }

        }

        private void radioButtonBruce_CheckedChanged(object sender, EventArgs e)
        {
            selectedFlooring = allFloorings.Where(x => x.Name.Equals("Bruce")).FirstOrDefault();
        }

        private void radioButtonSmartcore_CheckedChanged(object sender, EventArgs e)
        {
            selectedFlooring = allFloorings.Where(x => x.Name.Equals("Smartcore")).FirstOrDefault();
        }

        private void radioButtonFlexco_CheckedChanged(object sender, EventArgs e)
        {
            selectedFlooring = allFloorings.Where(x => x.Name.Equals("Flexco")).FirstOrDefault();

        }

        private void radioButtonTraffic_CheckedChanged(object sender, EventArgs e)
        {
            selectedFlooring = allFloorings.Where(x => x.Name.Equals("Traffic")).FirstOrDefault();

        }

        private void radioButtonVersatile_CheckedChanged(object sender, EventArgs e)
        {
            selectedFlooring = allFloorings.Where(x => x.Name.Equals("Versatile")).FirstOrDefault();

        }

        private void radioButtonFoss_CheckedChanged(object sender, EventArgs e)
        {
            selectedFlooring = allFloorings.Where(x => x.Name.Equals("Foss")).FirstOrDefault();

        }

        private void radioButtonPergo_CheckedChanged(object sender, EventArgs e)
        {
            selectedFlooring = allFloorings.Where(x => x.Name.Equals("Pergo")).FirstOrDefault();

        }

        private void radioButtonMaster_CheckedChanged(object sender, EventArgs e)
        {
            selectedFlooring = allFloorings.Where(x => x.Name.Equals("Master")).FirstOrDefault();
        }

        private void checkBoxFloorInstaller_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                selectedAddOns.Add(addOns["Floor Installer"]);

            }
            else
            {
                selectedAddOns.Remove(addOns["Floor Installer"]);
            }
        }

        private void checkBoxInstallationManual_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                selectedAddOns.Add(addOns["Installation Manual"]);

            }
            else
            {
                selectedAddOns.Remove(addOns["Installation Manual"]);
            }
        }

        private void radioButtonTruckDelivery_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                selectedAddOns.Add(addOns["Truck Delivery"]);

            }
            else
            {
                selectedAddOns.Remove(addOns["Truck Delivery"]);
            }
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            
            textBoxLength.Text = "";
            textBoxWidth.Text = "";        
            

        }

        private void FontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog1 = new FontDialog();
            fontDialog1.Font = richTextBoxSubtotal.Font;

            if(fontDialog1.ShowDialog() != DialogResult.Cancel)
            {
                richTextBoxSubtotal.Font = fontDialog1.Font;
            }
        }

        private void ColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog1 = new ColorDialog();

            colorDialog1.Color = richTextBoxSubtotal.ForeColor;

            if (colorDialog1.ShowDialog() != DialogResult.Cancel)
            {
                richTextBoxSubtotal.BackColor = colorDialog1.Color;
                textBoxLength.BackColor = colorDialog1.Color;
                textBoxWidth.BackColor = colorDialog1.Color;
            }

        }

        private void Clear_form()
        {

            richTextBoxSubtotal.Clear();
            richTextBoxSubtotal.Update();
            richTextBoxSubtotal.Text = "New Estimate";
            textBoxLength.Text = "";
            textBoxWidth.Text = "";
            radioButtonBruce.Checked = false;
            radioButtonFlexco.Checked = false;
            radioButtonFoss.Checked = false;
            radioButtonMaster.Checked = false;
            radioButtonPergo.Checked = false;
            radioButtonSmartcore.Checked = false;
            radioButtonTraffic.Checked = false;
            radioButtonVersatile.Checked = false;
            radioButtonTruckDelivery.Checked = false;
            radioButtonStorePickup.Checked = true;

            checkBoxFloorInstaller.Checked = false;
            checkBoxInstallationManual.Checked = false;
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clear_form();
        }

        private void PrintToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile1 = new SaveFileDialog();
            saveFile1.DefaultExt = "*.rtf";
            saveFile1.Filter = "RTF Files|*.rtf";

            
            if (saveFile1.ShowDialog() == System.Windows.Forms.DialogResult.OK &&
               saveFile1.FileName.Length > 0)
            {
                
                richTextBoxSubtotal.SaveFile(saveFile1.FileName, RichTextBoxStreamType.PlainText);
            }

        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("Close the Program?",
                    "Exit?", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    Application.Exit();
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                e.Cancel = true;
            }
        }



        private void InfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(GetFileContent(), "Floor+Bulbs Info", MessageBoxButtons.OK);
        }

        private String GetFileContent()
        {
            var fileContent = string.Empty;
            var filePath = "c:\\Users\\ketli\\Documents\\Spring 2021\\Advanced .Net\\Assigment\\Floor+BulbsInfo.txt";

            fileContent = File.ReadAllText(filePath);

            return fileContent;
        }
    }
}
