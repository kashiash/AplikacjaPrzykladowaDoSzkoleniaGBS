using DevExpress.ExpressApp;

namespace Solution2.Module.Win.Controllers
{
    internal class InvoiceFactory
    {
        private IObjectSpace objectSpace;

        public InvoiceFactory(IObjectSpace objectSpace)
        {
            this.objectSpace = objectSpace;
        }
    }
}