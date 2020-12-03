using System.Text;
using System.Windows.Forms;

namespace LexicalDiversity
{
    internal partial class SettingsForm_LexicalDiversity : Form
    {


        #region Get and Set Options

        public int WordWindowSize;
        public double mtldThreshold;

       #endregion



        public SettingsForm_LexicalDiversity(int WordWindowSetting, double threshold)
        {
            InitializeComponent();

            WordWindowTextbox.Text = WordWindowSetting.ToString();
            mtldThresholdUpDown.Value = (decimal)threshold;

        }



        

        private void OKButton_Click(object sender, System.EventArgs e)
        {

            bool isNumeric = int.TryParse(WordWindowTextbox.Text.Trim(), out int n);
            if (!isNumeric)
            {
                MessageBox.Show("Your word window parameter must be a positive integer.", "Parameter Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (int.Parse(WordWindowTextbox.Text.Trim()) <= 0)
                {
                    MessageBox.Show("Your word window parameter must be a positive integer.", "Parameter Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            this.WordWindowSize = int.Parse(WordWindowTextbox.Text.Trim());
            this.mtldThreshold = (double)mtldThresholdUpDown.Value;
            this.DialogResult = DialogResult.OK;
        }
    }
}
