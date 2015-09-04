namespace TreeBuilder
{
    partial class TreeBuilder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TreeBuilder));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.btnEat = new System.Windows.Forms.Button();
            this.tbJson = new System.Windows.Forms.TextBox();
            this.btnBuild = new System.Windows.Forms.Button();
            this.tbKey = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbParent = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbStart = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbAsk = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbQuery = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSendRequest = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(12, 12);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(980, 665);
            this.treeView1.TabIndex = 0;
            // 
            // btnEat
            // 
            this.btnEat.Location = new System.Drawing.Point(1002, 343);
            this.btnEat.Name = "btnEat";
            this.btnEat.Size = new System.Drawing.Size(159, 23);
            this.btnEat.TabIndex = 1;
            this.btnEat.Text = "Накормить";
            this.btnEat.UseVisualStyleBackColor = true;
            this.btnEat.Click += new System.EventHandler(this.btnEat_Click);
            // 
            // tbJson
            // 
            this.tbJson.Location = new System.Drawing.Point(1080, 225);
            this.tbJson.Multiline = true;
            this.tbJson.Name = "tbJson";
            this.tbJson.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbJson.Size = new System.Drawing.Size(303, 97);
            this.tbJson.TabIndex = 2;
            // 
            // btnBuild
            // 
            this.btnBuild.Location = new System.Drawing.Point(1208, 343);
            this.btnBuild.Name = "btnBuild";
            this.btnBuild.Size = new System.Drawing.Size(175, 23);
            this.btnBuild.TabIndex = 3;
            this.btnBuild.Text = "Построить";
            this.btnBuild.UseVisualStyleBackColor = true;
            this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);
            // 
            // tbKey
            // 
            this.tbKey.Location = new System.Drawing.Point(1080, 18);
            this.tbKey.Name = "tbKey";
            this.tbKey.Size = new System.Drawing.Size(303, 20);
            this.tbKey.TabIndex = 4;
            this.tbKey.Text = "\"categories_id\"";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(999, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Ключ:";
            // 
            // tbParent
            // 
            this.tbParent.Location = new System.Drawing.Point(1080, 43);
            this.tbParent.Name = "tbParent";
            this.tbParent.Size = new System.Drawing.Size(303, 20);
            this.tbParent.TabIndex = 4;
            this.tbParent.Text = "\"parent_id\"";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(999, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Родитель:";
            // 
            // tbStart
            // 
            this.tbStart.Location = new System.Drawing.Point(1080, 69);
            this.tbStart.Name = "tbStart";
            this.tbStart.Size = new System.Drawing.Size(303, 20);
            this.tbStart.TabIndex = 4;
            this.tbStart.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(999, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Старт:";
            // 
            // tbAsk
            // 
            this.tbAsk.Location = new System.Drawing.Point(1080, 95);
            this.tbAsk.Multiline = true;
            this.tbAsk.Name = "tbAsk";
            this.tbAsk.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbAsk.Size = new System.Drawing.Size(303, 54);
            this.tbAsk.TabIndex = 4;
            this.tbAsk.Text = "http://svk.rezerv.odezhda-master.ru/api/categories";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(999, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "Запрос:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(999, 228);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 17);
            this.label5.TabIndex = 5;
            this.label5.Text = "JSON:";
            // 
            // tbQuery
            // 
            this.tbQuery.Location = new System.Drawing.Point(1080, 155);
            this.tbQuery.Multiline = true;
            this.tbQuery.Name = "tbQuery";
            this.tbQuery.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbQuery.Size = new System.Drawing.Size(303, 54);
            this.tbQuery.TabIndex = 4;
            this.tbQuery.Text = "per-page=100";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(999, 155);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 17);
            this.label6.TabIndex = 5;
            this.label6.Text = "Query:";
            // 
            // btnSendRequest
            // 
            this.btnSendRequest.Location = new System.Drawing.Point(1002, 382);
            this.btnSendRequest.Name = "btnSendRequest";
            this.btnSendRequest.Size = new System.Drawing.Size(159, 23);
            this.btnSendRequest.TabIndex = 6;
            this.btnSendRequest.Text = "Отправить запрос";
            this.btnSendRequest.UseVisualStyleBackColor = true;
            this.btnSendRequest.Click += new System.EventHandler(this.btnSendRequest_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(1002, 428);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(381, 23);
            this.progressBar1.TabIndex = 7;
            // 
            // TreeBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1395, 689);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnSendRequest);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbQuery);
            this.Controls.Add(this.tbAsk);
            this.Controls.Add(this.tbStart);
            this.Controls.Add(this.tbParent);
            this.Controls.Add(this.tbKey);
            this.Controls.Add(this.btnBuild);
            this.Controls.Add(this.tbJson);
            this.Controls.Add(this.btnEat);
            this.Controls.Add(this.treeView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TreeBuilder";
            this.Text = "TreeBuilder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button btnEat;
        private System.Windows.Forms.TextBox tbJson;
        private System.Windows.Forms.Button btnBuild;
        private System.Windows.Forms.TextBox tbKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbParent;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbStart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbAsk;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbQuery;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSendRequest;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

