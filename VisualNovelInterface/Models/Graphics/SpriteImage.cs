using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using VisualNovelInterface.MVVM;

namespace VisualNovelInterface.Models
{
	public class SpriteImage : BaseObject
	{
		protected string m_image, m_name;

		public string Image {
			get => m_image;
			set => SetProperty(ref m_image, value);
		}

		public string Name {
			get => m_name;
			set => SetProperty(ref m_name, value);
		}

		public BitmapImage BitmapImage {
			get;
		}

		public Guid Id {
			get;
		}

		public SpriteImage(string _path, string _name, Guid? _Id = null) {
			Image = _path;
			Name = _name;
			BitmapImage = new BitmapImage();
			BitmapImage.BeginInit();
			BitmapImage.CreateOptions = BitmapCreateOptions.IgnoreColorProfile;
			BitmapImage.UriSource = new Uri(m_image, UriKind.RelativeOrAbsolute);
			BitmapImage.EndInit();
			if (_Id == null) {
				Id = Guid.NewGuid();
			} else {
				Id = (Guid)_Id;
			}
		}

		public SpriteImage() {
			BitmapImage = new BitmapImage();

			Bitmap B = new Bitmap(10,10);
			Graphics G = Graphics.FromImage(B);
			G.FillRectangle(Brushes.White, 0, 0, 10, 10);
			B = new Bitmap(10, 10, G);

			using (var memory = new MemoryStream()) {
				B.Save(memory, ImageFormat.Png);
				memory.Position = 0;

				var bitmapImage = new BitmapImage();
				BitmapImage.BeginInit();
				BitmapImage.StreamSource = memory;
				BitmapImage.CacheOption = BitmapCacheOption.OnLoad;
				BitmapImage.EndInit();
			}
			Id = Guid.NewGuid();
		}
	}
}
