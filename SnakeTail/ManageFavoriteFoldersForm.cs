using JWC;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SnakeTail
{
    public partial class ManageFavoriteFoldersForm : Form
    {
        public ManageFavoriteFoldersForm()
        {
            InitializeComponent();
        }

        private void ManageFavoriteFoldersForm_Load(object sender, EventArgs e)
        {
            var mainForm = Owner as MainForm;

            if (mainForm != null)
            {
                foreach (var favorite in mainForm._favoriteFoldersMenu.GetFavorites())
                {
                    var lvi = _favoriteFoldersListView.Items.Add(new ListViewItem());
                    UpdateListViewItem(favorite, ref lvi);
                }
            }
        }

        private void addFavoriteFolderBtn_Click(object sender, EventArgs e)
        {
            var configForm = new FavoriteFolderConfigForm();

            if (configForm.ShowDialog(this) == DialogResult.OK && !string.IsNullOrEmpty(configForm.Favorite.Path))
            {
                var lvi = _favoriteFoldersListView.Items.Add(new ListViewItem());
                UpdateListViewItem(configForm.Favorite, ref lvi);
            }
        }

        private void editFavoriteFolderBtn_Click(object sender, EventArgs e)
        {
            if (_favoriteFoldersListView.SelectedItems.Count == 0)
                return;

            var configForm = new FavoriteFolderConfigForm(_favoriteFoldersListView.SelectedItems[0].Tag as Favorite);

            if (configForm.ShowDialog(this) == DialogResult.OK && !string.IsNullOrEmpty(configForm.Favorite.Path))
            {
                var lvi = _favoriteFoldersListView.SelectedItems[0];
                UpdateListViewItem(configForm.Favorite, ref lvi);
                _favoriteFoldersListView.Update();
            }
        }

        private void removeFavoriteFolderBtn_Click(object sender, EventArgs e)
        {
            if (_favoriteFoldersListView.SelectedItems.Count == 0)
                return;

            _favoriteFoldersListView.Items.Remove(_favoriteFoldersListView.SelectedItems[0]);
        }

        private void UpdateListViewItem(Favorite favorite, ref ListViewItem lvi)
        {
            lvi.SubItems.Clear();
            lvi.Text = favorite.Path;
            lvi.SubItems.Add(favorite.Text);
            lvi.Tag = favorite;
        }

        private void moveFavoriteFolderUpBtn_Click(object sender, EventArgs e)
        {
            if (_favoriteFoldersListView.SelectedItems.Count == 0)
                return;

            int selectedIndex = _favoriteFoldersListView.SelectedItems[0].Index;
            if (selectedIndex == 0)
                return;

            var selectedItem = _favoriteFoldersListView.SelectedItems[0];
            _favoriteFoldersListView.Items.Remove(selectedItem);
            _favoriteFoldersListView.Items.Insert(selectedIndex - 1, selectedItem);
        }

        private void moveFavoriteFolderDownBtn_Click(object sender, EventArgs e)
        {
            if (_favoriteFoldersListView.SelectedItems.Count == 0)
                return;

            int selectedIndex = _favoriteFoldersListView.SelectedItems[0].Index;
            if (selectedIndex == _favoriteFoldersListView.Items.Count - 1)
                return;

            var selectedItem = _favoriteFoldersListView.SelectedItems[0];
            _favoriteFoldersListView.Items.Remove(selectedItem);
            _favoriteFoldersListView.Items.Insert(selectedIndex + 1, selectedItem);
        }

        private void _okBtn_Click(object sender, EventArgs e)
        {
            var mainForm = Owner as MainForm;

            if (mainForm != null)
            {
                var favorites = new List<Favorite>();

                foreach (ListViewItem lvi in _favoriteFoldersListView.Items)
                {
                    favorites.Add((Favorite)lvi.Tag);
                }

                mainForm._favoriteFoldersMenu.SetFavorites(favorites);
            }
        }
    }
}
