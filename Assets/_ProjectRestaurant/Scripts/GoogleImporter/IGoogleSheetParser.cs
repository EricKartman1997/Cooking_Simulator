using JetBrains.Annotations;

namespace GoogleSpreadsheets
{
    public interface IGoogleSheetParser
    {
        public void Save();
        public void Parse([CanBeNull] string header, string token);
    }
}