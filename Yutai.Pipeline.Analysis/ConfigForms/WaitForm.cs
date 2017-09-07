using System;
using System.ComponentModel;
using Yutai.UI.Forms;

namespace Yutai.Pipeline.Analysis.ConfigForms
{
    public partial class WaitForm : MapWindowView
    {
        private BackgroundWorker _worker;
        private int _count;

        public WaitForm()
        {
            InitializeComponent();
            _worker = new BackgroundWorker()
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            _worker.DoWork += WorkerOnDoWork;
            _worker.ProgressChanged += WorkerOnProgressChanged;
            _worker.RunWorkerCompleted += WorkerOnRunWorkerCompleted;
        }

        private void WorkerOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {
        }

        private void WorkerOnProgressChanged(object sender, ProgressChangedEventArgs progressChangedEventArgs)
        {
            if (this.progressBarControl1.Visible == true && _count > 0)
            {
                this.progressBarControl1.Position = progressChangedEventArgs.ProgressPercentage * 100 / _count;
            }
            this.progressPanel1.Description = progressChangedEventArgs.UserState.ToString();
        }

        private void WorkerOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            this.Close();
        }

        public BackgroundWorker Worker
        {
            get { return _worker; }
            set { _worker = value; }
        }

        public bool VisibleBackButton
        {
            get { return this.btnBack.Visible; }
            set { this.btnBack.Visible = value; }
        }

        public bool VisibleprogressBarControl
        {
            get { return this.progressBarControl1.Visible; }
            set { this.progressBarControl1.Visible = value; }
        }

        public int Count
        {
            get { return _count; }
            set { _count = value; }
        }

        public string Description
        {
            get { return this.progressPanel1.Description; }
            set { this.progressPanel1.Description = value; }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (_worker != null && _worker.IsBusy)
                _worker.CancelAsync();
        }
    }
}
