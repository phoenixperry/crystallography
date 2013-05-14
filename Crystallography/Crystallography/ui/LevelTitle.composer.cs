// AUTOMATICALLY GENERATED CODE

using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;

namespace Crystallography.UI
{
    partial class LevelTitle
    {
        Label NextLevelText;
        Label LevelNumberText;

        private void InitializeWidget()
        {
            InitializeWidget(LayoutOrientation.Horizontal);
        }

        private void InitializeWidget(LayoutOrientation orientation)
        {
            NextLevelText = new Label();
            NextLevelText.Name = "NextLevelText";
            LevelNumberText = new Label();
            LevelNumberText.Name = "LevelNumberText";

            // NextLevelText
            NextLevelText.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            NextLevelText.Font = new UIFont(FontAlias.System, 25, FontStyle.Regular);
            NextLevelText.LineBreak = LineBreak.Character;
            NextLevelText.VerticalAlignment = VerticalAlignment.Top;

            // LevelNumberText
            LevelNumberText.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            LevelNumberText.Font = new UIFont(FontAlias.System, 72, FontStyle.Regular);
            LevelNumberText.LineBreak = LineBreak.Character;

            // LevelTitle
            this.BackgroundColor = new UIColor(153f / 255f, 153f / 255f, 153f / 255f, 0f / 255f);
            this.Clip = true;
            this.AddChildLast(NextLevelText);
            this.AddChildLast(LevelNumberText);

            SetWidgetLayout(orientation);

            UpdateLanguage();
        }

        private LayoutOrientation _currentLayoutOrientation;
        public void SetWidgetLayout(LayoutOrientation orientation)
        {
            switch (orientation)
            {
                case LayoutOrientation.Vertical:
                    this.SetSize(200, 960);
                    this.Anchors = Anchors.None;

                    NextLevelText.SetPosition(299, 61);
                    NextLevelText.SetSize(214, 36);
                    NextLevelText.Anchors = Anchors.None;
                    NextLevelText.Visible = true;

                    LevelNumberText.SetPosition(69, 74);
                    LevelNumberText.SetSize(214, 36);
                    LevelNumberText.Anchors = Anchors.None;
                    LevelNumberText.Visible = true;

                    break;

                default:
                    this.SetSize(960, 200);
                    this.Anchors = Anchors.None;

                    NextLevelText.SetPosition(50, 0);
                    NextLevelText.SetSize(121, 36);
                    NextLevelText.Anchors = Anchors.None;
                    NextLevelText.Visible = true;

                    LevelNumberText.SetPosition(50, 56);
                    LevelNumberText.SetSize(147, 83);
                    LevelNumberText.Anchors = Anchors.None;
                    LevelNumberText.Visible = true;

                    break;
            }
            _currentLayoutOrientation = orientation;
        }

        public void UpdateLanguage()
        {
            NextLevelText.Text = "next level";

            LevelNumberText.Text = "000";
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
