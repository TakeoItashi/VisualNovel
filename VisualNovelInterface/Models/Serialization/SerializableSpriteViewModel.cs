﻿using System;

namespace VisualNovelInterface.Models.Serialization
{
	public class SerializableSpriteViewModel
	{
		public double PosX {
			get;
			set;
		}
		public double PosY {
			get;
			set;
		}
		public double Height {
			get;
			set;
		}
		public double Width {
			get;
			set;
		}
		public Guid ImageId {
			get;
			set;
		}
	}
}