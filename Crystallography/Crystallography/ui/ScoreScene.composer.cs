// AUTOMATICALLY GENERATED CODE

using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;

namespace Crystallography.UI
{
    partial class ScoreScene
    {
        ImageBox ImageBox_2;
        Label ScoreText;
        ImageBox ImageBox_1;
        Label TimerSeparatorText;
        Label TimerSecondsText;
        Label TimerMinutesText;
        Panel Panel_1;
        Label PauseMenuText;
        Button ResumeButton;
        Button GiveUpButton;
        Panel PauseMenu;
        Button NextLevelButton;

        private void InitializeWidget()
        {
            InitializeWidget(LayoutOrientation.Horizontal);
        }

        private void InitializeWidget(LayoutOrientation orientation)
        {
            ImageBox_2 = new ImageBox();
            ImageBox_2.Name = "ImageBox_2";
            ScoreText = new Label();
            ScoreText.Name = "ScoreText";
            ImageBox_1 = new ImageBox();
            ImageBox_1.Name = "ImageBox_1";
            TimerSeparatorText = new Label();
            TimerSeparatorText.Name = "TimerSeparatorText";
            TimerSecondsText = new Label();
            TimerSecondsText.Name = "TimerSecondsText";
            TimerMinutesText = new Label();
            TimerMinutesText.Name = "TimerMinutesText";
            Panel_1 = new Panel();
            Panel_1.Name = "Panel_1";
            PauseMenuText = new Label();
            PauseMenuText.Name = "PauseMenuText";
            ResumeButton = new Button();
            ResumeButton.Name = "ResumeButton";
            GiveUpButton = new Button();
            GiveUpButton.Name = "GiveUpButton";
            PauseMenu = new Panel();
            PauseMenu.Name = "PauseMenu";
            NextLevelButton = new Button();
            NextLevelButton.Name = "NextLevelButton";

            // ImageBox_2
            ImageBox_2.Image = new ImageAsset("/Application/assets/images/UI/score_now.png");
            ImageBox_2.ImageScaleType = ImageScaleType.Center;

            // ScoreText
            ScoreText.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            ScoreText.Font = new UIFont(FontAlias.System, 18, FontStyle.Bold);
            ScoreText.LineBreak = LineBreak.Character;

            // ImageBox_1
            ImageBox_1.Image = new ImageAsset("/Application/assets/images/UI/time_now.png");
            ImageBox_1.ImageScaleType = ImageScaleType.Center;

            // TimerSeparatorText
            TimerSeparatorText.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            TimerSeparatorText.Font = new UIFont(FontAlias.System, 18, FontStyle.Bold);
            TimerSeparatorText.TextTrimming = TextTrimming.None;
            TimerSeparatorText.LineBreak = LineBreak.Character;
            TimerSeparatorText.HorizontalAlignment = HorizontalAlignment.Center;

            // TimerSecondsText
            TimerSecondsText.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            TimerSecondsText.Font = new UIFont(FontAlias.System, 18, FontStyle.Bold);
            TimerSecondsText.TextTrimming = TextTrimming.None;
            TimerSecondsText.LineBreak = LineBreak.Character;

            // TimerMinutesText
            TimerMinutesText.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            TimerMinutesText.Font = new UIFont(FontAlias.System, 18, FontStyle.Bold);
            TimerMinutesText.LineBreak = LineBreak.Character;
            TimerMinutesText.HorizontalAlignment = HorizontalAlignment.Right;

            // Panel_1
            Panel_1.BackgroundColor = new UIColor(40f / 255f, 40f / 255f, 40f / 255f, 255f / 255f);
            Panel_1.Clip = true;
            Panel_1.AddChildLast(ImageBox_2);
            Panel_1.AddChildLast(ScoreText);
            Panel_1.AddChildLast(ImageBox_1);
            Panel_1.AddChildLast(TimerSeparatorText);
            Panel_1.AddChildLast(TimerSecondsText);
            Panel_1.AddChildLast(TimerMinutesText);

            // PauseMenuText
            PauseMenuText.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            PauseMenuText.Font = new UIFont(FontAlias.System, 44, FontStyle.Regular);
            PauseMenuText.LineBreak = LineBreak.Character;

            // ResumeButton
            ResumeButton.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            ResumeButton.TextFont = new UIFont(FontAlias.System, 25, FontStyle.Regular);
            ResumeButton.Style = ButtonStyle.Custom;
            ResumeButton.CustomImage = new CustomButtonImageSettings()
            {
                BackgroundNormalImage = new ImageAsset("/Application/assets/images/UI/blueBtn.png"),
                BackgroundPressedImage = new ImageAsset("/Application/assets/images/UI/blueBtnOver.png"),
                BackgroundDisabledImage = null,
                BackgroundNinePatchMargin = new NinePatchMargin(42, 27, 42, 27),
            };
            ResumeButton.BackgroundFilterColor = new UIColor(41f / 255f, 226f / 255f, 226f / 255f, 255f / 255f);

            // GiveUpButton
            GiveUpButton.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            GiveUpButton.TextFont = new UIFont(FontAlias.System, 25, FontStyle.Regular);
            GiveUpButton.Style = ButtonStyle.Custom;
            GiveUpButton.CustomImage = new CustomButtonImageSettings()
            {
                BackgroundNormalImage = new ImageAsset("/Application/assets/images/UI/redBtn.png"),
                BackgroundPressedImage = null,
                BackgroundDisabledImage = null,
                BackgroundNinePatchMargin = new NinePatchMargin(42, 27, 42, 27),
            };
            GiveUpButton.BackgroundFilterColor = new UIColor(229f / 255f, 19f / 255f, 19f / 255f, 255f / 255f);

            // PauseMenu
            PauseMenu.BackgroundColor = new UIColor(153f / 255f, 153f / 255f, 153f / 255f, 255f / 255f);
            PauseMenu.Clip = true;
            PauseMenu.AddChildLast(PauseMenuText);
            PauseMenu.AddChildLast(ResumeButton);
            PauseMenu.AddChildLast(GiveUpButton);

            // NextLevelButton
            NextLevelButton.IconImage = null;
            NextLevelButton.Style = ButtonStyle.Custom;
            NextLevelButton.CustomImage = new CustomButtonImageSettings()
            {
                BackgroundNormalImage = new ImageAsset("/Application/assets/images/UI/next_level_sm.png"),
                BackgroundPressedImage = new ImageAsset("/Application/assets/images/UI/next_level_smRoll.png"),
                BackgroundDisabledImage = null,
                BackgroundNinePatchMargin = new NinePatchMargin(0, 0, 0, 0),
            };

            // ScoreScene
            this.RootWidget.AddChildLast(Panel_1);
            this.RootWidget.AddChildLast(PauseMenu);
            this.RootWidget.AddChildLast(NextLevelButton);

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

                    ImageBox_2.SetPosition(-26, 416);
                    ImageBox_2.SetSize(200, 200);
                    ImageBox_2.Anchors = Anchors.None;
                    ImageBox_2.Visible = true;

                    ScoreText.SetPosition(0, -12);
                    ScoreText.SetSize(214, 36);
                    ScoreText.Anchors = Anchors.None;
                    ScoreText.Visible = true;

                    ImageBox_1.SetPosition(379, 16);
                    ImageBox_1.SetSize(200, 200);
                    ImageBox_1.Anchors = Anchors.None;
                    ImageBox_1.Visible = true;

                    TimerSeparatorText.SetPosition(382, 508);
                    TimerSeparatorText.SetSize(214, 36);
                    TimerSeparatorText.Anchors = Anchors.None;
                    TimerSeparatorText.Visible = true;

                    TimerSecondsText.SetPosition(502, 508);
                    TimerSecondsText.SetSize(214, 36);
                    TimerSecondsText.Anchors = Anchors.None;
                    TimerSecondsText.Visible = true;

                    TimerMinutesText.SetPosition(262, 508);
                    TimerMinutesText.SetSize(214, 36);
                    TimerMinutesText.Anchors = Anchors.None;
                    TimerMinutesText.Visible = true;

                    Panel_1.SetPosition(0, 444);
                    Panel_1.SetSize(100, 100);
                    Panel_1.Anchors = Anchors.None;
                    Panel_1.Visible = true;

                    PauseMenuText.SetPosition(112, 215);
                    PauseMenuText.SetSize(214, 36);
                    PauseMenuText.Anchors = Anchors.None;
                    PauseMenuText.Visible = true;

                    ResumeButton.SetPosition(364, 211);
                    ResumeButton.SetSize(214, 56);
                    ResumeButton.Anchors = Anchors.None;
                    ResumeButton.Visible = true;

                    GiveUpButton.SetPosition(268, 19);
                    GiveUpButton.SetSize(214, 56);
                    GiveUpButton.Anchors = Anchors.None;
                    GiveUpButton.Visible = true;

                    PauseMenu.SetPosition(0, 0);
                    PauseMenu.SetSize(100, 100);
                    PauseMenu.Anchors = Anchors.None;
                    PauseMenu.Visible = true;

                    NextLevelButton.SetPosition(746, 488);
                    NextLevelButton.SetSize(214, 56);
                    NextLevelButton.Anchors = Anchors.None;
                    NextLevelButton.Visible = true;

                    break;

                default:
                    this.DesignWidth = 960;
                    this.DesignHeight = 544;

                    ImageBox_2.SetPosition(0, -1);
                    ImageBox_2.SetSize(200, 36);
                    ImageBox_2.Anchors = Anchors.None;
                    ImageBox_2.Visible = true;

                    ScoreText.SetPosition(83, 1);
                    ScoreText.SetSize(145, 34);
                    ScoreText.Anchors = Anchors.None;
                    ScoreText.Visible = true;

                    ImageBox_1.SetPosition(214, 0);
                    ImageBox_1.SetSize(200, 36);
                    ImageBox_1.Anchors = Anchors.None;
                    ImageBox_1.Visible = true;

                    TimerSeparatorText.SetPosition(329, 1);
                    TimerSeparatorText.SetSize(12, 34);
                    TimerSeparatorText.Anchors = Anchors.None;
                    TimerSeparatorText.Visible = true;

                    TimerSecondsText.SetPosition(341, 1);
                    TimerSecondsText.SetSize(46, 34);
                    TimerSecondsText.Anchors = Anchors.None;
                    TimerSecondsText.Visible = true;

                    TimerMinutesText.SetPosition(283, 1);
                    TimerMinutesText.SetSize(46, 34);
                    TimerMinutesText.Anchors = Anchors.None;
                    TimerMinutesText.Visible = true;

                    Panel_1.SetPosition(0, 510);
                    Panel_1.SetSize(960, 34);
                    Panel_1.Anchors = Anchors.None;
                    Panel_1.Visible = true;

                    PauseMenuText.SetPosition(26, 14);
                    PauseMenuText.SetSize(242, 62);
                    PauseMenuText.Anchors = Anchors.None;
                    PauseMenuText.Visible = true;

                    ResumeButton.SetPosition(269, 13);
                    ResumeButton.SetSize(289, 71);
                    ResumeButton.Anchors = Anchors.None;
                    ResumeButton.Visible = true;

                    GiveUpButton.SetPosition(550, 13);
                    GiveUpButton.SetSize(289, 71);
                    GiveUpButton.Anchors = Anchors.None;
                    GiveUpButton.Visible = true;

                    PauseMenu.SetPosition(43, 227);
                    PauseMenu.SetSize(850, 100);
                    PauseMenu.Anchors = Anchors.None;
                    PauseMenu.Visible = false;

                    NextLevelButton.SetPosition(729, 501);
                    NextLevelButton.SetSize(231, 43);
                    NextLevelButton.Anchors = Anchors.Top;
                    NextLevelButton.Visible = true;

                    break;
            }
            _currentLayoutOrientation = orientation;
        }

        public void UpdateLanguage()
        {
            ScoreText.Text = "0123456789";

            TimerSeparatorText.Text = ":";

            TimerSecondsText.Text = "00.0";

            TimerMinutesText.Text = "000";

            PauseMenuText.Text = "pause";

            ResumeButton.Text = "resume";

            GiveUpButton.Text = "give up";
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
