using Gtk;
using System;

namespace RatchetEdit.UI
{
    public static class ErrorHelper
    {
        public static void ShowErrorAndQuit(String errorMessage)
        {
            ShowErrorAndQuit(errorMessage, "Unrecoverable Error!");
        }

        public static void ShowErrorAndQuit(String errorMessage, String dialogTitle)
        {
            MessageDialog alert = new MessageDialog(null, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, errorMessage);
            alert.Title = dialogTitle;
            alert.Run();
            Application.Quit();
        }
    }
}
