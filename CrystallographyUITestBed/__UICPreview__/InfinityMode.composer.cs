// AUTOMATICALLY GENERATED CODE

using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;

namespace Preview
{
    partial class InfinityMode
    {
        Panel sceneBackgroundPanel;
        ImageBox ImageBox_1;
        Button Button_1;
        ImageBox ImageBox_7;
        ImageBox ImageBox_8;
        ImageBox ImageBox_9;
        ImageBox ImageBox_10;
        ImageBox ImageBox_4;

        private void InitializeWidget()
        {
            InitializeWidget(LayoutOrientation.Horizontal);
        }

        private void InitializeWidget(LayoutOrientation orientation)
        {
            sceneBackgroundPanel = new Panel();
            sceneBackgroundPanel.Name = "sceneBackgroundPanel";
            ImageBox_1 = new ImageBox();
            ImageBox_1.Name = "ImageBox_1";
            Button_1 = new Button();
            Button_1.Name = "Button_1";
            ImageBox_7 = new ImageBox();
            ImageBox_7.Name = "ImageBox_7";
            ImageBox_8 = new ImageBox();
            ImageBox_8.Name = "ImageBox_8";
            ImageBox_9 = new ImageBox();
            ImageBox_9.Name = "ImageBox_9";
            ImageBox_10 = new ImageBox();
            ImageBox_10.Name = "ImageBox_10";
            ImageBox_4 = new ImageBox();
            ImageBox_4.Name = "ImageBox_4";

            // sceneBackgroundPanel
            sceneBackgroundPanel.BackgroundColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);

            // ImageBox_1
            ImageBox_1.Image = new ImageAsset("/Application/assets/new/newUI/authoredHeader.png");
            ImageBox_1.ImageScaleType = ImageScaleType.Center;

            // Button_1
            Button_1.IconImage = new ImageAsset("/Application/assets/new/featureBtns/colorRed.png");
            Button_1.Style = ButtonStyle.Custom;
            Button_1.CustomImage = new CustomButtonImageSettings()
            {
                BackgroundNormalImage = new ImageAsset("/Application/assets/new/featureBtns/colorRed.png"),
                BackgroundPressedImage = new ImageAsset("/Application/assets/new/featureBtns/colorBlue.png"),
                BackgroundDisabledImage = null,
                BackgroundNinePatchMargin = new NinePatchMargin(42, 27, 42, 27),
            };
            Button_1.BackgroundFilterColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 0f / 255f);

            // ImageBox_7
            ImageBox_7.Image = new ImageAsset("/Application/assets/new/featureBtns/animationRed.png");
            ImageBox_7.ImageScaleType = ImageScaleType.Center;

            // ImageBox_8
            ImageBox_8.Image = new ImageAsset("/Application/assets/new/featureBtns/patternRed.png");
            ImageBox_8.ImageScaleType = ImageScaleType.Center;

            // ImageBox_9
            ImageBox_9.Image = new ImageAsset("/Application/assets/new/featureBtns/soundRed.png");
            ImageBox_9.ImageScaleType = ImageScaleType.Center;

            // ImageBox_10
            ImageBox_10.Image = new ImageAsset("/Application/assets/new/featureBtns/particleRed.png");
            ImageBox_10.ImageScaleType = ImageScaleType.Center;

            // ImageBox_4
            ImageBox_4.Image = new ImageAsset("/Application/assets/new/newUI/play.png");
            ImageBox_4.ImageScaleType = ImageScaleType.Center;

            // InfinityMode
            this.RootWidget.AddChildLast(sceneBackgroundPanel);
            this.RootWidget.AddChildLast(ImageBox_1);
            this.RootWidget.AddChildLast(Button_1);
            this.RootWidget.AddChildLast(ImageBox_7);
            this.RootWidget.AddChildLast(ImageBox_8);
            this.RootWidget.AddChildLast(ImageBox_9);
            this.RootWidget.AddChildLast(ImageBox_10);
            this.RootWidget.AddChildLast(ImageBox_4);

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

                    ImageBox_1.SetPosition(0, 0);
                    ImageBox_1.SetSize(200, 200);
                    ImageBox_1.Anchors = Anchors.None;
                    ImageBox_1.Visible = true;

                    Button_1.SetPosition(21, 370);
                    Button_1.SetSize(214, 56);
                    Button_1.Anchors = Anchors.None;
                    Button_1.Visible = true;

                    ImageBox_7.SetPosition(199, 270);
                    ImageBox_7.SetSize(200, 200);
                    ImageBox_7.Anchors = Anchors.None;
                    ImageBox_7.Visible = true;

                    ImageBox_8.SetPosition(384, 262);
                    ImageBox_8.SetSize(200, 200);
                    ImageBox_8.Anchors = Anchors.None;
                    ImageBox_8.Visible = true;

                    ImageBox_9.SetPosition(576, 275);
                    ImageBox_9.SetSize(200, 200);
                    ImageBox_9.Anchors = Anchors.None;
                    ImageBox_9.Visible = true;

                    ImageBox_10.SetPosition(701, 252);
                    ImageBox_10.SetSize(200, 200);
                    ImageBox_10.Anchors = Anchors.None;
                    ImageBox_10.Visible = true;

                    ImageBox_4.SetPosition(678, 350);
                    ImageBox_4.SetSize(200, 200);
                    ImageBox_4.Anchors = Anchors.None;
                    ImageBox_4.Visible = true;

                    break;

                default:
                    this.DesignWidth = 960;
                    this.DesignHeight = 544;

                    sceneBackgroundPanel.SetPosition(0, 0);
                    sceneBackgroundPanel.SetSize(960, 544);
                    sceneBackgroundPanel.Anchors = Anchors.Top | Anchors.Bottom | Anchors.Left | Anchors.Right;
                    sceneBackgroundPanel.Visible = true;

                    ImageBox_1.SetPosition(52, 47);
                    ImageBox_1.SetSize(854, 191);
                    ImageBox_1.Anchors = Anchors.None;
                    ImageBox_1.Visible = true;

                    Button_1.SetPosition(52, 271);
                    Button_1.SetSize(162, 162);
                    Button_1.Anchors = Anchors.None;
                    Button_1.Visible = true;

                    ImageBox_7.SetPosition(189, 243);
                    ImageBox_7.SetSize(219, 218);
                    ImageBox_7.Anchors = Anchors.None;
                    ImageBox_7.Visible = true;

                    ImageBox_8.SetPosition(355, 243);
                    ImageBox_8.SetSize(219, 218);
                    ImageBox_8.Anchors = Anchors.None;
                    ImageBox_8.Visible = true;

                    ImageBox_9.SetPosition(522, 243);
                    ImageBox_9.SetSize(219, 218);
                    ImageBox_9.Anchors = Anchors.None;
                    ImageBox_9.Visible = true;

                    ImageBox_10.SetPosition(688, 243);
                    ImageBox_10.SetSize(219, 218);
                    ImageBox_10.Anchors = Anchors.None;
                    ImageBox_10.Visible = true;

                    ImageBox_4.SetPosition(788, 404);
                    ImageBox_4.SetSize(200, 200);
                    ImageBox_4.Anchors = Anchors.None;
                    ImageBox_4.Visible = true;

                    break;
            }
            _currentLayoutOrientation = orientation;
        }

        public void UpdateLanguage()
        {
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
