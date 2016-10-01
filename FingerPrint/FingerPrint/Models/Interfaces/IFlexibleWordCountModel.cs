namespace FingerPrint.Models
{
    public interface IFlexibleWordCountModel
    {
        int Length();
        ISingleWordCountModel CountsWithoutQuotes();
        ISingleWordCountModel CountsWithQuotes();
        FlexibleWordCountModel Copy();
    }
}