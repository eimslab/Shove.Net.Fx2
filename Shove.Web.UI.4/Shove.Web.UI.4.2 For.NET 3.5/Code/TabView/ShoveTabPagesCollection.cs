using System;
using System.Web.UI;
using System.ComponentModel;
using System.Drawing.Design;

[assembly: TagPrefix("Shove.Web.UI", "ShoveWebUI")]
namespace Shove.Web.UI
{
    //[Editor(typeof(TabPageCollectionEditor) , typeof(UITypeEditor))]
    public class ShoveTabPageCollection : ControlCollection
    {
        public ShoveTabPageCollection(ShoveTabView owner)
            : base(owner)
        {
        }


        private void VerifyChild(Control ctrl)
        {
            if (ctrl is ShoveTabPage)
            {
                return;
            }

            throw new Exception("Invalid Child Object");
        }

        public override void Add(Control child)
        {
            VerifyChild(child);
            base.Add(child);
        }

        public override void AddAt(int index, Control child)
        {
            VerifyChild(child);
            base.AddAt(index, child);


        }

        public override void Remove(Control value)
        {
            base.Remove(value);
        }

        public override void RemoveAt(int index)
        {
            base.RemoveAt(index);
        }

        public override void Clear()
        {
            base.Clear();
        }
    }
}
