// AUTOMATICALLY GENERATED CODE

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
        Label BenText;
        Label PhoenixText;
        Label MargaretText;

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
            BenText = new Label();
            BenText.Name = "BenText";
            PhoenixText = new Label();
            PhoenixText.Name = "PhoenixText";
            MargaretText = new Label();
            MargaretText.Name = "MargaretText";

            // sceneBackgroundPanel
            sceneBackgroundPanel.BackgroundColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);

            // CreditsTitleText
            CreditsTitleText.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            CreditsTitleText.Font = new UIFont(FontAlias.System, 72, FontStyle.Regular);
            CreditsTitleText.LineBreak = LineBreak.Character;

            // BenText
            BenText.TextColor = new UIColor(229f / 255f, 19f / 255f, 19f / 255f, 255f / 255f);
            BenText.Font = new UIFont(FontAlias.System, 36, FontStyle.Regular);
            BenText.LineBreak = LineBreak.Character;
            BenText.VerticalAlignment = VerticalAlignment.Top;

            // PhoenixText
            PhoenixText.TextColor = new UIColor(41f / 255f, 226f / 255f, 226f / 255f, 255f / 255f);
            PhoenixText.Font = new UIFont(FontAlias.System, 36, FontStyle.Regular);
            PhoenixText.LineBreak = LineBreak.Character;
            PhoenixText.VerticalAlignment = VerticalAlignment.Top;

            // MargaretText
            MargaretText.TextColor = new UIColor(247f / 255f, 226f / 255f, 246f / 255f, 255f / 255f);
            MargaretText.Font = new UIFont(FontAlias.System, 36, FontStyle.Regular);
            MargaretText.LineBreak = LineBreak.Character;
            MargaretText.VerticalAlignment = VerticalAlignment.Top;

            // CreditsScene
            this.RootWidget.AddChildLast(sceneBackgroundPanel);
            this.RootWidget.AddChildLast(CreditsTitleText);
            this.RootWidget.AddChildLast(BenText);
            this.RootWidget.AddChildLast(PhoenixText);
            this.RootWidget.AddChildLast(MargaretText);

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

                    BenText.SetPosition(20, 188);
                    BenText.SetSize(214, 36);
                    BenText.Anchors = Anchors.None;
                    BenText.Visible = true;

                    PhoenixText.SetPosition(20, 188);
                    PhoenixText.SetSize(214, 36);
                    PhoenixText.Anchors = Anchors.None;
                    PhoenixText.Visible = true;

                    MargaretText.SetPosition(20, 188);
                    MargaretText.SetSize(214, 36);
                    MargaretText.Anchors = Anchors.None;
                    MargaretText.Visible = true;

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

                    BenText.SetPosition(36, 199);
                    BenText.SetSize(389, 50);
                    BenText.Anchors = Anchors.None;
                    BenText.Visible = true;

                    PhoenixText.SetPosition(36, 299);
                    PhoenixText.SetSize(389, 50);
                    PhoenixText.Anchors = Anchors.None;
                    PhoenixText.Visible = true;

                    MargaretText.SetPosition(36, 399);
                    MargaretText.SetSize(389, 50);
                    MargaretText.Anchors = Anchors.None;
                    MargaretText.Visible = true;

                    break;
            }
            _currentLayoutOrientation = orientation;
        }

        public void UpdateLanguage()
        {
            CreditsTitleText.Text = "credits";

            BenText.Text = "ben johnson";

            PhoenixText.Text = "phoenix perry";

            MargaretText.Text = "margaret schedel";

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
