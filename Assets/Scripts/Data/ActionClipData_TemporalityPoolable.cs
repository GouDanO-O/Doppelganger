using GameFrame.World;
using QFramework;

namespace GameFrame
{
 	public class ActionClipData_TemporalityPoolable : TemporalityData_Pool
    {
        public WorldObj owner;

        public WorldObj[] triggerTargets;
        
        public bool IsRecycled { get; set; }

        public static ActionClipData_TemporalityPoolable Allocate()
        {
            return SafeObjectPool<ActionClipData_TemporalityPoolable>.Instance.Allocate();
        }

        public void SetOwner(WorldObj owner)
        {
            this.owner = owner;
        }

        public override void OnRecycled()
        {
            DeInitData();
        }
        
        public override void Recycle2Cache()
        {
            SafeObjectPool<ActionClipData_TemporalityPoolable>.Instance.Recycle(this);
        }

        public override void DeInitData()
        {
            owner = null;
        }
    }
}