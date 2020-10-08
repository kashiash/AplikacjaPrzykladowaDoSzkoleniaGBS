using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
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

     public class FakturaViewController :ObjectViewController<ListView,Faktura>
    {

        SimpleAction WystawKorekteAction;


        public FakturaViewController()
        {
            //   TargetViewType = ViewType.ListView;
            //  TargetObjectType = typeof(Kontrahent);

            WystawKorekteAction = new SimpleAction(this,
                                                   $"{GetType().FullName}.{nameof(WystawKorekteAction)}",
                                                   PredefinedCategory.Edit)
            { 
            Caption= "Wystaw korektę",
            ConfirmationMessage = "Czy jesteś w pełni władz umysłowych",
            ImageName = "BO_Audit_ChangeHistory",
            
            };

            WystawKorekteAction.Execute += WystawKorekteAction_Execute;
        }

        private void WystawKorekteAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace();
            var faktura = objectSpace.GetObject(ViewCurrentObject) ;
            InvoiceFactory factory = new InvoiceFactory(objectSpace);
           var korekta =   factory.WystawKorekte(faktura);
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
            View.ObjectSpace.Refresh();
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
}
