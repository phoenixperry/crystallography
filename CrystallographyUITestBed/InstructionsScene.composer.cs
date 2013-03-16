// AUTOMATICALLY GENERATED CODE

using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;

namespace Crystallography.UI
{
    partial class InstructionsScene
    {
        Panel sceneBackgroundPanel;
        Label InstructionsTitleText;
        PagePanel PagePanel_1;

        private void InitializeWidget()
        {
            InitializeWidget(LayoutOrientation.Horizontal);
        }

        private void InitializeWidget(LayoutOrientation orientation)
        {
            sceneBackgroundPanel = new Panel();
            sceneBackgroundPanel.Name = "sceneBackgroundPanel";
            InstructionsTitleText = new Label();
            InstructionsTitleText.Name = "InstructionsTitleText";
            PagePanel_1 = new PagePanel();
            PagePanel_1.Name = "PagePanel_1";

            // sceneBackgroundPanel
            sceneBackgroundPanel.BackgroundColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);

            // InstructionsTitleText
            InstructionsTitleText.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            InstructionsTitleText.Font = new UIFont(FontAlias.System, 25, FontStyle.Regular);
            InstructionsTitleText.LineBreak = LineBreak.Character;

            // PagePanel_1
            PagePanel_1.AddPage(new InstructionsPanel());
            PagePanel_1.AddPage(new Instructions2());

            // InstructionsScene
            this.RootWidget.AddChildLast(sceneBackgroundPanel);
            this.RootWidget.AddChildLast(InstructionsTitleText);
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

                    InstructionsTitleText.SetPosition(19, 29);
                    InstructionsTitleText.SetSize(214, 36);
                    InstructionsTitleText.Anchors = Anchors.None;
                    InstructionsTitleText.Visible = true;

                    PagePanel_1.SetPosition(206, 201);
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

                    InstructionsTitleText.SetPosition(19, 29);
                    InstructionsTitleText.SetSize(214, 36);
                    InstructionsTitleText.Anchors = Anchors.None;
                    InstructionsTitleText.Visible = true;

                    PagePanel_1.SetPosition(19, 88);
                    PagePanel_1.SetSize(924, 425);
                    PagePanel_1.Anchors = Anchors.None;
                    PagePanel_1.Visible = true;

                    break;
            }
            _currentLayoutOrientation = orientation;
        }

        public void UpdateLanguage()
        {
            InstructionsTitleText.Text = "instructions";

            this.Title = "Instructions";
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
