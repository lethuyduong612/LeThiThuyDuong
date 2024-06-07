using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;

namespace ĐATN
{
    [Transaction(TransactionMode.Manual)]
    public class Class1:IExternalCommand
    {
 
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Formgioithieu frm = new Formgioithieu();
            frm.ShowDialog();

            return Result.Succeeded;
            //
        }
    }
}
