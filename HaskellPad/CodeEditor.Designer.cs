namespace HaskellPad
{
	partial class CodeEditor
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.flpTabView = new System.Windows.Forms.FlowLayoutPanel();
			this.rtbCodeView = new System.Windows.Forms.RichTextBox();
			this.lFocusLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// flpTabView
			// 
			this.flpTabView.Dock = System.Windows.Forms.DockStyle.Top;
			this.flpTabView.Location = new System.Drawing.Point(0, 0);
			this.flpTabView.Name = "flpTabView";
			this.flpTabView.Size = new System.Drawing.Size(439, 20);
			this.flpTabView.TabIndex = 1;
			// 
			// rtbCodeView
			// 
			this.rtbCodeView.AcceptsTab = true;
			this.rtbCodeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.rtbCodeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtbCodeView.Location = new System.Drawing.Point(0, 20);
			this.rtbCodeView.Name = "rtbCodeView";
			this.rtbCodeView.Size = new System.Drawing.Size(439, 209);
			this.rtbCodeView.TabIndex = 3;
			this.rtbCodeView.Text = "";
			// 
			// lFocusLabel
			// 
			this.lFocusLabel.AutoSize = true;
			this.lFocusLabel.Location = new System.Drawing.Point(300, 121);
			this.lFocusLabel.Name = "lFocusLabel";
			this.lFocusLabel.Size = new System.Drawing.Size(0, 13);
			this.lFocusLabel.TabIndex = 4;
			// 
			// CodeEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lFocusLabel);
			this.Controls.Add(this.rtbCodeView);
			this.Controls.Add(this.flpTabView);
			this.Name = "CodeEditor";
			this.Size = new System.Drawing.Size(439, 229);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel flpTabView;
		private System.Windows.Forms.RichTextBox rtbCodeView;
		private System.Windows.Forms.VScrollBar vScrollBar1;
		private System.Windows.Forms.Label lFocusLabel;
	}
}
