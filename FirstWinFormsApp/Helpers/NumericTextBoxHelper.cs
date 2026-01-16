namespace FirstWinFormsApp.Helpers;

public static class NumericTextBoxHelper
{
    public static void AttachNumericValidation(TextBox textBox)
    {
        textBox.KeyPress += OnKeyPress;
        textBox.Click += OnClick;
        textBox.TextChanged += OnTextChanged;
    }

    public static void DetachNumericValidation(TextBox textBox)
    {
        textBox.KeyPress -= OnKeyPress;
        textBox.Click -= OnClick;
        textBox.TextChanged -= OnTextChanged;
    }

    private static void OnKeyPress(object? sender, KeyPressEventArgs e)
    {
        if (sender is not TextBox textBox)
            return;

        // Don't adjust caret for control characters (Ctrl+C, Ctrl+V, etc.)
        if (!char.IsControl(e.KeyChar))
            EnsureCaretAfterMinus(textBox);

        e.Handled = !HandleNumericInput(textBox, e.KeyChar);
    }

    private static void OnClick(object? sender, EventArgs e)
    {
        if (sender is TextBox textBox)
            EnsureCaretAfterMinus(textBox);
    }

    private static void OnTextChanged(object? sender, EventArgs e)
    {
        if (sender is not TextBox textBox)
            return;

        var sanitized = SanitizeNumericInput(textBox.Text);
        if (sanitized != textBox.Text)
        {
            var caretPos = textBox.SelectionStart;
            textBox.Text = sanitized;
            textBox.SelectionStart = Math.Min(caretPos, sanitized.Length);
        }
    }

    private static string SanitizeNumericInput(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        var hasLeadingMinus = false;
        var digits = new List<char>();

        foreach (var c in input)
        {
            if (c == '-' && !hasLeadingMinus && digits.Count == 0)
                hasLeadingMinus = true;
            else if (char.IsDigit(c))
                digits.Add(c);
        }

        return hasLeadingMinus ? "-" + new string([.. digits]) : new string([.. digits]);
    }

    private static void EnsureCaretAfterMinus(TextBox textBox)
    {
        if (textBox.Text.StartsWith('-') && textBox.SelectionStart == 0)
            textBox.SelectionStart = 1;
    }

    private static bool HandleNumericInput(TextBox textBox, char keyChar)
    {
        if (char.IsControl(keyChar))
            return true;

        if (char.IsDigit(keyChar))
            return true;

        if (keyChar == '-')
            return HandleMinusSign(textBox);

        return false;
    }

    private static bool HandleMinusSign(TextBox textBox)
    {
        if (textBox.Text.StartsWith('-'))
        {
            var caretPos = textBox.SelectionStart;
            textBox.Text = textBox.Text[1..];
            textBox.SelectionStart = Math.Max(0, caretPos - 1);
            return false;
        }

        var pos = textBox.SelectionStart;
        textBox.Text = "-" + textBox.Text;
        textBox.SelectionStart = pos + 1;
        return false;
    }
}
