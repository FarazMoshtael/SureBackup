using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SureBackup.Presentation.Controls
{
    /// <summary>
    /// Interaction logic for CustomBorderButton.xaml
    /// </summary>
    public partial class CustomBorderButton : UserControl
    {

        // 1️⃣ Register the dependency property
        public static readonly DependencyProperty BorderBackgroundProperty =
            DependencyProperty.Register(
                nameof(BorderBackground),      
                typeof(Brush),             
                typeof(CustomBorderButton));

        // 2️⃣ CLR wrapper
        public Brush BorderBackground
        {
            get => (Brush)GetValue(BorderBackgroundProperty);
            set => SetValue(BorderBackgroundProperty, value);
        }


        public static readonly DependencyProperty AvailabilityProperty =
    DependencyProperty.Register(
        nameof(Availability),
        typeof(bool),
        typeof(CustomBorderButton),
        new FrameworkPropertyMetadata(
            true,
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, // ✅ makes it bindable
            OnValueChanged));

        public bool Availability
        {
            get => (bool)GetValue(AvailabilityProperty);
            set => SetValue(AvailabilityProperty, value);
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (CustomBorderButton)d;
            bool isAvailable = (bool)e.NewValue;
            if (!isAvailable && control.IsVisible)
                control.Visibility=Visibility.Collapsed;
            else if(isAvailable && !control.IsVisible)
                control.Visibility = Visibility.Visible;

        }

        public static readonly DependencyProperty CommandProperty =
          DependencyProperty.Register(
              nameof(Command),
              typeof(ICommand),
              typeof(CustomBorderButton),
        new PropertyMetadata(null));

        //  CLR wrapper
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }


        public static readonly DependencyProperty FontContentProperty =
         DependencyProperty.Register(
             nameof(FontContent),
             typeof(string),
             typeof(CustomBorderButton));

        //  CLR wrapper
        public string FontContent
        {
            get => (string)GetValue(FontContentProperty);
            set => SetValue(FontContentProperty, value);
        }
        public static readonly DependencyProperty ButtonPaddingProperty =
       DependencyProperty.Register(
           nameof(ButtonPadding),
           typeof(int),
           typeof(CustomBorderButton));

        //  CLR wrapper
        public int ButtonPadding
        {
            get => (int)GetValue(ButtonPaddingProperty);
            set => SetValue(ButtonPaddingProperty, value);
        }




        public static readonly RoutedEvent ClickEvent =
       EventManager.RegisterRoutedEvent(
           "Click",
           RoutingStrategy.Bubble,
           typeof(RoutedEventHandler),
           typeof(CustomBorderButton));

        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        protected virtual void OnClick()
        {
            RaiseEvent(new RoutedEventArgs(ClickEvent));
        }



        public CustomBorderButton()
        {
            InitializeComponent();
            //DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OnClick();
        }
    }
}
