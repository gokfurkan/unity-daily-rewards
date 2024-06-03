using UnityEngine;

namespace DailyRewards_V1.Scripts.DailyReward
{
    [CreateAssetMenu(fileName = "DailyRewardOptions", menuName = "ScriptableObjects/DailyRewardOptions")]
    public class DailyRewardOptions : ScriptableObject
    {
        public float perCheckClaimRewardLeft;
        public float rewardLoopHours;
        public float rewardSpawnUISize;
    }
}