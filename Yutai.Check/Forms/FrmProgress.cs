using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yutai.Check.Forms
{
    public partial class FrmProgress : Form
    {
        public FrmProgress(BackgroundWorker worker)
        {
            InitializeComponent();
            worker.RunWorkerCompleted += WorkerOnRunWorkerCompleted;
            worker.ProgressChanged += WorkerOnProgressChanged;
        }

        private void WorkerOnProgressChanged(object sender, ProgressChangedEventArgs progressChangedEventArgs)
        {
            try
            {
                this.progressPanel1.Description = progressChangedEventArgs.UserState.ToString();
            }
            catch (Exception exception)
            {
                // ignored
            }
        }

        private void WorkerOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            this.Close();
        }
    }
}
