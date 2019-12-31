﻿using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System.Collections.Generic;

namespace PrismFrameworkApps.src._01_HelloPrism.ViewModels
{
    public class ViewModelBase : BindableBase, IInitialize, INavigatedAware
    {
        protected INavigationService _navigationService { get; }

        public string Title { get; }

        public DelegateCommand GoHomeCommand { get; }

        public DelegateCommand BackToMenuCommand { get; }

        public DelegateCommand<string> NavigateCommand { get; }

        private IEnumerable<string> messages;

        private int initializedCount;

        private int onNavigatedFromCount;

        private int onNavigatedToCount;

        // Creating Accessors (Getters and Setters)
        public IEnumerable<string> Messages
        {
            get => messages;
            set => SetProperty(ref messages, value);
        }

        public int InitializedCount
        {
            get => initializedCount;
            set => SetProperty(ref initializedCount, value, UpdateMessage);
        }

        public int OnNavigatedFromCount
        {
            get => onNavigatedFromCount;
            set => SetProperty(ref onNavigatedFromCount, value, UpdateMessage);
        }

        public int OnNavigatedToCount
        {
            get => onNavigatedToCount;
            set => SetProperty(ref onNavigatedToCount, value, UpdateMessage);
        }

        void UpdateMessage()
        {
            Messages = new[]
            {
                $"Initialized Called: {InitializedCount} time(s)",
                $"OnNavigatedFrom Called: {OnNavigatedFromCount} time(s)",
                $"OnNavigatedTo Called: {OnNavigatedToCount} time(s)"
            };
        }

        // Constructor
        public ViewModelBase(INavigationService navigationService)
        {
            _navigationService = navigationService;

            Title = GetType().Name.Replace("ViewModel", string.Empty);

            GoHomeCommand = new DelegateCommand(OnGoHomeCommandExecuted);
            BackToMenuCommand = new DelegateCommand(OnBackToMenuCommandExecuted);
            NavigateCommand = new DelegateCommand<string>(OnNavigateCommandExecuted);
        }

        async void OnNavigateCommandExecuted(string path)
        {
            // _navigationService.NavigateAsync("ViewB", useModalNavigation: true);
            // The statement above equivalent to CommandParameter Property's Value as "ViewB?useModalNavigation=True".

            var result = await _navigationService.NavigateAsync(path);

            if (!result.Success)
            {
                System.Diagnostics.Debugger.Break();
            }
        }

        async void OnBackToMenuCommandExecuted()
        {
            var result = await _navigationService.NavigateAsync("/HomePageView");

            if (!result.Success)
            {
                System.Diagnostics.Debugger.Break();
            }
        }

        async void OnGoHomeCommandExecuted()
        {
            var result = await _navigationService.NavigateAsync("/HelloPrismMainPage");

            if (!result.Success)
            {
                System.Diagnostics.Debugger.Break();
            }
        }

        public void Initialize(INavigationParameters parameters) => InitializedCount++;

        public void OnNavigatedFrom(INavigationParameters parameters) => OnNavigatedFromCount++;

        public void OnNavigatedTo(INavigationParameters parameters) => OnNavigatedToCount++;
    }
}