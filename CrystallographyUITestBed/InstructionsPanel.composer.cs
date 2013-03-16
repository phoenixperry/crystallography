﻿// AUTOMATICALLY GENERATED CODE

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
        Button StartButton;

        private void InitializeWidget()
        {
            InitializeWidget(LayoutOrientation.Horizontal);
        }

        private void InitializeWidget(LayoutOrientation orientation)
        {
            StartButton = new Button();
            StartButton.Name = "StartButton";

            // StartButton
            StartButton.IconImage = new ImageAsset("/Application/assets/images/UI/play.png");
            StartButton.Style = ButtonStyle.Custom;
            StartButton.CustomImage = new CustomButtonImageSettings()
            {
                BackgroundNormalImage = new ImageAsset("/Application/assets/images/UI/play.png"),
                BackgroundPressedImage = new ImageAsset("/Application/assets/images/UI/playOVer.png"),
                BackgroundDisabledImage = null,
                BackgroundNinePatchMargin = new NinePatchMargin(42, 27, 42, 27),
            };

            // InstructionsPanel
            this.BackgroundColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            this.Clip = true;
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

                    StartButton.SetPosition(688, 346);
                    StartButton.SetSize(214, 56);
                    StartButton.Anchors = Anchors.None;
                    StartButton.Visible = true;

                    break;

                default:
                    this.SetSize(924, 425);
                    this.Anchors = Anchors.None;

                    StartButton.SetPosition(643, 343);
                    StartButton.SetSize(258, 63);
                    StartButton.Anchors = Anchors.None;
                    StartButton.Visible = true;

                    break;
            }
            _currentLayoutOrientation = orientation;
        }

        public void UpdateLanguage()
        {
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
