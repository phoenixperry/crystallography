// AUTOMATICALLY GENERATED CODE

using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;

namespace Crystallography.UI
{
    partial class ThanksPanel
    {
        Label ThanksText;
        Label ThanksNamesText;

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

            // ThanksText
            ThanksText.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            ThanksText.Font = new UIFont(FontAlias.System, 36, FontStyle.Regular);
            ThanksText.LineBreak = LineBreak.Character;
            ThanksText.VerticalAlignment = VerticalAlignment.Top;

            // ThanksNamesText
            ThanksNamesText.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            ThanksNamesText.Font = new UIFont(FontAlias.System, 25, FontStyle.Regular);
            ThanksNamesText.LineBreak = LineBreak.Character;
            ThanksNamesText.VerticalAlignment = VerticalAlignment.Top;

            // ThanksPanel
            this.BackgroundColor = new UIColor(34f / 255f, 34f / 255f, 34f / 255f, 255f / 255f);
            this.Clip = true;
            this.AddChildLast(ThanksText);
            this.AddChildLast(ThanksNamesText);

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

                    break;

                default:
                    this.SetSize(885, 344);
                    this.Anchors = Anchors.None;

                    ThanksText.SetPosition(32, 49);
                    ThanksText.SetSize(389, 50);
                    ThanksText.Anchors = Anchors.None;
                    ThanksText.Visible = true;

                    ThanksNamesText.SetPosition(32, 129);
                    ThanksNamesText.SetSize(821, 184);
                    ThanksNamesText.Anchors = Anchors.None;
                    ThanksNamesText.Visible = true;

                    break;
            }
            _currentLayoutOrientation = orientation;
        }

        public void UpdateLanguage()
        {
            ThanksText.Text = "thanks to";

            ThanksNamesText.Text = "Indiecade, Matt Parker, Nicolas Fortuna, Jim Wallace and the PSM team";
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
