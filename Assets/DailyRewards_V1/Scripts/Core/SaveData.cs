using System;
using System.Collections.Generic;
using UnityEngine;

namespace DailyRewards_V1.Scripts.Core
{
    [Serializable]
    public class SaveData
    {
        public int moneys;
        
        [Header("Daily Rewards")]
        public int lastRewardClaimIndex;
        public int firstSetRewardUnlockStatus = 0;
        public string lastRewardClaimDate;
        public List<int> rewardsUnlockStatus = new List<int>(7);
    }
}