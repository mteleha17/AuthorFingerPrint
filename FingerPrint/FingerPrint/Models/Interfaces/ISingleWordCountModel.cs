namespace FingerPrint.Models
{
    public interface ISingleWordCountModel
    {
        int Length();
        int[] Counts();
        ISingleWordCountModel Copy();
        int this[int i] { get; set; }
    }
}