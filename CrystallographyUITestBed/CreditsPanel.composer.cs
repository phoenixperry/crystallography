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
        Label Label_4;
        Label PhoenixText;
        ImageBox ImageBox_1;
        ImageBox ImageBox_2;
        Label Label_5;

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
            Label_4 = new Label();
            Label_4.Name = "Label_4";
            PhoenixText = new Label();
            PhoenixText.Name = "PhoenixText";
            ImageBox_1 = new ImageBox();
            ImageBox_1.Name = "ImageBox_1";
            ImageBox_2 = new ImageBox();
            ImageBox_2.Name = "ImageBox_2";
            Label_5 = new Label();
            Label_5.Name = "Label_5";

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

            // Label_4
            Label_4.TextColor = new UIColor(229f / 255f, 19f / 255f, 19f / 255f, 255f / 255f);
            Label_4.Font = new UIFont(FontAlias.System, 25, FontStyle.Regular);
            Label_4.LineBreak = LineBreak.Character;

            // PhoenixText
            PhoenixText.TextColor = new UIColor(41f / 255f, 226f / 255f, 226f / 255f, 255f / 255f);
            PhoenixText.Font = new UIFont(FontAlias.System, 26, FontStyle.Regular);
            PhoenixText.LineBreak = LineBreak.Character;
            PhoenixText.VerticalAlignment = VerticalAlignment.Top;

            // ImageBox_1
            ImageBox_1.Image = new ImageAsset("/Application/assets/images/UI/icons/tie.png");
            ImageBox_1.ImageScaleType = ImageScaleType.Center;

            // ImageBox_2
            ImageBox_2.Image = new ImageAsset("/Application/assets/images/UI/icons/glasses.png");
            ImageBox_2.ImageScaleType = ImageScaleType.Center;

            // Label_5
            Label_5.TextColor = new UIColor(244f / 255f, 234f / 255f, 244f / 255f, 255f / 255f);
            Label_5.Font = new UIFont(FontAlias.System, 25, FontStyle.Regular);
            Label_5.LineBreak = LineBreak.Character;

            // CreditsPanel
            this.BackgroundColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            this.Clip = true;
            this.AddChildLast(Label_1);
            this.AddChildLast(Label_2);
            this.AddChildLast(Label_3);
            this.AddChildLast(Label_4);
            this.AddChildLast(PhoenixText);
            this.AddChildLast(ImageBox_1);
            this.AddChildLast(ImageBox_2);
            this.AddChildLast(Label_5);

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

                    Label_4.SetPosition(32, 49);
                    Label_4.SetSize(214, 36);
                    Label_4.Anchors = Anchors.None;
                    Label_4.Visible = true;

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

                    Label_5.SetPosition(17, 242);
                    Label_5.SetSize(214, 36);
                    Label_5.Anchors = Anchors.None;
                    Label_5.Visible = true;

                    break;

                default:
                    this.SetSize(885, 344);
                    this.Anchors = Anchors.None;

                    Label_1.SetPosition(16, 67);
                    Label_1.SetSize(876, 36);
                    Label_1.Anchors = Anchors.None;
                    Label_1.Visible = true;

                    Label_2.SetPosition(16, 170);
                    Label_2.SetSize(658, 46);
                    Label_2.Anchors = Anchors.None;
                    Label_2.Visible = true;

                    Label_3.SetPosition(16, 269);
                    Label_3.SetSize(433, 36);
                    Label_3.Anchors = Anchors.None;
                    Label_3.Visible = true;

                    Label_4.SetPosition(16, 41);
                    Label_4.SetSize(214, 36);
                    Label_4.Anchors = Anchors.None;
                    Label_4.Visible = true;

                    PhoenixText.SetPosition(16, 146);
                    PhoenixText.SetSize(392, 37);
                    PhoenixText.Anchors = Anchors.None;
                    PhoenixText.Visible = true;

                    ImageBox_1.SetPosition(169, 41);
                    ImageBox_1.SetSize(73, 31);
                    ImageBox_1.Anchors = Anchors.None;
                    ImageBox_1.Visible = true;

                    ImageBox_2.SetPosition(186, 114);
                    ImageBox_2.SetSize(197, 62);
                    ImageBox_2.Anchors = Anchors.None;
                    ImageBox_2.Visible = true;

                    Label_5.SetPosition(16, 244);
                    Label_5.SetSize(301, 36);
                    Label_5.Anchors = Anchors.None;
                    Label_5.Visible = true;

                    break;
            }
            _currentLayoutOrientation = orientation;
        }

        public void UpdateLanguage()
        {
            Label_1.Text = "Game Designer, System Design, Level Design, Lead Engineer  ";

            Label_2.Text = "Game Designer, Creative Director, Developer, User Interface Design, User Testing";

            Label_3.Text = "Game Designer, Audio design, Level Design";

            Label_4.Text = "ben johnson";

            PhoenixText.Text = "phoenix perry";

            Label_5.Text = "margaret schedel";
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
