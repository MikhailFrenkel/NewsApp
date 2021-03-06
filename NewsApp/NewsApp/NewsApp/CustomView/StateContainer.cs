﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NewsApp
{
    [ContentProperty("Conditions")]
    public class StateContainer : ContentView
    {
        /// <summary>
        /// List of conditions.
        /// </summary>
        public List<StateCondition> Conditions { get; set; } = new List<StateCondition>();

        public static readonly BindableProperty StateProperty = BindableProperty.Create(nameof(State), typeof(object), 
            typeof(StateContainer), null, BindingMode.Default, null, StateChanged);

        public static void Init()
        {
        }

        private static async void StateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is StateContainer parent)
                await parent.ChooseStateProperty(newValue);
        }

        /// <summary>
        /// Application state.
        /// </summary>
        public object State
        {
            get => GetValue(StateProperty);
            set => SetValue(StateProperty, value);
        }

        private async Task ChooseStateProperty(object newValue)
        {
            if (Conditions != null && Conditions?.Count == 0) return;
            try
            {
                foreach (var stateCondition in Conditions.Where(stateCondition => stateCondition.Is != null 
                                                                && stateCondition.Is.ToString().Equals(newValue.ToString())))
                {
                    if (Content != null)
                    {
                        await Content.FadeTo(0, 100U);
                        Content.IsVisible = false;

                        await Task.Delay(30);
                    }

                    stateCondition.Content.Opacity = 0;
                    Content = stateCondition.Content;
                    Content.IsVisible = true;
                    await Content.FadeTo(1);
                    break;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"StateContainer ChooseStateProperty {newValue} error: {e}");
            }
        }
    }
}
