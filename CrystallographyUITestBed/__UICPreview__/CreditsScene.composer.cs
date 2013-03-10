﻿// AUTOMATICALLY GENERATED CODE

using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;

namespace Preview
{
    partial class CreditsScene
    {
        Panel sceneBackgroundPanel;
        Label CreditsTitleText;
        PagePanel PagePanel_1;

        private void InitializeWidget()
        {
            InitializeWidget(LayoutOrientation.Horizontal);
        }

        private void InitializeWidget(LayoutOrientation orientation)
        {
            sceneBackgroundPanel = new Panel();
            sceneBackgroundPanel.Name = "sceneBackgroundPanel";
            CreditsTitleText = new Label();
            CreditsTitleText.Name = "CreditsTitleText";
            PagePanel_1 = new PagePanel();
            PagePanel_1.Name = "PagePanel_1";

            // sceneBackgroundPanel
            sceneBackgroundPanel.BackgroundColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);

            // CreditsTitleText
            CreditsTitleText.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            CreditsTitleText.Font = new UIFont(FontAlias.System, 72, FontStyle.Regular);
            CreditsTitleText.LineBreak = LineBreak.Character;

            // CreditsScene
            this.RootWidget.AddChildLast(sceneBackgroundPanel);
            this.RootWidget.AddChildLast(CreditsTitleText);
            this.RootWidget.AddChildLast(PagePanel_1);

            SetWidgetLayout(orientation);

            UpdateLanguage();
        }

        private LayoutOrientation _currentLayoutOrientation;
        public void SetWidgetLayout(LayoutOrientation orientation)
        {
            switch (orientation)
            {
                case LayoutOrientation.Vertical:
                    this.DesignWidth = 544;
                    this.DesignHeight = 960;

                    sceneBackgroundPanel.SetPosition(0, 0);
                    sceneBackgroundPanel.SetSize(544, 960);
                    sceneBackgroundPanel.Anchors = Anchors.Top | Anchors.Bottom | Anchors.Left | Anchors.Right;
                    sceneBackgroundPanel.Visible = true;

                    CreditsTitleText.SetPosition(20, 27);
                    CreditsTitleText.SetSize(214, 36);
                    CreditsTitleText.Anchors = Anchors.None;
                    CreditsTitleText.Visible = true;

                    PagePanel_1.SetPosition(36, 154);
                    PagePanel_1.SetSize(100, 50);
                    PagePanel_1.Anchors = Anchors.None;
                    PagePanel_1.Visible = true;

                    break;

                default:
                    this.DesignWidth = 960;
                    this.DesignHeight = 544;

                    sceneBackgroundPanel.SetPosition(0, 0);
                    sceneBackgroundPanel.SetSize(960, 544);
                    sceneBackgroundPanel.Anchors = Anchors.Top | Anchors.Bottom | Anchors.Left | Anchors.Right;
                    sceneBackgroundPanel.Visible = true;

                    CreditsTitleText.SetPosition(36, 28);
                    CreditsTitleText.SetSize(327, 72);
                    CreditsTitleText.Anchors = Anchors.None;
                    CreditsTitleText.Visible = true;

                    PagePanel_1.SetPosition(36, 150);
                    PagePanel_1.SetSize(885, 344);
                    PagePanel_1.Anchors = Anchors.None;
                    PagePanel_1.Visible = true;

                    break;
            }
            _currentLayoutOrientation = orientation;
        }

        public void UpdateLanguage()
        {
            CreditsTitleText.Text = "credits";

            this.Title = "CreditsScene";
        }

        private void onShowing(object sender, EventArgs e)
        {
            switch (_currentLayoutOrientation)
            {
                case LayoutOrientation.Vertical:
                    break;

                default:
                    break;
            }
        }

        private void onShown(object sender, EventArgs e)
        {
            switch (_currentLayoutOrientation)
            {
                case LayoutOrientation.Vertical:
                    break;

                default:
                    break;
            }
        }

    }
}
