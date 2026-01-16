using FirstWinFormsApp.Models;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace FirstWinFormsApp.ViewModels;

public class MainFormViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private readonly TypeSelector _typeSelector;

    private string _minValueText = string.Empty;
    private string _maxValueText = string.Empty;
    private bool _integralOnly = true;
    private bool _requiresPrecision;
    private string _suggestion = "Not enough data";
    private bool _isRangeValid = true;
    private bool _isPrecisionVisible;

    public string MinValueText
    {
        get => _minValueText;
        set => SetField(ref _minValueText, value);
    }

    public string MaxValueText
    {
        get => _maxValueText;
        set => SetField(ref _maxValueText, value);
    }

    public bool IntegralOnly
    {
        get => _integralOnly;
        set
        {
            if (SetField(ref _integralOnly, value))
            {
                // When integral only is set, reset precision
                if (value)
                    RequiresPrecision = false;

                UpdateDerivedState();
            }
        }
    }

    public bool RequiresPrecision
    {
        get => _requiresPrecision;
        set => SetField(ref _requiresPrecision, value);
    }

    public string Suggestion
    {
        get => _suggestion;
        private set => SetField(ref _suggestion, value);
    }

    public bool IsRangeValid
    {
        get => _isRangeValid;
        private set => SetField(ref _isRangeValid, value);
    }

    public bool IsPrecisionVisible
    {
        get => _isPrecisionVisible;
        private set => SetField(ref _isPrecisionVisible, value);
    }

    public MainFormViewModel(ITypeSelectorStrategy? strategy = null)
    {
        _typeSelector = new TypeSelector(strategy);
        PropertyChanged += OnPropertyChanged;
        UpdateDerivedState();
    }

    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        // Recalculate when input properties change
        if (e.PropertyName is nameof(MinValueText) or nameof(MaxValueText) or nameof(IntegralOnly) or nameof(RequiresPrecision))
        {
            Recalculate();
        }
    }

    private void UpdateDerivedState()
    {
        IsPrecisionVisible = !IntegralOnly;
    }

    private void Recalculate()
    {
        ValidateRange();
        UpdateSuggestion();
    }

    private void ValidateRange()
    {
        if (string.IsNullOrEmpty(MinValueText) || string.IsNullOrEmpty(MaxValueText))
        {
            IsRangeValid = true;
            return;
        }

        if (MinValueText == "-" || MaxValueText == "-")
        {
            IsRangeValid = true;
            return;
        }

        if (!BigInteger.TryParse(MinValueText, out var minValue) ||
            !BigInteger.TryParse(MaxValueText, out var maxValue))
        {
            IsRangeValid = true;
            return;
        }

        IsRangeValid = maxValue >= minValue;
    }

    private void UpdateSuggestion()
    {
        if (!TryGetValidRange(out var minValue, out var maxValue))
        {
            Suggestion = "Not enough data";
            return;
        }

        var type = _typeSelector.GetBestType(
            minValue,
            maxValue,
            IntegralOnly,
            RequiresPrecision);

        Suggestion = type?.Name ?? "No suitable type";
    }

    private bool TryGetValidRange(out BigInteger minValue, out BigInteger maxValue)
    {
        minValue = default;
        maxValue = default;

        if (string.IsNullOrEmpty(MinValueText) || string.IsNullOrEmpty(MaxValueText))
            return false;

        if (!BigInteger.TryParse(MinValueText, out minValue) ||
            !BigInteger.TryParse(MaxValueText, out maxValue))
            return false;

        return maxValue >= minValue;
    }

    private bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
            return false;

        field = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        return true;
    }
}
