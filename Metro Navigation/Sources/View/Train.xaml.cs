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
        public List<bool> IsPedestrian { get; set; }

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
            var ChangeToPedestrianAnimation = new ObjectAnimationUsingKeyFrames();
            for (int i = 0; i < IsPedestrian.Count; i++)
            {     
                if (IsPedestrian[i])
                {
                    var f = new DiscreteObjectKeyFrame();
                    f.KeyTime = TimeSpan.FromSeconds(i);
                    f.Value = Visibility.Visible;
                    ChangeToPedestrianAnimation.KeyFrames.Add(f);
                    f = new DiscreteObjectKeyFrame();
                    f.KeyTime = TimeSpan.FromSeconds(i+1);
                    f.Value = Visibility.Collapsed;
                    ChangeToPedestrianAnimation.KeyFrames.Add(f);
                }
            }

            Storyboard.SetTarget(ChangeToPedestrianAnimation, PedestrianImage);
            Storyboard.SetTargetProperty(ChangeToPedestrianAnimation, new PropertyPath("Visibility"));

            Storyboard.SetTargetProperty(animX,
                new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)"));
            Storyboard.SetTarget(animX, this);
            Storyboard.SetTargetProperty(animY,
                new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)"));
            Storyboard.SetTarget(animY, this);

            storyboard.Children.Add(animX);
            storyboard.Children.Add(animY);
            storyboard.Children.Add(ChangeToPedestrianAnimation);
            PointsToPath.Clear();
            storyboard.Begin();
        }
    }
}
