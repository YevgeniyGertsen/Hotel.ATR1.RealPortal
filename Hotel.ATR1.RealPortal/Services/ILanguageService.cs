using Hotel.ATR1.RealPortal.Models;

namespace Hotel.ATR1.RealPortal.Services
{
    public interface ILanguageService
    {
        IEnumerable<Language> GetLanguages();
        Language GetLanguageByCulture(string culture);
    }

}
