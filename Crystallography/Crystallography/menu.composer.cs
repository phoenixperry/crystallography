// AUTOMATICALLY GENERATED CODE

using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;

namespace Crystallography
{
    partial class menu
    {
        Panel sceneBackgroundPanel;
        Button buttonUI1;
        Button buttonUI2;

        private void InitializeWidget()
        {
            InitializeWidget(LayoutOrientation.Horizontal);
        }

        private void InitializeWidget(LayoutOrientation orientation)
        {
            sceneBackgroundPanel = new Panel();
            sceneBackgroundPanel.Name = "sceneBackgroundPanel";
            buttonUI1 = new Button();
            buttonUI1.Name = "buttonUI1";
            buttonUI2 = new Button();
            buttonUI2.Name = "buttonUI2";

            // sceneBackgroundPanel
            sceneBackgroundPanel.BackgroundColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);

            // buttonUI1
            buttonUI1.IconImage = null;
            buttonUI1.Style = ButtonStyle.Custom;
            buttonUI1.CustomImage = new CustomButtonImageSettings()
            {
                BackgroundNormalImage = new ImageAsset("/Application/assets/play.png"),
                BackgroundPressedImage = new ImageAsset("/Application/assets/playOver.png"),
                BackgroundDisabledImage = new ImageAsset("/Application/assets/playOver.png"),
                BackgroundNinePatchMargin = new NinePatchMargin(42, 27, 42, 27),
            };
            buttonUI1.BackgroundFilterColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);

            // buttonUI2
            buttonUI2.IconImage = null;
            buttonUI2.Style = ButtonStyle.Custom;
            buttonUI2.CustomImage = new CustomButtonImageSettings()
            {
                BackgroundNormalImage = new ImageAsset("/Application/assets/instructions.png"),
                BackgroundPressedImage = new ImageAsset("/Application/assets/instructionsOver.png"),
                BackgroundDisabledImage = null,
                BackgroundNinePatchMargin = new NinePatchMargin(42, 27, 42, 27),
            };

            // menu
            this.RootWidget.AddChildLast(sceneBackgroundPanel);
            this.RootWidget.AddChildLast(buttonUI1);
            this.RootWidget.AddChildLast(buttonUI2);

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

                    buttonUI1.SetPosition(356, 170);
                    buttonUI1.SetSize(214, 56);
                    buttonUI1.Anchors = Anchors.None;
                    buttonUI1.Visible = true;

                    buttonUI2.SetPosition(356, 356);
                    buttonUI2.SetSize(214, 56);
                    buttonUI2.Anchors = Anchors.None;
                    buttonUI2.Visible = true;

                    break;

                default:
                    this.DesignWidth = 960;
                    this.DesignHeight = 544;

                    sceneBackgroundPanel.SetPosition(0, 0);
                    sceneBackgroundPanel.SetSize(960, 544);
                    sceneBackgroundPanel.Anchors = Anchors.Top | Anchors.Bottom | Anchors.Left | Anchors.Right;
                    sceneBackgroundPanel.Visible = true;

                    buttonUI1.SetPosition(356, 24);
                    buttonUI1.SetSize(289, 280);
                    buttonUI1.Anchors = Anchors.None;
                    buttonUI1.Visible = true;

                    buttonUI2.SetPosition(356, 281);
                    buttonUI2.SetSize(289, 233);
                    buttonUI2.Anchors = Anchors.None;
                    buttonUI2.Visible = true;

                    break;
            }
            _currentLayoutOrientation = orientation;
        }

        public void UpdateLanguage()
        {
            this.Title = "Menu";
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
