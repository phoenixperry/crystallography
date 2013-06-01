// AUTOMATICALLY GENERATED CODE

using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;

namespace Preview
{
    partial class won
    {
        ImageBox ImageBox_1;
        Panel Panel_1;
        Button Button_1;
        ImageBox ImageBox_2;
        ImageBox ImageBox_3;

        private void InitializeWidget()
        {
            InitializeWidget(LayoutOrientation.Horizontal);
        }

        private void InitializeWidget(LayoutOrientation orientation)
        {
            ImageBox_1 = new ImageBox();
            ImageBox_1.Name = "ImageBox_1";
            Panel_1 = new Panel();
            Panel_1.Name = "Panel_1";
            Button_1 = new Button();
            Button_1.Name = "Button_1";
            ImageBox_2 = new ImageBox();
            ImageBox_2.Name = "ImageBox_2";
            ImageBox_3 = new ImageBox();
            ImageBox_3.Name = "ImageBox_3";

            // ImageBox_1
            ImageBox_1.Image = new ImageAsset("/Application/assets/new/newUI/bestTimevsCurrentTime.png");
            ImageBox_1.ImageScaleType = ImageScaleType.Center;

            // Panel_1
            Panel_1.BackgroundColor = new UIColor(30f / 255f, 30f / 255f, 30f / 255f, 255f / 255f);
            Panel_1.Clip = true;
            Panel_1.AddChildLast(ImageBox_1);

            // Button_1
            Button_1.IconImage = new ImageAsset("/Application/assets/new/newUI/quit.png");
            Button_1.BackgroundFilterColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 0f / 255f);

            // ImageBox_2
            ImageBox_2.Image = new ImageAsset("/Application/assets/new/newUI/mainMenu.png");
            ImageBox_2.ImageScaleType = ImageScaleType.Center;

            // ImageBox_3
            ImageBox_3.Image = new ImageAsset("/Application/assets/new/newUI/newsetup.png");
            ImageBox_3.ImageScaleType = ImageScaleType.Center;

            // won
            this.RootWidget.AddChildLast(Panel_1);
            this.RootWidget.AddChildLast(Button_1);
            this.RootWidget.AddChildLast(ImageBox_2);
            this.RootWidget.AddChildLast(ImageBox_3);

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

                    ImageBox_1.SetPosition(375, 90);
                    ImageBox_1.SetSize(200, 200);
                    ImageBox_1.Anchors = Anchors.None;
                    ImageBox_1.Visible = true;

                    Panel_1.SetPosition(357, 225);
                    Panel_1.SetSize(100, 100);
                    Panel_1.Anchors = Anchors.None;
                    Panel_1.Visible = true;

                    Button_1.SetPosition(180, 326);
                    Button_1.SetSize(214, 56);
                    Button_1.Anchors = Anchors.None;
                    Button_1.Visible = true;

                    ImageBox_2.SetPosition(299, 256);
                    ImageBox_2.SetSize(200, 200);
                    ImageBox_2.Anchors = Anchors.None;
                    ImageBox_2.Visible = true;

                    ImageBox_3.SetPosition(578, 257);
                    ImageBox_3.SetSize(200, 200);
                    ImageBox_3.Anchors = Anchors.None;
                    ImageBox_3.Visible = true;

                    break;

                default:
                    this.DesignWidth = 960;
                    this.DesignHeight = 544;

                    ImageBox_1.SetPosition(61, 10);
                    ImageBox_1.SetSize(493, 205);
                    ImageBox_1.Anchors = Anchors.None;
                    ImageBox_1.Visible = true;

                    Panel_1.SetPosition(166, 90);
                    Panel_1.SetSize(616, 310);
                    Panel_1.Anchors = Anchors.None;
                    Panel_1.Visible = true;

                    Button_1.SetPosition(164, 325);
                    Button_1.SetSize(162, 75);
                    Button_1.Anchors = Anchors.None;
                    Button_1.Visible = true;

                    ImageBox_2.SetPosition(328, 325);
                    ImageBox_2.SetSize(232, 75);
                    ImageBox_2.Anchors = Anchors.None;
                    ImageBox_2.Visible = true;

                    ImageBox_3.SetPosition(562, 325);
                    ImageBox_3.SetSize(220, 75);
                    ImageBox_3.Anchors = Anchors.None;
                    ImageBox_3.Visible = true;

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
