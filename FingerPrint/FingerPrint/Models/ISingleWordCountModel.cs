namespace FingerPrint.Models
{
    public interface ISingleWordCountModel
    {
        int Length();
        int[] Counts();
        SingleWordCountModel Copy();
        int this[int i] { get; set; }
    }
}