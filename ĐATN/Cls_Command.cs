using System;
using System.Collections.Generic;
using Autodesk.Revit.UI;
using System.Reflection;
using System.Windows.Media.Imaging;
using Autodesk.Revit.Attributes;

namespace ĐATN
{
    [Transaction(TransactionMode.Manual)]
    internal class Cls_Command:IExternalApplication
    {
            public Result OnShutdown(UIControlledApplication application)
            {
                throw new NotImplementedException();
            }
        //add nút vào revit
            public Result OnStartup(UIControlledApplication application)
            {
            application.CreateRibbonTab("LTTD");

            //Create The Plugin Panel.
            RibbonPanel Panel = application.CreateRibbonPanel("LTTD", "Tinh cot");
            Assembly assembly = Assembly.GetExecutingAssembly();

            //Create The Plugin Button.
            PushButtonData Button = new PushButtonData("btn_Loc1", "Tinh cot", assembly.Location, "ĐATN.Class1"); 
            PushButton pushButton = Panel.AddItem(Button) as PushButton;
            
            return Result.Succeeded;
            }

            private void addPushButton(RibbonPanel panel, string btnname, string btntext, string classname)
            {

                PushButtonData pushButtonData = new PushButtonData(btnname, btntext, Return_App_Path() + "ĐATN.dll", "ĐATN." + classname);
                PushButton pushButton = panel.AddItem(pushButtonData) as PushButton;
                

            }
            private string Return_App_Path()
            {
                String AppPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                AppPath = System.IO.Path.GetDirectoryName(AppPath) + @"\";

                return AppPath;

            }
        
    }

}

