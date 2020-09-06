using Newtonsoft.Json;
using System;
using System.Drawing;

namespace VisualNovelInterface.Models.Serialization
{
	public struct SerializableSprite
	{
		public string Name {
			get;
			set;
		}
		public string Path {
			get;
			set;
		}
		public Guid Id {
			get;
			set;
		}
		public string Base64Image {
			get;
			set;
		}

		public SerializableSprite(string _name, string _path, Guid _id) {
			Name = _name;
			byte[] imageArray = System.IO.File.ReadAllBytes(_path);
			string base64 = Convert.ToBase64String(imageArray);
			Base64Image = base64;
			Path = _path;
			Id = _id;
		}
		//TODO: Create a constructor
	}
}