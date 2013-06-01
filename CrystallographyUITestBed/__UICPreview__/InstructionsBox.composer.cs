// AUTOMATICALLY GENERATED CODE

using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;

namespace Preview
{
    partial class InstructionsBox
    {
        Button Button_1;
        Label Label_1;
        Label Label_2;
        Panel Panel_2;

        private void InitializeWidget()
        {
            InitializeWidget(LayoutOrientation.Horizontal);
        }

        private void InitializeWidget(LayoutOrientation orientation)
        {
            Button_1 = new Button();
            Button_1.Name = "Button_1";
            Label_1 = new Label();
            Label_1.Name = "Label_1";
            Label_2 = new Label();
            Label_2.Name = "Label_2";
            Panel_2 = new Panel();
            Panel_2.Name = "Panel_2";

            // Button_1
            Button_1.IconImage = new ImageAsset("/Application/assets/new/newUI/gotIt.png");
            Button_1.Style = ButtonStyle.Custom;
            Button_1.CustomImage = new CustomButtonImageSettings()
            {
                BackgroundNormalImage = null,
                BackgroundPressedImage = null,
                BackgroundDisabledImage = null,
                BackgroundNinePatchMargin = new NinePatchMargin(42, 27, 42, 27),
            };

            // Label_1
            Label_1.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            Label_1.Font = new UIFont(FontAlias.System, 30, FontStyle.Bold);
            Label_1.LineBreak = LineBreak.Character;

            // Label_2
            Label_2.TextColor = new UIColor(41f / 255f, 226f / 255f, 226f / 255f, 255f / 255f);
            Label_2.Font = new UIFont(FontAlias.System, 24, FontStyle.Regular);
            Label_2.LineBreak = LineBreak.Character;

            // Panel_2
            Panel_2.BackgroundColor = new UIColor(30f / 255f, 30f / 255f, 30f / 255f, 255f / 255f);
            Panel_2.Clip = true;
            Panel_2.AddChildLast(Button_1);
            Panel_2.AddChildLast(Label_1);
            Panel_2.AddChildLast(Label_2);

            // InstructionsBox
            this.RootWidget.AddChildLast(Panel_2);

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

                    Button_1.SetPosition(234, 258);
                    Button_1.SetSize(214, 56);
                    Button_1.Anchors = Anchors.None;
                    Button_1.Visible = true;

                    Label_1.SetPosition(-23, 130);
                    Label_1.SetSize(214, 36);
                    Label_1.Anchors = Anchors.None;
                    Label_1.Visible = true;

                    Label_2.SetPosition(-26, 57);
                    Label_2.SetSize(214, 36);
                    Label_2.Anchors = Anchors.None;
                    Label_2.Visible = true;

                    Panel_2.SetPosition(1, 63);
                    Panel_2.SetSize(100, 100);
                    Panel_2.Anchors = Anchors.None;
                    Panel_2.Visible = true;

                    break;

                default:
                    this.DesignWidth = 960;
                    this.DesignHeight = 544;

                    Button_1.SetPosition(246, 186);
                    Button_1.SetSize(214, 56);
                    Button_1.Anchors = Anchors.None;
                    Button_1.Visible = true;

                    Label_1.SetPosition(13, 23);
                    Label_1.SetSize(214, 44);
                    Label_1.Anchors = Anchors.None;
                    Label_1.Visible = true;

                    Label_2.SetPosition(13, 60);
                    Label_2.SetSize(355, 36);
                    Label_2.Anchors = Anchors.None;
                    Label_2.Visible = true;

                    Panel_2.SetPosition(3, 73);
                    Panel_2.SetSize(445, 241);
                    Panel_2.Anchors = Anchors.None;
                    Panel_2.Visible = true;

                    break;
            }
            _currentLayoutOrientation = orientation;
        }

        public void UpdateLanguage()
        {
            Label_1.Text = "headline";

            Label_2.Text = "here's some instructions";
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
