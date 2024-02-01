using System.Globalization;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Kahua.WeatherApp.Controls
{
    public sealed partial class LabeledTextBox : UserControl
    {
        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(
            nameof(Label), typeof(string), typeof(LabeledTextBox), new PropertyMetadata(default(string)));

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            nameof(Text), typeof(string), typeof(LabeledTextBox), new PropertyMetadata(default(string)));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public double? Min { get; set; }
        public double? Max { get; set; }
        public bool IsNumber { get; set; }

        public LabeledTextBox()
        {
            this.InitializeComponent();
        }


        private void TextBox_OnBeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = IsNumber && !string.IsNullOrWhiteSpace(args.NewText) && (!double.TryParse(args.NewText, out var number ) || number < Min || Max < number);
        }
    }
}