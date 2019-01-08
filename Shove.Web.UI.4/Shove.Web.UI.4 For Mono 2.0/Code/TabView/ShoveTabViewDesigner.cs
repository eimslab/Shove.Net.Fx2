using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.Design.WebControls;
using System.Web.UI.WebControls;
using System.Text;

[assembly: TagPrefix("Shove.Web.UI", "ShoveWebUI")]
namespace Shove.Web.UI
{
    class ShoveTabViewDesigner : CompositeControlDesigner, INamingContainer
    {

        private ShoveTabView tabView;
        private int currentTabIndex;

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            tabView = (ShoveTabView)component;
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            Table tabViewTable;
            Table headerTable, contentsTable;

            //parent table
            tabViewTable = (Table)tabView.Controls[0];

            if (tabViewTable.Rows.Count > 0)
            {
                //header table 
                headerTable = (Table)tabViewTable.Rows[0].Cells[0].Controls[0];

                //make sure we have headers.
                if (headerTable != null)
                {
                    //we are creating designer regions consiting of headers and 
                    //contents.
                    int i;

                    //header tabs.
                    for (i = 0; i < headerTable.Rows[0].Cells.Count; i++)
                        headerTable.Rows[0].Cells[i].Attributes[DesignerRegion.DesignerRegionAttributeName] = i.ToString();

                    //header contents.Single region for all contents b/c only single 
                    //content is active at a time.
                    contentsTable = (Table)tabViewTable.Rows[1].Cells[0].Controls[0];
                    contentsTable.Rows[0].Cells[0].Attributes[DesignerRegion.DesignerRegionAttributeName] = i.ToString();
                }
            }
        }

        protected override void OnClick(DesignerRegionMouseEventArgs e)
        {
            base.OnClick(e);

            //make sure that we click on header.
            if (e.Region.Name.IndexOf("Header") == -1)
                return;

            if (e.Region != null)
            {
                currentTabIndex = tabView.CurrentTabIndex = int.Parse(e.Region.Name.Substring("Header".Length));
                base.UpdateDesignTimeHtml();
            }

        }

        protected override string GetEmptyDesignTimeHtml()
        {
            string table = "<table border=0 cellspacing=0 cellpading=0>";
            table += "<tr><td>Tab1</td><td>Tab2</td></tr>";
            table += "<tr><td colspan=2>Contents</td></tr>";
            table += "</table>";
            return table;

        }


        public override string GetDesignTimeHtml(DesignerRegionCollection regions)
        {

            Table tabViewTable = (Table)tabView.Controls[0];
            int i = 0, designerRegionIndex = 0;
            string designTimeHtml = "";

            if (tabViewTable.Rows.Count > 0)
            {
                Table headerTable = (Table)tabViewTable.Rows[0].Cells[0].Controls[0];
                for (i = 0; i < headerTable.Rows[0].Cells.Count; i++)
                {
                    regions.Add(new DesignerRegion(this, "Header" + designerRegionIndex.ToString()));
                    ++designerRegionIndex;
                }

                Table contentTable = (Table)tabViewTable.Rows[1].Cells[0].Controls[0];
                //for (i = 1; i < /*2*/tabView.Tabs.Count; i++)
                //for (i = 0; i < /*2*/tabView.Tabs.Count; i++)
                {
                    EditableDesignerRegion editableRegion =
                      new EditableDesignerRegion(this,
                          "Content" + tabView.CurrentTabIndex, false);
                    ++designerRegionIndex;
                    editableRegion.UserData = i;
                    regions.Add(editableRegion);
                }


                //Set the highlight for the selected region
                if (tabView.Tabs.Count > 0)
                    regions[tabView.CurrentTabIndex].Highlight = true;

                designTimeHtml = base.GetDesignTimeHtml(regions);
            }
            else designTimeHtml = GetDesignTimeHtml();


            return designTimeHtml;
        }

        // Get the content string for the selected region. Called by the designer host?    
        public override string GetEditableDesignerRegionContent(EditableDesignerRegion region)
        {
            //Get a reference to the designer host
            IDesignerHost host = (IDesignerHost)Component.Site.GetService(typeof(IDesignerHost));
            if (host != null)
            {
                ShoveTabPage tb;
                tb = (ShoveTabPage)tabView.Tabs[tabView.CurrentTabIndex];
                return ControlPersister.PersistControl(tb, host);
            }
            return String.Empty;
        }

        public override void SetEditableDesignerRegionContent(EditableDesignerRegion region, string content)
        {
            if (content == null)
                return;

            // Get a reference to the designer host
            IDesignerHost host = (IDesignerHost)Component.Site.GetService(typeof(IDesignerHost));
            if (host != null)
            {

                ShoveTabPage view = (ShoveTabPage)ControlParser.ParseControl(host, content);

                if (view != null)
                {
                    int tabIndex = int.Parse(region.Name.Substring("Content".Length));
                    tabView.Tabs.RemoveAt(tabIndex);
                    tabView.Tabs.AddAt(tabIndex, view);

                }
            }
        }

        // Create a custom ActionLists collection
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                // Create the collection
                DesignerActionListCollection actionLists = new DesignerActionListCollection();

                // Get the base items, if any
                actionLists.AddRange(base.ActionLists);

                // Add a custom list of actions
                actionLists.Add(new ShoveTabViewActionList(this));

                return actionLists;

            }
        }

        // Create an embedded DesignerActionList class
        class ShoveTabViewActionList : DesignerActionList
        {
            // Create private fields
            private ShoveTabViewDesigner _parent;
            private DesignerActionItemCollection items;

            // Constructor
            public ShoveTabViewActionList(ShoveTabViewDesigner parent)
                : base(parent.Component)
            {
                _parent = parent;
            }

            // Create a set of transacted callback methods
            // Callback for the wide format
            public void CreateTabPage()
            {
                ShoveTabView tabView = (ShoveTabView)_parent.Component;


                // Create the callback
                TransactedChangeCallback toCall = new TransactedChangeCallback(AddTabPage);
                // Create the transacted change in the control
                ControlDesigner.InvokeTransactedChange(tabView, toCall, null, "Add Tab Page");
            }


            // Get the sorted list of Action items
            public override DesignerActionItemCollection GetSortedActionItems()
            {
                if (items == null)
                {
                    // Create the collection
                    items = new DesignerActionItemCollection();

                    // Add Tab Page command
                    items.Add(new DesignerActionMethodItem(this, "CreateTabPage", "Add Tab Page", true));
                }
                return items;
            }

            // Function for the callbacks to call
            public bool AddTabPage(object arg)
            {
                // Get a reference to the designer's associated component
                ShoveTabView tabView = (ShoveTabView)_parent.Component;

                ShoveTabPage tp = new ShoveTabPage();
                tp.ID = GetNewTabPageID(tabView);
                tp.Text = "TabItem";

                tabView.Tabs.Add(tp);

                _parent.UpdateDesignTimeHtml();

                // Return an indication of success
                return true;
            }

            public string GetNewTabPageID(ShoveTabView ctv)
            {
                if (ctv == null)
                {
                    return "";
                }

                int i = 1;
                string ID = "";

                while (true)
                {
                    ID = ctv.ID + "_Tab" + i.ToString();

                    Control ctl = ctv.FindControl(ID);

                    if (ctl == null)
                    {
                        return ID;
                    }

                    i++;
                }
                
            }
        }
    }



}
