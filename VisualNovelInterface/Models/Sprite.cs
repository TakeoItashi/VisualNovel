using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualNovelInterface.Models
{
    public class Sprite
    {
        string image;
        int posX, posY;

        public string Image {
            get => image;
            set => image = value;
        }
        public int PosX {
            get => posX;
            set => posX = value;
        }
        public int PosY {
            get => posY;
            set => posY = value;
        }
    }
}