namespace SnakeTail
{
    partial class ManageFavoriteFoldersForm
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
            System.Windows.Forms.ColumnHeader favoriteFolderPathColumnHeader;
            System.Windows.Forms.ColumnHeader favoriteFolderTextColumnHeader;
            this.addFavoriteFolderBtn = new System.Windows.Forms.Button();
            this.editFavoriteFolderBtn = new System.Windows.Forms.Button();
            this.removeFavoriteFolderBtn = new System.Windows.Forms.Button();
            this.moveFavoriteFolderUpBtn = new System.Windows.Forms.Button();
            this.moveFavoriteFolderDownBtn = new System.Windows.Forms.Button();
            this._favoriteFoldersListView = new System.Windows.Forms.ListView();
            this._cancelBtn = new System.Windows.Forms.Button();
            this._okBtn = new System.Windows.Forms.Button();
            favoriteFolderPathColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            favoriteFolderTextColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // favoriteFolderPathColumnHeader
            // 
            favoriteFolderPathColumnHeader.Text = "Path";
            favoriteFolderPathColumnHeader.Width = 232;
            // 
            // favoriteFolderTextColumnHeader
            // 
            favoriteFolderTextColumnHeader.Text = "Text";
            favoriteFolderTextColumnHeader.Width = 200;
            // 
            // addFavoriteFolderBtn
            // 
            this.addFavoriteFolderBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addFavoriteFolderBtn.Location = new System.Drawing.Point(515, 7);
            this.addFavoriteFolderBtn.Margin = new System.Windows.Forms.Padding(4);
            this.addFavoriteFolderBtn.Name = "addFavoriteFolderBtn";
            this.addFavoriteFolderBtn.Size = new System.Drawing.Size(100, 28);
            this.addFavoriteFolderBtn.TabIndex = 1;
            this.addFavoriteFolderBtn.Text = "Add";
            this.addFavoriteFolderBtn.UseVisualStyleBackColor = true;
            this.addFavoriteFolderBtn.Click += new System.EventHandler(this.addFavoriteFolderBtn_Click);
            // 
            // editFavoriteFolderBtn
            // 
            this.editFavoriteFolderBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.editFavoriteFolderBtn.Location = new System.Drawing.Point(515, 43);
            this.editFavoriteFolderBtn.Margin = new System.Windows.Forms.Padding(4);
            this.editFavoriteFolderBtn.Name = "editFavoriteFolderBtn";
            this.editFavoriteFolderBtn.Size = new System.Drawing.Size(100, 28);
            this.editFavoriteFolderBtn.TabIndex = 2;
            this.editFavoriteFolderBtn.Text = "Edit...";
            this.editFavoriteFolderBtn.UseVisualStyleBackColor = true;
            this.editFavoriteFolderBtn.Click += new System.EventHandler(this.editFavoriteFolderBtn_Click);
            // 
            // removeFavoriteFolderBtn
            // 
            this.removeFavoriteFolderBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.removeFavoriteFolderBtn.Location = new System.Drawing.Point(515, 79);
            this.removeFavoriteFolderBtn.Margin = new System.Windows.Forms.Padding(4);
            this.removeFavoriteFolderBtn.Name = "removeFavoriteFolderBtn";
            this.removeFavoriteFolderBtn.Size = new System.Drawing.Size(100, 28);
            this.removeFavoriteFolderBtn.TabIndex = 3;
            this.removeFavoriteFolderBtn.Text = "Remove";
            this.removeFavoriteFolderBtn.UseVisualStyleBackColor = true;
            this.removeFavoriteFolderBtn.Click += new System.EventHandler(this.removeFavoriteFolderBtn_Click);
            // 
            // moveFavoriteFolderUpBtn
            // 
            this.moveFavoriteFolderUpBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.moveFavoriteFolderUpBtn.Location = new System.Drawing.Point(515, 145);
            this.moveFavoriteFolderUpBtn.Margin = new System.Windows.Forms.Padding(4);
            this.moveFavoriteFolderUpBtn.Name = "moveFavoriteFolderUpBtn";
            this.moveFavoriteFolderUpBtn.Size = new System.Drawing.Size(100, 28);
            this.moveFavoriteFolderUpBtn.TabIndex = 4;
            this.moveFavoriteFolderUpBtn.Text = "Move Up";
            this.moveFavoriteFolderUpBtn.UseVisualStyleBackColor = true;
            this.moveFavoriteFolderUpBtn.Click += new System.EventHandler(this.moveFavoriteFolderUpBtn_Click);
            // 
            // moveFavoriteFolderDownBtn
            // 
            this.moveFavoriteFolderDownBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.moveFavoriteFolderDownBtn.Location = new System.Drawing.Point(515, 181);
            this.moveFavoriteFolderDownBtn.Margin = new System.Windows.Forms.Padding(4);
            this.moveFavoriteFolderDownBtn.Name = "moveFavoriteFolderDownBtn";
            this.moveFavoriteFolderDownBtn.Size = new System.Drawing.Size(100, 28);
            this.moveFavoriteFolderDownBtn.TabIndex = 5;
            this.moveFavoriteFolderDownBtn.Text = "Move Down";
            this.moveFavoriteFolderDownBtn.UseVisualStyleBackColor = true;
            this.moveFavoriteFolderDownBtn.Click += new System.EventHandler(this.moveFavoriteFolderDownBtn_Click);
            // 
            // _favoriteFoldersListView
            // 
            this._favoriteFoldersListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._favoriteFoldersListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            favoriteFolderPathColumnHeader,
            favoriteFolderTextColumnHeader});
            this._favoriteFoldersListView.FullRowSelect = true;
            this._favoriteFoldersListView.HideSelection = false;
            this._favoriteFoldersListView.Location = new System.Drawing.Point(8, 7);
            this._favoriteFoldersListView.Margin = new System.Windows.Forms.Padding(4);
            this._favoriteFoldersListView.MultiSelect = false;
            this._favoriteFoldersListView.Name = "_favoriteFoldersListView";
            this._favoriteFoldersListView.Size = new System.Drawing.Size(499, 262);
            this._favoriteFoldersListView.TabIndex = 0;
            this._favoriteFoldersListView.UseCompatibleStateImageBehavior = false;
            this._favoriteFoldersListView.View = System.Windows.Forms.View.Details;
            // 
            // _cancelBtn
            // 
            this._cancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancelBtn.Location = new System.Drawing.Point(539, 288);
            this._cancelBtn.Name = "_cancelBtn";
            this._cancelBtn.Size = new System.Drawing.Size(75, 23);
            this._cancelBtn.TabIndex = 6;
            this._cancelBtn.Text = "Cancel";
            this._cancelBtn.UseVisualStyleBackColor = true;
            // 
            // _okBtn
            // 
            this._okBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._okBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._okBtn.Location = new System.Drawing.Point(458, 287);
            this._okBtn.Name = "_okBtn";
            this._okBtn.Size = new System.Drawing.Size(75, 23);
            this._okBtn.TabIndex = 7;
            this._okBtn.Text = "OK";
            this._okBtn.UseVisualStyleBackColor = true;
            this._okBtn.Click += new System.EventHandler(this._okBtn_Click);
            // 
            // ManageFavoriteFoldersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 318);
            this.Controls.Add(this._okBtn);
            this.Controls.Add(this._cancelBtn);
            this.Controls.Add(this._favoriteFoldersListView);
            this.Controls.Add(this.moveFavoriteFolderDownBtn);
            this.Controls.Add(this.moveFavoriteFolderUpBtn);
            this.Controls.Add(this.removeFavoriteFolderBtn);
            this.Controls.Add(this.editFavoriteFolderBtn);
            this.Controls.Add(this.addFavoriteFolderBtn);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ManageFavoriteFoldersForm";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.Text = "Manage Favorite Folders";
            this.Load += new System.EventHandler(this.ManageFavoriteFoldersForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button addFavoriteFolderBtn;
        private System.Windows.Forms.Button editFavoriteFolderBtn;
        private System.Windows.Forms.Button removeFavoriteFolderBtn;
        private System.Windows.Forms.Button moveFavoriteFolderUpBtn;
        private System.Windows.Forms.Button moveFavoriteFolderDownBtn;
        private System.Windows.Forms.ListView _favoriteFoldersListView;
        private System.Windows.Forms.Button _cancelBtn;
        private System.Windows.Forms.Button _okBtn;
    }
}