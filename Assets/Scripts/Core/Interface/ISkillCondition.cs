using GameFrame.World;

namespace GameFrame
{
    public interface ISkillCondition
    {
        public bool CheckCondition(WorldObj owner, int curSkillLevel = 1);
        
        public void ExcuteCondition(WorldObj owner, int curSkillLevel = 1);
    }
}