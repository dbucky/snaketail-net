using System;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using Microsoft.Win32;
using System.Collections.Generic;

namespace JWC
{
	public class FavoritesMenu : IDisposable
	{
		private   ClickedHandler    clickedHandler;
		protected ToolStripMenuItem favoriteMenuItem;
		protected string			registryKeyName;
		protected int				numEntries = 0;
		protected int				maxEntries = 4;
		protected int				maxShortenPathLength = 96;
		protected Mutex				mutex;
		protected Dictionary<string, Favorite> AllFavorites = new Dictionary<string, Favorite>();

		#region Construction

		protected FavoritesMenu()	{}

		public FavoritesMenu(ToolStripMenuItem recentFileMenuItem, ClickedHandler clickedHandler)
			: this(recentFileMenuItem, clickedHandler, null, false, 4)
		{
		}

		public FavoritesMenu(ToolStripMenuItem recentFileMenuItem, ClickedHandler clickedHandler, int maxEntries)
			: this(recentFileMenuItem, clickedHandler, null, false, maxEntries)
		{
			
		}

		public FavoritesMenu(ToolStripMenuItem recentFileMenuItem, ClickedHandler clickedHandler, string registryKeyName)
			: this(recentFileMenuItem, clickedHandler, registryKeyName, true, 4)
		{
		}

		public FavoritesMenu(ToolStripMenuItem recentFileMenuItem, ClickedHandler clickedHandler, string registryKeyName, int maxEntries)
			: this(recentFileMenuItem, clickedHandler, registryKeyName, true, maxEntries)
		{
		}

		public FavoritesMenu(ToolStripMenuItem recentFileMenuItem, ClickedHandler clickedHandler, string registryKeyName, bool loadFromRegistry)
			: this(recentFileMenuItem, clickedHandler, registryKeyName, loadFromRegistry, 4)
  		{
		}

		public FavoritesMenu(ToolStripMenuItem recentFileMenuItem, ClickedHandler clickedHandler, string registryKeyName, bool loadFromRegistry, int maxEntries)
		{
			Init(recentFileMenuItem, clickedHandler, registryKeyName, loadFromRegistry, maxEntries);
		}

		protected void Init(ToolStripMenuItem favoriteMenuItem, ClickedHandler clickedHandler, string registryKeyName, bool loadFromRegistry, int maxEntries)
		{
			if (favoriteMenuItem == null)
				throw new ArgumentNullException("favoriteMenuItem");

			this.favoriteMenuItem = favoriteMenuItem;
			this.favoriteMenuItem.Checked = false;
			this.favoriteMenuItem.Enabled = false;

            this.maxEntries = maxEntries > 16 ? 16 : maxEntries;
			this.clickedHandler = clickedHandler;

			if (registryKeyName != null)
			{
				RegistryKeyName = registryKeyName;
				if (loadFromRegistry)
					LoadFromRegistry();
			}
		}

		#endregion

		#region Event Handling

		public delegate void ClickedHandler(int number, Favorite favorite);

		protected void OnClick(object sender, System.EventArgs e)
		{
            var menuItem = (ToolStripMenuItem)sender;
			var favorite = AllFavorites[(string)menuItem.Tag];
			clickedHandler(MenuItems.IndexOf(menuItem) - StartIndex, favorite);
		}


		#endregion

		#region Properties

		public virtual ToolStripItemCollection MenuItems
		{
			get
			{
				return favoriteMenuItem.DropDownItems;
			}
		}

		public virtual int StartIndex
		{
			get
			{
				return 0;
			}
		}

		public virtual int EndIndex
		{
			get
			{
				return numEntries;
			}
		}

		public int NumEntries
		{
			get 
			{
				return numEntries; 
			}
		}

		public int MaxEntries
		{
			get 
			{
				return maxEntries; 
			}
			set 
			{
				if (value > 16)
				{
					maxEntries = 16;
				}
				else
				{
					maxEntries = value < 4 ? 4 : value;

					int index = StartIndex + maxEntries;
					while (numEntries > maxEntries)
					{
						MenuItems.RemoveAt(index);
						numEntries--;
					}
				}
			}
		}

		public int MaxShortenPathLength
		{
			get
			{
				return maxShortenPathLength;
			}
			set
			{
				maxShortenPathLength = value < 16 ? 16 : value;
			}
		}

		public virtual bool IsInline
		{
			get
			{
				return false;
			}
		}

		#endregion

		#region Helper Methods

		protected virtual void Enable()
		{
			favoriteMenuItem.Enabled = true;
		}

		protected virtual void Disable()
		{
			favoriteMenuItem.Enabled = false;
		}

		protected virtual void SetFirstFile(ToolStripMenuItem menuItem)
		{
		}

		public static string FixupEntryname(int number, string entryname)
		{
			if (number < 9)
				return "&" + (number + 1) + "  " + entryname;
			else if (number == 9)
				return "1&0" + "  " + entryname;
			else
				return (number + 1) + "  " + entryname;
		}

		protected void FixupPrefixes(int startNumber)
		{
			if (startNumber < 0)
				startNumber = 0;

			if (startNumber < maxEntries)
			{
				for (int i = StartIndex + startNumber; i < EndIndex; i++, startNumber++)
				{
					int offset = MenuItems[i].Text.Substring(0, 3) == "1&0" ? 5 : 4;
					MenuItems[i].Text = FixupEntryname(startNumber, MenuItems[i].Text.Substring(offset));
				}
			}
		}

		static public string ShortenPathname(string pathname, int maxLength)
		{
			if (pathname.Length <= maxLength)
				return pathname;

			string root = Path.GetPathRoot(pathname);
			if (root.Length > 3)
				root += Path.DirectorySeparatorChar;

			string[] elements = pathname.Substring(root.Length).Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

			int filenameIndex = elements.GetLength(0) - 1;

			if (elements.GetLength(0) == 1) // pathname is just a root and filename
			{
				if (elements[0].Length > 5) // long enough to shorten
				{
					// if path is a UNC path, root may be rather long
					if (root.Length + 6 >= maxLength)
					{
						return root + elements[0].Substring(0, 3) + "...";
					}
					else
					{
						return pathname.Substring(0, maxLength - 3) + "...";
					}
				}
			}
			else if ((root.Length + 4 + elements[filenameIndex].Length) > maxLength) // pathname is just a root and filename
			{
				root += "...\\";

				int len = elements[filenameIndex].Length;
				if (len < 6)
					return root + elements[filenameIndex];

				if ((root.Length + 6) >= maxLength)
				{
					len = 3;
				}
				else
				{
					len = maxLength - root.Length - 3;
				}
				return root + elements[filenameIndex].Substring(0, len) + "...";
			}
			else if (elements.GetLength(0) == 2)
			{
				return root + "...\\" + elements[1];
			}
			else
			{
				int len = 0;
				int begin = 0;

				for (int i = 0; i < filenameIndex; i++)
				{
					if (elements[i].Length > len)
					{
						begin = i;
						len = elements[i].Length;
					}
				}

				int totalLength = pathname.Length - len + 3;
				int end = begin + 1;

				while (totalLength > maxLength)
				{
					if (begin > 0)
						totalLength -= elements[--begin].Length - 1;

					if (totalLength <= maxLength)
						break;

					if (end < filenameIndex)
						totalLength -= elements[++end].Length - 1;

					if (begin == 0 && end == filenameIndex)
						break;
				}

				// assemble final string

				for (int i = 0; i < begin; i++)
				{
					root += elements[i] + '\\';
				}

				root += "...\\";

				for (int i = end; i < filenameIndex; i++)
				{
					root += elements[i] + '\\';
				}

				return root + elements[filenameIndex];
			}
			return pathname;
		}

		#endregion

		#region Get Methods

		public int GetIndex(Favorite favorite)
		{
			for (var i = 0; i < MenuItems.Count; i++)
			{
				if ((string)MenuItems[i].Tag == favorite.Path)
				{
					return i;
				}
			}

			return -1;
		}

		public Favorite GetFavorite(int index)
		{
			return AllFavorites[(string)MenuItems[index].Tag];
		}

		public Favorite[] GetFavorites()
		{
			var favorites = new Favorite[numEntries];
			var index = StartIndex;

			for (int i = 0; i < favorites.GetLength(0); i++, index++)
			{
				favorites[i] = AllFavorites[(string)MenuItems[index].Tag];
			}

			return favorites;
		}

		#endregion

		#region Add Methods

		public void AddFavorite(string path)
		{
			AddFavorite(new Favorite(path, null, 1));
		}

		public void AddFavorite(Favorite favorite)
		{
			if (AllFavorites.ContainsKey(favorite.Path))
			{
				favorite = AllFavorites[favorite.Path];
				favorite.RecordUsage();

				if (!favorite.IsExplicit)
				{
					var index = GetIndex(favorite);

					if (index > 0)
					{
						var previous = GetFavorite(index - 1);

						if (!previous.IsExplicit && previous.Usage < favorite.Usage)
						{
							MoveInMenu(favorite, index, -1);
						}
					}
				}
			}
			else
			{
				AllFavorites.Add(favorite.Path, favorite);
				AddToMenu(favorite);
			}
		}

		public void SetFavorites(List<Favorite> favorites)
		{
			RemoveAll();

			foreach (var favorite in favorites)
			{
				if (AllFavorites.ContainsKey(favorite.Path))
				{
					AllFavorites[favorite.Path] = favorite;

				}
				else
				{
					AllFavorites.Add(favorite.Path, favorite);
				}

				AddToMenu(favorite);
			}
		}

		private void AddToMenu(Favorite favorite)
		{
			if (numEntries < maxEntries)
			{
				var text = !string.IsNullOrEmpty(favorite.Text) ? favorite.Text : favorite.Path;

				text = FixupEntryname(numEntries, text);

				var menuItem = new ToolStripMenuItem(text, null, new EventHandler(OnClick))
				{
					Tag = favorite.Path
				};

				MenuItems.Insert(StartIndex + numEntries, menuItem);

				if (numEntries++ == 0)
				{
					Enable();
					SetFirstFile(menuItem);
				}
			}
		}

		private void MoveInMenu(Favorite favorite, int index, int offset)
		{
			MenuItems.RemoveAt(index);

			var target = index + offset;
			var text = !string.IsNullOrEmpty(favorite.Text) ? favorite.Text : favorite.Path;

			text = FixupEntryname(target, text);

			var menuItem = new ToolStripMenuItem(text, null, new EventHandler(OnClick))
			{
				Tag = favorite.Path
			};

			MenuItems.Insert(StartIndex + target, menuItem);

			if (target == 0)
			{
				SetFirstFile(menuItem);
			}

			FixupPrefixes(target + 1);
		}

		#endregion

		#region Remove Methods

		public virtual void RemoveAll()
		{
			if (numEntries > 0)
			{
				// remove all items in the sub menu
				MenuItems.Clear();
				Disable();
				numEntries = 0;
			}
		}

		#endregion

		#region Registry Methods

		public string RegistryKeyName
		{
			get
			{
				return registryKeyName;
			}
			set
			{
				if (mutex != null)
					mutex.Close();

				registryKeyName = value.Trim();
				if (registryKeyName.Length == 0)
				{
					registryKeyName = null;
					mutex = null;
				}
				else
				{
					string mutexName = registryKeyName.Replace('\\', '_').Replace('/', '_') + "Mutex";
					mutex = new Mutex(false, mutexName);
				}
			}
		}

		public void LoadFromRegistry(string keyName)
		{
			RegistryKeyName = keyName;
			LoadFromRegistry();
		}

		public void LoadFromRegistry()
		{
			if (RegistryKeyName != null)
			{
				mutex.WaitOne();

				RemoveAll();

				RegistryKey regKey = Registry.CurrentUser.OpenSubKey(RegistryKeyName);
				if (regKey != null)
				{
					maxEntries = (int)regKey.GetValue("max", maxEntries);

					var favorites = new List<Favorite>();

					for (int number = 1; number <= maxEntries; number++)
					{
						var favorite = Favorite.FromConfig((string)regKey.GetValue("Favorite" + number.ToString()));
						
						if (favorite != null)
						{
							favorites.Add(favorite);
						}
					}

					SetFavorites(favorites);

					regKey.Close();
				}
				mutex.ReleaseMutex();
			}
		}

		public void SaveToRegistry(string keyName)
		{
			RegistryKeyName = keyName;
			SaveToRegistry();
		}

		public void SaveToRegistry()
		{
			if (RegistryKeyName != null)
			{
				mutex.WaitOne();

				RegistryKey regKey = Registry.CurrentUser.CreateSubKey(RegistryKeyName);
				if (regKey != null)
				{
					regKey.SetValue("max", maxEntries);

					int number = 1;
					int i = StartIndex;
					for (; i < EndIndex; i++, number++)
					{
						var favorite = AllFavorites[(string)MenuItems[i].Tag];
						regKey.SetValue("Favorite" + number.ToString(), favorite);
					}

					for (; number <= 16; number++)
					{
						regKey.DeleteValue("Favorite" + number.ToString(), false);
					}

					regKey.Close();
				}
				mutex.ReleaseMutex();
			}
		}

        #region IDisposable Support
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                mutex.Close();
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion

        #endregion
    }

	public class FavoritesMenuInline : FavoritesMenu
	{
		protected ToolStripMenuItem owningMenu;
		protected ToolStripMenuItem firstMenuItem;

		#region Construction

		public FavoritesMenuInline(ToolStripMenuItem owningMenu, ToolStripMenuItem recentFileMenuItem, ClickedHandler clickedHandler)
			: this(owningMenu, recentFileMenuItem, clickedHandler, null, false, 4)
		{
		}

		public FavoritesMenuInline(ToolStripMenuItem owningMenu, ToolStripMenuItem recentFileMenuItem, ClickedHandler clickedHandler, int maxEntries)
			: this(owningMenu, recentFileMenuItem, clickedHandler, null, false, maxEntries)
		{
		}

		public FavoritesMenuInline(ToolStripMenuItem owningMenu, ToolStripMenuItem recentFileMenuItem, ClickedHandler clickedHandler, string registryKeyName)
			: this(owningMenu, recentFileMenuItem, clickedHandler, registryKeyName, true, 4)
		{
		}

		public FavoritesMenuInline(ToolStripMenuItem owningMenu, ToolStripMenuItem recentFileMenuItem, ClickedHandler clickedHandler, string registryKeyName, int maxEntries)
			: this(owningMenu, recentFileMenuItem, clickedHandler, registryKeyName, true, maxEntries)
		{
		}

		public FavoritesMenuInline(ToolStripMenuItem owningMenu, ToolStripMenuItem recentFileMenuItem, ClickedHandler clickedHandler, string registryKeyName, bool loadFromRegistry)
			: this(owningMenu, recentFileMenuItem, clickedHandler, registryKeyName, loadFromRegistry, 4)
		{
		}

		public FavoritesMenuInline(ToolStripMenuItem owningMenu, ToolStripMenuItem recentFileMenuItem, ClickedHandler clickedHandler, string registryKeyName, bool loadFromRegistry, int maxEntries)
		{
			maxShortenPathLength = 48;
			this.owningMenu = owningMenu;
			this.firstMenuItem = recentFileMenuItem;
			Init(recentFileMenuItem, clickedHandler, registryKeyName, loadFromRegistry, maxEntries);
		}

		#endregion

		#region Overridden Properties

		public override ToolStripItemCollection MenuItems
		{
			get
			{
				return owningMenu.DropDownItems;
			}
		}

		public override int StartIndex
		{
			get
			{
				return MenuItems.IndexOf(firstMenuItem);
			}
		}

		public override int EndIndex
		{
			get
			{
				return StartIndex + numEntries;
			}
		}

		public override bool IsInline
		{
			get
			{
				return true;
			}
		}

		#endregion

		#region Overridden Methods

		protected override void Enable()
		{
			MenuItems.Remove(favoriteMenuItem);
		}

		protected override void Disable()
		{
			int index = MenuItems.IndexOf(firstMenuItem);
			MenuItems.RemoveAt(index);
			MenuItems.Insert(index, favoriteMenuItem);
			firstMenuItem = favoriteMenuItem;
		}

		protected override void SetFirstFile(ToolStripMenuItem menuItem)
		{
			firstMenuItem = menuItem;
		}

		public override void RemoveAll()
		{
			// inline menu must remove items from the containing menu
			if (numEntries > 0)
			{
				for (int index = EndIndex - 1; index > StartIndex; index--)
				{
					MenuItems.RemoveAt(index);
				}
				Disable();
				numEntries = 0;
			}
		}

		#endregion
	}

	public class Favorite
	{
		public string Path { get; set; }
		public string Text { get; set; }
		public int Usage { get; private set; } = 0;
		public bool IsExplicit { get => !string.IsNullOrEmpty(Text); }

		public Favorite() { }

		public Favorite(string path)
		{
			Path = path;
		}

		public Favorite(string path, string text)
		{
			Path = path;
			Text = text;
		}

		public Favorite(string path, string text, int usage)
		{
			Path = path;
			Text = text;
			Usage = usage;
		}

		public void RecordUsage()
		{
			Usage++;
		}

		public override string ToString()
		{
			return string.Format("{0}|{1}|{2}", Path, Text, Usage.ToString());
		}

		public static Favorite FromConfig(string config)
		{
			if (string.IsNullOrEmpty(config))
			{
				return null;
			}

			var segments = config.Split('|');
			var path = segments[0];

			if (segments.Length == 1)
			{
				return new Favorite(path);
			}

			var text = segments[1];

			if (segments.Length == 2)
			{
				return !string.IsNullOrEmpty(text) ? new Favorite(path, text) : new Favorite(path);
			}

			var usage = int.Parse(segments[2]);

			return new Favorite(path, text, usage);
		}
	}
}