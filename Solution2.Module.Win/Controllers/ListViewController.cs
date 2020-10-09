using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraGrid.Views.Grid;
using Solution2.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Solution2.Module.Win.Controllers
{
    public class ListViewController : ViewController<DevExpress.ExpressApp.ListView>
    {
        public ListViewController()
        {
            ViewControlsCreated += ListViewController_ViewControlsCreated;
        }

        private void ListViewController_ViewControlsCreated(object sender, EventArgs e)
        {
            GridListEditor listEditor = ((ListView)View).Editor as GridListEditor;
            if (listEditor != null)
            {
                GridView gridView = listEditor.GridView;
                gridView.OptionsView.EnableAppearanceEvenRow = true;
                gridView.OptionsView.ShowAutoFilterRow = false;
                gridView.OptionsView.ShowFooter = false;
                gridView.OptionsView.ColumnAutoWidth = true;

                var currentObjectModel = View.Model.ModelClass;
                if (currentObjectModel != null && currentObjectModel.GetType() == typeof(Kontrahent))
                {
                    gridView.OptionsView.RowAutoHeight = true;
                    gridView.OptionsView.ShowFooter = false;
                }
            }
        }
    }
}
