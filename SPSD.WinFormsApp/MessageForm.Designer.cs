namespace SPSD.WinFormsApp
{
    partial class MessageForm
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
            label1 = new Label();
            label2 = new Label();
            txtCpu = new TextBox();
            txtMemory = new TextBox();
            btnSave = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(73, 31);
            label1.Name = "label1";
            label1.Size = new Size(48, 17);
            label1.TabIndex = 0;
            label1.Text = "CPU： ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(73, 85);
            label2.Name = "label2";
            label2.Size = new Size(64, 17);
            label2.TabIndex = 1;
            label2.Text = "内存(M)：";
            // 
            // txtCpu
            // 
            txtCpu.Location = new Point(138, 30);
            txtCpu.Name = "txtCpu";
            txtCpu.Size = new Size(162, 23);
            txtCpu.TabIndex = 2;
            // 
            // txtMemory
            // 
            txtMemory.Location = new Point(138, 79);
            txtMemory.Name = "txtMemory";
            txtMemory.Size = new Size(162, 23);
            txtMemory.TabIndex = 3;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(76, 136);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 4;
            btnSave.Text = "确认";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(201, 138);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "取消";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // MessageForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(437, 183);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(txtMemory);
            Controls.Add(txtCpu);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "MessageForm";
            Text = "MessageForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox txtCpu;
        private TextBox txtMemory;
        private Button btnSave;
        private Button btnCancel;
    }
}