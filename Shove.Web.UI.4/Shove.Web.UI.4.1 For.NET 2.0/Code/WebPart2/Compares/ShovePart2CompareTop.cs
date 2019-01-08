using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

namespace Shove.Web.UI
{
    /// <summary>
    /// 比较2个Part的Top
    /// </summary>
    internal class ShovePart2CompareTop : IComparer
    {
        int IComparer.Compare(Object x, Object y)
        {
            int xTop = 0;

            try
            {
                xTop = int.Parse(((DataRow)x)["Top"].ToString().Replace("px", ""));
            }
            catch { }

            int yTop = 0;

            try
            {
                yTop = int.Parse(((DataRow)y)["Top"].ToString().Replace("px", ""));
            }
            catch { }

            if (xTop == yTop)
            {
                int xLeft = 0;

                try
                {
                    xLeft = int.Parse(((DataRow)x)["Left"].ToString().Replace("px", ""));
                }
                catch { }

                int yLeft = 0;

                try
                {
                    yLeft = int.Parse(((DataRow)y)["Left"].ToString().Replace("px", ""));
                }
                catch { }

                return xLeft.CompareTo(yLeft);
            }

            return xTop.CompareTo(yTop);
        }
    }
}
