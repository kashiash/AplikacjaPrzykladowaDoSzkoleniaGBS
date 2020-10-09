using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Utils;
using DevExpress.Xpo;
using Solution2.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution2.Module.Controllers
{
  public  class KontrahentListViewController : ObjectViewController<ListView,Kontrahent>
    {

        SingleChoiceAction UstawTypKontrahentaAction;
        PopupWindowShowAction WystawFakturyAction;

        public KontrahentListViewController()
        {
            UstawTypKontrahentaAction = new SingleChoiceAction(this, $"{GetType().FullName}" +
                $".{nameof(UstawTypKontrahentaAction)}", DevExpress.Persistent.Base.PredefinedCategory.Edit)
            {
                Caption ="Ustaw typ",
                ItemType = SingleChoiceActionItemType.ItemIsOperation,
            };
            SetActionItems(UstawTypKontrahentaAction, typeof(TypKontrahenta));
            UstawTypKontrahentaAction.Execute += UstawTypKontrahentaAction_Execute;


            WystawFakturyAction = new PopupWindowShowAction(this, $"{GetType().FullName}" +
                $".{nameof(WystawFakturyAction)}", DevExpress.Persistent.Base.PredefinedCategory.Edit)
            {
                Caption = "Wystaw faktury",
                ImageName = "BO_Skull",
                SelectionDependencyType = SelectionDependencyType.RequireMultipleObjects,
            };

            WystawFakturyAction.CustomizePopupWindowParams += WystawFakturyAction_CustomizePopupWindowParams;
            WystawFakturyAction.Execute += WystawFakturyAction_Execute;

        }



        private void WystawFakturyAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace os = Application.CreateObjectSpace();
            e.Context = TemplateContext.PopupWindow;
            e.View = Application.CreateDetailView(os, new InvoiceTemplate(((DevExpress.ExpressApp.Xpo.XPObjectSpace)os).Session));
            ((DetailView)e.View).ViewEditMode = ViewEditMode.Edit;
        }
        private void WystawFakturyAction_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            InvoiceTemplate parameters = e.PopupWindow.View.CurrentObject as InvoiceTemplate;

            ListPropertyEditor listPropertyEditor = ((DetailView)e.PopupWindow.View).FindItem("Products") as ListPropertyEditor;
            IObjectSpace os = Application.CreateObjectSpace();
            foreach (Kontrahent klient in e.SelectedObjects)
            {
                var faktura = os.CreateObject<Faktura>();
                faktura.DataFaktury = parameters.DataFaktury;
                faktura.Kontrahent = os.GetObject(klient);

                foreach (Towar towar in listPropertyEditor.ListView.SelectedObjects)
                {
                    var pozycja = os.CreateObject<PozycjaFaktury>();
                    pozycja.Towar = os.GetObject<Towar>(towar);
                    pozycja.Ilosc = 1;
                    faktura.PozycjeFaktury.Add(pozycja);
                }
                faktura.Save();
            }
            os.CommitChanges();
     
        }


        private void UstawTypKontrahentaAction_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace(typeof(Kontrahent));
            var typDoPrzypisania = (TypKontrahenta)e.SelectedChoiceActionItem.Data;
            foreach (var obj in e.SelectedObjects)
            {
                var kontrahent = (Kontrahent)objectSpace.GetObject(obj);
                if (kontrahent != null)
                {
                    kontrahent.Typ = typDoPrzypisania;
                }
            
            }

            objectSpace.CommitChanges();
            View.ObjectSpace.Refresh();
        }

        private void SetActionItems(SingleChoiceAction akcja, Type type)
        {
            foreach (var item in Enum.GetValues(type))
            {
                var descriptor = new EnumDescriptor(type);
                var it = new ChoiceActionItem(descriptor.GetCaption(item), item);
                it.ImageName = ImageLoader.Instance.GetEnumValueImageName(item);
                akcja.Items.Add(it);
            }
        }
    }

    [DevExpress.ExpressApp.DC.DomainComponent]
    public class InvoiceTemplate
    {

        public InvoiceTemplate(Session session)
        {
            _Products = new XPCollection<Produkt>(session);
        }

        public DateTime DataFaktury { get; set; }
        public DateTime DataPlatnosci { get; set; }
        private XPCollection<Produkt> _Products;
        //   [XafDisplayName("Lista produktów")]
        public XPCollection<Produkt> Products { get { return _Products; } }
    }
}
