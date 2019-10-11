using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VisualNovelInterface
{
	public class DLLImporter
	{
		[DllImport("VisualNovel.dll",CharSet = CharSet.Ansi,CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr CreateDataValue_int(string _name, int _type, int _value);

		[DllImport("VisualNovel.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr CreateDataValue_float(string _name, int _type, float _value);

		[DllImport("VisualNovel.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr CreateDataValue_bool(string _name, bool _value);

		[DllImport("VisualNovel.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr CreateDataValue_string(string _name, int _type, string _value);

		[DllImport("VisualNovel.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern bool ReadDataValue_bool(IntPtr _ptr);

		[DllImport("VisualNovel.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SetDataValue_bool(IntPtr _ptr, bool _value);

		[DllImport("VisualNovel.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern void FreeDataValue(IntPtr _ptr);
	}
}
