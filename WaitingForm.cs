using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpotlightWallpaper
{
    public partial class WaitingForm : Form
    {
        public Action Worker { get; set; }

        public WaitingForm(Action worker)
        {
            InitializeComponent();
            if (worker == null)
                throw new ArgumentNullException();
            Worker = worker;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Task.Factory.StartNew(Worker)
                .ContinueWith(x => { this.Close(); }, TaskScheduler.FromCurrentSynchronizationContext());
        }
        
    }
}