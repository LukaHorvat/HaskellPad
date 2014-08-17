using HaskellPad.Styling;
namespace HaskellPad
{
	partial class MainWindow
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
			this.components = new System.ComponentModel.Container();
			HaskellPad.Styling.EditorTheme editorTheme1 = new HaskellPad.Styling.EditorTheme();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.richTextBox2 = new System.Windows.Forms.RichTextBox();
			this.ceCodeEditor = new HaskellPad.CodeEditor();
			this.bndEditorTheme = new System.Windows.Forms.BindingSource(this.components);
			this.listBox1 = new System.Windows.Forms.ListBox();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.bndEditorTheme)).BeginInit();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.Location = new System.Drawing.Point(0, 470);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(777, 22);
			this.statusStrip1.TabIndex = 0;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.listBox1);
			this.splitContainer1.Size = new System.Drawing.Size(777, 470);
			this.splitContainer1.SplitterDistance = 359;
			this.splitContainer1.TabIndex = 1;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.richTextBox2);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.ceCodeEditor);
			this.splitContainer2.Size = new System.Drawing.Size(777, 359);
			this.splitContainer2.SplitterDistance = 188;
			this.splitContainer2.TabIndex = 0;
			// 
			// richTextBox2
			// 
			this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.richTextBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBox2.Location = new System.Drawing.Point(0, 0);
			this.richTextBox2.Name = "richTextBox2";
			this.richTextBox2.Size = new System.Drawing.Size(188, 359);
			this.richTextBox2.TabIndex = 0;
			this.richTextBox2.Text = "";
			// 
			// ceCodeEditor
			// 
			this.ceCodeEditor.CodeBackgroundColor = System.Drawing.Color.Empty;
			this.ceCodeEditor.DataBindings.Add(new System.Windows.Forms.Binding("CodeBackgroundColor", this.bndEditorTheme, "CodeBackgroundColor", true));
			this.ceCodeEditor.DataBindings.Add(new System.Windows.Forms.Binding("Font", this.bndEditorTheme, "Font", true));
			this.ceCodeEditor.DataBindings.Add(new System.Windows.Forms.Binding("LineNumberBackgroundColor", this.bndEditorTheme, "LineNumberBackgroundColor", true));
			this.ceCodeEditor.DataBindings.Add(new System.Windows.Forms.Binding("LineNumberTextColor", this.bndEditorTheme, "LineNumberTextColor", true));
			this.ceCodeEditor.DataBindings.Add(new System.Windows.Forms.Binding("TabBackgroundColor", this.bndEditorTheme, "TabLabelBackgroundColor", true));
			this.ceCodeEditor.DataBindings.Add(new System.Windows.Forms.Binding("TabLabelBackgroundColor", this.bndEditorTheme, "TabLabelBackgroundColor", true));
			this.ceCodeEditor.DataBindings.Add(new System.Windows.Forms.Binding("TabLabelTextColor", this.bndEditorTheme, "TabLabelTextColor", true));
			this.ceCodeEditor.DataBindings.Add(new System.Windows.Forms.Binding("TabLength", this.bndEditorTheme, "TabLength", true));
			this.ceCodeEditor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ceCodeEditor.LineNumberBackgroundColor = System.Drawing.Color.Empty;
			this.ceCodeEditor.LineNumberTextColor = System.Drawing.Color.Empty;
			this.ceCodeEditor.Location = new System.Drawing.Point(0, 0);
			this.ceCodeEditor.Name = "ceCodeEditor";
			this.ceCodeEditor.Size = new System.Drawing.Size(585, 359);
			this.ceCodeEditor.TabBackgroundColor = System.Drawing.Color.Empty;
			this.ceCodeEditor.TabIndex = 0;
			this.ceCodeEditor.TabLabelBackgroundColor = System.Drawing.Color.Empty;
			this.ceCodeEditor.TabLabelTextColor = System.Drawing.Color.Empty;
			this.ceCodeEditor.TabLength = 0;
			// 
			// bndEditorTheme
			// 
			this.bndEditorTheme.DataSource = editorTheme1;
			this.bndEditorTheme.Position = 0;
			// 
			// listBox1
			// 
			this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listBox1.FormattingEnabled = true;
			this.listBox1.Location = new System.Drawing.Point(0, 0);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(777, 107);
			this.listBox1.TabIndex = 0;
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(777, 492);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.statusStrip1);
			this.Name = "MainWindow";
			this.Text = "HaskellPad";
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.bndEditorTheme)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.RichTextBox richTextBox2;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.BindingSource bndEditorTheme;
		private CodeEditor ceCodeEditor;


	}
}

