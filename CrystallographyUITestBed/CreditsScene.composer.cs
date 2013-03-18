// AUTOMATICALLY GENERATED CODE

using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;

namespace Crystallography.UI
{
    partial class CreditsScene
    {
        Panel sceneBackgroundPanel;
        PagePanel PagePanel_1;
        Label CreditsTitleText;
        Button BackButton;

        private void InitializeWidget()
        {
            InitializeWidget(LayoutOrientation.Horizontal);
        }

        private void InitializeWidget(LayoutOrientation orientation)
        {
            sceneBackgroundPanel = new Panel();
            sceneBackgroundPanel.Name = "sceneBackgroundPanel";
            PagePanel_1 = new PagePanel();
            PagePanel_1.Name = "PagePanel_1";
            CreditsTitleText = new Label();
            CreditsTitleText.Name = "CreditsTitleText";
            BackButton = new Button();
            BackButton.Name = "BackButton";

            // sceneBackgroundPanel
            sceneBackgroundPanel.BackgroundColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);

            // PagePanel_1
            PagePanel_1.AddPage(new CreditsPanel());
            PagePanel_1.AddPage(new ThanksPanel());

            // CreditsTitleText
            CreditsTitleText.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            CreditsTitleText.Font = new UIFont(FontAlias.System, 72, FontStyle.Regular);
            CreditsTitleText.LineBreak = LineBreak.Character;

            // BackButton
            BackButton.TextColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            BackButton.TextFont = new UIFont(FontAlias.System, 25, FontStyle.Regular);
            BackButton.Style = ButtonStyle.Custom;
            BackButton.CustomImage = new CustomButtonImageSettings()
            {
                BackgroundNormalImage = new ImageAsset("/Application/assets/images/UI/blueBtn.png"),
                BackgroundPressedImage = new ImageAsset("/Application/assets/images/UI/blueBtnOver.png"),
                BackgroundDisabledImage = null,
                BackgroundNinePatchMargin = new NinePatchMargin(42, 27, 42, 27),
            };

            // CreditsScene
            this.RootWidget.AddChildLast(sceneBackgroundPanel);
            this.RootWidget.AddChildLast(PagePanel_1);
            this.RootWidget.AddChildLast(CreditsTitleText);
            this.RootWidget.AddChildLast(BackButton);
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

                    sceneBackgroundPanel.SetPosition(0, 0);
                    sceneBackgroundPanel.SetSize(544, 960);
                    sceneBackgroundPanel.Anchors = Anchors.Top | Anchors.Bottom | Anchors.Left | Anchors.Right;
                    sceneBackgroundPanel.Visible = true;

                    PagePanel_1.SetPosition(36, 154);
                    PagePanel_1.SetSize(100, 50);
                    PagePanel_1.Anchors = Anchors.None;
                    PagePanel_1.Visible = true;

                    CreditsTitleText.SetPosition(20, 27);
                    CreditsTitleText.SetSize(214, 36);
                    CreditsTitleText.Anchors = Anchors.None;
                    CreditsTitleText.Visible = true;

                    BackButton.SetPosition(744, 488);
                    BackButton.SetSize(214, 56);
                    BackButton.Anchors = Anchors.None;
                    BackButton.Visible = true;

                    break;

                default:
                    this.DesignWidth = 960;
                    this.DesignHeight = 544;

                    sceneBackgroundPanel.SetPosition(0, 0);
                    sceneBackgroundPanel.SetSize(960, 544);
                    sceneBackgroundPanel.Anchors = Anchors.Top | Anchors.Bottom | Anchors.Left | Anchors.Right;
                    sceneBackgroundPanel.Visible = true;

                    PagePanel_1.SetPosition(44, 112);
                    PagePanel_1.SetSize(885, 344);
                    PagePanel_1.Anchors = Anchors.None;
                    PagePanel_1.Visible = true;

                    CreditsTitleText.SetPosition(36, 28);
                    CreditsTitleText.SetSize(327, 72);
                    CreditsTitleText.Anchors = Anchors.None;
                    CreditsTitleText.Visible = true;

                    BackButton.SetPosition(671, 473);
                    BackButton.SetSize(289, 71);
                    BackButton.Anchors = Anchors.None;
                    BackButton.Visible = true;

                    break;
            }
            _currentLayoutOrientation = orientation;
        }

        public void UpdateLanguage()
        {
            CreditsTitleText.Text = "credits";

            BackButton.Text = "back";

            this.Title = "BackButton";
        }

        private void onShowing(object sender, EventArgs e)
        {
            switch (_currentLayoutOrientation)
            {
                case LayoutOrientation.Vertical:
                    PagePanel_1.Visible = false;
                    break;

                default:
                    PagePanel_1.Visible = false;
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
                        Widget = PagePanel_1,
                    }.Start();
                    break;

                default:
                    new FadeInEffect()
                    {
                        Widget = PagePanel_1,
                    }.Start();
                    break;
            }
        }

    }
}
