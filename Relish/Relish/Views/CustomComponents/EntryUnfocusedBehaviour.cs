using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Relish.Views.CustomComponents
{
    /// <summary>
    /// Behaviour for binding Picker Unfocused events to an ICommand in the ViewModel.
    /// </summary>
    public class EntryUnfocusedBehaviour : Behavior<Entry>
    {
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(
                nameof(Command),
                typeof(ICommand),
                typeof(EntryUnfocusedBehaviour));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public Entry AssociatedObject { get; private set; }

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            AssociatedObject = bindable;
            bindable.BindingContextChanged += OnBindingContextChanged;
            bindable.Unfocused += OnEntryUnfocused;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.BindingContextChanged -= OnBindingContextChanged;
            bindable.Unfocused -= OnEntryUnfocused;
            AssociatedObject = null;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = AssociatedObject.BindingContext;
        }

        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }

        private void OnEntryUnfocused(object sender, FocusEventArgs e)
        {
            Command?.Execute(null);
        }
    }
}
