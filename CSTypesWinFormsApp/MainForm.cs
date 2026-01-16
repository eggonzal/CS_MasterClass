using FirstWinFormsApp.Helpers;
using FirstWinFormsApp.ViewModels;

namespace FirstWinFormsApp
{
    public partial class MainForm : Form
    {
        private readonly Color _defaultBackColor;
        private readonly MainFormViewModel _viewModel;

        public MainForm()
        {
            InitializeComponent();

            _defaultBackColor = MaxValueTextBox.BackColor;
            _viewModel = new MainFormViewModel();

            // Attach numeric validation to textboxes
            NumericTextBoxHelper.AttachNumericValidation(MinValueTextBox);
            NumericTextBoxHelper.AttachNumericValidation(MaxValueTextBox);

            // Bind UI to ViewModel
            BindControls();

            // React to ViewModel changes
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void BindControls()
        {
            // UI ? ViewModel (user input)
            MinValueTextBox.TextChanged += (s, e) => _viewModel.MinValueText = MinValueTextBox.Text;
            MaxValueTextBox.TextChanged += (s, e) => _viewModel.MaxValueText = MaxValueTextBox.Text;
            IntegralOnlyCheckBox.CheckedChanged += (s, e) => _viewModel.IntegralOnly = IntegralOnlyCheckBox.Checked;
            PrecisionRequiredCheckBox.CheckedChanged += (s, e) => _viewModel.RequiresPrecision = PrecisionRequiredCheckBox.Checked;

            // Initialize ViewModel with current UI state
            _viewModel.IntegralOnly = IntegralOnlyCheckBox.Checked;

            // Sync UI with ViewModel initial state
            SyncUiWithViewModel();
        }

        private void SyncUiWithViewModel()
        {
            SuggestionLabel.Text = _viewModel.Suggestion;
            MaxValueTextBox.BackColor = _viewModel.IsRangeValid ? _defaultBackColor : Color.LightCoral;
            PrecisionRequiredCheckBox.Visible = _viewModel.IsPrecisionVisible;
            PrecisionRequiredCheckBox.Checked = _viewModel.RequiresPrecision;
        }

        private void OnViewModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // ViewModel ? UI (state changes)
            switch (e.PropertyName)
            {
                case nameof(MainFormViewModel.Suggestion):
                    SuggestionLabel.Text = _viewModel.Suggestion;
                    break;

                case nameof(MainFormViewModel.IsRangeValid):
                    MaxValueTextBox.BackColor = _viewModel.IsRangeValid ? _defaultBackColor : Color.LightCoral;
                    break;

                case nameof(MainFormViewModel.IsPrecisionVisible):
                    PrecisionRequiredCheckBox.Visible = _viewModel.IsPrecisionVisible;
                    break;

                case nameof(MainFormViewModel.RequiresPrecision):
                    if (PrecisionRequiredCheckBox.Checked != _viewModel.RequiresPrecision)
                        PrecisionRequiredCheckBox.Checked = _viewModel.RequiresPrecision;
                    break;
            }
        }
    }
}
