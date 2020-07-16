using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using VisualNovelInterface.Models.Args.SenderEventArgs;
using VisualNovelInterface.MVVM;

namespace VisualNovelInterface.ViewModels
{
	public class SpriteViewModel : BaseObject
	{
		string m_image, m_name;
		double m_posX, m_posY, m_height, m_width;
		private Rect m_geometryRect;
		bool m_isSelected;

		public delegate void SpriteMoveEventHandler(SpriteViewModel _sender);

		public event SpriteMoveEventHandler OnSpriteMoveEvent;

		public string Image {
			get => m_image;
			set => SetProperty(ref m_image, value);
		}

		public string Name {
			get => m_name;
			set => SetProperty(ref m_name, value);
		}

		public double PosX {
			get => m_posX;
			set => SetProperty(ref m_posX, value);
		}
		public double PosY {
			get => m_posY;
			set => SetProperty(ref m_posY, value);
		}
		public double Height {
			get => m_height;
			set => SetProperty(ref m_height, value);
		}
		public double Width {
			get => m_width;
			set => SetProperty(ref m_width, value);
		}
		public Rect GeometryRect {
			get => m_geometryRect;
			set => SetProperty(ref m_geometryRect, value);
		}
		public bool IsSelected {
			get => m_isSelected;
			set => SetProperty(ref m_isSelected, value);
		}

		public RelayCommand<CustomEventCommandParameter> MouseDownOnSpriteCommand {
			get;
			set;
		}

		public RelayCommand<CustomEventCommandParameter> MoveMouseCommand {
			get;
			set;
		}

		public SpriteViewModel(string _image, string _name, int _posX, int _posY, int _height, int _width) {
			m_image = _image;
			m_name = _name;
			m_posX = _posX;
			m_posY = _posY;
			m_height = _height;
			m_width = _width;
			m_geometryRect = new Rect(_posX, _posY, _width, _height);
			MouseDownOnSpriteCommand = new RelayCommand<CustomEventCommandParameter>(MouseDownOnSprite);
			MoveMouseCommand = new RelayCommand<CustomEventCommandParameter>(MoveMouse);
			//OnSpriteMoveEvent += MouseDownOnSprite;
		}

		private void MouseDownOnSprite(CustomEventCommandParameter _args) {
			OnSpriteMoveEvent.Invoke(this);
		}

		private void MoveMouse(CustomEventCommandParameter _args) {
			DragDeltaEventArgs args = _args.Args as DragDeltaEventArgs;
			double newPosX = PosX + args.HorizontalChange;
			double newPosY = PosY + args.VerticalChange;
			if (newPosX > 0) {
				PosX = newPosX;
			}
			if (newPosY > 0) {
				PosY = newPosY;
			}
		}
	}
}