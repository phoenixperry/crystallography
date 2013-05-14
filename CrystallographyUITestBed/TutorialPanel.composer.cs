// AUTOMATICALLY GENERATED CODE

using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;

namespace Crystallography.UI
{
    partial class TutorialPanel
    {
        Label TutorialTitleText;
        Label TutorialBodyText;

        private void InitializeWidget()
        {
            InitializeWidget(LayoutOrientation.Horizontal);
        }

        private void InitializeWidget(LayoutOrientation orientation)
        {
            TutorialTitleText = new Label();
            TutorialTitleText.Name = "TutorialTitleText";
            TutorialBodyText = new Label();
            TutorialBodyText.Name = "TutorialBodyText";

            // TutorialTitleText
            TutorialTitleText.TextColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            TutorialTitleText.Font = new UIFont(FontAlias.System, 36, FontStyle.Regular);
            TutorialTitleText.LineBreak = LineBreak.Character;
            TutorialTitleText.HorizontalAlignment = HorizontalAlignment.Center;
            TutorialTitleText.VerticalAlignment = VerticalAlignment.Bottom;

            // TutorialBodyText
            TutorialBodyText.TextColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            TutorialBodyText.Font = new UIFont(FontAlias.System, 25, FontStyle.Regular);
            TutorialBodyText.LineBreak = LineBreak.Character;
            TutorialBodyText.VerticalAlignment = VerticalAlignment.Top;

            // TutorialPanel
            this.BackgroundColor = new UIColor(153f / 255f, 153f / 255f, 153f / 255f, 255f / 255f);
            this.Clip = true;
            this.AddChildLast(TutorialTitleText);
            this.AddChildLast(TutorialBodyText);

            SetWidgetLayout(orientation);

            UpdateLanguage();
        }

        private LayoutOrientation _currentLayoutOrientation;
        public void SetWidgetLayout(LayoutOrientation orientation)
        {
            switch (orientation)
            {
                case LayoutOrientation.Vertical:
                    this.SetSize(400, 760);
                    this.Anchors = Anchors.None;

                    TutorialTitleText.SetPosition(259, 53);
                    TutorialTitleText.SetSize(214, 36);
                    TutorialTitleText.Anchors = Anchors.None;
                    TutorialTitleText.Visible = true;

                    TutorialBodyText.SetPosition(21, 98);
                    TutorialBodyText.SetSize(214, 36);
                    TutorialBodyText.Anchors = Anchors.None;
                    TutorialBodyText.Visible = true;

                    break;

                default:
                    this.SetSize(760, 400);
                    this.Anchors = Anchors.None;

                    TutorialTitleText.SetPosition(0, 30);
                    TutorialTitleText.SetSize(760, 45);
                    TutorialTitleText.Anchors = Anchors.None;
                    TutorialTitleText.Visible = true;

                    TutorialBodyText.SetPosition(21, 98);
                    TutorialBodyText.SetSize(712, 276);
                    TutorialBodyText.Anchors = Anchors.None;
                    TutorialBodyText.Visible = true;

                    break;
            }
            _currentLayoutOrientation = orientation;
        }

        public void UpdateLanguage()
        {
            TutorialTitleText.Text = "TutorialTitleText";

            TutorialBodyText.Text = "Tutorial body text.";
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
