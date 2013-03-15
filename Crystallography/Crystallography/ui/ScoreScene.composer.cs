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
        Label ScoreLabelText;
        Label ScoreText;
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
            ScoreLabelText = new Label();
            ScoreLabelText.Name = "ScoreLabelText";
            ScoreText = new Label();
            ScoreText.Name = "ScoreText";
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

            // ScoreLabelText
            ScoreLabelText.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            ScoreLabelText.Font = new UIFont(FontAlias.System, 25, FontStyle.Regular);
            ScoreLabelText.LineBreak = LineBreak.Character;

            // ScoreText
            ScoreText.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            ScoreText.Font = new UIFont(FontAlias.System, 18, FontStyle.Regular);
            ScoreText.LineBreak = LineBreak.Character;

            // TimerSeparatorText
            TimerSeparatorText.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            TimerSeparatorText.Font = new UIFont(FontAlias.System, 25, FontStyle.Regular);
            TimerSeparatorText.TextTrimming = TextTrimming.None;
            TimerSeparatorText.LineBreak = LineBreak.Character;
            TimerSeparatorText.HorizontalAlignment = HorizontalAlignment.Center;

            // TimerSecondsText
            TimerSecondsText.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            TimerSecondsText.Font = new UIFont(FontAlias.System, 25, FontStyle.Regular);
            TimerSecondsText.TextTrimming = TextTrimming.None;
            TimerSecondsText.LineBreak = LineBreak.Character;

            // TimerMinutesText
            TimerMinutesText.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            TimerMinutesText.Font = new UIFont(FontAlias.System, 25, FontStyle.Regular);
            TimerMinutesText.LineBreak = LineBreak.Character;
            TimerMinutesText.HorizontalAlignment = HorizontalAlignment.Right;

            // Panel_1
            Panel_1.BackgroundColor = new UIColor(153f / 255f, 153f / 255f, 153f / 255f, 255f / 255f);
            Panel_1.Clip = true;
            Panel_1.AddChildLast(ScoreLabelText);
            Panel_1.AddChildLast(ScoreText);
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
            ResumeButton.BackgroundFilterColor = new UIColor(41f / 255f, 226f / 255f, 226f / 255f, 255f / 255f);

            // GiveUpButton
            GiveUpButton.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            GiveUpButton.TextFont = new UIFont(FontAlias.System, 25, FontStyle.Regular);
            GiveUpButton.BackgroundFilterColor = new UIColor(229f / 255f, 19f / 255f, 19f / 255f, 255f / 255f);

            // PauseMenu
            PauseMenu.BackgroundColor = new UIColor(153f / 255f, 153f / 255f, 153f / 255f, 255f / 255f);
            PauseMenu.Clip = true;
            PauseMenu.AddChildLast(PauseMenuText);
            PauseMenu.AddChildLast(ResumeButton);
            PauseMenu.AddChildLast(GiveUpButton);

            // NextLevelButton
            NextLevelButton.TextColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            NextLevelButton.TextFont = new UIFont(FontAlias.System, 25, FontStyle.Regular);

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

                    ScoreLabelText.SetPosition(0, -12);
                    ScoreLabelText.SetSize(214, 36);
                    ScoreLabelText.Anchors = Anchors.None;
                    ScoreLabelText.Visible = true;

                    ScoreText.SetPosition(0, -12);
                    ScoreText.SetSize(214, 36);
                    ScoreText.Anchors = Anchors.None;
                    ScoreText.Visible = true;

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

                    ScoreLabelText.SetPosition(0, -2);
                    ScoreLabelText.SetSize(74, 36);
                    ScoreLabelText.Anchors = Anchors.None;
                    ScoreLabelText.Visible = true;

                    ScoreText.SetPosition(74, 0);
                    ScoreText.SetSize(145, 36);
                    ScoreText.Anchors = Anchors.None;
                    ScoreText.Visible = true;

                    TimerSeparatorText.SetPosition(476, -2);
                    TimerSeparatorText.SetSize(26, 36);
                    TimerSeparatorText.Anchors = Anchors.None;
                    TimerSeparatorText.Visible = true;

                    TimerSecondsText.SetPosition(490, -1);
                    TimerSecondsText.SetSize(65, 36);
                    TimerSecondsText.Anchors = Anchors.None;
                    TimerSecondsText.Visible = true;

                    TimerMinutesText.SetPosition(352, -1);
                    TimerMinutesText.SetSize(135, 36);
                    TimerMinutesText.Anchors = Anchors.None;
                    TimerMinutesText.Visible = true;

                    Panel_1.SetPosition(0, 510);
                    Panel_1.SetSize(960, 34);
                    Panel_1.Anchors = Anchors.None;
                    Panel_1.Visible = true;

                    PauseMenuText.SetPosition(21, 19);
                    PauseMenuText.SetSize(214, 62);
                    PauseMenuText.Anchors = Anchors.None;
                    PauseMenuText.Visible = true;

                    ResumeButton.SetPosition(277, 22);
                    ResumeButton.SetSize(214, 56);
                    ResumeButton.Anchors = Anchors.None;
                    ResumeButton.Visible = true;

                    GiveUpButton.SetPosition(516, 22);
                    GiveUpButton.SetSize(214, 56);
                    GiveUpButton.Anchors = Anchors.None;
                    GiveUpButton.Visible = true;

                    PauseMenu.SetPosition(105, 227);
                    PauseMenu.SetSize(750, 100);
                    PauseMenu.Anchors = Anchors.None;
                    PauseMenu.Visible = false;

                    NextLevelButton.SetPosition(746, 488);
                    NextLevelButton.SetSize(214, 56);
                    NextLevelButton.Anchors = Anchors.None;
                    NextLevelButton.Visible = true;

                    break;
            }
            _currentLayoutOrientation = orientation;
        }

        public void UpdateLanguage()
        {
            ScoreLabelText.Text = "score:";

            ScoreText.Text = "0123456789";

            TimerSeparatorText.Text = ":";

            TimerSecondsText.Text = "00.0";

            TimerMinutesText.Text = "000";

            PauseMenuText.Text = "pause.";

            ResumeButton.Text = "Resume";

            GiveUpButton.Text = "Give Up";

            NextLevelButton.Text = "Next Level";
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
