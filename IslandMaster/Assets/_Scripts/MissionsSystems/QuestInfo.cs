using UnityEngine;

namespace _Scripts.MissionsSystems
{
    [CreateAssetMenu(fileName = "New QuestInfo", menuName = "QuestInfo")]
    public class QuestInfo : ScriptableObject
    {
        public string questObjective;
        public string questText;
        public int rewardAmount;
        public int questNumberToFinish;
    }
}