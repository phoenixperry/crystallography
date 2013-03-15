// AUTOMATICALLY GENERATED CODE

using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;

namespace Crystallography.UI
{
    partial class MenuScene
    {
        Panel sceneBackgroundPanel;
        Button NewGameButton;
        Button LevelSelectButton;
        Button CreditsButton;
        Button OptionsButton;
        ImageBox ImageBox_1;

        private void InitializeWidget()
        {
            InitializeWidget(LayoutOrientation.Horizontal);
        }

        private void InitializeWidget(LayoutOrientation orientation)
        {
            sceneBackgroundPanel = new Panel();
            sceneBackgroundPanel.Name = "sceneBackgroundPanel";
            NewGameButton = new Button();
            NewGameButton.Name = "NewGameButton";
            LevelSelectButton = new Button();
            LevelSelectButton.Name = "LevelSelectButton";
            CreditsButton = new Button();
            CreditsButton.Name = "CreditsButton";
            OptionsButton = new Button();
            OptionsButton.Name = "OptionsButton";
            ImageBox_1 = new ImageBox();
            ImageBox_1.Name = "ImageBox_1";

            // sceneBackgroundPanel
            sceneBackgroundPanel.BackgroundColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);

            // NewGameButton
            NewGameButton.TextColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            NewGameButton.TextFont = new UIFont(FontAlias.System, 25, FontStyle.Regular);

            // LevelSelectButton
            LevelSelectButton.TextColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            LevelSelectButton.TextFont = new UIFont(FontAlias.System, 25, FontStyle.Regular);

            // CreditsButton
            CreditsButton.TextColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            CreditsButton.TextFont = new UIFont(FontAlias.System, 25, FontStyle.Regular);

            // OptionsButton
            OptionsButton.TextColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            OptionsButton.TextFont = new UIFont(FontAlias.System, 25, FontStyle.Regular);

            // ImageBox_1
            ImageBox_1.Image = null;
            ImageBox_1.ImageScaleType = ImageScaleType.Center;

            // MenuScene
            this.RootWidget.AddChildLast(sceneBackgroundPanel);
            this.RootWidget.AddChildLast(NewGameButton);
            this.RootWidget.AddChildLast(LevelSelectButton);
            this.RootWidget.AddChildLast(CreditsButton);
            this.RootWidget.AddChildLast(OptionsButton);
            this.RootWidget.AddChildLast(ImageBox_1);

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

                    NewGameButton.SetPosition(329, 168);
                    NewGameButton.SetSize(214, 56);
                    NewGameButton.Anchors = Anchors.None;
                    NewGameButton.Visible = true;

                    LevelSelectButton.SetPosition(373, 270);
                    LevelSelectButton.SetSize(214, 56);
                    LevelSelectButton.Anchors = Anchors.None;
                    LevelSelectButton.Visible = true;

                    CreditsButton.SetPosition(373, 384);
                    CreditsButton.SetSize(214, 56);
                    CreditsButton.Anchors = Anchors.None;
                    CreditsButton.Visible = true;

                    OptionsButton.SetPosition(373, 435);
                    OptionsButton.SetSize(214, 56);
                    OptionsButton.Anchors = Anchors.None;
                    OptionsButton.Visible = true;

                    ImageBox_1.SetPosition(146, 158);
                    ImageBox_1.SetSize(200, 200);
                    ImageBox_1.Anchors = Anchors.None;
                    ImageBox_1.Visible = true;

                    break;

                default:
                    this.DesignWidth = 960;
                    this.DesignHeight = 544;

                    sceneBackgroundPanel.SetPosition(0, 0);
                    sceneBackgroundPanel.SetSize(960, 544);
                    sceneBackgroundPanel.Anchors = Anchors.Top | Anchors.Bottom | Anchors.Left | Anchors.Right;
                    sceneBackgroundPanel.Visible = true;

                    NewGameButton.SetPosition(373, 92);
                    NewGameButton.SetSize(214, 56);
                    NewGameButton.Anchors = Anchors.None;
                    NewGameButton.Visible = true;

                    LevelSelectButton.SetPosition(373, 194);
                    LevelSelectButton.SetSize(214, 56);
                    LevelSelectButton.Anchors = Anchors.None;
                    LevelSelectButton.Visible = true;

                    CreditsButton.SetPosition(373, 398);
                    CreditsButton.SetSize(214, 56);
                    CreditsButton.Anchors = Anchors.None;
                    CreditsButton.Visible = true;

                    OptionsButton.SetPosition(373, 294);
                    OptionsButton.SetSize(214, 56);
                    OptionsButton.Anchors = Anchors.None;
                    OptionsButton.Visible = true;

                    ImageBox_1.SetPosition(350, 27);
                    ImageBox_1.SetSize(259, 473);
                    ImageBox_1.Anchors = Anchors.Height;
                    ImageBox_1.Visible = true;

                    break;
            }
            _currentLayoutOrientation = orientation;
        }

        public void UpdateLanguage()
        {
            NewGameButton.Text = "New Game";

            LevelSelectButton.Text = "Level Select";

            CreditsButton.Text = "Credits";

            OptionsButton.Text = "Options";

            this.Title = "MenuScene";
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
