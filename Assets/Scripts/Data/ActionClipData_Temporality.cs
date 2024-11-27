using GameFrame.World;
using QFramework;

namespace GameFrame
{
 	public class ActionClipData_Temporality : TemporalityData_Pool
    {
        public WorldObj owner;

        public WorldObj[] triggerTargets;
        
        public bool IsRecycled { get; set; }

        public static ActionClipData_Temporality Allocate()
        {
            return SafeObjectPool<ActionClipData_Temporality>.Instance.Allocate();
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
            SafeObjectPool<ActionClipData_Temporality>.Instance.Recycle(this);
        }

        public override void DeInitData()
        {
            owner = null;
        }
    }
}