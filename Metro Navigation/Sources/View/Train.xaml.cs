using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Metro_Navigation.Sources.View
{
    public partial class Train : UserControl
    {
        Storyboard storyboard;

        public Train()
        {
            InitializeComponent();
        }

        public List<Point> PointsToPath { get; set; }

        public void StartMoving()
        {
            storyboard = new Storyboard();
            storyboard.SpeedRatio = 2;
            DoubleAnimationUsingKeyFrames animX = new DoubleAnimationUsingKeyFrames();
            animX.Duration = TimeSpan.FromSeconds(PointsToPath.Count);
            DoubleAnimationUsingKeyFrames animY = new DoubleAnimationUsingKeyFrames();
            animY.Duration = TimeSpan.FromSeconds(PointsToPath.Count);
            for (int i = 0; i < PointsToPath.Count; i++)
            {
                var ease = new ExponentialEase();
                ease.EasingMode = EasingMode.EaseIn;

                var kx = new EasingDoubleKeyFrame();
                kx.Value = PointsToPath[i].X;
                kx.KeyTime = TimeSpan.FromSeconds(i);
                kx.EasingFunction = ease;
                animX.KeyFrames.Add(kx);

                var ky = new EasingDoubleKeyFrame();
                ky.Value = PointsToPath[i].Y;
                ky.KeyTime = TimeSpan.FromSeconds(i);
                ky.EasingFunction = ease;
                animY.KeyFrames.Add(ky);
            }
            
            Storyboard.SetTargetProperty(animX, 
                new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)"));
            Storyboard.SetTarget(animX, this);
            Storyboard.SetTargetProperty(animY,
                new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)"));
            Storyboard.SetTarget(animY, this);

            storyboard.Children.Add(animX);
            storyboard.Children.Add(animY);
            PointsToPath.Clear();
            storyboard.Begin();
        }
    }
}
