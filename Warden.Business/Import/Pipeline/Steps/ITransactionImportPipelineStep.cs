namespace Warden.Business.Import.Pipeline.Steps
{
    public interface ITransactionImportPipelineStep
    {
        void Execute(TransactionImportPipelineContext context);
    }
}
