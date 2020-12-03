using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using PluginContracts;
using OutputHelperLib;
using System.Linq;


namespace LexicalDiversity
{
    public class LexicalDiversity : Plugin
    {


        public string[] InputType { get; } = { "Tokens" };
        public string OutputType { get; } = "OutputArray";

        public Dictionary<int, string> OutputHeaderData { get; set; } = new Dictionary<int, string>() { { 0, "Tokens" },
                                                                                                        { 1, "Types" },
                                                                                                        { 2, "TTR" },
                                                                                                        { 3, "RTTR" },
                                                                                                        { 4, "CTTR" },
                                                                                                        { 5, "Herdan_C" },
                                                                                                        { 6, "SummerIndex" },
                                                                                                        { 7, "Dugast" },
                                                                                                        { 8, "Maas" },
                                                                                                        { 9, "MATTR" },
                                                                                                        { 10, "Evenness" },
                                                                                                        { 11, "MTLD" }
                                                                                                        };
        public bool InheritHeader { get; } = false;

        #region Plugin Details and Info

        public string PluginName { get; } = "Lexical Richness";
        public string PluginType { get; } = "Language Analysis";
        public string PluginVersion { get; } = "1.1.01";
        public string PluginAuthor { get; } = "Ryan L. Boyd (ryan@ryanboyd.io)";
        public string PluginDescription { get; } = "Calculates various measures of lexical richness including type-token ratio (TTR), Carroll's corrected TTR, Guiraud's Root TTR, Dugast's Uber Index, Summer's Index, moving average type-token ratio (MATTR), MTLD, and others. Additional measures of lexical diversity will likely be added in the future.";
        public bool TopLevel { get; } = false;

        private int WordWindowSize { get; set; } = 50;
        private double mtldThreshold { get; set; } = 0.72;
        public string PluginTutorial { get; } = "https://youtu.be/Y_KZKz1WK_g";

        public Icon GetPluginIcon
        {
            get
            {
                return Properties.Resources.icon;
            }
        }

        #endregion



        public void ChangeSettings()
        {

            using (var form = new SettingsForm_LexicalDiversity(WordWindowSize, mtldThreshold))
            {


                form.Icon = Properties.Resources.icon;
                form.Text = PluginName;

                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {

                    WordWindowSize = form.WordWindowSize;
                    mtldThreshold = form.mtldThreshold;
                }
            }

        }





        public Payload RunPlugin(Payload Input)
        {



            Payload pData = new Payload();
            pData.FileID = Input.FileID;
            pData.SegmentID = Input.SegmentID;

            List<string[]> TextToAnalyze = Input.StringArrayList.Select(item => (string[])((string[])item.Clone()).ToArray()).ToList();

            for (int i = 0; i < TextToAnalyze.Count; i++)
            {

                if (TextToAnalyze[i].Length > 0)
                {
                    

                    Dictionary<string,double> ttrMetrics = TypeTokenTools.TypeTokenRatioMetrics(TextToAnalyze[i]);
                    double MATTR = TypeTokenTools.MATTR(TextToAnalyze[i].ToList(), WordWindowSize);
                    double mtld = TypeTokenTools.CalcMTLDFrontBack(TextToAnalyze[i], mtldThreshold);

                    pData.StringArrayList.Add(new string[] { ttrMetrics["Tokens"].ToString(),
                                                             ttrMetrics["Types"].ToString(),
                                                             ttrMetrics["TTR"].ToString(),
                                                             ttrMetrics["RTTR"].ToString(),
                                                             ttrMetrics["CTTR"].ToString(),
                                                             ttrMetrics["HerdanC"].ToString(),
                                                             ttrMetrics["SummerIndex"].ToString(),
                                                             ttrMetrics["Dugast"].ToString(),
                                                             ttrMetrics["Maas"].ToString(),
                                                             MATTR.ToString(),
                                                             ttrMetrics["Evenness"].ToString(),
                                                             mtld.ToString(),
                                                                });
                }
                else
                {
                    pData.StringArrayList.Add(new string[] { "0",
                                                             "0",
                                                             "",
                                                             "",
                                                             "",
                                                             "",
                                                             "",
                                                             "",
                                                             "",
                                                             "",
                                                             "",
                                                             ""});
                }

                pData.SegmentNumber.Add(Input.SegmentNumber[i]);
            }

            return (pData);

        }



        public void Initialize() { }

        public bool InspectSettings()
        {
            return true;
        }

        public Payload FinishUp(Payload Input)
        {
            return (Input);
        }


        #region Import/Export Settings
        public void ImportSettings(Dictionary<string, string> SettingsDict)
        {
            WordWindowSize = int.Parse(SettingsDict["WordWindowSize"]);
            mtldThreshold = double.Parse(SettingsDict["mtldThreshold"]);
        }

        public Dictionary<string, string> ExportSettings(bool suppressWarnings)
        {
            Dictionary<string, string> SettingsDict = new Dictionary<string, string>();
            SettingsDict.Add("WordWindowSize", WordWindowSize.ToString());
            SettingsDict.Add("mtldThreshold", mtldThreshold.ToString());
            return (SettingsDict);
        }
        #endregion


    }
}
