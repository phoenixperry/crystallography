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
        ImageBox ImageBox_1;
        Button NewGameButton;
        Button LevelSelectButton;
        Button CreditsButton;
        Button OptionsButton;
        Button InfiniteModeButton;
        Button TimedModeButton;

        private void InitializeWidget()
        {
            InitializeWidget(LayoutOrientation.Horizontal);
        }

        private void InitializeWidget(LayoutOrientation orientation)
        {
            sceneBackgroundPanel = new Panel();
            sceneBackgroundPanel.Name = "sceneBackgroundPanel";
            ImageBox_1 = new ImageBox();
            ImageBox_1.Name = "ImageBox_1";
            NewGameButton = new Button();
            NewGameButton.Name = "NewGameButton";
            LevelSelectButton = new Button();
            LevelSelectButton.Name = "LevelSelectButton";
            CreditsButton = new Button();
            CreditsButton.Name = "CreditsButton";
            OptionsButton = new Button();
            OptionsButton.Name = "OptionsButton";
            InfiniteModeButton = new Button();
            InfiniteModeButton.Name = "InfiniteModeButton";
            TimedModeButton = new Button();
            TimedModeButton.Name = "TimedModeButton";

            // sceneBackgroundPanel
            sceneBackgroundPanel.BackgroundColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);

            // ImageBox_1
            ImageBox_1.Image = new ImageAsset("/Application/assets/images/UI/menuButtonBackground.png");
            ImageBox_1.ImageScaleType = ImageScaleType.AspectInside;

            // NewGameButton
            NewGameButton.IconImage = null;
            NewGameButton.Style = ButtonStyle.Custom;
            NewGameButton.CustomImage = new CustomButtonImageSettings()
            {
                BackgroundNormalImage = new ImageAsset("/Application/assets/images/UI/play.png"),
                BackgroundPressedImage = new ImageAsset("/Application/assets/images/UI/playOVer.png"),
                BackgroundDisabledImage = null,
                BackgroundNinePatchMargin = new NinePatchMargin(42, 27, 42, 27),
            };

            // LevelSelectButton
            LevelSelectButton.IconImage = null;
            LevelSelectButton.Style = ButtonStyle.Custom;
            LevelSelectButton.CustomImage = new CustomButtonImageSettings()
            {
                BackgroundNormalImage = new ImageAsset("/Application/assets/images/UI/selectlevel.png"),
                BackgroundPressedImage = new ImageAsset("/Application/assets/images/UI/selectLevelOver.png"),
                BackgroundDisabledImage = null,
                BackgroundNinePatchMargin = new NinePatchMargin(42, 27, 42, 27),
            };

            // CreditsButton
            CreditsButton.IconImage = null;
            CreditsButton.Style = ButtonStyle.Custom;
            CreditsButton.CustomImage = new CustomButtonImageSettings()
            {
                BackgroundNormalImage = new ImageAsset("/Application/assets/images/UI/credits_btn.png"),
                BackgroundPressedImage = new ImageAsset("/Application/assets/images/UI/quitOver.png"),
                BackgroundDisabledImage = null,
                BackgroundNinePatchMargin = new NinePatchMargin(0, 0, 0, 0),
            };

            // OptionsButton
            OptionsButton.IconImage = null;
            OptionsButton.Style = ButtonStyle.Custom;
            OptionsButton.CustomImage = new CustomButtonImageSettings()
            {
                BackgroundNormalImage = new ImageAsset("/Application/assets/images/UI/instructions.png"),
                BackgroundPressedImage = new ImageAsset("/Application/assets/images/UI/instructionsOver.png"),
                BackgroundDisabledImage = null,
                BackgroundNinePatchMargin = new NinePatchMargin(42, 27, 42, 27),
            };

            // InfiniteModeButton
            InfiniteModeButton.TextColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            InfiniteModeButton.TextFont = new UIFont(FontAlias.System, 25, FontStyle.Regular);

            // TimedModeButton
            TimedModeButton.TextColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            TimedModeButton.TextFont = new UIFont(FontAlias.System, 25, FontStyle.Regular);

            // MenuScene
            this.RootWidget.AddChildLast(sceneBackgroundPanel);
            this.RootWidget.AddChildLast(ImageBox_1);
            this.RootWidget.AddChildLast(NewGameButton);
            this.RootWidget.AddChildLast(LevelSelectButton);
            this.RootWidget.AddChildLast(CreditsButton);
            this.RootWidget.AddChildLast(OptionsButton);
            this.RootWidget.AddChildLast(InfiniteModeButton);
            this.RootWidget.AddChildLast(TimedModeButton);

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

                    ImageBox_1.SetPosition(400, 71);
                    ImageBox_1.SetSize(200, 200);
                    ImageBox_1.Anchors = Anchors.None;
                    ImageBox_1.Visible = true;

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

                    InfiniteModeButton.SetPosition(674, 109);
                    InfiniteModeButton.SetSize(214, 56);
                    InfiniteModeButton.Anchors = Anchors.None;
                    InfiniteModeButton.Visible = true;

                    TimedModeButton.SetPosition(63, 109);
                    TimedModeButton.SetSize(214, 56);
                    TimedModeButton.Anchors = Anchors.None;
                    TimedModeButton.Visible = true;

                    break;

                default:
                    this.DesignWidth = 960;
                    this.DesignHeight = 544;

                    sceneBackgroundPanel.SetPosition(0, 0);
                    sceneBackgroundPanel.SetSize(960, 544);
                    sceneBackgroundPanel.Anchors = Anchors.Top | Anchors.Bottom | Anchors.Left | Anchors.Right;
                    sceneBackgroundPanel.Visible = true;

                    ImageBox_1.SetPosition(351, 34);
                    ImageBox_1.SetSize(258, 478);
                    ImageBox_1.Anchors = Anchors.None;
                    ImageBox_1.Visible = true;

                    NewGameButton.SetPosition(351, 109);
                    NewGameButton.SetSize(258, 63);
                    NewGameButton.Anchors = Anchors.None;
                    NewGameButton.Visible = true;

                    LevelSelectButton.SetPosition(351, 197);
                    LevelSelectButton.SetSize(258, 63);
                    LevelSelectButton.Anchors = Anchors.None;
                    LevelSelectButton.Visible = true;

                    CreditsButton.SetPosition(351, 369);
                    CreditsButton.SetSize(258, 63);
                    CreditsButton.Anchors = Anchors.None;
                    CreditsButton.Visible = true;

                    OptionsButton.SetPosition(351, 283);
                    OptionsButton.SetSize(258, 63);
                    OptionsButton.Anchors = Anchors.None;
                    OptionsButton.Visible = true;

                    InfiniteModeButton.SetPosition(674, 109);
                    InfiniteModeButton.SetSize(214, 56);
                    InfiniteModeButton.Anchors = Anchors.None;
                    InfiniteModeButton.Visible = true;

                    TimedModeButton.SetPosition(63, 109);
                    TimedModeButton.SetSize(214, 56);
                    TimedModeButton.Anchors = Anchors.None;
                    TimedModeButton.Visible = true;

                    break;
            }
            _currentLayoutOrientation = orientation;
        }

        public void UpdateLanguage()
        {
            InfiniteModeButton.Text = "Infinite Mode";

            TimedModeButton.Text = "Timed Mode";

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
