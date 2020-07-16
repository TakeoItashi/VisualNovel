﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VisualNovelInterface.ViewModels;

namespace VisualNovelInterface
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow() {
			try {
				InitializeComponent();
				MainViewModel vm = new MainViewModel();
				DataContext = vm;
			} catch (Exception ex) {

			}
		}

		private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e) {
			TextBlock item = sender as TextBlock;

			if (item != null && e.LeftButton == MouseButtonState.Pressed) {
				DragDrop.DoDragDrop(item,
									item.DataContext,
									DragDropEffects.Copy);
			}
		}
	}
}
