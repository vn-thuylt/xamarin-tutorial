﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LoginForm.Source.Views.AnimationPages.Basic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RelativeScaleAnimationPage : ContentPage
    {
        public RelativeScaleAnimationPage() => InitializeComponent();

        void SetIsEnabledButtonState(bool startButtonState, bool cancelButtonState)
        {
            startButton.IsEnabled = startButtonState;
            cancelButton.IsEnabled = cancelButtonState;
        }

        async void OnStartAnimationButtonClicked(object sender, EventArgs e)
        {
            SetIsEnabledButtonState(false, true);
            await image.RelScaleTo(2, 2000);
            SetIsEnabledButtonState(true, false);
        }

        void OnCancelAnimationButtonClicked(object sender, EventArgs e)
        {
            ViewExtensions.CancelAnimations(image);
            SetIsEnabledButtonState(true, false);
        }
    }
}