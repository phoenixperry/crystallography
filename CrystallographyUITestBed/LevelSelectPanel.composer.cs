// AUTOMATICALLY GENERATED CODE

using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;

namespace Crystallography.UI
{
    partial class LevelSelectPanel
    {
        Button Button_1;
        Panel Panel_1;
        Panel Panel_2;
        Panel Panel_3;
        Panel Panel_4;
        Panel Panel_5;
        Panel Panel_6;
        Panel Panel_7;
        Panel Panel_8;
        Panel Panel_9;
        Panel Panel_10;
        Panel Panel_11;
        Panel Panel_12;

        private void InitializeWidget()
        {
            InitializeWidget(LayoutOrientation.Horizontal);
        }

        private void InitializeWidget(LayoutOrientation orientation)
        {
            Button_1 = new Button();
            Button_1.Name = "Button_1";
            Panel_1 = new Panel();
            Panel_1.Name = "Panel_1";
            Panel_2 = new Panel();
            Panel_2.Name = "Panel_2";
            Panel_3 = new Panel();
            Panel_3.Name = "Panel_3";
            Panel_4 = new Panel();
            Panel_4.Name = "Panel_4";
            Panel_5 = new Panel();
            Panel_5.Name = "Panel_5";
            Panel_6 = new Panel();
            Panel_6.Name = "Panel_6";
            Panel_7 = new Panel();
            Panel_7.Name = "Panel_7";
            Panel_8 = new Panel();
            Panel_8.Name = "Panel_8";
            Panel_9 = new Panel();
            Panel_9.Name = "Panel_9";
            Panel_10 = new Panel();
            Panel_10.Name = "Panel_10";
            Panel_11 = new Panel();
            Panel_11.Name = "Panel_11";
            Panel_12 = new Panel();
            Panel_12.Name = "Panel_12";

            // Button_1
            Button_1.IconImage = null;
            Button_1.Style = ButtonStyle.Custom;
            Button_1.CustomImage = new CustomButtonImageSettings()
            {
                BackgroundNormalImage = new ImageAsset("/Application/assets/images/UI/cubeUI6.png"),
                BackgroundPressedImage = new ImageAsset("/Application/assets/images/UI/cubeUIRollOver6.png"),
                BackgroundDisabledImage = null,
                BackgroundNinePatchMargin = new NinePatchMargin(0, 0, 0, 0),
            };
            Button_1.BackgroundFilterColor = new UIColor(229f / 255f, 19f / 255f, 19f / 255f, 255f / 255f);

            // Panel_1
            Panel_1.BackgroundColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            Panel_1.Clip = true;
            Panel_1.AddChildLast(Button_1);

            // Panel_2
            Panel_2.BackgroundColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            Panel_2.Clip = true;

            // Panel_3
            Panel_3.BackgroundColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            Panel_3.Clip = true;

            // Panel_4
            Panel_4.BackgroundColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            Panel_4.Clip = true;

            // Panel_5
            Panel_5.BackgroundColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            Panel_5.Clip = true;

            // Panel_6
            Panel_6.BackgroundColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            Panel_6.Clip = true;

            // Panel_7
            Panel_7.BackgroundColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            Panel_7.Clip = true;

            // Panel_8
            Panel_8.BackgroundColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            Panel_8.Clip = true;

            // Panel_9
            Panel_9.BackgroundColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            Panel_9.Clip = true;

            // Panel_10
            Panel_10.BackgroundColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            Panel_10.Clip = true;

            // Panel_11
            Panel_11.BackgroundColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            Panel_11.Clip = true;

            // Panel_12
            Panel_12.BackgroundColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            Panel_12.Clip = true;

            // LevelSelectPanel
            this.BackgroundColor = new UIColor(40f / 255f, 40f / 255f, 40f / 255f, 255f / 255f);
            this.Clip = true;
            this.AddChildLast(Panel_1);
            this.AddChildLast(Panel_2);
            this.AddChildLast(Panel_3);
            this.AddChildLast(Panel_4);
            this.AddChildLast(Panel_5);
            this.AddChildLast(Panel_6);
            this.AddChildLast(Panel_7);
            this.AddChildLast(Panel_8);
            this.AddChildLast(Panel_9);
            this.AddChildLast(Panel_10);
            this.AddChildLast(Panel_11);
            this.AddChildLast(Panel_12);

            SetWidgetLayout(orientation);

            UpdateLanguage();
        }

        private LayoutOrientation _currentLayoutOrientation;
        public void SetWidgetLayout(LayoutOrientation orientation)
        {
            switch (orientation)
            {
                case LayoutOrientation.Vertical:
                    this.SetSize(396, 567);
                    this.Anchors = Anchors.None;

                    Button_1.SetPosition(-68, 21);
                    Button_1.SetSize(214, 56);
                    Button_1.Anchors = Anchors.None;
                    Button_1.Visible = true;

                    Panel_1.SetPosition(372, 124);
                    Panel_1.SetSize(100, 100);
                    Panel_1.Anchors = Anchors.None;
                    Panel_1.Visible = true;

                    Panel_2.SetPosition(372, 124);
                    Panel_2.SetSize(100, 100);
                    Panel_2.Anchors = Anchors.None;
                    Panel_2.Visible = true;

                    Panel_3.SetPosition(372, 124);
                    Panel_3.SetSize(100, 100);
                    Panel_3.Anchors = Anchors.None;
                    Panel_3.Visible = true;

                    Panel_4.SetPosition(372, 124);
                    Panel_4.SetSize(100, 100);
                    Panel_4.Anchors = Anchors.None;
                    Panel_4.Visible = true;

                    Panel_5.SetPosition(372, 124);
                    Panel_5.SetSize(100, 100);
                    Panel_5.Anchors = Anchors.None;
                    Panel_5.Visible = true;

                    Panel_6.SetPosition(372, 124);
                    Panel_6.SetSize(100, 100);
                    Panel_6.Anchors = Anchors.None;
                    Panel_6.Visible = true;

                    Panel_7.SetPosition(372, 124);
                    Panel_7.SetSize(100, 100);
                    Panel_7.Anchors = Anchors.None;
                    Panel_7.Visible = true;

                    Panel_8.SetPosition(372, 124);
                    Panel_8.SetSize(100, 100);
                    Panel_8.Anchors = Anchors.None;
                    Panel_8.Visible = true;

                    Panel_9.SetPosition(372, 124);
                    Panel_9.SetSize(100, 100);
                    Panel_9.Anchors = Anchors.None;
                    Panel_9.Visible = true;

                    Panel_10.SetPosition(372, 124);
                    Panel_10.SetSize(100, 100);
                    Panel_10.Anchors = Anchors.None;
                    Panel_10.Visible = true;

                    Panel_11.SetPosition(372, 124);
                    Panel_11.SetSize(100, 100);
                    Panel_11.Anchors = Anchors.None;
                    Panel_11.Visible = true;

                    Panel_12.SetPosition(372, 124);
                    Panel_12.SetSize(100, 100);
                    Panel_12.Anchors = Anchors.None;
                    Panel_12.Visible = true;

                    break;

                default:
                    this.SetSize(567, 396);
                    this.Anchors = Anchors.None;

                    Button_1.SetPosition(13, 5);
                    Button_1.SetSize(76, 88);
                    Button_1.Anchors = Anchors.Top;
                    Button_1.Visible = true;

                    Panel_1.SetPosition(40, 24);
                    Panel_1.SetSize(100, 100);
                    Panel_1.Anchors = Anchors.None;
                    Panel_1.Visible = true;

                    Panel_2.SetPosition(171, 24);
                    Panel_2.SetSize(100, 100);
                    Panel_2.Anchors = Anchors.None;
                    Panel_2.Visible = true;

                    Panel_3.SetPosition(297, 24);
                    Panel_3.SetSize(100, 100);
                    Panel_3.Anchors = Anchors.None;
                    Panel_3.Visible = true;

                    Panel_4.SetPosition(427, 24);
                    Panel_4.SetSize(100, 100);
                    Panel_4.Anchors = Anchors.None;
                    Panel_4.Visible = true;

                    Panel_5.SetPosition(40, 150);
                    Panel_5.SetSize(100, 100);
                    Panel_5.Anchors = Anchors.None;
                    Panel_5.Visible = true;

                    Panel_6.SetPosition(171, 150);
                    Panel_6.SetSize(100, 100);
                    Panel_6.Anchors = Anchors.None;
                    Panel_6.Visible = true;

                    Panel_7.SetPosition(297, 150);
                    Panel_7.SetSize(100, 100);
                    Panel_7.Anchors = Anchors.None;
                    Panel_7.Visible = true;

                    Panel_8.SetPosition(427, 150);
                    Panel_8.SetSize(100, 100);
                    Panel_8.Anchors = Anchors.None;
                    Panel_8.Visible = true;

                    Panel_9.SetPosition(40, 273);
                    Panel_9.SetSize(100, 100);
                    Panel_9.Anchors = Anchors.None;
                    Panel_9.Visible = true;

                    Panel_10.SetPosition(171, 273);
                    Panel_10.SetSize(100, 100);
                    Panel_10.Anchors = Anchors.None;
                    Panel_10.Visible = true;

                    Panel_11.SetPosition(297, 273);
                    Panel_11.SetSize(100, 100);
                    Panel_11.Anchors = Anchors.None;
                    Panel_11.Visible = true;

                    Panel_12.SetPosition(427, 273);
                    Panel_12.SetSize(100, 100);
                    Panel_12.Anchors = Anchors.None;
                    Panel_12.Visible = true;

                    break;
            }
            _currentLayoutOrientation = orientation;
        }

        public void UpdateLanguage()
        {
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
