using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using Gdk;
using System.Collections.Generic;
using UIElement = Gtk.Builder.ObjectAttribute;

namespace RatchetEdit
{
    class MainWindow : Gtk.Window
    {
        [UIElement] private readonly Paned ViewportPaned;
		[UIElement] private readonly Alignment ViewportAlignment;
		[UIElement] private readonly ToolButton OpenFileButton;

        private RenderManager renderManager;

        public MainWindow(Builder builder) : base(builder.GetObject("MainWindow").Handle)
        {
            builder.Autoconnect(this);
            DeleteEvent += Window_DeleteEvent;

            this.renderManager = new RenderManager(this);
            this.ViewportAlignment.Add(renderManager.ViewportWidget);
			this.ViewportAlignment.ShowAll();

            OpenFileButton.Clicked += (sender, args) => 
            {
                FileChooserDialog dialog = new FileChooserDialog(
                    "Please select folder with many files",
                    this,
                    FileChooserAction.SelectFolder,
                    "Cancel",
                    ResponseType.Cancel,
                    "Ok",
                    ResponseType.Ok
                );
                if (dialog.Run() == (int)ResponseType.Ok) {
                    String levelToOpen = dialog.Filename;
                    LoadLevel(levelToOpen);
                }
                dialog.Dispose();
            };
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }

        void LoadLevel(string fileName)
        {
            Level level = new Level(fileName);
            if (!level.valid) return;

            this.renderManager.LoadLevel(level);

            //objectTree.UpdateEntries(level);
            //UpdateProperties(null);
        }

    }
}
