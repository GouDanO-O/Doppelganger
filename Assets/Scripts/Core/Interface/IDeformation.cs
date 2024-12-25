using GameFrame.Config;

namespace GameFrame
{
    /// <summary>
    /// 变形
    /// </summary>
    public interface IDeformation
    {
        /// <summary>
        /// 仅变化外貌,技能还是本体的技能
        /// </summary>
        /// <param name="deformationData"></param>
        void TriggerDeformationAppearance(WorldObjData_Config deformationData);
        
        /// <summary>
        /// 完全变形,技能和外貌都以新物体为准
        /// </summary>
        /// <param name="deformationData"></param>
        void TriggerDeformationCompletely(WorldObjData_Config deformationData);
    }
}