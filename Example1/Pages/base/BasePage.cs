using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows;
using System;
using System.Threading.Tasks;

namespace Example1
{
    public class BasePage : Page
    {
        public PageAnimation PageLoadAnimation { set; get; } = PageAnimation.FromRightToLeft;

        public PageAnimation PageUnloadAnimation { set; get; } = PageAnimation.FromLeftToRight;

        public float SlideSeconds { set; get; } = 0.8f;

        public BasePage()
        {
            if (this.PageLoadAnimation != PageAnimation.None)
                this.Visibility = Visibility.Collapsed;

            this.Loaded += BasePage_Loaded;
        }

        private async void BasePage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            await AnimatIn();
        }

        public async Task AnimatIn()
        {
            if (this.PageLoadAnimation == PageAnimation.None)
                return;

            switch(this.PageLoadAnimation)
            {
                case PageAnimation.FromRightToLeft:

                    await this.SlideAndFadeInFromRightAsync(this.SlideSeconds);
                    break;
            }
        }

        public async Task AnimatOut()
        {
            if (this.PageUnloadAnimation == PageAnimation.None)
                return;

            switch (this.PageUnloadAnimation)
            {
                case PageAnimation.FromLeftToRight:

                    await this.SlideAndFadeOutToLeftAsync(this.SlideSeconds);
                    break;
            }
        }
    }
}
