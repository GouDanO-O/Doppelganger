using GameFrame.World;
using Sirenix.OdinInspector;

namespace GameFrame.Config
{
    [LabelText("消耗物品")]
    public class SkillCondition_Item : SkillCondition
    {
        public override bool CheckCondition(WorldObj owner, int curSkillLevel = 1)
        {
            return true;
        }

        public override void ExcuteCondition(WorldObj owner, int curSkillLevel = 1)
        {
            
        }
    }
}