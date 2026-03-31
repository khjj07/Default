namespace KCoreKit
{
    public abstract class LocalizedDataTableRowBase<T> : DataTableRowBase
    {
        public abstract T Get(Language language);
    }
}