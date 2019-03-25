using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Relish.Views.CustomComponents
{
    /// <summary>
    /// Behaviour for binding ListViewTapped events to an ICommand in the ViewModel.
    /// </summary>
    public class ListViewTappedBehaviour : Behavior<ListView>
    {
        public static readonly BindableProperty CommandProperty = 
            BindableProperty.Create(
                nameof(Command),
                typeof(ICommand),
                typeof(ListViewTappedBehaviour));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public ListView AssociatedObject { get; private set; }

        protected override void OnAttachedTo(ListView bindable)
        {
            base.OnAttachedTo(bindable);
            AssociatedObject = bindable;
            bindable.BindingContextChanged += OnBindingContextChanged;
            bindable.ItemTapped += OnListViewItemTapped;
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.BindingContextChanged -= OnBindingContextChanged;
            bindable.ItemTapped -= OnListViewItemTapped;
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

        private void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (Command == null)
            {
                return;
            }

            if (Command.CanExecute(e.Item))
            {
                Command.Execute(e.Item);
            }
        }
    }
}
