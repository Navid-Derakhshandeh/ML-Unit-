using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.ML;
using Efficiency_of_Power_PlantML.Model;

namespace Efficiency_of_Power_Plant
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            try
            {
                MLContext mlContext = new MLContext();
                ITransformer mlModel = mlContext.Model.Load("MLModel.zip", out var modelInputSchema);
                var predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);
                var input = new ModelInput();

                int Ga = Int32.Parse(txtG.Text);


                int Wa = Int32.Parse(txtW.Text);

                input.Gas = Ga;
                input.Water = Wa;

                ModelOutput result = predEngine.Predict(input);

                int b = (int)Math.Round(result.Score);
                txtRes.Text = b.ToString();
                if (b >= 100)
                {
                    string c = "Your System is Efficient";
                    LS.Text = c;
                }
                else if (b <= 99)
                {
                    string c1 = "your system is not Efficient";

                    LS.Text = c1;
                }
            }
            catch(Exception ex)
            {
                LS.Text = ex.Message;
            }
            
        }
    }
}
