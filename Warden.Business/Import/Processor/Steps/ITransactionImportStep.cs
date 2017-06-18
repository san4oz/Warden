namespace Warden.Business.Import.Processor.Steps
{
    public interface ITransactionImportStep
    {
        void Execute(TransactionImportContext context);
    }
}
