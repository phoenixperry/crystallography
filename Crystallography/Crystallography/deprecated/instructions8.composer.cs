// AUTOMATICALLY GENERATED CODE

using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;

namespace Crystallography.UI.Deprecated
{
    partial class instructions8
    {
        Panel sceneBackgroundPanel;
        Button playGame;
        ImageBox ImageBox_1;

        private void InitializeWidget()
        {
            InitializeWidget(LayoutOrientation.Horizontal);
        }

        private void InitializeWidget(LayoutOrientation orientation)
        {
            sceneBackgroundPanel = new Panel();
            sceneBackgroundPanel.Name = "sceneBackgroundPanel";
            playGame = new Button();
            playGame.Name = "playGame";
            ImageBox_1 = new ImageBox();
            ImageBox_1.Name = "ImageBox_1";

            // sceneBackgroundPanel
            sceneBackgroundPanel.BackgroundColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);

            // playGame
            playGame.IconImage = null;
            playGame.Style = ButtonStyle.Custom;
            playGame.CustomImage = new CustomButtonImageSettings()
            {
                BackgroundNormalImage = new ImageAsset("/Application/assets/images/UI/play.png"),
                BackgroundPressedImage = null,
                BackgroundDisabledImage = null,
                BackgroundNinePatchMargin = new NinePatchMargin(42, 27, 42, 27),
            };

            // ImageBox_1
            ImageBox_1.Image = new ImageAsset("/Application/assets/images/UI/instructions8.png");
            ImageBox_1.ImageScaleType = ImageScaleType.Center;

            // instructions8
            this.RootWidget.AddChildLast(sceneBackgroundPanel);
            this.RootWidget.AddChildLast(playGame);
            this.RootWidget.AddChildLast(ImageBox_1);
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

                    playGame.SetPosition(734, 488);
                    playGame.SetSize(214, 56);
                    playGame.Anchors = Anchors.None;
                    playGame.Visible = true;

                    ImageBox_1.SetPosition(480, 134);
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

                    playGame.SetPosition(351, 483);
                    playGame.SetSize(258, 63);
                    playGame.Anchors = Anchors.None;
                    playGame.Visible = true;

                    ImageBox_1.SetPosition(7, -234);
                    ImageBox_1.SetSize(945, 971);
                    ImageBox_1.Anchors = Anchors.None;
                    ImageBox_1.Visible = true;

                    break;
            }
            _currentLayoutOrientation = orientation;
        }

        public void UpdateLanguage()
        {
            this.Title = "Instructions8";
        }

        private void onShowing(object sender, EventArgs e)
        {
            switch (_currentLayoutOrientation)
            {
                case LayoutOrientation.Vertical:
                    playGame.Visible = false;
                    break;

                default:
                    playGame.Visible = false;
                    break;
            }
        }

        private void onShown(object sender, EventArgs e)
        {
            switch (_currentLayoutOrientation)
            {
                case LayoutOrientation.Vertical:
                    new SlideInEffect()
                    {
                        Widget = playGame,
                        MoveDirection = FourWayDirection.Up,
                    }.Start();
                    break;

                default:
                    new SlideInEffect()
                    {
                        Widget = playGame,
                        MoveDirection = FourWayDirection.Up,
                    }.Start();
                    break;
            }
        }

    }
}
