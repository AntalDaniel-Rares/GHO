
namespace GHO
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btn_new_game = new System.Windows.Forms.Button();
            this.pct1 = new System.Windows.Forms.PictureBox();
            this.btn_quit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pct1)).BeginInit();
            this.SuspendLayout();
            // 
            // Btn_new_game
            // 
            this.btn_new_game.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_new_game.Location = new System.Drawing.Point(353, 165);
            this.btn_new_game.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_new_game.Name = "btn_new_game";
            this.btn_new_game.Size = new System.Drawing.Size(115, 46);
            this.btn_new_game.TabIndex = 0;
            this.btn_new_game.Text = "New game";
            this.btn_new_game.UseVisualStyleBackColor = true;
            this.btn_new_game.Click += new System.EventHandler(this.btn_new_game_Click);
            // 
            // Btn_quit
            // 
            this.btn_quit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_quit.Location = new System.Drawing.Point(353, 165);
            this.btn_quit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_quit.Name = "btn_quit";
            this.btn_quit.Size = new System.Drawing.Size(115, 46);
            this.btn_quit.TabIndex = 0;
            this.btn_quit.Text = "Quit";
            this.btn_quit.UseVisualStyleBackColor = true;
            this.btn_quit.Click += new System.EventHandler(this.btn_quit_Click);
            // 
            // pct1
            // 
            this.pct1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pct1.BackgroundImage")));
            this.pct1.Location = new System.Drawing.Point(335, 66);
            this.pct1.Margin = new System.Windows.Forms.Padding(4);
            this.pct1.Name = "pct1";
            this.pct1.Size = new System.Drawing.Size(858, 632);
            this.pct1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pct1.TabIndex = 1;
            this.pct1.TabStop = false;
            this.pct1.Click += new System.EventHandler(this.pct1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1535, 602);
            this.Controls.Add(this.pct1);
            this.Controls.Add(this.btn_new_game);
            this.Controls.Add(this.btn_quit);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "GHO";
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pct1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_new_game;
        private System.Windows.Forms.PictureBox pct1;
        private System.Windows.Forms.Button btn_quit;
    }
}

