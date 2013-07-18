// AUTOMATICALLY GENERATED CODE

using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;

namespace Crystallography.UI.Deprecated
{
    partial class CreditsPanel
    {
        Label BenText;
        Label PhoenixText;
        ImageBox ImageBox_1;
        ImageBox ImageBox_2;
        Label MargaretText;
        ImageBox ImageBox_3;

        private void InitializeWidget()
        {
            InitializeWidget(LayoutOrientation.Horizontal);
        }

        private void InitializeWidget(LayoutOrientation orientation)
        {
            BenText = new Label();
            BenText.Name = "BenText";
            PhoenixText = new Label();
            PhoenixText.Name = "PhoenixText";
            ImageBox_1 = new ImageBox();
            ImageBox_1.Name = "ImageBox_1";
            ImageBox_2 = new ImageBox();
            ImageBox_2.Name = "ImageBox_2";
            MargaretText = new Label();
            MargaretText.Name = "MargaretText";
            ImageBox_3 = new ImageBox();
            ImageBox_3.Name = "ImageBox_3";

            // BenText
            BenText.TextColor = new UIColor(229f / 255f, 19f / 255f, 19f / 255f, 255f / 255f);
            BenText.Font = new UIFont(FontAlias.System, 30, FontStyle.Regular);
            BenText.LineBreak = LineBreak.Character;

            // PhoenixText
            PhoenixText.TextColor = new UIColor(41f / 255f, 226f / 255f, 226f / 255f, 255f / 255f);
            PhoenixText.Font = new UIFont(FontAlias.System, 30, FontStyle.Regular);
            PhoenixText.LineBreak = LineBreak.Character;
            PhoenixText.VerticalAlignment = VerticalAlignment.Top;

            // ImageBox_1
            ImageBox_1.Image = new ImageAsset("/Application/assets/images/UI/icons/tie.png");
            ImageBox_1.ImageScaleType = ImageScaleType.Center;

            // ImageBox_2
            ImageBox_2.Image = new ImageAsset("/Application/assets/images/UI/icons/glasses.png");
            ImageBox_2.ImageScaleType = ImageScaleType.Center;

            // MargaretText
            MargaretText.TextColor = new UIColor(244f / 255f, 234f / 255f, 244f / 255f, 255f / 255f);
            MargaretText.Font = new UIFont(FontAlias.System, 30, FontStyle.Regular);
            MargaretText.LineBreak = LineBreak.Character;

            // ImageBox_3
            ImageBox_3.Image = new ImageAsset("/Application/assets/images/UI/icons/phones.png");
            ImageBox_3.ImageScaleType = ImageScaleType.Center;

            // CreditsPanel
            this.BackgroundColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            this.Clip = true;
            this.AddChildLast(BenText);
            this.AddChildLast(PhoenixText);
            this.AddChildLast(ImageBox_1);
            this.AddChildLast(ImageBox_2);
            this.AddChildLast(MargaretText);
            this.AddChildLast(ImageBox_3);

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

                    BenText.SetPosition(32, 49);
                    BenText.SetSize(214, 36);
                    BenText.Anchors = Anchors.None;
                    BenText.Visible = true;

                    PhoenixText.SetPosition(20, 188);
                    PhoenixText.SetSize(214, 36);
                    PhoenixText.Anchors = Anchors.None;
                    PhoenixText.Visible = true;

                    ImageBox_1.SetPosition(91, -51);
                    ImageBox_1.SetSize(200, 200);
                    ImageBox_1.Anchors = Anchors.None;
                    ImageBox_1.Visible = true;

                    ImageBox_2.SetPosition(197, 65);
                    ImageBox_2.SetSize(200, 200);
                    ImageBox_2.Anchors = Anchors.None;
                    ImageBox_2.Visible = true;

                    MargaretText.SetPosition(17, 242);
                    MargaretText.SetSize(214, 36);
                    MargaretText.Anchors = Anchors.None;
                    MargaretText.Visible = true;

                    ImageBox_3.SetPosition(211, 144);
                    ImageBox_3.SetSize(200, 200);
                    ImageBox_3.Anchors = Anchors.None;
                    ImageBox_3.Visible = true;

                    break;

                default:
                    this.SetSize(885, 344);
                    this.Anchors = Anchors.None;

                    BenText.SetPosition(113, 122);
                    BenText.SetSize(214, 36);
                    BenText.Anchors = Anchors.None;
                    BenText.Visible = true;

                    PhoenixText.SetPosition(385, 119);
                    PhoenixText.SetSize(392, 37);
                    PhoenixText.Anchors = Anchors.None;
                    PhoenixText.Visible = true;

                    ImageBox_1.SetPosition(297, 129);
                    ImageBox_1.SetSize(73, 31);
                    ImageBox_1.Anchors = Anchors.None;
                    ImageBox_1.Visible = true;

                    ImageBox_2.SetPosition(575, 98);
                    ImageBox_2.SetSize(197, 62);
                    ImageBox_2.Anchors = Anchors.None;
                    ImageBox_2.Visible = true;

                    MargaretText.SetPosition(272, 202);
                    MargaretText.SetSize(301, 42);
                    MargaretText.Anchors = Anchors.None;
                    MargaretText.Visible = true;

                    ImageBox_3.SetPosition(506, 172);
                    ImageBox_3.SetSize(106, 76);
                    ImageBox_3.Anchors = Anchors.None;
                    ImageBox_3.Visible = true;

                    break;
            }
            _currentLayoutOrientation = orientation;
        }

        public void UpdateLanguage()
        {
            BenText.Text = "ben johnson";

            PhoenixText.Text = "phoenix perry";

            MargaretText.Text = "margaret schedel";
        }

        public void InitializeDefaultEffect()
        {
            switch (_currentLayoutOrientation)
            {
                case LayoutOrientation.Vertical:
                    break;

                default:
                    break;
            }
        }

        public void StartDefaultEffect()
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
