using System;
using System.ServiceProcess;

namespace UpdateElastic
{
    partial class UpdateDataService
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

      #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            // 
            // UpdateDataService
            // 
            this.ServiceName = "UpdateDataService";

        }
        protected override void OnContinue()
        {
            
        }
        #endregion
    }  
}
