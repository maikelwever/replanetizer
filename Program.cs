using System;
using Gtk;
using OpenTK;
using RatchetEdit.UI;

namespace RatchetEdit
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // OpenGL
			Toolkit.Init(new ToolkitOptions
			{
				Backend = PlatformBackend.PreferNative,
				EnableHighResolution = true
			});

			// GTK
			Application.Init();
            Builder builder = new Builder();
            builder.AddFromFile("UI/MainWindow.glade");
			MainWindow win = new MainWindow(builder);
			win.Show();
			Application.Run();
        }
    }
}
