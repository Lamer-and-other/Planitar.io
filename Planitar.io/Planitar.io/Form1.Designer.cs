namespace Planitar.io
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.actionButton = new System.Windows.Forms.Button();
            this.NameBox = new System.Windows.Forms.TextBox();
            this.WellcomePanel = new System.Windows.Forms.Panel();
            this.labelName = new System.Windows.Forms.Label();
            this.labelChekNotifyLable = new System.Windows.Forms.Label();
            this.getPlayerByHandButton = new System.Windows.Forms.Button();
            this.chekNotifyButton = new System.Windows.Forms.Button();
            this.PlayerList = new System.Windows.Forms.ListBox();
            this.testPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.labelBONUS = new System.Windows.Forms.Label();
            this.labelPersonalRecord = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.T_MouseMove = new System.Windows.Forms.Timer(this.components);
            this.labelX = new System.Windows.Forms.Label();
            this.labelY = new System.Windows.Forms.Label();
            this.labelScore = new System.Windows.Forms.Label();
            this.WellcomePanel.SuspendLayout();
            this.testPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // actionButton
            // 
            this.actionButton.BackColor = System.Drawing.Color.Red;
            this.actionButton.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.actionButton.Location = new System.Drawing.Point(220, 188);
            this.actionButton.Name = "actionButton";
            this.actionButton.Size = new System.Drawing.Size(402, 35);
            this.actionButton.TabIndex = 0;
            this.actionButton.Text = "В бой";
            this.actionButton.UseVisualStyleBackColor = false;
            this.actionButton.Click += new System.EventHandler(this.actionButton_Click);
            // 
            // NameBox
            // 
            this.NameBox.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NameBox.Location = new System.Drawing.Point(220, 145);
            this.NameBox.Name = "NameBox";
            this.NameBox.Size = new System.Drawing.Size(402, 37);
            this.NameBox.TabIndex = 1;
            this.NameBox.TextChanged += new System.EventHandler(this.NameBox_TextChanged);
            // 
            // WellcomePanel
            // 
            this.WellcomePanel.BackColor = System.Drawing.Color.Gainsboro;
            this.WellcomePanel.Controls.Add(this.labelName);
            this.WellcomePanel.Controls.Add(this.NameBox);
            this.WellcomePanel.Controls.Add(this.actionButton);
            this.WellcomePanel.Controls.Add(this.labelChekNotifyLable);
            this.WellcomePanel.Location = new System.Drawing.Point(145, 128);
            this.WellcomePanel.Name = "WellcomePanel";
            this.WellcomePanel.Size = new System.Drawing.Size(793, 355);
            this.WellcomePanel.TabIndex = 2;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Comic Sans MS", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelName.Location = new System.Drawing.Point(383, 91);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(75, 40);
            this.labelName.TabIndex = 4;
            this.labelName.Text = "Имя";
            // 
            // labelChekNotifyLable
            // 
            this.labelChekNotifyLable.AutoSize = true;
            this.labelChekNotifyLable.Location = new System.Drawing.Point(10, 327);
            this.labelChekNotifyLable.Name = "labelChekNotifyLable";
            this.labelChekNotifyLable.Size = new System.Drawing.Size(35, 13);
            this.labelChekNotifyLable.TabIndex = 3;
            this.labelChekNotifyLable.Text = "label1";
            // 
            // getPlayerByHandButton
            // 
            this.getPlayerByHandButton.Location = new System.Drawing.Point(3, 3);
            this.getPlayerByHandButton.Name = "getPlayerByHandButton";
            this.getPlayerByHandButton.Size = new System.Drawing.Size(171, 35);
            this.getPlayerByHandButton.TabIndex = 4;
            this.getPlayerByHandButton.Text = "Получить игроков";
            this.getPlayerByHandButton.UseVisualStyleBackColor = true;
            this.getPlayerByHandButton.Click += new System.EventHandler(this.getPlayerByHandButton_Click);
            // 
            // chekNotifyButton
            // 
            this.chekNotifyButton.Location = new System.Drawing.Point(180, 3);
            this.chekNotifyButton.Name = "chekNotifyButton";
            this.chekNotifyButton.Size = new System.Drawing.Size(217, 38);
            this.chekNotifyButton.TabIndex = 2;
            this.chekNotifyButton.Text = "Проверить уведомление игроков";
            this.chekNotifyButton.UseVisualStyleBackColor = true;
            this.chekNotifyButton.Click += new System.EventHandler(this.chekNotifyButton_Click);
            // 
            // PlayerList
            // 
            this.PlayerList.BackColor = System.Drawing.Color.White;
            this.PlayerList.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PlayerList.FormattingEnabled = true;
            this.PlayerList.ItemHeight = 15;
            this.PlayerList.Location = new System.Drawing.Point(966, 10);
            this.PlayerList.Name = "PlayerList";
            this.PlayerList.Size = new System.Drawing.Size(210, 259);
            this.PlayerList.TabIndex = 3;
            // 
            // testPanel
            // 
            this.testPanel.Controls.Add(this.getPlayerByHandButton);
            this.testPanel.Controls.Add(this.chekNotifyButton);
            this.testPanel.Controls.Add(this.labelBONUS);
            this.testPanel.Controls.Add(this.labelX);
            this.testPanel.Controls.Add(this.labelY);
            this.testPanel.Controls.Add(this.labelScore);
            this.testPanel.Location = new System.Drawing.Point(13, 547);
            this.testPanel.Name = "testPanel";
            this.testPanel.Size = new System.Drawing.Size(1189, 43);
            this.testPanel.TabIndex = 4;
            // 
            // labelBONUS
            // 
            this.labelBONUS.AutoSize = true;
            this.labelBONUS.Location = new System.Drawing.Point(403, 0);
            this.labelBONUS.Name = "labelBONUS";
            this.labelBONUS.Size = new System.Drawing.Size(35, 13);
            this.labelBONUS.TabIndex = 6;
            this.labelBONUS.Text = "label1";
            // 
            // labelPersonalRecord
            // 
            this.labelPersonalRecord.AutoSize = true;
            this.labelPersonalRecord.Font = new System.Drawing.Font("Comic Sans MS", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPersonalRecord.Location = new System.Drawing.Point(12, 10);
            this.labelPersonalRecord.Name = "labelPersonalRecord";
            this.labelPersonalRecord.Size = new System.Drawing.Size(145, 38);
            this.labelPersonalRecord.TabIndex = 5;
            this.labelPersonalRecord.Text = "Рекорд:  0";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.WellcomePanel);
            this.panel1.Controls.Add(this.labelPersonalRecord);
            this.panel1.Controls.Add(this.PlayerList);
            this.panel1.Location = new System.Drawing.Point(2, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1190, 538);
            this.panel1.TabIndex = 6;
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_MouseMove);
            this.panel1.Resize += new System.EventHandler(this.panel_Resize);
            // 
            // T_MouseMove
            // 
            this.T_MouseMove.Tick += new System.EventHandler(this.T_MouseMove_Tick);
            // 
            // labelX
            // 
            this.labelX.AutoSize = true;
            this.labelX.Location = new System.Drawing.Point(444, 0);
            this.labelX.Name = "labelX";
            this.labelX.Size = new System.Drawing.Size(35, 13);
            this.labelX.TabIndex = 7;
            this.labelX.Text = "label1";
            // 
            // labelY
            // 
            this.labelY.AutoSize = true;
            this.labelY.Location = new System.Drawing.Point(485, 0);
            this.labelY.Name = "labelY";
            this.labelY.Size = new System.Drawing.Size(35, 13);
            this.labelY.TabIndex = 8;
            this.labelY.Text = "label2";
            // 
            // labelScore
            // 
            this.labelScore.AutoSize = true;
            this.labelScore.Location = new System.Drawing.Point(526, 0);
            this.labelScore.Name = "labelScore";
            this.labelScore.Size = new System.Drawing.Size(35, 13);
            this.labelScore.TabIndex = 9;
            this.labelScore.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1204, 589);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.testPanel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.WellcomePanel.ResumeLayout(false);
            this.WellcomePanel.PerformLayout();
            this.testPanel.ResumeLayout(false);
            this.testPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button actionButton;
        private System.Windows.Forms.TextBox NameBox;
        private System.Windows.Forms.Panel WellcomePanel;
        private System.Windows.Forms.Label labelChekNotifyLable;
        private System.Windows.Forms.Button chekNotifyButton;
        private System.Windows.Forms.ListBox PlayerList;
        private System.Windows.Forms.Button getPlayerByHandButton;
        private System.Windows.Forms.FlowLayoutPanel testPanel;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelPersonalRecord;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer T_MouseMove;
        private System.Windows.Forms.Label labelBONUS;
        private System.Windows.Forms.Label labelX;
        private System.Windows.Forms.Label labelY;
        private System.Windows.Forms.Label labelScore;
    }
}

