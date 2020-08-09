using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

		public SpriteImage(string _path, string _name) {
			Image = _path;
			Name = _name;
		}

		public SpriteImage() {
		}
	}
}
