// AUTOMATICALLY GENERATED CODE

using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;

namespace Crystallography.UI
{
    partial class Instructions7Panel
    {
        ImageBox ImageBox_1;
        Button Button_1;

        private void InitializeWidget()
        {
            InitializeWidget(LayoutOrientation.Horizontal);
        }

        private void InitializeWidget(LayoutOrientation orientation)
        {
            ImageBox_1 = new ImageBox();
            ImageBox_1.Name = "ImageBox_1";
            Button_1 = new Button();
            Button_1.Name = "Button_1";

            // ImageBox_1
            ImageBox_1.Image = new ImageAsset("/Application/assets/images/UI/instructions7.png");
            ImageBox_1.ImageScaleType = ImageScaleType.Center;

            // Button_1
            Button_1.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            Button_1.TextFont = new UIFont(FontAlias.System, 48, FontStyle.Regular);
            Button_1.Style = ButtonStyle.Custom;
            Button_1.CustomImage = new CustomButtonImageSettings()
            {
                BackgroundNormalImage = new ImageAsset("/Application/assets/images/UI/redBtn.png"),
                BackgroundPressedImage = new ImageAsset("/Application/assets/images/UI/redBtnOver.png"),
                BackgroundDisabledImage = null,
                BackgroundNinePatchMargin = new NinePatchMargin(42, 27, 42, 27),
            };

            // Instructions7Panel
            this.BackgroundColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            this.Clip = true;
            this.AddChildLast(ImageBox_1);
            this.AddChildLast(Button_1);

            SetWidgetLayout(orientation);

            UpdateLanguage();
        }

        private LayoutOrientation _currentLayoutOrientation;
        public void SetWidgetLayout(LayoutOrientation orientation)
        {
            switch (orientation)
            {
                case LayoutOrientation.Vertical:
                    this.SetSize(544, 960);
                    this.Anchors = Anchors.None;

                    ImageBox_1.SetPosition(336, 115);
                    ImageBox_1.SetSize(200, 200);
                    ImageBox_1.Anchors = Anchors.None;
                    ImageBox_1.Visible = true;

                    Button_1.SetPosition(306, 455);
                    Button_1.SetSize(214, 56);
                    Button_1.Anchors = Anchors.None;
                    Button_1.Visible = true;

                    break;

                default:
                    this.SetSize(924, 511);
                    this.Anchors = Anchors.None;

                    ImageBox_1.SetPosition(72, 39);
                    ImageBox_1.SetSize(779, 432);
                    ImageBox_1.Anchors = Anchors.None;
                    ImageBox_1.Visible = true;

                    Button_1.SetPosition(317, 440);
                    Button_1.SetSize(289, 71);
                    Button_1.Anchors = Anchors.None;
                    Button_1.Visible = true;

                    break;
            }
            _currentLayoutOrientation = orientation;
        }

        public void UpdateLanguage()
        {
            Button_1.Text = "play";
        }

        public void InitializeDefaultEffect()
        {
            switch (_currentLayoutOrientation)
            {
                case LayoutOrientation.Vertical:
                    Button_1.Visible = false;
                    break;

                default:
                    Button_1.Visible = false;
                    break;
            }
        }

        public void StartDefaultEffect()
        {
            switch (_currentLayoutOrientation)
            {
                case LayoutOrientation.Vertical:
                    new SlideInEffect()
                    {
                        Widget = Button_1,
                        MoveDirection = FourWayDirection.Up,
                    }.Start();
                    break;

                default:
                    new SlideInEffect()
                    {
                        Widget = Button_1,
                        MoveDirection = FourWayDirection.Up,
                    }.Start();
                    break;
            }
        }

    }
}
