// AUTOMATICALLY GENERATED CODE

using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;

namespace Preview
{
    partial class LevelSelectScene
    {
        Panel sceneBackgroundPanel;
        Label LevelSelectTitleText;
        PagePanel PagePanel_1;
        ImageBox ImageBox_1;
        Button StartButton;
        Label LevelNumberText;
        Label LevelTimeText;
        Label GradeText;
        Label ScoreText;
        Button BackButton;
        Label stats;
        Label howToSelect;
        Label cubeNumber;

        private void InitializeWidget()
        {
            InitializeWidget(LayoutOrientation.Horizontal);
        }

        private void InitializeWidget(LayoutOrientation orientation)
        {
            sceneBackgroundPanel = new Panel();
            sceneBackgroundPanel.Name = "sceneBackgroundPanel";
            LevelSelectTitleText = new Label();
            LevelSelectTitleText.Name = "LevelSelectTitleText";
            PagePanel_1 = new PagePanel();
            PagePanel_1.Name = "PagePanel_1";
            ImageBox_1 = new ImageBox();
            ImageBox_1.Name = "ImageBox_1";
            StartButton = new Button();
            StartButton.Name = "StartButton";
            LevelNumberText = new Label();
            LevelNumberText.Name = "LevelNumberText";
            LevelTimeText = new Label();
            LevelTimeText.Name = "LevelTimeText";
            GradeText = new Label();
            GradeText.Name = "GradeText";
            ScoreText = new Label();
            ScoreText.Name = "ScoreText";
            BackButton = new Button();
            BackButton.Name = "BackButton";
            stats = new Label();
            stats.Name = "stats";
            howToSelect = new Label();
            howToSelect.Name = "howToSelect";
            cubeNumber = new Label();
            cubeNumber.Name = "cubeNumber";

            // sceneBackgroundPanel
            sceneBackgroundPanel.BackgroundColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);

            // LevelSelectTitleText
            LevelSelectTitleText.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            LevelSelectTitleText.Font = new UIFont(FontAlias.System, 36, FontStyle.Regular);
            LevelSelectTitleText.LineBreak = LineBreak.Character;

            // PagePanel_1
            PagePanel_1.AddPage(new LevelSelectPanel());
            PagePanel_1.AddPage(new LevelSelectPanel2());
            PagePanel_1.AddPage(new LevelSelectPanel3());

            // ImageBox_1
            ImageBox_1.Image = new ImageAsset("/Application/assets/images/UI/statsBox.png");
            ImageBox_1.ImageScaleType = ImageScaleType.Center;

            // StartButton
            StartButton.IconImage = null;
            StartButton.Style = ButtonStyle.Custom;
            StartButton.CustomImage = new CustomButtonImageSettings()
            {
                BackgroundNormalImage = new ImageAsset("/Application/assets/images/UI/start.png"),
                BackgroundPressedImage = new ImageAsset("/Application/assets/images/UI/startOver.png"),
                BackgroundDisabledImage = null,
                BackgroundNinePatchMargin = new NinePatchMargin(42, 27, 42, 27),
            };

            // LevelNumberText
            LevelNumberText.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            LevelNumberText.Font = new UIFont(FontAlias.System, 32, FontStyle.Regular);
            LevelNumberText.LineBreak = LineBreak.Character;
            LevelNumberText.HorizontalAlignment = HorizontalAlignment.Right;

            // LevelTimeText
            LevelTimeText.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            LevelTimeText.Font = new UIFont(FontAlias.System, 24, FontStyle.Regular);
            LevelTimeText.LineBreak = LineBreak.Character;
            LevelTimeText.HorizontalAlignment = HorizontalAlignment.Right;

            // GradeText
            GradeText.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            GradeText.Font = new UIFont(FontAlias.System, 72, FontStyle.Regular);
            GradeText.LineBreak = LineBreak.Character;
            GradeText.HorizontalAlignment = HorizontalAlignment.Right;

            // ScoreText
            ScoreText.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            ScoreText.Font = new UIFont(FontAlias.System, 32, FontStyle.Regular);
            ScoreText.LineBreak = LineBreak.Character;
            ScoreText.HorizontalAlignment = HorizontalAlignment.Right;

            // BackButton
            BackButton.IconImage = null;
            BackButton.Style = ButtonStyle.Custom;
            BackButton.CustomImage = new CustomButtonImageSettings()
            {
                BackgroundNormalImage = new ImageAsset("/Application/assets/images/UI/back.png"),
                BackgroundPressedImage = new ImageAsset("/Application/assets/images/UI/backOver.png"),
                BackgroundDisabledImage = null,
                BackgroundNinePatchMargin = new NinePatchMargin(0, 0, 0, 0),
            };

            // stats
            stats.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            stats.Font = new UIFont(FontAlias.System, 30, FontStyle.Regular);
            stats.LineBreak = LineBreak.Character;
            stats.HorizontalAlignment = HorizontalAlignment.Right;

            // howToSelect
            howToSelect.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            howToSelect.Font = new UIFont(FontAlias.System, 24, FontStyle.Regular);
            howToSelect.LineBreak = LineBreak.Character;

            // cubeNumber
            cubeNumber.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            cubeNumber.Font = new UIFont(FontAlias.System, 32, FontStyle.Regular);
            cubeNumber.LineBreak = LineBreak.Character;

            // LevelSelectScene
            this.RootWidget.AddChildLast(sceneBackgroundPanel);
            this.RootWidget.AddChildLast(LevelSelectTitleText);
            this.RootWidget.AddChildLast(PagePanel_1);
            this.RootWidget.AddChildLast(ImageBox_1);
            this.RootWidget.AddChildLast(StartButton);
            this.RootWidget.AddChildLast(LevelNumberText);
            this.RootWidget.AddChildLast(LevelTimeText);
            this.RootWidget.AddChildLast(GradeText);
            this.RootWidget.AddChildLast(ScoreText);
            this.RootWidget.AddChildLast(BackButton);
            this.RootWidget.AddChildLast(stats);
            this.RootWidget.AddChildLast(howToSelect);
            this.RootWidget.AddChildLast(cubeNumber);

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

                    LevelSelectTitleText.SetPosition(16, 20);
                    LevelSelectTitleText.SetSize(214, 36);
                    LevelSelectTitleText.Anchors = Anchors.None;
                    LevelSelectTitleText.Visible = true;

                    PagePanel_1.SetPosition(173, 99);
                    PagePanel_1.SetSize(100, 50);
                    PagePanel_1.Anchors = Anchors.None;
                    PagePanel_1.Visible = true;

                    ImageBox_1.SetPosition(682, 73);
                    ImageBox_1.SetSize(200, 200);
                    ImageBox_1.Anchors = Anchors.None;
                    ImageBox_1.Visible = true;

                    StartButton.SetPosition(61, 441);
                    StartButton.SetSize(214, 56);
                    StartButton.Anchors = Anchors.None;
                    StartButton.Visible = true;

                    LevelNumberText.SetPosition(61, 124);
                    LevelNumberText.SetSize(214, 36);
                    LevelNumberText.Anchors = Anchors.None;
                    LevelNumberText.Visible = true;

                    LevelTimeText.SetPosition(61, 124);
                    LevelTimeText.SetSize(214, 36);
                    LevelTimeText.Anchors = Anchors.None;
                    LevelTimeText.Visible = true;

                    GradeText.SetPosition(61, 124);
                    GradeText.SetSize(214, 36);
                    GradeText.Anchors = Anchors.None;
                    GradeText.Visible = true;

                    ScoreText.SetPosition(61, 124);
                    ScoreText.SetSize(214, 36);
                    ScoreText.Anchors = Anchors.None;
                    ScoreText.Visible = true;

                    BackButton.SetPosition(61, 441);
                    BackButton.SetSize(214, 56);
                    BackButton.Anchors = Anchors.None;
                    BackButton.Visible = true;

                    stats.SetPosition(703, 57);
                    stats.SetSize(214, 36);
                    stats.Anchors = Anchors.None;
                    stats.Visible = true;

                    howToSelect.SetPosition(74, 81);
                    howToSelect.SetSize(214, 36);
                    howToSelect.Anchors = Anchors.None;
                    howToSelect.Visible = true;

                    cubeNumber.SetPosition(657, 132);
                    cubeNumber.SetSize(214, 36);
                    cubeNumber.Anchors = Anchors.None;
                    cubeNumber.Visible = true;

                    break;

                default:
                    this.DesignWidth = 960;
                    this.DesignHeight = 544;

                    sceneBackgroundPanel.SetPosition(0, 0);
                    sceneBackgroundPanel.SetSize(960, 544);
                    sceneBackgroundPanel.Anchors = Anchors.Top | Anchors.Bottom | Anchors.Left | Anchors.Right;
                    sceneBackgroundPanel.Visible = true;

                    LevelSelectTitleText.SetPosition(74, 24);
                    LevelSelectTitleText.SetSize(329, 62);
                    LevelSelectTitleText.Anchors = Anchors.None;
                    LevelSelectTitleText.Visible = true;

                    PagePanel_1.SetPosition(18, 102);
                    PagePanel_1.SetSize(567, 396);
                    PagePanel_1.Anchors = Anchors.None;
                    PagePanel_1.Visible = true;

                    ImageBox_1.SetPosition(555, -5);
                    ImageBox_1.SetSize(418, 418);
                    ImageBox_1.Anchors = Anchors.None;
                    ImageBox_1.Visible = true;

                    StartButton.SetPosition(762, 388);
                    StartButton.SetSize(125, 82);
                    StartButton.Anchors = Anchors.None;
                    StartButton.Visible = true;

                    LevelNumberText.SetPosition(657, 139);
                    LevelNumberText.SetSize(214, 36);
                    LevelNumberText.Anchors = Anchors.None;
                    LevelNumberText.Visible = true;

                    LevelTimeText.SetPosition(657, 282);
                    LevelTimeText.SetSize(214, 36);
                    LevelTimeText.Anchors = Anchors.None;
                    LevelTimeText.Visible = true;

                    GradeText.SetPosition(657, 207);
                    GradeText.SetSize(214, 76);
                    GradeText.Anchors = Anchors.None;
                    GradeText.Visible = true;

                    ScoreText.SetPosition(657, 81);
                    ScoreText.SetSize(214, 36);
                    ScoreText.Anchors = Anchors.None;
                    ScoreText.Visible = true;

                    BackButton.SetPosition(638, 344);
                    BackButton.SetSize(163, 81);
                    BackButton.Anchors = Anchors.None;
                    BackButton.Visible = true;

                    stats.SetPosition(672, 37);
                    stats.SetSize(214, 36);
                    stats.Anchors = Anchors.None;
                    stats.Visible = true;

                    howToSelect.SetPosition(76, 73);
                    howToSelect.SetSize(511, 36);
                    howToSelect.Anchors = Anchors.None;
                    howToSelect.Visible = true;

                    cubeNumber.SetPosition(761, 139);
                    cubeNumber.SetSize(88, 36);
                    cubeNumber.Anchors = Anchors.None;
                    cubeNumber.Visible = true;

                    break;
            }
            _currentLayoutOrientation = orientation;
        }

        public void UpdateLanguage()
        {
            LevelSelectTitleText.Text = "select a level";

            LevelNumberText.Text = "00";

            LevelTimeText.Text = "00:00:00.0";

            GradeText.Text = "A+";

            ScoreText.Text = "0";

            stats.Text = "stats";

            howToSelect.Text = "select a cube and then press start";

            cubeNumber.Text = "cube:";

            this.Title = "LevelSelectScene";
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
