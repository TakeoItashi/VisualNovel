using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualNovelInterface.Models.Enums
{
	public enum DataValueTypeEnum
	{
		trigger,    //TODO byte statt ints benutzen?
		variable,
		decimalCCPlus,	//Name is stupid, but decimal is a reserved keyword
		text
	}
}
