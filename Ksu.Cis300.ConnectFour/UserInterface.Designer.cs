namespace Ksu.Cis300.ConnectFour
{
    partial class UserInterface
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.uxTopContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.uxTurnLabel = new System.Windows.Forms.Label();
            this.uxPlayerLabel = new System.Windows.Forms.Label();
            this.uxPlaceButtonContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.uxBoardContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.uxTopContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // uxTopContainer
            // 
            this.uxTopContainer.Controls.Add(this.uxTurnLabel);
            this.uxTopContainer.Controls.Add(this.uxPlayerLabel);
            this.uxTopContainer.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.uxTopContainer.Location = new System.Drawing.Point(12, 4);
            this.uxTopContainer.Name = "uxTopContainer";
            this.uxTopContainer.Size = new System.Drawing.Size(386, 27);
            this.uxTopContainer.TabIndex = 0;
            // 
            // uxTurnLabel
            // 
            this.uxTurnLabel.AutoSize = true;
            this.uxTurnLabel.BackColor = System.Drawing.Color.White;
            this.uxTurnLabel.Location = new System.Drawing.Point(346, 0);
            this.uxTurnLabel.Name = "uxTurnLabel";
            this.uxTurnLabel.Size = new System.Drawing.Size(37, 13);
            this.uxTurnLabel.TabIndex = 0;
            this.uxTurnLabel.Text = "          ";
            // 
            // uxPlayerLabel
            // 
            this.uxPlayerLabel.AutoSize = true;
            this.uxPlayerLabel.Location = new System.Drawing.Point(269, 0);
            this.uxPlayerLabel.Name = "uxPlayerLabel";
            this.uxPlayerLabel.Size = new System.Drawing.Size(71, 13);
            this.uxPlayerLabel.TabIndex = 1;
            this.uxPlayerLabel.Text = "Player\'s Turn:";
            // 
            // uxPlaceButtonContainer
            // 
            this.uxPlaceButtonContainer.Location = new System.Drawing.Point(12, 37);
            this.uxPlaceButtonContainer.Name = "uxPlaceButtonContainer";
            this.uxPlaceButtonContainer.Size = new System.Drawing.Size(386, 26);
            this.uxPlaceButtonContainer.TabIndex = 1;
            // 
            // uxBoardContainer
            // 
            this.uxBoardContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uxBoardContainer.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.uxBoardContainer.Location = new System.Drawing.Point(12, 66);
            this.uxBoardContainer.Name = "uxBoardContainer";
            this.uxBoardContainer.Size = new System.Drawing.Size(386, 332);
            this.uxBoardContainer.TabIndex = 2;
            // 
            // UserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 397);
            this.Controls.Add(this.uxBoardContainer);
            this.Controls.Add(this.uxPlaceButtonContainer);
            this.Controls.Add(this.uxTopContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "UserInterface";
            this.Text = "Connect Four";
            this.uxTopContainer.ResumeLayout(false);
            this.uxTopContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel uxTopContainer;
        private System.Windows.Forms.FlowLayoutPanel uxPlaceButtonContainer;
        private System.Windows.Forms.FlowLayoutPanel uxBoardContainer;
        private System.Windows.Forms.Label uxTurnLabel;
        private System.Windows.Forms.Label uxPlayerLabel;
    }
}

