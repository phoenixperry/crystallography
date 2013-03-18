// AUTOMATICALLY GENERATED CODE

using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;

namespace Preview
{
    partial class LevelEndPanel
    {
        ImageBox ImageBox_1;
        Button NextLevelButton;
        Button LevelSelectButton;
        Button QuitButton;
        Label YourScoreText;
        Label BestScoreText;
        Label YourTimeText;
        Label BestTimeText;

        private void InitializeWidget()
        {
            InitializeWidget(LayoutOrientation.Horizontal);
        }

        private void InitializeWidget(LayoutOrientation orientation)
        {
            ImageBox_1 = new ImageBox();
            ImageBox_1.Name = "ImageBox_1";
            NextLevelButton = new Button();
            NextLevelButton.Name = "NextLevelButton";
            LevelSelectButton = new Button();
            LevelSelectButton.Name = "LevelSelectButton";
            QuitButton = new Button();
            QuitButton.Name = "QuitButton";
            YourScoreText = new Label();
            YourScoreText.Name = "YourScoreText";
            BestScoreText = new Label();
            BestScoreText.Name = "BestScoreText";
            YourTimeText = new Label();
            YourTimeText.Name = "YourTimeText";
            BestTimeText = new Label();
            BestTimeText.Name = "BestTimeText";

            // ImageBox_1
            ImageBox_1.Image = new ImageAsset("/Application/assets/images/UI/levelComplete.png");
            ImageBox_1.ImageScaleType = ImageScaleType.Center;

            // NextLevelButton
            NextLevelButton.IconImage = null;
            NextLevelButton.Style = ButtonStyle.Custom;
            NextLevelButton.CustomImage = new CustomButtonImageSettings()
            {
                BackgroundNormalImage = new ImageAsset("/Application/assets/images/UI/nextLevel.png"),
                BackgroundPressedImage = new ImageAsset("/Application/assets/images/UI/nextLevelOver.png"),
                BackgroundDisabledImage = null,
                BackgroundNinePatchMargin = new NinePatchMargin(0, 0, 0, 0),
            };

            // LevelSelectButton
            LevelSelectButton.IconImage = null;
            LevelSelectButton.Style = ButtonStyle.Custom;
            LevelSelectButton.CustomImage = new CustomButtonImageSettings()
            {
                BackgroundNormalImage = new ImageAsset("/Application/assets/images/UI/levelSelectBtn.png"),
                BackgroundPressedImage = new ImageAsset("/Application/assets/images/UI/levelSelectBtnRoll.png"),
                BackgroundDisabledImage = null,
                BackgroundNinePatchMargin = new NinePatchMargin(42, 27, 42, 27),
            };

            // QuitButton
            QuitButton.IconImage = null;
            QuitButton.Style = ButtonStyle.Custom;
            QuitButton.CustomImage = new CustomButtonImageSettings()
            {
                BackgroundNormalImage = new ImageAsset("/Application/assets/images/UI/quit_game.png"),
                BackgroundPressedImage = new ImageAsset("/Application/assets/images/UI/quit_game_Roll.png"),
                BackgroundDisabledImage = null,
                BackgroundNinePatchMargin = new NinePatchMargin(0, 0, 0, 0),
            };

            // YourScoreText
            YourScoreText.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            YourScoreText.Font = new UIFont(FontAlias.System, 28, FontStyle.Regular);
            YourScoreText.LineBreak = LineBreak.Character;

            // BestScoreText
            BestScoreText.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            BestScoreText.Font = new UIFont(FontAlias.System, 28, FontStyle.Regular);
            BestScoreText.LineBreak = LineBreak.Character;

            // YourTimeText
            YourTimeText.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            YourTimeText.Font = new UIFont(FontAlias.System, 28, FontStyle.Regular);
            YourTimeText.LineBreak = LineBreak.Character;

            // BestTimeText
            BestTimeText.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            BestTimeText.Font = new UIFont(FontAlias.System, 25, FontStyle.Regular);
            BestTimeText.LineBreak = LineBreak.Character;

            // LevelEndPanel
            this.BackgroundColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            this.Clip = true;
            this.AddChildLast(ImageBox_1);
            this.AddChildLast(NextLevelButton);
            this.AddChildLast(LevelSelectButton);
            this.AddChildLast(QuitButton);
            this.AddChildLast(YourScoreText);
            this.AddChildLast(BestScoreText);
            this.AddChildLast(YourTimeText);
            this.AddChildLast(BestTimeText);

            SetWidgetLayout(orientation);

            UpdateLanguage();
        }

        private LayoutOrientation _currentLayoutOrientation;
        public void SetWidgetLayout(LayoutOrientation orientation)
        {
            switch (orientation)
            {
                case LayoutOrientation.Vertical:
                    this.SetSize(544, 960);
                    this.Anchors = Anchors.None;

                    ImageBox_1.SetPosition(0, 0);
                    ImageBox_1.SetSize(200, 200);
                    ImageBox_1.Anchors = Anchors.None;
                    ImageBox_1.Visible = true;

                    NextLevelButton.SetPosition(392, 180);
                    NextLevelButton.SetSize(214, 56);
                    NextLevelButton.Anchors = Anchors.None;
                    NextLevelButton.Visible = true;

                    LevelSelectButton.SetPosition(356, 376);
                    LevelSelectButton.SetSize(214, 56);
                    LevelSelectButton.Anchors = Anchors.None;
                    LevelSelectButton.Visible = true;

                    QuitButton.SetPosition(672, 332);
                    QuitButton.SetSize(214, 56);
                    QuitButton.Anchors = Anchors.None;
                    QuitButton.Visible = true;

                    YourScoreText.SetPosition(476, 66);
                    YourScoreText.SetSize(214, 36);
                    YourScoreText.Anchors = Anchors.None;
                    YourScoreText.Visible = true;

                    BestScoreText.SetPosition(656, 114);
                    BestScoreText.SetSize(214, 36);
                    BestScoreText.Anchors = Anchors.None;
                    BestScoreText.Visible = true;

                    YourTimeText.SetPosition(60, 340);
                    YourTimeText.SetSize(214, 36);
                    YourTimeText.Anchors = Anchors.None;
                    YourTimeText.Visible = true;

                    BestTimeText.SetPosition(40, 406);
                    BestTimeText.SetSize(214, 36);
                    BestTimeText.Anchors = Anchors.None;
                    BestTimeText.Visible = true;

                    break;

                default:
                    this.SetSize(960, 544);
                    this.Anchors = Anchors.None;

                    ImageBox_1.SetPosition(90, 8);
                    ImageBox_1.SetSize(780, 442);
                    ImageBox_1.Anchors = Anchors.None;
                    ImageBox_1.Visible = true;

                    NextLevelButton.SetPosition(491, 477);
                    NextLevelButton.SetSize(366, 68);
                    NextLevelButton.Anchors = Anchors.None;
                    NextLevelButton.Visible = true;

                    LevelSelectButton.SetPosition(260, 477);
                    LevelSelectButton.SetSize(225, 68);
                    LevelSelectButton.Anchors = Anchors.None;
                    LevelSelectButton.Visible = true;

                    QuitButton.SetPosition(107, 477);
                    QuitButton.SetSize(148, 68);
                    QuitButton.Anchors = Anchors.None;
                    QuitButton.Visible = true;

                    YourScoreText.SetPosition(276, 254);
                    YourScoreText.SetSize(115, 36);
                    YourScoreText.Anchors = Anchors.None;
                    YourScoreText.Visible = true;

                    BestScoreText.SetPosition(656, 254);
                    BestScoreText.SetSize(102, 35);
                    BestScoreText.Anchors = Anchors.None;
                    BestScoreText.Visible = true;

                    YourTimeText.SetPosition(276, 314);
                    YourTimeText.SetSize(159, 36);
                    YourTimeText.Anchors = Anchors.None;
                    YourTimeText.Visible = true;

                    BestTimeText.SetPosition(656, 314);
                    BestTimeText.SetSize(163, 36);
                    BestTimeText.Anchors = Anchors.None;
                    BestTimeText.Visible = true;

                    break;
            }
            _currentLayoutOrientation = orientation;
        }

        public void UpdateLanguage()
        {
            YourScoreText.Text = "00:00.0";

            BestScoreText.Text = "00:00.0";

            YourTimeText.Text = "1234567890";

            BestTimeText.Text = "12345678789";
        }

        public void InitializeDefaultEffect()
        {
            switch (_currentLayoutOrientation)
            {
                case LayoutOrientation.Vertical:
                    NextLevelButton.Visible = false;
                    break;

                default:
                    NextLevelButton.Visible = false;
                    break;
            }
        }

        public void StartDefaultEffect()
        {
            switch (_currentLayoutOrientation)
            {
                case LayoutOrientation.Vertical:
                    new SlideInEffect()
                    {
                        Widget = NextLevelButton,
                        MoveDirection = FourWayDirection.Up,
                    }.Start();
                    break;

                default:
                    new SlideInEffect()
                    {
                        Widget = NextLevelButton,
                        MoveDirection = FourWayDirection.Up,
                    }.Start();
                    break;
            }
        }

    }
}
