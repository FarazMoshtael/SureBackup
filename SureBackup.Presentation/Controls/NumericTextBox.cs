
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace SureBackup.Presentation.Controls;
public class NumericTextBox : TextBox
{
    //private static readonly Regex _regex = new Regex("[0-9]+");

    public NumericTextBox()
    {
        this.TextChanged += (sender, arg) =>
        {
            OnTextChanged(arg);
        };

    }
    protected override void OnPreviewTextInput(TextCompositionEventArgs e)
    {
        // e.Handled = _regex.IsMatch(e.Text);
        base.OnPreviewTextInput(e);
    }

    protected override void OnTextChanged(TextChangedEventArgs e)
    {
        if (!double.TryParse(Text, out _))
        {
            Text = "0";
            CaretIndex = Text.Length;
        }
    }

}

