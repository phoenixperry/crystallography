// AUTOMATICALLY GENERATED CODE

using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;

namespace Preview
{
    partial class TitleScene
    {
        ImageBox TitleImage;
        Label TouchToStartText;

        private void InitializeWidget()
        {
            InitializeWidget(LayoutOrientation.Horizontal);
        }

        private void InitializeWidget(LayoutOrientation orientation)
        {
            TitleImage = new ImageBox();
            TitleImage.Name = "TitleImage";
            TouchToStartText = new Label();
            TouchToStartText.Name = "TouchToStartText";

            // TitleImage
            TitleImage.Image = new ImageAsset("/Application/assets/header.png");
            TitleImage.ImageScaleType = ImageScaleType.Center;

            // TouchToStartText
            TouchToStartText.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            TouchToStartText.Font = new UIFont(FontAlias.System, 25, FontStyle.Regular);
            TouchToStartText.LineBreak = LineBreak.Character;
            TouchToStartText.HorizontalAlignment = HorizontalAlignment.Center;

            // TitleScene
            this.RootWidget.AddChildLast(TitleImage);
            this.RootWidget.AddChildLast(TouchToStartText);

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

                    TitleImage.SetPosition(0, 0);
                    TitleImage.SetSize(200, 200);
                    TitleImage.Anchors = Anchors.None;
                    TitleImage.Visible = true;

                    TouchToStartText.SetPosition(0, 471);
                    TouchToStartText.SetSize(214, 36);
                    TouchToStartText.Anchors = Anchors.None;
                    TouchToStartText.Visible = true;

                    break;

                default:
                    this.DesignWidth = 960;
                    this.DesignHeight = 544;

                    TitleImage.SetPosition(0, 0);
                    TitleImage.SetSize(960, 544);
                    TitleImage.Anchors = Anchors.None;
                    TitleImage.Visible = true;

                    TouchToStartText.SetPosition(56, 488);
                    TouchToStartText.SetSize(214, 36);
                    TouchToStartText.Anchors = Anchors.None;
                    TouchToStartText.Visible = false;

                    break;
            }
            _currentLayoutOrientation = orientation;
        }

        public void UpdateLanguage()
        {
            TouchToStartText.Text = "(Touch To Start)";
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
