using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

namespace Shove.Web.UI
{
    /// <summary>
    /// 比较2个Part的ID
    /// </summary>
    internal class ShoveWebPartCompareID : IComparer
    {
        int IComparer.Compare(Object x, Object y)
        {
            string xID = ((DataRow)x)["ID"].ToString();
            string yID = ((DataRow)y)["ID"].ToString();

            return xID.CompareTo(yID);
        }
    }
}
