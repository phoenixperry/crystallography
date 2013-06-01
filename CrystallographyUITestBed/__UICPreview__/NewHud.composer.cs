// AUTOMATICALLY GENERATED CODE

using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;

namespace Preview
{
    partial class NewHud
    {
        ImageBox ImageBox_1;
        ImageBox ImageBox_2;
        ImageBox ImageBox_3;
        ImageBox ImageBox_4;
        ImageBox ImageBox_5;
        ImageBox ImageBox_6;
        Label currentTime;
        Label bestTime;
        Label vs;
        Panel Panel_1;
        ImageBox ImageBox_7;
        ImageBox ImageBox_8;
        ImageBox ImageBox_9;
        Panel Panel_2;

        private void InitializeWidget()
        {
            InitializeWidget(LayoutOrientation.Horizontal);
        }

        private void InitializeWidget(LayoutOrientation orientation)
        {
            ImageBox_1 = new ImageBox();
            ImageBox_1.Name = "ImageBox_1";
            ImageBox_2 = new ImageBox();
            ImageBox_2.Name = "ImageBox_2";
            ImageBox_3 = new ImageBox();
            ImageBox_3.Name = "ImageBox_3";
            ImageBox_4 = new ImageBox();
            ImageBox_4.Name = "ImageBox_4";
            ImageBox_5 = new ImageBox();
            ImageBox_5.Name = "ImageBox_5";
            ImageBox_6 = new ImageBox();
            ImageBox_6.Name = "ImageBox_6";
            currentTime = new Label();
            currentTime.Name = "currentTime";
            bestTime = new Label();
            bestTime.Name = "bestTime";
            vs = new Label();
            vs.Name = "vs";
            Panel_1 = new Panel();
            Panel_1.Name = "Panel_1";
            ImageBox_7 = new ImageBox();
            ImageBox_7.Name = "ImageBox_7";
            ImageBox_8 = new ImageBox();
            ImageBox_8.Name = "ImageBox_8";
            ImageBox_9 = new ImageBox();
            ImageBox_9.Name = "ImageBox_9";
            Panel_2 = new Panel();
            Panel_2.Name = "Panel_2";

            // ImageBox_1
            ImageBox_1.Image = new ImageAsset("/Application/assets/new/newUI/hud/score.png");
            ImageBox_1.ImageScaleType = ImageScaleType.Center;

            // ImageBox_2
            ImageBox_2.Image = new ImageAsset("/Application/assets/new/newUI/hud/goal.png");
            ImageBox_2.ImageScaleType = ImageScaleType.Center;

            // ImageBox_3
            ImageBox_3.Image = new ImageAsset("/Application/assets/new/newUI/hud/hitMe.png");
            ImageBox_3.ImageScaleType = ImageScaleType.Center;

            // ImageBox_4
            ImageBox_4.Image = new ImageAsset("/Application/assets/new/newUI/blueBox.png");
            ImageBox_4.ImageScaleType = ImageScaleType.Center;

            // ImageBox_5
            ImageBox_5.Image = new ImageAsset("/Application/assets/new/newUI/redBox.png");
            ImageBox_5.ImageScaleType = ImageScaleType.Center;

            // ImageBox_6
            ImageBox_6.Image = new ImageAsset("/Application/assets/new/newUI/timerIcon.png");
            ImageBox_6.ImageScaleType = ImageScaleType.Center;

            // currentTime
            currentTime.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            currentTime.Font = new UIFont(FontAlias.System, 20, FontStyle.Regular);
            currentTime.LineBreak = LineBreak.Character;

            // bestTime
            bestTime.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            bestTime.Font = new UIFont(FontAlias.System, 20, FontStyle.Regular);
            bestTime.LineBreak = LineBreak.Character;

            // vs
            vs.TextColor = new UIColor(226f / 255f, 20f / 255f, 20f / 255f, 255f / 255f);
            vs.Font = new UIFont(FontAlias.System, 25, FontStyle.Regular);
            vs.LineBreak = LineBreak.Character;

            // Panel_1
            Panel_1.BackgroundColor = new UIColor(30f / 255f, 30f / 255f, 30f / 255f, 255f / 255f);
            Panel_1.Clip = true;
            Panel_1.AddChildLast(ImageBox_1);
            Panel_1.AddChildLast(ImageBox_2);
            Panel_1.AddChildLast(ImageBox_3);
            Panel_1.AddChildLast(ImageBox_4);
            Panel_1.AddChildLast(ImageBox_5);
            Panel_1.AddChildLast(ImageBox_6);
            Panel_1.AddChildLast(currentTime);
            Panel_1.AddChildLast(bestTime);
            Panel_1.AddChildLast(vs);

            // ImageBox_7
            ImageBox_7.Image = new ImageAsset("/Application/assets/new/newUI/quitBtn.png");
            ImageBox_7.ImageScaleType = ImageScaleType.Center;

            // ImageBox_8
            ImageBox_8.Image = new ImageAsset("/Application/assets/new/newUI/levelSelect.png");
            ImageBox_8.ImageScaleType = ImageScaleType.Center;

            // ImageBox_9
            ImageBox_9.Image = new ImageAsset("/Application/assets/new/newUI/nextLevel.png");
            ImageBox_9.ImageScaleType = ImageScaleType.Center;

            // Panel_2
            Panel_2.BackgroundColor = new UIColor(30f / 255f, 30f / 255f, 30f / 255f, 255f / 255f);
            Panel_2.Clip = true;
            Panel_2.AddChildLast(ImageBox_7);
            Panel_2.AddChildLast(ImageBox_8);
            Panel_2.AddChildLast(ImageBox_9);

            // NewHud
            this.RootWidget.AddChildLast(Panel_1);
            this.RootWidget.AddChildLast(Panel_2);

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

                    ImageBox_1.SetPosition(-24, -49);
                    ImageBox_1.SetSize(200, 200);
                    ImageBox_1.Anchors = Anchors.None;
                    ImageBox_1.Visible = true;

                    ImageBox_2.SetPosition(157, -51);
                    ImageBox_2.SetSize(200, 200);
                    ImageBox_2.Anchors = Anchors.None;
                    ImageBox_2.Visible = true;

                    ImageBox_3.SetPosition(610, -70);
                    ImageBox_3.SetSize(200, 200);
                    ImageBox_3.Anchors = Anchors.None;
                    ImageBox_3.Visible = true;

                    ImageBox_4.SetPosition(271, -52);
                    ImageBox_4.SetSize(200, 200);
                    ImageBox_4.Anchors = Anchors.None;
                    ImageBox_4.Visible = true;

                    ImageBox_5.SetPosition(329, -65);
                    ImageBox_5.SetSize(200, 200);
                    ImageBox_5.Anchors = Anchors.None;
                    ImageBox_5.Visible = true;

                    ImageBox_6.SetPosition(434, -49);
                    ImageBox_6.SetSize(200, 200);
                    ImageBox_6.Anchors = Anchors.None;
                    ImageBox_6.Visible = true;

                    currentTime.SetPosition(487, 33);
                    currentTime.SetSize(214, 36);
                    currentTime.Anchors = Anchors.None;
                    currentTime.Visible = true;

                    bestTime.SetPosition(632, 7);
                    bestTime.SetSize(214, 36);
                    bestTime.Anchors = Anchors.None;
                    bestTime.Visible = true;

                    vs.SetPosition(542, 7);
                    vs.SetSize(214, 36);
                    vs.Anchors = Anchors.None;
                    vs.Visible = true;

                    Panel_1.SetPosition(34, 7);
                    Panel_1.SetSize(100, 100);
                    Panel_1.Anchors = Anchors.None;
                    Panel_1.Visible = true;

                    ImageBox_7.SetPosition(-18, 175);
                    ImageBox_7.SetSize(200, 200);
                    ImageBox_7.Anchors = Anchors.None;
                    ImageBox_7.Visible = true;

                    ImageBox_8.SetPosition(122, 185);
                    ImageBox_8.SetSize(200, 200);
                    ImageBox_8.Anchors = Anchors.None;
                    ImageBox_8.Visible = true;

                    ImageBox_9.SetPosition(302, 210);
                    ImageBox_9.SetSize(200, 200);
                    ImageBox_9.Anchors = Anchors.None;
                    ImageBox_9.Visible = true;

                    Panel_2.SetPosition(1, 63);
                    Panel_2.SetSize(100, 100);
                    Panel_2.Anchors = Anchors.None;
                    Panel_2.Visible = true;

                    break;

                default:
                    this.DesignWidth = 960;
                    this.DesignHeight = 544;

                    ImageBox_1.SetPosition(1, 7);
                    ImageBox_1.SetSize(121, 56);
                    ImageBox_1.Anchors = Anchors.None;
                    ImageBox_1.Visible = true;

                    ImageBox_2.SetPosition(226, -2);
                    ImageBox_2.SetSize(103, 69);
                    ImageBox_2.Anchors = Anchors.None;
                    ImageBox_2.Visible = true;

                    ImageBox_3.SetPosition(826, -27);
                    ImageBox_3.SetSize(127, 127);
                    ImageBox_3.Anchors = Anchors.None;
                    ImageBox_3.Visible = true;

                    ImageBox_4.SetPosition(68, -65);
                    ImageBox_4.SetSize(200, 200);
                    ImageBox_4.Anchors = Anchors.None;
                    ImageBox_4.Visible = true;

                    ImageBox_5.SetPosition(295, -64);
                    ImageBox_5.SetSize(200, 200);
                    ImageBox_5.Anchors = Anchors.None;
                    ImageBox_5.Visible = true;

                    ImageBox_6.SetPosition(396, -64);
                    ImageBox_6.SetSize(200, 200);
                    ImageBox_6.Anchors = Anchors.None;
                    ImageBox_6.Visible = true;

                    currentTime.SetPosition(535, 7);
                    currentTime.SetSize(151, 36);
                    currentTime.Anchors = Anchors.None;
                    currentTime.Visible = true;

                    bestTime.SetPosition(685, 7);
                    bestTime.SetSize(214, 36);
                    bestTime.Anchors = Anchors.None;
                    bestTime.Visible = true;

                    vs.SetPosition(652, 7);
                    vs.SetSize(33, 36);
                    vs.Anchors = Anchors.None;
                    vs.Visible = true;

                    Panel_1.SetPosition(0, 0);
                    Panel_1.SetSize(960, 71);
                    Panel_1.Anchors = Anchors.None;
                    Panel_1.Visible = true;

                    ImageBox_7.SetPosition(-42, 114);
                    ImageBox_7.SetSize(203, 199);
                    ImageBox_7.Anchors = Anchors.None;
                    ImageBox_7.Visible = true;

                    ImageBox_8.SetPosition(103, 114);
                    ImageBox_8.SetSize(203, 199);
                    ImageBox_8.Anchors = Anchors.None;
                    ImageBox_8.Visible = true;

                    ImageBox_9.SetPosition(269, 114);
                    ImageBox_9.SetSize(203, 199);
                    ImageBox_9.Anchors = Anchors.None;
                    ImageBox_9.Visible = true;

                    Panel_2.SetPosition(0, 66);
                    Panel_2.SetSize(445, 241);
                    Panel_2.Anchors = Anchors.None;
                    Panel_2.Visible = true;

                    break;
            }
            _currentLayoutOrientation = orientation;
        }

        public void UpdateLanguage()
        {
            currentTime.Text = "current time";

            bestTime.Text = "best time";

            vs.Text = "vs";
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
