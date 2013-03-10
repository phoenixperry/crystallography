// AUTOMATICALLY GENERATED CODE

using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;

namespace Preview
{
    partial class CreditsPanel
    {
        Label BenText;
        Label PhoenixText;
        Label MargaretText;

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
            MargaretText = new Label();
            MargaretText.Name = "MargaretText";

            // BenText
            BenText.TextColor = new UIColor(229f / 255f, 19f / 255f, 19f / 255f, 255f / 255f);
            BenText.Font = new UIFont(FontAlias.System, 36, FontStyle.Regular);
            BenText.LineBreak = LineBreak.Character;
            BenText.VerticalAlignment = VerticalAlignment.Top;

            // PhoenixText
            PhoenixText.TextColor = new UIColor(41f / 255f, 226f / 255f, 226f / 255f, 255f / 255f);
            PhoenixText.Font = new UIFont(FontAlias.System, 36, FontStyle.Regular);
            PhoenixText.LineBreak = LineBreak.Character;
            PhoenixText.VerticalAlignment = VerticalAlignment.Top;

            // MargaretText
            MargaretText.TextColor = new UIColor(247f / 255f, 226f / 255f, 246f / 255f, 255f / 255f);
            MargaretText.Font = new UIFont(FontAlias.System, 36, FontStyle.Regular);
            MargaretText.LineBreak = LineBreak.Character;
            MargaretText.VerticalAlignment = VerticalAlignment.Top;

            // CreditsPanel
            this.BackgroundColor = new UIColor(34f / 255f, 34f / 255f, 34f / 255f, 255f / 255f);
            this.Clip = true;
            this.AddChildLast(BenText);
            this.AddChildLast(PhoenixText);
            this.AddChildLast(MargaretText);

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

                    BenText.SetPosition(20, 188);
                    BenText.SetSize(214, 36);
                    BenText.Anchors = Anchors.None;
                    BenText.Visible = true;

                    PhoenixText.SetPosition(20, 188);
                    PhoenixText.SetSize(214, 36);
                    PhoenixText.Anchors = Anchors.None;
                    PhoenixText.Visible = true;

                    MargaretText.SetPosition(20, 188);
                    MargaretText.SetSize(214, 36);
                    MargaretText.Anchors = Anchors.None;
                    MargaretText.Visible = true;

                    break;

                default:
                    this.SetSize(885, 344);
                    this.Anchors = Anchors.None;

                    BenText.SetPosition(32, 49);
                    BenText.SetSize(389, 50);
                    BenText.Anchors = Anchors.None;
                    BenText.Visible = true;

                    PhoenixText.SetPosition(32, 149);
                    PhoenixText.SetSize(389, 50);
                    PhoenixText.Anchors = Anchors.None;
                    PhoenixText.Visible = true;

                    MargaretText.SetPosition(32, 249);
                    MargaretText.SetSize(389, 50);
                    MargaretText.Anchors = Anchors.None;
                    MargaretText.Visible = true;

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
