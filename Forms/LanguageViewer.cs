﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using RatchetEdit.LevelObjects;

namespace RatchetEdit
{
    public partial class LanguageViewer : Form
    {
        LevelUserControl main;
        public LanguageViewer(LevelUserControl main)
        {
            InitializeComponent();
            this.main = main;
        }

        private void ShowLanguageText(Dictionary<int, String> languageData)
        {
            foreach (KeyValuePair<int, String> entry in languageData)
            {
                ListViewItem item = new ListViewItem(entry.Key.ToString());
                item.SubItems.Add(entry.Value);
                languageTextList.Items.Add(item);
            }
        }

        private void UpdateList()
        {
            languageTextList.Items.Clear();

            switch (languageList.SelectedIndex)
            {
                case 0:
                    ShowLanguageText(main.level.english);
                    break;
                case 1:
                    ShowLanguageText(main.level.lang2);
                    break;
                case 2:
                    ShowLanguageText(main.level.french);
                    break;
                case 3:
                    ShowLanguageText(main.level.german);
                    break;
                case 4:
                    ShowLanguageText(main.level.spanish);
                    break;
                case 5:
                    ShowLanguageText(main.level.italian);
                    break;
                case 6:
                    ShowLanguageText(main.level.lang7);
                    break;
                case 7:
                    ShowLanguageText(main.level.lang8);
                    break;
            }
        }

        private void languageList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateList();
        }

        private void LanguageViewer_Load(object sender, EventArgs e)
        {
            UpdateList();
        }
    }
}
