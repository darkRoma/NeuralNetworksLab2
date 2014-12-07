namespace NeuralNetworksLab2
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.buttonShowLorenz = new System.Windows.Forms.Button();
            this.buttonTrainNetwork = new System.Windows.Forms.Button();
            this.buttonPredict = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(34, 26);
            this.chart1.Name = "chart1";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(1091, 322);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // buttonShowLorenz
            // 
            this.buttonShowLorenz.Location = new System.Drawing.Point(34, 389);
            this.buttonShowLorenz.Name = "buttonShowLorenz";
            this.buttonShowLorenz.Size = new System.Drawing.Size(177, 28);
            this.buttonShowLorenz.TabIndex = 1;
            this.buttonShowLorenz.Text = "Show Lorenz";
            this.buttonShowLorenz.UseVisualStyleBackColor = true;
            this.buttonShowLorenz.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonTrainNetwork
            // 
            this.buttonTrainNetwork.Location = new System.Drawing.Point(249, 386);
            this.buttonTrainNetwork.Name = "buttonTrainNetwork";
            this.buttonTrainNetwork.Size = new System.Drawing.Size(177, 28);
            this.buttonTrainNetwork.TabIndex = 2;
            this.buttonTrainNetwork.Text = "Train Network";
            this.buttonTrainNetwork.UseVisualStyleBackColor = true;
            this.buttonTrainNetwork.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonPredict
            // 
            this.buttonPredict.Location = new System.Drawing.Point(456, 386);
            this.buttonPredict.Name = "buttonPredict";
            this.buttonPredict.Size = new System.Drawing.Size(177, 28);
            this.buttonPredict.TabIndex = 3;
            this.buttonPredict.Text = "Predict behavior";
            this.buttonPredict.UseVisualStyleBackColor = true;
            this.buttonPredict.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(255, 442);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Error";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(301, 442);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1150, 487);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonPredict);
            this.Controls.Add(this.buttonTrainNetwork);
            this.Controls.Add(this.buttonShowLorenz);
            this.Controls.Add(this.chart1);
            this.Name = "Form1";
            this.Text = "NeuralNetworksLab2";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button buttonShowLorenz;
        private System.Windows.Forms.Button buttonTrainNetwork;
        private System.Windows.Forms.Button buttonPredict;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
    }
}

