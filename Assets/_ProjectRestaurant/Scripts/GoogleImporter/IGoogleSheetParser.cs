using JetBrains.Annotations;

namespace GoogleSpreadsheets
{
    public interface IGoogleSheetParser
    {
        //public string NameSheet { get; }

        public void Parse([CanBeNull] string header, string token);
    }
}