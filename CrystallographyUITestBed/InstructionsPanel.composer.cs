// AUTOMATICALLY GENERATED CODE

using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;

namespace Crystallography.UI
{
    partial class InstructionsPanel
    {
        Label InstructionsText;
        Button StartButton;

        private void InitializeWidget()
        {
            InitializeWidget(LayoutOrientation.Horizontal);
        }

        private void InitializeWidget(LayoutOrientation orientation)
        {
            InstructionsText = new Label();
            InstructionsText.Name = "InstructionsText";
            StartButton = new Button();
            StartButton.Name = "StartButton";

            // InstructionsText
            InstructionsText.TextColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            InstructionsText.Font = new UIFont(FontAlias.System, 25, FontStyle.Regular);
            InstructionsText.LineBreak = LineBreak.Character;

            // StartButton
            StartButton.TextColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            StartButton.TextFont = new UIFont(FontAlias.System, 25, FontStyle.Regular);

            // InstructionsPanel
            this.BackgroundColor = new UIColor(153f / 255f, 153f / 255f, 153f / 255f, 255f / 255f);
            this.Clip = true;
            this.AddChildLast(InstructionsText);
            this.AddChildLast(StartButton);

            SetWidgetLayout(orientation);

            UpdateLanguage();
        }

        private LayoutOrientation _currentLayoutOrientation;
        public void SetWidgetLayout(LayoutOrientation orientation)
        {
            switch (orientation)
            {
                case LayoutOrientation.Vertical:
                    this.SetSize(425, 924);
                    this.Anchors = Anchors.None;

                    InstructionsText.SetPosition(19, 34);
                    InstructionsText.SetSize(214, 36);
                    InstructionsText.Anchors = Anchors.None;
                    InstructionsText.Visible = true;

                    StartButton.SetPosition(688, 346);
                    StartButton.SetSize(214, 56);
                    StartButton.Anchors = Anchors.None;
                    StartButton.Visible = true;

                    break;

                default:
                    this.SetSize(924, 425);
                    this.Anchors = Anchors.None;

                    InstructionsText.SetPosition(19, 34);
                    InstructionsText.SetSize(214, 36);
                    InstructionsText.Anchors = Anchors.None;
                    InstructionsText.Visible = true;

                    StartButton.SetPosition(688, 346);
                    StartButton.SetSize(214, 56);
                    StartButton.Anchors = Anchors.None;
                    StartButton.Visible = true;

                    break;
            }
            _currentLayoutOrientation = orientation;
        }

        public void UpdateLanguage()
        {
            InstructionsText.Text = "blah blah blah";

            StartButton.Text = "Start";
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
