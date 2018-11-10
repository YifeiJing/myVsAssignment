using System;
using System.Windows;

namespace Example1
{
    /// <summary>
    /// Base attached property for all element animations
    /// </summary>
    /// <typeparam name="Parent"></typeparam>
    public abstract class BaseAnimationAttachedProperty<Parent> : BaseAttachedProperty<Parent, bool>
        where Parent : BaseAttachedProperty<Parent, bool>, new()
    {

        public bool FirstLoad { get; set; } = true;

        public override void OnValueUpdated(DependencyObject sender, object value)
        {
            if (!(sender is FrameworkElement element))
                return;

            if (sender.GetValue(ValueProperty) == value && !FirstLoad)
                return;

            if (FirstLoad)
            {
                RoutedEventHandler onLoad = null;
                onLoad = (s, e) =>
                {
                    (s as FrameworkElement).Loaded -= onLoad;
                    OnAnimation(element, (bool)value, FirstLoad);
                    FirstLoad = false;
                };
                element.Loaded += onLoad;
                
            }
            else
            {
                OnAnimation(element, (bool)value, FirstLoad);
            }
        }

        public virtual void OnAnimation(FrameworkElement element, bool value, bool Firstload) {}
    }


    /// <summary>
    /// The attached property on legend list, which controls the slide in and slide out
    /// </summary>
    public class LegendListAnimationAttachedProperty : BaseAnimationAttachedProperty<LegendListAnimationAttachedProperty>
    {
        public override async void OnAnimation(FrameworkElement element, bool value, bool FirstLoad)
        {
            if(value)
            {
                await element.SlideAndFadeInFromLeftAsync(FirstLoad? 0f : 0.9f);
            }
            else
            {
                await element.SlideAndFadeOutToLeftAsync(FirstLoad ? 0f : 0.9f);
            }
        }
    }
}
