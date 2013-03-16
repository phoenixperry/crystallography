// AUTOMATICALLY GENERATED CODE

using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;

namespace Crystallography.UI
{
    partial class LevelEndPanel
    {
        Label SequenceCompleteText;
        Label ScoreText;
        Label TimeText;
        Label YoursText;
        Label BestText;
        Label YourScoreText;
        Label BestScoreText;
        Label YourTimeText;
        Label BestTimeText;
        Button NextLevelButton;
        Button LevelSelectButton;
        Button QuitButton;

        private void InitializeWidget()
        {
            InitializeWidget(LayoutOrientation.Horizontal);
        }

        private void InitializeWidget(LayoutOrientation orientation)
        {
            SequenceCompleteText = new Label();
            SequenceCompleteText.Name = "SequenceCompleteText";
            ScoreText = new Label();
            ScoreText.Name = "ScoreText";
            TimeText = new Label();
            TimeText.Name = "TimeText";
            YoursText = new Label();
            YoursText.Name = "YoursText";
            BestText = new Label();
            BestText.Name = "BestText";
            YourScoreText = new Label();
            YourScoreText.Name = "YourScoreText";
            BestScoreText = new Label();
            BestScoreText.Name = "BestScoreText";
            YourTimeText = new Label();
            YourTimeText.Name = "YourTimeText";
            BestTimeText = new Label();
            BestTimeText.Name = "BestTimeText";
            NextLevelButton = new Button();
            NextLevelButton.Name = "NextLevelButton";
            LevelSelectButton = new Button();
            LevelSelectButton.Name = "LevelSelectButton";
            QuitButton = new Button();
            QuitButton.Name = "QuitButton";

            // SequenceCompleteText
            SequenceCompleteText.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            SequenceCompleteText.Font = new UIFont(FontAlias.System, 72, FontStyle.Regular);
            SequenceCompleteText.LineBreak = LineBreak.Character;

            // ScoreText
            ScoreText.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            ScoreText.Font = new UIFont(FontAlias.System, 48, FontStyle.Regular);
            ScoreText.LineBreak = LineBreak.Character;

            // TimeText
            TimeText.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            TimeText.Font = new UIFont(FontAlias.System, 48, FontStyle.Regular);
            TimeText.LineBreak = LineBreak.Character;

            // YoursText
            YoursText.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            YoursText.Font = new UIFont(FontAlias.System, 48, FontStyle.Regular);
            YoursText.LineBreak = LineBreak.Character;
            YoursText.HorizontalAlignment = HorizontalAlignment.Center;

            // BestText
            BestText.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            BestText.Font = new UIFont(FontAlias.System, 48, FontStyle.Regular);
            BestText.LineBreak = LineBreak.Character;
            BestText.HorizontalAlignment = HorizontalAlignment.Center;

            // YourScoreText
            YourScoreText.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            YourScoreText.Font = new UIFont(FontAlias.System, 48, FontStyle.Regular);
            YourScoreText.LineBreak = LineBreak.Character;
            YourScoreText.HorizontalAlignment = HorizontalAlignment.Center;

            // BestScoreText
            BestScoreText.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            BestScoreText.Font = new UIFont(FontAlias.System, 48, FontStyle.Regular);
            BestScoreText.LineBreak = LineBreak.Character;
            BestScoreText.HorizontalAlignment = HorizontalAlignment.Center;

            // YourTimeText
            YourTimeText.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            YourTimeText.Font = new UIFont(FontAlias.System, 48, FontStyle.Regular);
            YourTimeText.LineBreak = LineBreak.Character;
            YourTimeText.HorizontalAlignment = HorizontalAlignment.Center;

            // BestTimeText
            BestTimeText.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            BestTimeText.Font = new UIFont(FontAlias.System, 48, FontStyle.Regular);
            BestTimeText.LineBreak = LineBreak.Character;
            BestTimeText.HorizontalAlignment = HorizontalAlignment.Center;

            // NextLevelButton
            NextLevelButton.TextColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            NextLevelButton.TextFont = new UIFont(FontAlias.System, 25, FontStyle.Regular);

            // LevelSelectButton
            LevelSelectButton.TextColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            LevelSelectButton.TextFont = new UIFont(FontAlias.System, 25, FontStyle.Regular);

            // QuitButton
            QuitButton.TextColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            QuitButton.TextFont = new UIFont(FontAlias.System, 25, FontStyle.Regular);

            // LevelEndPanel
            this.BackgroundColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            this.Clip = true;
            this.AddChildLast(SequenceCompleteText);
            this.AddChildLast(ScoreText);
            this.AddChildLast(TimeText);
            this.AddChildLast(YoursText);
            this.AddChildLast(BestText);
            this.AddChildLast(YourScoreText);
            this.AddChildLast(BestScoreText);
            this.AddChildLast(YourTimeText);
            this.AddChildLast(BestTimeText);
            this.AddChildLast(NextLevelButton);
            this.AddChildLast(LevelSelectButton);
            this.AddChildLast(QuitButton);

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

                    SequenceCompleteText.SetPosition(30, 35);
                    SequenceCompleteText.SetSize(214, 36);
                    SequenceCompleteText.Anchors = Anchors.None;
                    SequenceCompleteText.Visible = true;

                    ScoreText.SetPosition(31, 205);
                    ScoreText.SetSize(214, 36);
                    ScoreText.Anchors = Anchors.None;
                    ScoreText.Visible = true;

                    TimeText.SetPosition(31, 324);
                    TimeText.SetSize(214, 36);
                    TimeText.Anchors = Anchors.None;
                    TimeText.Visible = true;

                    YoursText.SetPosition(31, 205);
                    YoursText.SetSize(214, 36);
                    YoursText.Anchors = Anchors.None;
                    YoursText.Visible = true;

                    BestText.SetPosition(31, 205);
                    BestText.SetSize(214, 36);
                    BestText.Anchors = Anchors.None;
                    BestText.Visible = true;

                    YourScoreText.SetPosition(31, 205);
                    YourScoreText.SetSize(214, 36);
                    YourScoreText.Anchors = Anchors.None;
                    YourScoreText.Visible = true;

                    BestScoreText.SetPosition(31, 205);
                    BestScoreText.SetSize(214, 36);
                    BestScoreText.Anchors = Anchors.None;
                    BestScoreText.Visible = true;

                    YourTimeText.SetPosition(31, 205);
                    YourTimeText.SetSize(214, 36);
                    YourTimeText.Anchors = Anchors.None;
                    YourTimeText.Visible = true;

                    BestTimeText.SetPosition(31, 205);
                    BestTimeText.SetSize(214, 36);
                    BestTimeText.Anchors = Anchors.None;
                    BestTimeText.Visible = true;

                    NextLevelButton.SetPosition(716, 447);
                    NextLevelButton.SetSize(214, 56);
                    NextLevelButton.Anchors = Anchors.None;
                    NextLevelButton.Visible = true;

                    LevelSelectButton.SetPosition(716, 447);
                    LevelSelectButton.SetSize(214, 56);
                    LevelSelectButton.Anchors = Anchors.None;
                    LevelSelectButton.Visible = true;

                    QuitButton.SetPosition(716, 447);
                    QuitButton.SetSize(214, 56);
                    QuitButton.Anchors = Anchors.None;
                    QuitButton.Visible = true;

                    break;

                default:
                    this.SetSize(960, 544);
                    this.Anchors = Anchors.None;

                    SequenceCompleteText.SetPosition(31, 32);
                    SequenceCompleteText.SetSize(866, 92);
                    SequenceCompleteText.Anchors = Anchors.None;
                    SequenceCompleteText.Visible = true;

                    ScoreText.SetPosition(31, 247);
                    ScoreText.SetSize(143, 49);
                    ScoreText.Anchors = Anchors.None;
                    ScoreText.Visible = true;

                    TimeText.SetPosition(31, 342);
                    TimeText.SetSize(214, 54);
                    TimeText.Anchors = Anchors.None;
                    TimeText.Visible = true;

                    YoursText.SetPosition(225, 171);
                    YoursText.SetSize(143, 60);
                    YoursText.Anchors = Anchors.None;
                    YoursText.Visible = true;

                    BestText.SetPosition(451, 170);
                    BestText.SetSize(143, 49);
                    BestText.Anchors = Anchors.None;
                    BestText.Visible = true;

                    YourScoreText.SetPosition(207, 251);
                    YourScoreText.SetSize(180, 60);
                    YourScoreText.Anchors = Anchors.None;
                    YourScoreText.Visible = true;

                    BestScoreText.SetPosition(433, 251);
                    BestScoreText.SetSize(180, 60);
                    BestScoreText.Anchors = Anchors.None;
                    BestScoreText.Visible = true;

                    YourTimeText.SetPosition(207, 344);
                    YourTimeText.SetSize(180, 60);
                    YourTimeText.Anchors = Anchors.None;
                    YourTimeText.Visible = true;

                    BestTimeText.SetPosition(433, 344);
                    BestTimeText.SetSize(180, 60);
                    BestTimeText.Anchors = Anchors.None;
                    BestTimeText.Visible = true;

                    NextLevelButton.SetPosition(716, 287);
                    NextLevelButton.SetSize(214, 56);
                    NextLevelButton.Anchors = Anchors.None;
                    NextLevelButton.Visible = true;

                    LevelSelectButton.SetPosition(716, 371);
                    LevelSelectButton.SetSize(214, 56);
                    LevelSelectButton.Anchors = Anchors.None;
                    LevelSelectButton.Visible = true;

                    QuitButton.SetPosition(716, 455);
                    QuitButton.SetSize(214, 56);
                    QuitButton.Anchors = Anchors.None;
                    QuitButton.Visible = true;

                    break;
            }
            _currentLayoutOrientation = orientation;
        }

        public void UpdateLanguage()
        {
            SequenceCompleteText.Text = "sequence complete.";

            ScoreText.Text = "score";

            TimeText.Text = "time";

            YoursText.Text = "yours";

            BestText.Text = "best";

            YourScoreText.Text = "00000";

            BestScoreText.Text = "00000";

            YourTimeText.Text = "00:00.0";

            BestTimeText.Text = "00:00.0";

            NextLevelButton.Text = "Next Level";

            LevelSelectButton.Text = "Level Select";

            QuitButton.Text = "Quit";
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
