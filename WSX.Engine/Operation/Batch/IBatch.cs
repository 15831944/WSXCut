using System.Threading;
using WSX.Engine.Utilities;

namespace WSX.Engine.Operation.Batch
{
    public interface IBatch
    {
        void MakeMachining(DataProvider data, CancellationToken token);
    }
}
