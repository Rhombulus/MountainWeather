﻿using System;
using NUnit.Framework;
using Silkweb.Mobile.Core.Tests.Mocks;

namespace Silkweb.Mobile.Core.Tests.ViewModels
{
    [TestFixture]
    public class ViewModelBaseFixture
    {
        [Test]
        public void HandlesPropertyChanged()
        {
            bool propertyChanged = false;
            var viewModel = new MockViewModel();

            viewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "Title" && viewModel.Title == "Test")
                    propertyChanged = true;
            };

            viewModel.Title = "Test";

            Assert.That(propertyChanged, Is.True);
        }

        [Test]
        public void SetsViewModelSet()
        {
            var viewModel = new MockViewModel();

            viewModel.SetState<MockViewModel>(x => x.Title = "Test");

            Assert.That(viewModel.Title, Is.EqualTo("Test"));
        }
    }
}

