using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using VisualNovelInterface.Models;
using VisualNovelInterface.Models.Args.SenderEventArgs;
using VisualNovelInterface.MVVM;

namespace VisualNovelInterface.ViewModels
{
	public class SpriteViewModel : BaseObject //, SpriteImage
	{
		double m_posX, m_posY, m_height, m_width;
		private Rect m_geometryRect;
		bool m_isSelected;
		bool m_isAutoPositioned;
		SpriteImage m_image;

		public delegate void SpriteMoveEventHandler(SpriteViewModel _sender);
		public event SpriteMoveEventHandler OnSpriteMoveEvent;

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
		public SpriteImage SpriteImage {
			get => m_image;
			set => SetProperty(ref m_image, value);
		}

		public bool IsAutoPositioned {
			get => m_isAutoPositioned;
		}

		public RelayCommand<CustomEventCommandParameter> MouseDownOnSpriteCommand {
			get;
			set;
		}

		public RelayCommand<CustomEventCommandParameter> MoveMouseCommand {
			get;
			set;
		}

		public SpriteViewModel(SpriteImage _image, double _posX = 0, double _posY = 0, double _height = 100, double _width = 100) {
			SpriteImage = _image;
			m_posX = _posX;
			m_posY = _posY;
			m_height = _height;
			m_width = _width;
			m_geometryRect = new Rect(_posX, _posY, _width, _height);
			MouseDownOnSpriteCommand = new RelayCommand<CustomEventCommandParameter>(MouseDownOnSprite);
			MoveMouseCommand = new RelayCommand<CustomEventCommandParameter>(MoveMouse);
		}

		public SpriteViewModel(SpriteViewModel _sprite) {
			SpriteImage = _sprite.SpriteImage;
			m_posX = _sprite.PosX;
			m_posY = _sprite.PosY;
			m_height = _sprite.Height;
			m_width = _sprite.Width;
			m_geometryRect = new Rect(m_posX, m_posY, m_height, m_width);
			MouseDownOnSpriteCommand = new RelayCommand<CustomEventCommandParameter>(MouseDownOnSprite);
			MoveMouseCommand = new RelayCommand<CustomEventCommandParameter>(MoveMouse);
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

		public void SetPositionAndSize(Point _position, double _width, double _height) {
			PosX = _position.X;
			PosY = _position.Y;
			Width = _width;
			Height = _height;
		}
	}
}