using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KinEmote
{
    public partial class CustomKeySetup : Form
    {
        public CustomKeySetup()
        {
            InitializeComponent();
        }

        private void CustomKeySetup_Load(object sender, EventArgs e)
        {
            foreach (GroupBox groupBox in this.Controls.OfType<GroupBox>())
            {
                foreach (ComboBox comboBox in groupBox.Controls.OfType<ComboBox>())
                {
                    foreach (string keyName in Enum.GetNames(typeof(Keys)))
                    {
                        comboBox.Items.Add(keyName);
                    }
                }
            }

            LoadKeymapFromSettings();
        }

        private void SaveKeymapToSettings()
        {
            KeysConverter keysConverter = new KeysConverter();

            Properties.CustomKeys.Default.BackUp = (Keys)keysConverter.ConvertFromString(comboBoxBackUp.SelectedItem.ToString());
            Properties.CustomKeys.Default.BackLeft = (Keys)keysConverter.ConvertFromString(comboBoxBackLeft.SelectedItem.ToString()) ;
            Properties.CustomKeys.Default.BackRight = (Keys)keysConverter.ConvertFromString(comboBoxBackRight.SelectedItem.ToString());

            Properties.CustomKeys.Default.MidUp = (Keys)keysConverter.ConvertFromString(comboBoxMidUp.SelectedItem.ToString());
            Properties.CustomKeys.Default.MidDown = (Keys)keysConverter.ConvertFromString(comboBoxMidDown.SelectedItem.ToString());
            Properties.CustomKeys.Default.MidLeft = (Keys)keysConverter.ConvertFromString(comboBoxMidLeft.SelectedItem.ToString());
            Properties.CustomKeys.Default.MidRight = (Keys)keysConverter.ConvertFromString(comboBoxMidRight.SelectedItem.ToString());

            Properties.CustomKeys.Default.Push = (Keys)keysConverter.ConvertFromString(comboBoxMidPush.SelectedItem.ToString());

            Properties.CustomKeys.Default.Save();
        }

        private void LoadKeymapFromSettings()
        {
            comboBoxBackUp.SelectedItem = Properties.CustomKeys.Default.BackUp.ToString();
            comboBoxBackLeft.SelectedItem = Properties.CustomKeys.Default.BackLeft.ToString();
            comboBoxBackRight.SelectedItem = Properties.CustomKeys.Default.BackRight.ToString();

            comboBoxMidUp.SelectedItem = Properties.CustomKeys.Default.MidUp.ToString();
            comboBoxMidDown.SelectedItem = Properties.CustomKeys.Default.MidDown.ToString();
            comboBoxMidLeft.SelectedItem = Properties.CustomKeys.Default.MidLeft.ToString();
            comboBoxMidRight.SelectedItem = Properties.CustomKeys.Default.MidRight.ToString();

            comboBoxMidPush.SelectedItem = Properties.CustomKeys.Default.Push.ToString();
        }

        private void butSave_Click(object sender, EventArgs e)
        {
            SaveKeymapToSettings();

            this.Close();
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
