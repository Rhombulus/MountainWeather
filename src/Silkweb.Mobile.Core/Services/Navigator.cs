﻿using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using Silkweb.Mobile.Core.Factories;
using Silkweb.Mobile.Core.ViewModels;
using Autofac;
using Silkweb.Mobile.Core.Interfaces;

namespace Silkweb.Mobile.Core.Services
{
    public class Navigator : INavigator
    {
        private readonly Func<IPage> _pageResolver;
        private readonly IViewFactory _viewFactory;

        public Navigator(Func<IPage> pageResolver, IViewFactory viewFactory)
        {
            _pageResolver = pageResolver;
            _viewFactory = viewFactory;
        }

        private INavigation Navigation
        {
            get { return _pageResolver().Navigation; }
        }

        public async Task<IViewModel> PopAsync()
        {
            Page view = await Navigation.PopAsync();
            return view.BindingContext as IViewModel;
        }

        public async Task<IViewModel> PopModalAsync()
        {
            Page view = await Navigation.PopAsync();
            return view.BindingContext as IViewModel;
        }

        public async Task PopToRootAsync()
        {
            await Navigation.PopToRootAsync();
        }

        public async Task<TViewModel> PushAsync<TViewModel>(Action<TViewModel> setStateAction = null) 
            where TViewModel : class, IViewModel
        {
            TViewModel viewModel;
            var view = _viewFactory.Resolve<TViewModel>(out viewModel, setStateAction);
            await Navigation.PushAsync(view);
            return viewModel;
        }

        public async Task<TViewModel> PushAsync<TViewModel>(TViewModel viewModel) 
            where TViewModel : class, IViewModel
        {
            var view = _viewFactory.Resolve(viewModel);
            await Navigation.PushAsync(view);
            return viewModel;
        }

        public async Task<TViewModel> PushModalAsync<TViewModel>(Action<TViewModel> setStateAction = null) 
            where TViewModel : class, IViewModel
        {
            TViewModel viewModel;
            var view = _viewFactory.Resolve<TViewModel>(out viewModel, setStateAction);
            await Navigation.PushModalAsync(view);
            return viewModel;
        }

        public async Task<TViewModel> PushModalAsync<TViewModel>(TViewModel viewModel) 
            where TViewModel : class, IViewModel
        {
            var view = _viewFactory.Resolve(viewModel);
            await Navigation.PushModalAsync(view);
            return viewModel;
        }
    }
}

