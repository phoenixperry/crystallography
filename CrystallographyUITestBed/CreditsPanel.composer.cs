// AUTOMATICALLY GENERATED CODE

using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;

namespace Crystallography.UI
{
    partial class CreditsPanel
    {
        Label Label_1;
        Label Label_2;
        Label Label_3;
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
            Label_1 = new Label();
            Label_1.Name = "Label_1";
            Label_2 = new Label();
            Label_2.Name = "Label_2";
            Label_3 = new Label();
            Label_3.Name = "Label_3";
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

            // Label_1
            Label_1.TextColor = new UIColor(229f / 255f, 19f / 255f, 19f / 255f, 255f / 255f);
            Label_1.Font = new UIFont(FontAlias.System, 16, FontStyle.Regular);
            Label_1.LineBreak = LineBreak.Character;

            // Label_2
            Label_2.TextColor = new UIColor(41f / 255f, 226f / 255f, 226f / 255f, 255f / 255f);
            Label_2.Font = new UIFont(FontAlias.System, 16, FontStyle.Regular);
            Label_2.LineBreak = LineBreak.Character;

            // Label_3
            Label_3.TextColor = new UIColor(244f / 255f, 234f / 255f, 244f / 255f, 255f / 255f);
            Label_3.Font = new UIFont(FontAlias.System, 16, FontStyle.Regular);
            Label_3.LineBreak = LineBreak.Character;

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
            this.AddChildLast(Label_1);
            this.AddChildLast(Label_2);
            this.AddChildLast(Label_3);
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

                    Label_1.SetPosition(32, 99);
                    Label_1.SetSize(214, 36);
                    Label_1.Anchors = Anchors.None;
                    Label_1.Visible = true;

                    Label_2.SetPosition(32, 213);
                    Label_2.SetSize(214, 36);
                    Label_2.Anchors = Anchors.None;
                    Label_2.Visible = true;

                    Label_3.SetPosition(44, 228);
                    Label_3.SetSize(214, 36);
                    Label_3.Anchors = Anchors.None;
                    Label_3.Visible = true;

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

                    Label_1.SetPosition(16, 68);
                    Label_1.SetSize(876, 36);
                    Label_1.Anchors = Anchors.None;
                    Label_1.Visible = true;

                    Label_2.SetPosition(16, 170);
                    Label_2.SetSize(658, 46);
                    Label_2.Anchors = Anchors.None;
                    Label_2.Visible = true;

                    Label_3.SetPosition(16, 276);
                    Label_3.SetSize(433, 36);
                    Label_3.Anchors = Anchors.None;
                    Label_3.Visible = true;

                    BenText.SetPosition(16, 34);
                    BenText.SetSize(214, 36);
                    BenText.Anchors = Anchors.None;
                    BenText.Visible = true;

                    PhoenixText.SetPosition(16, 142);
                    PhoenixText.SetSize(392, 37);
                    PhoenixText.Anchors = Anchors.None;
                    PhoenixText.Visible = true;

                    ImageBox_1.SetPosition(200, 41);
                    ImageBox_1.SetSize(73, 31);
                    ImageBox_1.Anchors = Anchors.None;
                    ImageBox_1.Visible = true;

                    ImageBox_2.SetPosition(211, 117);
                    ImageBox_2.SetSize(197, 62);
                    ImageBox_2.Anchors = Anchors.None;
                    ImageBox_2.Visible = true;

                    MargaretText.SetPosition(16, 239);
                    MargaretText.SetSize(301, 36);
                    MargaretText.Anchors = Anchors.None;
                    MargaretText.Visible = true;

                    ImageBox_3.SetPosition(247, 206);
                    ImageBox_3.SetSize(106, 76);
                    ImageBox_3.Anchors = Anchors.None;
                    ImageBox_3.Visible = true;

                    break;
            }
            _currentLayoutOrientation = orientation;
        }

        public void UpdateLanguage()
        {
            Label_1.Text = "Game Designer, System Design, Lead Engineer  ";

            Label_2.Text = "Game Designer, Creative Director, Developer, User Testing";

            Label_3.Text = "Game Designer, Audio design, Level Design";

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
