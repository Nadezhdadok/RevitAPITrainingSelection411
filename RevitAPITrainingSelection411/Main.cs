﻿using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITrainingSelection411
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            string roomInfo = string.Empty;

            var rooms = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Rooms).Cast<Room>().ToList();

            foreach (Room room in rooms)
            {
                string roomName = room.get_Parameter(BuiltInParameter.ROOM_NAME).AsString();
                roomInfo += $"{roomName}  {room.Volume}{Environment.NewLine}";
            }

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string csvPath = Path.Combine(desktopPath, "roomInfo.csv");

            File.WriteAllText(csvPath, roomInfo);

            return Result.Succeeded;
        }
    }
}
