using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Kahua.WeatherApp.Controls;

public sealed partial class LabeledTextBox : UserControl
{
    public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(
        nameof(Label), typeof(string), typeof(LabeledTextBox), new PropertyMetadata(default(string)));

    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        nameof(Text), typeof(string), typeof(LabeledTextBox), new PropertyMetadata(default(string)));

    public LabeledTextBox()
    {
        InitializeComponent();
    }

    public string Label
    {
        get => (string)GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public double? Min { get; set; }
    public double? Max { get; set; }
    public bool IsNumber { get; set; }


    private void TextBox_OnBeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
    {
        if (args.NewText == "-") return;
        args.Cancel = IsNumber && !string.IsNullOrWhiteSpace(args.NewText) &&
                      (!double.TryParse(args.NewText, out var number) || number < Min || Max < number);
    }

    private void UIElement_OnLostFocus(object sender, RoutedEventArgs e)
    {
        if (IsNumber && sender is TextBox tb && (tb.Text == "-" || string.IsNullOrWhiteSpace(tb.Text))) tb.Text = "0";
    }
}