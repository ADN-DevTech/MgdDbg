using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

using MgdDbg.Snoop.Collectors;

using Autodesk.AutoCAD.LayerManager;

namespace MgdDbg.Snoop.CollectorExts {
    class LayerManager : CollectorExt {

        public
        LayerManager()
		{
		}

        protected override void
        CollectEvent(object sender, CollectorEventArgs e)
        {
            // cast the sender object to the SnoopCollector we are expecting
            Collector snoopCollector = sender as Collector;
            if (snoopCollector == null) {
                Debug.Assert(false);    // why did someone else send us the message?
                return;
            }

            // see if it is a type we are responsible for
            AndExpression andExprn = e.ObjToSnoop as AndExpression;
            if (andExprn != null) {
                Stream(snoopCollector.Data(), andExprn);
                return;
            }

            LayerFilter layerFilter = e.ObjToSnoop as LayerFilter;
            if (layerFilter != null) {
                Stream(snoopCollector.Data(), layerFilter);
                return;
            }

            RelationalExpression relExprn = e.ObjToSnoop as RelationalExpression;
            if (relExprn != null) {
                Stream(snoopCollector.Data(), relExprn);
                return;
            }

            // ValueTypes we have to treat a bit different
            if (e.ObjToSnoop is LayerFilterDisplayImages) {
                Stream(snoopCollector.Data(), (LayerFilterDisplayImages)e.ObjToSnoop);
                return;
            }

            if (e.ObjToSnoop is LayerFilterTree) {
                Stream(snoopCollector.Data(), (LayerFilterTree)e.ObjToSnoop);
                return;
            }
        }

        private void
        Stream(ArrayList data, AndExpression andExprn)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(AndExpression)));

            data.Add(new Snoop.Data.Enumerable("Get relational expressions", andExprn.GetRelationalExpressions()));           
        }

        private void
        Stream(ArrayList data, LayerFilter layerFilter)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(LayerFilter)));

            data.Add(new Snoop.Data.Bool("Allow delete", layerFilter.AllowDelete));
            data.Add(new Snoop.Data.Bool("Allow nested", layerFilter.AllowNested));
            data.Add(new Snoop.Data.Bool("Allow rename", layerFilter.AllowRename));
            data.Add(new Snoop.Data.Object("Display images", layerFilter.DisplayImages));
            data.Add(new Snoop.Data.Bool("Dynamically generated", layerFilter.DynamicallyGenerated));
            data.Add(new Snoop.Data.String("Filter expression", layerFilter.FilterExpression));
            data.Add(new Snoop.Data.Bool("Is Id filter", layerFilter.IsIdFilter));
            data.Add(new Snoop.Data.Bool("Is proxy", layerFilter.IsProxy));
            data.Add(new Snoop.Data.String("Name", layerFilter.Name));
            data.Add(new Snoop.Data.Enumerable("Nested filters", layerFilter.NestedFilters));
            data.Add(new Snoop.Data.Object("Parent", layerFilter.Parent));

            LayerGroup layerGrp = layerFilter as LayerGroup;
            if (layerGrp != null) {
                Stream(data, layerGrp);
                return;
            }
        }

        private void
        Stream(ArrayList data, LayerGroup layerGrp)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(LayerGroup)));

            data.Add(new Snoop.Data.Enumerable("Layer Ids", layerGrp.LayerIds));
        }

        private void
        Stream(ArrayList data, RelationalExpression relExprn)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(RelationalExpression)));

            data.Add(new Snoop.Data.String("Constant", relExprn.Constant));
            data.Add(new Snoop.Data.String("Variable", relExprn.Variable));
        }

        private void
        Stream(ArrayList data, LayerFilterDisplayImages layerFilterDispImgs)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(LayerFilterDisplayImages)));

            data.Add(new Snoop.Data.Int("Image index", layerFilterDispImgs.ImageIndex));
            data.Add(new Snoop.Data.Int("Selected image index", layerFilterDispImgs.SelectedImageIndex));           
        }

        private void
        Stream(ArrayList data, LayerFilterTree layerFilterTree)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(LayerFilterTree)));

            data.Add(new Snoop.Data.Object("Current", layerFilterTree.Current));
            data.Add(new Snoop.Data.Object("Root", layerFilterTree.Root));
        }
    }
}
