namespace FirstWinFormsApp
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            IntegralOnlyCheckBox = new CheckBox();
            PrecisionRequiredCheckBox = new CheckBox();
            label1 = new Label();
            label2 = new Label();
            MinValueTextBox = new TextBox();
            MaxValueTextBox = new TextBox();
            SuggestionLabel = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // IntegralOnlyCheckBox
            // 
            IntegralOnlyCheckBox.AutoSize = true;
            IntegralOnlyCheckBox.CheckAlign = ContentAlignment.MiddleRight;
            IntegralOnlyCheckBox.Checked = true;
            IntegralOnlyCheckBox.CheckState = CheckState.Checked;
            IntegralOnlyCheckBox.Font = new Font("Segoe UI", 14F);
            IntegralOnlyCheckBox.Location = new Point(31, 110);
            IntegralOnlyCheckBox.Name = "IntegralOnlyCheckBox";
            IntegralOnlyCheckBox.Size = new Size(149, 29);
            IntegralOnlyCheckBox.TabIndex = 0;
            IntegralOnlyCheckBox.Text = "Integral Only?";
            IntegralOnlyCheckBox.UseVisualStyleBackColor = true;
            // 
            // PrecisionRequiredCheckBox
            // 
            PrecisionRequiredCheckBox.AutoSize = true;
            PrecisionRequiredCheckBox.CheckAlign = ContentAlignment.MiddleRight;
            PrecisionRequiredCheckBox.Font = new Font("Segoe UI", 14F);
            PrecisionRequiredCheckBox.Location = new Point(7, 145);
            PrecisionRequiredCheckBox.Name = "PrecisionRequiredCheckBox";
            PrecisionRequiredCheckBox.Size = new Size(173, 29);
            PrecisionRequiredCheckBox.TabIndex = 1;
            PrecisionRequiredCheckBox.Text = "Must be precise?";
            PrecisionRequiredCheckBox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F);
            label1.Location = new Point(61, 25);
            label1.Name = "label1";
            label1.Size = new Size(99, 25);
            label1.TabIndex = 2;
            label1.Text = "Min value:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14F);
            label2.Location = new Point(58, 71);
            label2.Name = "label2";
            label2.Size = new Size(102, 25);
            label2.TabIndex = 3;
            label2.Text = "Max value:";
            // 
            // MinValueTextBox
            // 
            MinValueTextBox.Font = new Font("Segoe UI", 14F);
            MinValueTextBox.Location = new Point(166, 21);
            MinValueTextBox.Name = "MinValueTextBox";
            MinValueTextBox.Size = new Size(458, 32);
            MinValueTextBox.TabIndex = 4;
            // 
            // MaxValueTextBox
            // 
            MaxValueTextBox.Font = new Font("Segoe UI", 14F);
            MaxValueTextBox.Location = new Point(166, 67);
            MaxValueTextBox.Name = "MaxValueTextBox";
            MaxValueTextBox.Size = new Size(458, 32);
            MaxValueTextBox.TabIndex = 5;
            // 
            // SuggestionLabel
            // 
            SuggestionLabel.AutoSize = true;
            SuggestionLabel.Font = new Font("Segoe UI", 16F);
            SuggestionLabel.Location = new Point(166, 192);
            SuggestionLabel.Name = "SuggestionLabel";
            SuggestionLabel.Size = new Size(177, 30);
            SuggestionLabel.TabIndex = 6;
            SuggestionLabel.Text = "Not enough data";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label3.Location = new Point(2, 196);
            label3.Name = "label3";
            label3.Size = new Size(158, 25);
            label3.TabIndex = 7;
            label3.Text = "Suggested Type:";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightGray;
            ClientSize = new Size(636, 232);
            Controls.Add(label3);
            Controls.Add(SuggestionLabel);
            Controls.Add(MaxValueTextBox);
            Controls.Add(MinValueTextBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(PrecisionRequiredCheckBox);
            Controls.Add(IntegralOnlyCheckBox);
            Name = "MainForm";
            Text = "C# Numeric Types";
            Load += MainForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox IntegralOnlyCheckBox;
        private CheckBox PrecisionRequiredCheckBox;
        private Label label1;
        private Label label2;
        private TextBox MinValueTextBox;
        private TextBox MaxValueTextBox;
        private Label SuggestionLabel;
        private Label label3;
    }
}
