namespace QuoteFinder.Services;

using QuoteFinder.Infrastructure;
using QuoteFinder.Resources;

public class UserInputService(IUserInteractor userInteractor, IInputValidator inputValidator) : IUserInputService
{
    private readonly IUserInteractor _userInteractor = userInteractor;
    private readonly IInputValidator _inputValidator = inputValidator;

    public string GetSearchableWord()
    {
        _userInteractor.ShowMessage(QuoteFinderUserPrompts.AskForWordPrompt);

        while (true)
        {
            string input = _userInteractor.ReadInput().Trim();

            if (_inputValidator.IsValidSearchWord(input))
            {
                return input;
            }
            _userInteractor.ShowMessage(QuoteFinderUserPrompts.InvalidWordMessage);
        }
    }

    public int GetPageCount()
    {
        _userInteractor.ShowMessage(QuoteFinderUserPrompts.AskForPageCountPrompt);

        while (true)
        {
            string input = _userInteractor.ReadInput().Trim();

            if (_inputValidator.IsValidPageCount(input, out int pageCount))
            {
                return pageCount;
            }

            _userInteractor.ShowMessage(QuoteFinderUserPrompts.InvalidPageCountMessage);
        }
    }

    public int GetQuotesPerPage()
    {
        _userInteractor.ShowMessage(QuoteFinderUserPrompts.AskForQuotesCountPrompt);

        while (true)
        {
            string input = _userInteractor.ReadInput().Trim();

            if (_inputValidator.IsValidQuotesPerPage(input, out int quotesPerPage))
            {
                return quotesPerPage;
            }

            _userInteractor.ShowMessage(QuoteFinderUserPrompts.InvalidQuotesCountMessage);
        }
    }

    public bool GetParallelExecutionChoice()
    {
        _userInteractor.ShowMessage(QuoteFinderUserPrompts.AskForParallelExecutionChoicePrompt);

        while (true)
        {
            string input = _userInteractor.ReadInput().Trim();

            if (_inputValidator.IsValidParallelExecutionChoice(input, out bool parallelExecutionChoice))
            {
                return parallelExecutionChoice;
            }

            _userInteractor.ShowMessage(QuoteFinderUserPrompts.InvalidParallelExecutionChoiceMessage);
        }
    }
}
