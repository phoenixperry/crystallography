// AUTOMATICALLY GENERATED CODE

using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;

namespace Preview
{
    partial class levelInfo
    {
        Label Label_1;
        Label Label_2;
        Panel Panel_1;

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
            Panel_1 = new Panel();
            Panel_1.Name = "Panel_1";

            // Label_1
            Label_1.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            Label_1.Font = new UIFont(FontAlias.System, 72, FontStyle.Regular);
            Label_1.LineBreak = LineBreak.Character;

            // Label_2
            Label_2.TextColor = new UIColor(41f / 255f, 226f / 255f, 226f / 255f, 255f / 255f);
            Label_2.Font = new UIFont(FontAlias.System, 72, FontStyle.Bold);
            Label_2.LineBreak = LineBreak.Character;

            // Panel_1
            Panel_1.BackgroundColor = new UIColor(30f / 255f, 30f / 255f, 30f / 255f, 255f / 255f);
            Panel_1.Clip = true;
            Panel_1.AddChildLast(Label_1);
            Panel_1.AddChildLast(Label_2);

            // levelInfo
            this.RootWidget.AddChildLast(Panel_1);

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

                    Label_1.SetPosition(0, 192);
                    Label_1.SetSize(214, 36);
                    Label_1.Anchors = Anchors.None;
                    Label_1.Visible = true;

                    Label_2.SetPosition(0, 298);
                    Label_2.SetSize(214, 36);
                    Label_2.Anchors = Anchors.None;
                    Label_2.Visible = true;

                    Panel_1.SetPosition(24, 82);
                    Panel_1.SetSize(100, 100);
                    Panel_1.Anchors = Anchors.None;
                    Panel_1.Visible = true;

                    break;

                default:
                    this.DesignWidth = 960;
                    this.DesignHeight = 544;

                    Label_1.SetPosition(37, 36);
                    Label_1.SetSize(156, 105);
                    Label_1.Anchors = Anchors.None;
                    Label_1.Visible = true;

                    Label_2.SetPosition(62, 124);
                    Label_2.SetSize(106, 73);
                    Label_2.Anchors = Anchors.None;
                    Label_2.Visible = true;

                    Panel_1.SetPosition(3, 93);
                    Panel_1.SetSize(230, 422);
                    Panel_1.Anchors = Anchors.None;
                    Panel_1.Visible = true;

                    break;
            }
            _currentLayoutOrientation = orientation;
        }

        public void UpdateLanguage()
        {
            Label_1.Text = "level";

            Label_2.Text = "25";
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
