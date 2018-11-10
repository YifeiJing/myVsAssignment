using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Example1
{
    /// <summary>
    /// Helpers to animate pages in specific ways
    /// </summary>
    public static class ElementAnimations
    {
        /// <summary>
        /// Slides a page in from the right
        /// </summary>
        /// <param name="page">The page to animate</param>
        /// <param name="seconds">The time the animation will take</param>
        /// <returns></returns>
        public static async Task SlideAndFadeInFromRightAsync(this FrameworkElement element, float seconds)
        {
            // Create the storyboard
            var sb = new Storyboard();

            // Add slide from right animation
            sb.AddSlideFromRight(seconds, element.ActualWidth, keepMargin: false);

            // Add fade in animation
            sb.AddFadeIn(seconds);

            // Start animating
            sb.Begin(element);

            // Make page visible
            element.Visibility = Visibility.Visible;

            // Wait for it to finish
            await Task.Delay((int)(seconds * 1000));
        }

        /// <summary>
        /// Slides a page out to the left
        /// </summary>
        /// <param name="element">The page to animate</param>
        /// <param name="seconds">The time the animation will take</param>
        /// <returns></returns>
        public static async Task SlideAndFadeOutToLeftAsync(this FrameworkElement element, float seconds)
        {
            // Create the storyboard
            var sb = new Storyboard();

            // Add slide from right animation
            sb.AddSlideToLeft(seconds, element.ActualWidth, keepMargin: false, decelerationRatio: 0f);

            // Add fade in animation
            sb.AddFadeOut(seconds);

            // Start animating
            sb.Begin(element);

            // Make page visible
            element.Visibility = Visibility.Visible;

            // Wait for it to finish
            await Task.Delay((int)(seconds * 1000));
        }

        /// <summary>
        /// Slides a page in from the left
        /// </summary>
        /// <param name="element">The page to animate</param>
        /// <param name="seconds">The time the animation will take</param>
        /// <returns></returns>
        public static async Task SlideAndFadeInFromLeftAsync(this FrameworkElement element, float seconds)
        {
            // Create the storyboard
            var sb = new Storyboard();

            // Add slide from right animation
            sb.AddSlideFromLeft(seconds, element.ActualWidth, keepMargin: false);

            // Add fade in animation
            sb.AddFadeIn(seconds);

            // Start animating
            sb.Begin(element);

            // Make page visible
            element.Visibility = Visibility.Visible;

            // Wait for it to finish
            await Task.Delay((int)(seconds * 1000));
        }

    }
}
