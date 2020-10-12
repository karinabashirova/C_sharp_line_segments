namespace test_task_c_sharp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.highlight_off = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.highlight_on = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.rnd_btn = new System.Windows.Forms.Button();
            this.draw_btn = new System.Windows.Forms.Button();
            this.save_txt = new System.Windows.Forms.Button();
            this.open_txt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // highlight_off
            // 
            this.highlight_off.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.highlight_off.AutoSize = true;
            this.highlight_off.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.highlight_off.Location = new System.Drawing.Point(670, 400);
            this.highlight_off.Name = "highlight_off";
            this.highlight_off.Size = new System.Drawing.Size(190, 50);
            this.highlight_off.TabIndex = 1;
            this.highlight_off.Text = "Выключить подсветку";
            this.highlight_off.UseVisualStyleBackColor = false;
            this.highlight_off.Click += new System.EventHandler(this.highlight_off_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(640, 590);
            this.flowLayoutPanel1.TabIndex = 2;
            this.flowLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.flowLayoutPanel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.flowLayoutPanel1_MouseClick);
            // 
            // highlight_on
            // 
            this.highlight_on.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.highlight_on.Location = new System.Drawing.Point(670, 456);
            this.highlight_on.Name = "highlight_on";
            this.highlight_on.Size = new System.Drawing.Size(190, 44);
            this.highlight_on.TabIndex = 3;
            this.highlight_on.Text = "Включить подсветку";
            this.highlight_on.UseVisualStyleBackColor = false;
            this.highlight_on.Click += new System.EventHandler(this.highlight_on_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.label1.Location = new System.Drawing.Point(658, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(213, 153);
            this.label1.TabIndex = 4;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // rnd_btn
            // 
            this.rnd_btn.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.rnd_btn.Location = new System.Drawing.Point(670, 290);
            this.rnd_btn.Name = "rnd_btn";
            this.rnd_btn.Size = new System.Drawing.Size(190, 47);
            this.rnd_btn.TabIndex = 5;
            this.rnd_btn.Text = "Нарисовать рандомные отрезки";
            this.rnd_btn.UseVisualStyleBackColor = false;
            this.rnd_btn.Click += new System.EventHandler(this.rnd_btn_Click);
            // 
            // draw_btn
            // 
            this.draw_btn.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.draw_btn.Location = new System.Drawing.Point(670, 343);
            this.draw_btn.Name = "draw_btn";
            this.draw_btn.Size = new System.Drawing.Size(190, 51);
            this.draw_btn.TabIndex = 6;
            this.draw_btn.Text = "Нарисовать новые отрезки вручную";
            this.draw_btn.UseVisualStyleBackColor = false;
            this.draw_btn.Click += new System.EventHandler(this.draw_btn_Click);
            // 
            // save_txt
            // 
            this.save_txt.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.save_txt.Location = new System.Drawing.Point(670, 506);
            this.save_txt.Name = "save_txt";
            this.save_txt.Size = new System.Drawing.Size(190, 45);
            this.save_txt.TabIndex = 7;
            this.save_txt.Text = "Сохранить координаты в файл";
            this.save_txt.UseVisualStyleBackColor = false;
            this.save_txt.Click += new System.EventHandler(this.save_txt_Click);
            // 
            // open_txt
            // 
            this.open_txt.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.open_txt.Location = new System.Drawing.Point(670, 557);
            this.open_txt.Name = "open_txt";
            this.open_txt.Size = new System.Drawing.Size(190, 45);
            this.open_txt.TabIndex = 8;
            this.open_txt.Text = "Считать из файла";
            this.open_txt.UseVisualStyleBackColor = false;
            this.open_txt.Click += new System.EventHandler(this.open_txt_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 653);
            this.Controls.Add(this.open_txt);
            this.Controls.Add(this.save_txt);
            this.Controls.Add(this.draw_btn);
            this.Controls.Add(this.rnd_btn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.highlight_on);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.highlight_off);
            this.MaximumSize = new System.Drawing.Size(900, 700);
            this.MinimumSize = new System.Drawing.Size(900, 700);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button highlight_off;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button highlight_on;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button rnd_btn;
        private System.Windows.Forms.Button draw_btn;
        private System.Windows.Forms.Button save_txt;
        private System.Windows.Forms.Button open_txt;
    }
}

