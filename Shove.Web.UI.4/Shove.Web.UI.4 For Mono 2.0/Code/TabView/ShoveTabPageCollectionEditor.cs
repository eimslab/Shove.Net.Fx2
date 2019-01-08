using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Web.UI;

[assembly: TagPrefix("Shove.Web.UI", "ShoveWebUI")]
namespace Shove.Web.UI
{

    class ShoveTabPageCollectionEditor : CollectionEditor
    {
        public ShoveTabPageCollectionEditor(Type type)
            : base(type)
        {
        }

        protected override Type[] CreateNewItemTypes()
        {
            return new Type[] { typeof(ShoveTabPage) };
        }

        protected override Type CreateCollectionItemType()
        {
            return typeof(ShoveTabPage);
        }
    }
}
