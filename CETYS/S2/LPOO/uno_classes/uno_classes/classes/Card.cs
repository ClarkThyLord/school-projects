using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace uno_classes.classes
{
    class Card
    {
        public int value { get; }
        public int color { get; }
        private string img_src;

        public Card(int value, int color, string img_src)
        {
            this.value = value;
            this.color = color;
            this.img_src = img_src;
        }

        public override bool Equals(object obj)
        {
            return obj is Card && (((Card)obj).value == this.value && ((Card)obj).color == this.color);
        }

        public Image draw(float x, float y, float scale) { return null; }
    }
}
