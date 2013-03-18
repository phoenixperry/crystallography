// AUTOMATICALLY GENERATED CODE

using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;

namespace Crystallography.UI
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
            TitleImage.Image = new ImageAsset("/Application/assets/images/UI/header.png");
            TitleImage.ImageScaleType = ImageScaleType.Center;

            // TouchToStartText
            TouchToStartText.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
			TouchToStartText.Font = FontManager.Instance.Get("Bariol", 32); 			
         
            TouchToStartText.LineBreak = LineBreak.Character;
            TouchToStartText.HorizontalAlignment = HorizontalAlignment.Center;

            // TitleScene
            this.RootWidget.AddChildLast(TitleImage);
            this.RootWidget.AddChildLast(TouchToStartText);
            this.Showing += new EventHandler(onShowing);
            this.Shown += new EventHandler(onShown);

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

                    TouchToStartText.SetPosition(229, 455);
                    TouchToStartText.SetSize(370, 36);
                    TouchToStartText.Anchors = Anchors.None;
                    TouchToStartText.Visible = false;

                    break;
            }
            _currentLayoutOrientation = orientation;
        }

        public void UpdateLanguage()
        {
            TouchToStartText.Text = "touch to start";
        }

        private void onShowing(object sender, EventArgs e)
        {
            switch (_currentLayoutOrientation)
            {
                case LayoutOrientation.Vertical:
                    TitleImage.Visible = false;
                    break;

                default:
                    TitleImage.Visible = false;
                    break;
            }
        }

        private void onShown(object sender, EventArgs e)
        {
            switch (_currentLayoutOrientation)
            {
                case LayoutOrientation.Vertical:
                    new FadeInEffect()
                    {
                        Widget = TitleImage,
                    }.Start();
                    break;

                default:
                    new FadeInEffect()
                    {
                        Widget = TitleImage,
                    }.Start();
                    break;
            }
        }

    }
}
