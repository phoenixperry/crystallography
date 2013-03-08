// AUTOMATICALLY GENERATED CODE

using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;

namespace Preview
{
    partial class ScoreScene
    {
        Label ScoreLabelText;
        Label ScoreText;
        Panel Panel_1;

        private void InitializeWidget()
        {
            InitializeWidget(LayoutOrientation.Horizontal);
        }

        private void InitializeWidget(LayoutOrientation orientation)
        {
            ScoreLabelText = new Label();
            ScoreLabelText.Name = "ScoreLabelText";
            ScoreText = new Label();
            ScoreText.Name = "ScoreText";
            Panel_1 = new Panel();
            Panel_1.Name = "Panel_1";

            // ScoreLabelText
            ScoreLabelText.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            ScoreLabelText.Font = new UIFont(FontAlias.System, 25, FontStyle.Regular);
            ScoreLabelText.LineBreak = LineBreak.Character;

            // ScoreText
            ScoreText.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            ScoreText.Font = new UIFont(FontAlias.System, 18, FontStyle.Regular);
            ScoreText.LineBreak = LineBreak.Character;

            // Panel_1
            Panel_1.BackgroundColor = new UIColor(153f / 255f, 153f / 255f, 153f / 255f, 255f / 255f);
            Panel_1.Clip = true;
            Panel_1.AddChildLast(ScoreLabelText);
            Panel_1.AddChildLast(ScoreText);

            // ScoreScene
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

                    ScoreLabelText.SetPosition(0, -12);
                    ScoreLabelText.SetSize(214, 36);
                    ScoreLabelText.Anchors = Anchors.None;
                    ScoreLabelText.Visible = true;

                    ScoreText.SetPosition(0, -12);
                    ScoreText.SetSize(214, 36);
                    ScoreText.Anchors = Anchors.None;
                    ScoreText.Visible = true;

                    Panel_1.SetPosition(0, 444);
                    Panel_1.SetSize(100, 100);
                    Panel_1.Anchors = Anchors.None;
                    Panel_1.Visible = true;

                    break;

                default:
                    this.DesignWidth = 960;
                    this.DesignHeight = 544;

                    ScoreLabelText.SetPosition(0, 64);
                    ScoreLabelText.SetSize(74, 36);
                    ScoreLabelText.Anchors = Anchors.None;
                    ScoreLabelText.Visible = true;

                    ScoreText.SetPosition(74, 66);
                    ScoreText.SetSize(145, 36);
                    ScoreText.Anchors = Anchors.None;
                    ScoreText.Visible = true;

                    Panel_1.SetPosition(0, 444);
                    Panel_1.SetSize(960, 100);
                    Panel_1.Anchors = Anchors.None;
                    Panel_1.Visible = true;

                    break;
            }
            _currentLayoutOrientation = orientation;
        }

        public void UpdateLanguage()
        {
            ScoreLabelText.Text = "score:";

            ScoreText.Text = "0123456789";
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
