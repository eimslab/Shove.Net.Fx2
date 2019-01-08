using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

[assembly: TagPrefix("Shove.Web.UI", "ShoveWebUI")]
namespace Shove.Web.UI
{
    public class ShoveTabSelectionChangingEventArgs : EventArgs
    {
        private int prevIndex;
        private int newIndex;

        public ShoveTabSelectionChangingEventArgs(int prevIndex, int newIndex)
        {
            this.prevIndex = prevIndex;
            this.newIndex = newIndex;
        }

        public int NewIndex
        {
            get { return newIndex; }
        }

        public int PreviousIndex
        {
            get { return prevIndex; }
        }
    }
}
