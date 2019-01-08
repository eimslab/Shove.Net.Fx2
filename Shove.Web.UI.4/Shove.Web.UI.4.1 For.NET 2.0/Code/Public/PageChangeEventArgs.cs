using System;
using System.Collections.Generic;
using System.Text;

namespace Shove.Web.UI
{
    [Serializable]
    public class PageChangeEventArgs : EventArgs
    {
        public int PageIndex;

        public PageChangeEventArgs(int NewPageIndex)
        {
            this.PageIndex = NewPageIndex;
        }
    }
}
