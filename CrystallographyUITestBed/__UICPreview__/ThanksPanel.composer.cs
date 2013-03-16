// AUTOMATICALLY GENERATED CODE

using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;

namespace Preview
{
    partial class ThanksPanel
    {
        Label ThanksText;
        Label ThanksNamesText;
        Label Label_1;

        private void InitializeWidget()
        {
            InitializeWidget(LayoutOrientation.Horizontal);
        }

        private void InitializeWidget(LayoutOrientation orientation)
        {
            ThanksText = new Label();
            ThanksText.Name = "ThanksText";
            ThanksNamesText = new Label();
            ThanksNamesText.Name = "ThanksNamesText";
            Label_1 = new Label();
            Label_1.Name = "Label_1";

            // ThanksText
            ThanksText.TextColor = new UIColor(229f / 255f, 19f / 255f, 19f / 255f, 255f / 255f);
            ThanksText.Font = new UIFont(FontAlias.System, 36, FontStyle.Regular);
            ThanksText.LineBreak = LineBreak.Character;
            ThanksText.VerticalAlignment = VerticalAlignment.Top;

            // ThanksNamesText
            ThanksNamesText.TextColor = new UIColor(244f / 255f, 234f / 255f, 244f / 255f, 255f / 255f);
            ThanksNamesText.Font = new UIFont(FontAlias.System, 25, FontStyle.Regular);
            ThanksNamesText.LineBreak = LineBreak.Character;
            ThanksNamesText.VerticalAlignment = VerticalAlignment.Top;

            // Label_1
            Label_1.TextColor = new UIColor(250f / 255f, 234f / 255f, 250f / 255f, 255f / 255f);
            Label_1.Font = new UIFont(FontAlias.System, 14, FontStyle.Regular);
            Label_1.LineBreak = LineBreak.Character;

            // ThanksPanel
            this.BackgroundColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            this.Clip = true;
            this.AddChildLast(ThanksText);
            this.AddChildLast(ThanksNamesText);
            this.AddChildLast(Label_1);

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

                    ThanksText.SetPosition(20, 188);
                    ThanksText.SetSize(214, 36);
                    ThanksText.Anchors = Anchors.None;
                    ThanksText.Visible = true;

                    ThanksNamesText.SetPosition(32, 129);
                    ThanksNamesText.SetSize(214, 36);
                    ThanksNamesText.Anchors = Anchors.None;
                    ThanksNamesText.Visible = true;

                    Label_1.SetPosition(32, 154);
                    Label_1.SetSize(214, 36);
                    Label_1.Anchors = Anchors.None;
                    Label_1.Visible = true;

                    break;

                default:
                    this.SetSize(885, 344);
                    this.Anchors = Anchors.None;

                    ThanksText.SetPosition(32, 49);
                    ThanksText.SetSize(389, 50);
                    ThanksText.Anchors = Anchors.None;
                    ThanksText.Visible = true;

                    ThanksNamesText.SetPosition(32, 99);
                    ThanksNamesText.SetSize(821, 40);
                    ThanksNamesText.Anchors = Anchors.None;
                    ThanksNamesText.Visible = true;

                    Label_1.SetPosition(32, 154);
                    Label_1.SetSize(801, 53);
                    Label_1.Anchors = Anchors.None;
                    Label_1.Visible = true;

                    break;
            }
            _currentLayoutOrientation = orientation;
        }

        public void UpdateLanguage()
        {
            ThanksText.Text = "thanks to";

            ThanksNamesText.Text = "Indiecade, Matt Parker, Nicolas Fortugno, Jim Wallace and the PSM team. ";

            Label_1.Text = "open source fonts by ismael gonzález gonzález & raúl garcía del pomar";
        }

        public void InitializeDefaultEffect()
        {
            switch (_currentLayoutOrientation)
            {
                case LayoutOrientation.Vertical:
                    ThanksNamesText.Visible = false;
                    Label_1.Visible = false;
                    break;

                default:
                    ThanksNamesText.Visible = false;
                    Label_1.Visible = false;
                    break;
            }
        }

        public void StartDefaultEffect()
        {
            switch (_currentLayoutOrientation)
            {
                case LayoutOrientation.Vertical:
                    new FadeInEffect()
                    {
                        Widget = ThanksNamesText,
                    }.Start();
                    new FadeInEffect()
                    {
                        Widget = Label_1,
                    }.Start();
                    break;

                default:
                    new FadeInEffect()
                    {
                        Widget = ThanksNamesText,
                    }.Start();
                    new FadeInEffect()
                    {
                        Widget = Label_1,
                    }.Start();
                    break;
            }
        }

    }
}
