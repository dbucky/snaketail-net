using JWC;
using System;
using System.Windows.Forms;

namespace SnakeTail
{
    public partial class FavoriteFolderConfigForm : Form
    {
        public Favorite Favorite { get; }

        public FavoriteFolderConfigForm(Favorite favorite = null)
        {
            InitializeComponent();
            Favorite = favorite ?? new Favorite();
        }

        private void FavoriteFolderConfigForm_Load(object sender, EventArgs e)
        {
            _pathEdt.Text = Favorite.Path;
            _textEdt.Text = Favorite.Text;
        }

        private void _okBtn_Click(object sender, EventArgs e)
        {
            Favorite.Path = _pathEdt.Text;
            Favorite.Text = _textEdt.Text;
            DialogResult = DialogResult.OK;
        }

        private void _browseBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();

            if (!string.IsNullOrEmpty(Favorite.Path))
            {
                folderDialog.SelectedPath = Favorite.Path;
            }

            if (folderDialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            _pathEdt.Text = folderDialog.SelectedPath;
        }
    }
}
