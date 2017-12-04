using ECNC3.Models;
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

namespace ECNC3.Views.Popup
{
    public partial class LoadStatusView : ECNC3Form
    {
        #region Constractor
        public LoadStatusView(string title = "", string message = "")
        {
            InitializeComponent();
            _StatusTitleLabel.Text = title;
            _StatusTextLabel.Text = message;
            string imagePath = "elenix_logo.png";
            pictureBox1.BackgroundImage = Image.FromFile(imagePath);
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            if (title == "") _StatusTitleLabel.Visible = false;
            if(message == "") _StatusTextLabel.Visible = false;
            Refresh();
        }
        #endregion

        public bool WaitCount
        {
            set
            {
                if(value == true)
                {

                }
            }
        }
        public string MessageTitle
        {
            get
            {
                return _StatusTitleLabel.Text;
            }
            set
            {
                _StatusTitleLabel.Text = value;
                this.Refresh();
            }
        }
        public string MessageText
        {
            get
            {
                return _StatusTextLabel.Text;
            }
            set
            {
                _StatusTextLabel.Text = value;
                this.Refresh();
            }
        }
        public int ProgressBarMaxLimit
        {
            get
            {
                return _ProgressBar.ProgressBarMaxValue;
            }
            set
            {
                _ProgressBar.ProgressBarMaxValue = value;
            }
        }
        public int ProgressBarMinLimit
        {
            get
            {
                return _ProgressBar.ProgressBarMinValue;
            }
            set
            {
                _ProgressBar.ProgressBarMinValue = value;
            }
        }
        public int ProgressBarValue
        {
            get
            {
                return _ProgressBar.ProgressBarValue;
            }
            set
            {
                _ProgressBar.Text = (_ProgressBar.ProgressBarValue = value).ToString() + "%";
                this.TopMost = true;
                this.Refresh();
            }
        }
        public int SubProgressBarMaxLimit
        {
            get
            {
                return _SubProgressBar.ProgressBarMaxValue;
            }
            set
            {
                _SubProgressBar.ProgressBarMaxValue = value;
            }
        }
        public int SubProgressBarMinLimit
        {
            get
            {
                return _SubProgressBar.ProgressBarMinValue;
            }
            set
            {
                _SubProgressBar.ProgressBarMinValue = value;
            }
        }
        public int SubProgressBarValue
        {
            get
            {
                return _SubProgressBar.ProgressBarValue;
            }
            set
            {
                //_SubProgressBar.Text = (_SubProgressBar.ProgressBarValue = value).ToString() + "%";
                _SubProgressBar.ProgressBarValue = value;
                this.TopMost = true;
                this.Refresh();
            }
        }

        public int SecondSubProgressBarMaxLimit
        {
            get
            {
                return _SecondSubProgressBar.ProgressBarMaxValue;
            }
            set
            {
                _SecondSubProgressBar.ProgressBarMaxValue = value;
            }
        }
        public int SecondSubProgressBarMinLimit
        {
            get
            {
                return _SecondSubProgressBar.ProgressBarMinValue;
            }
            set
            {
                _SecondSubProgressBar.ProgressBarMinValue = value;
            }
        }
        public int SecondSubProgressBarValue
        {
            get
            {
                return _SecondSubProgressBar.ProgressBarValue;
            }
            set
            {
                _SecondSubProgressBar.ProgressBarValue = value;
                this.TopMost = true;
                this.Refresh();
            }
        }
        private void DisposeMember()
        {
        }

    }
}
