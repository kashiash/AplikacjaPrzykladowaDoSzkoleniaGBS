using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using Solution2.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution2.Module.Win.Controllers
{
    // public class KlientViewController : ViewController
    // public class KlientViewController : ObjectViewController<ObjectView, Kontrahent>

    public class FakturaViewController : ObjectViewController<ListView, Faktura>
    {

        SimpleAction WystawKorekteAction;

        SingleChoiceAction MojaAkcja2;

        public FakturaViewController()
        {
            //   TargetViewType = ViewType.ListView;
            //  TargetObjectType = typeof(Kontrahent);

            WystawKorekteAction = new SimpleAction(this,
                                                   $"{GetType().FullName}.{nameof(WystawKorekteAction)}",
                                                   PredefinedCategory.Edit)
            {
                Caption = "Wystaw korektę",
                ConfirmationMessage = "Czy jesteś w pełni władz umysłowych",
                ImageName = "BO_Audit_ChangeHistory",

            };

            WystawKorekteAction.Execute += WystawKorekteAction_Execute;



            MojaAkcja2 = new SingleChoiceAction(this, $"{GetType().FullName}.{nameof(MojaAkcja2)}", PredefinedCategory.Edit)
            {
                Caption = "Zrób coś",
                ImageName = "BO_Task",
                ItemType = SingleChoiceActionItemType.ItemIsOperation,
            };

            SetActionItems(MojaAkcja2,typeof(listaDoAkcji));
            MojaAkcja2.Execute += MojaAkcja2_Execute;
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

        private void SetActionItems()
        {
            var item1 = new ChoiceActionItem("wybór 1", 1);
            MojaAkcja2.Items.Add(item1);
            var item2 = new ChoiceActionItem("wybór 2", 2);
            MojaAkcja2.Items.Add(item2);
            var item3 = new ChoiceActionItem("wybór 3", 3);
            MojaAkcja2.Items.Add(item3);
            var item4 = new ChoiceActionItem("wybór 4", 4);
            MojaAkcja2.Items.Add(item4);
        }

        private void MojaAkcja2_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            var wybrano = e.SelectedChoiceActionItem.Data;
        }

        private void WystawKorekteAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace();
            var faktura = objectSpace.GetObject(ViewCurrentObject);
            InvoiceFactory factory = new InvoiceFactory(objectSpace);
            var korekta = factory.WystawKorekte(faktura);
            if (korekta != null)
            {
                DetailView dv = Application.CreateDetailView(objectSpace, korekta);
                e.ShowViewParameters.CreatedView = dv;
                e.ShowViewParameters.Context = TemplateContext.View;
                e.ShowViewParameters.TargetWindow = TargetWindow.Default;

                dv.Closed += Dv_Closed;
                // Wyswietl utworzona korekte
            }
        }

        private void Dv_Closed(object sender, EventArgs e)
        {
            ObjectSpace.Refresh();
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            // tu wpisujemy
        }

        protected override void OnDeactivated()
        {
            // tu wpisujemy kod
            base.OnDeactivated();
        }

    }

    public enum listaDoAkcji
    {
        [XafDisplayName("Wyświetl coś")]
        [ImageName("BO_Invoice")]
        Wybor1 = 1,
        [ImageName("BO_Invoice")]
        [XafDisplayName("Wyświetl coś innego")]
        Wybor2 = 2,
        [ImageName("BO_Skull")]
        Wybor3 = 3,
        [ImageName("BO_Task")]
        Wybor21 = 21,
        Wybor22 = 22,
        Wybor23 = 23,
        Wybor31 = 31,
        Wybor32 = 32,
        Wybor33 = 33
    }
}
