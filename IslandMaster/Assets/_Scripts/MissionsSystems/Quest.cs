using System;
using _Scripts.CharacterCore;
using _Scripts.EnemyCore;
using _Scripts.InventorySystem;
using UnityEngine;

namespace _Scripts.MissionsSystems
{
    public class Quest : MonoBehaviour
    {
        private QuestInfo _questInfo;
        private int _questCurrentNumber;
        private bool _questStarted;
        
        public QuestInfo QuestInfo => _questInfo;

        [SerializeField] private PlayerBalance playerBalance;
        
        public static event Action QuestFinished;

        private void OnEnable()
        {
            InventoryItemBase.QuestObjective += UpdateQuest;
            Enemy.QuestObjective += UpdateQuest;
        }

        private void OnDisable()
        {
            InventoryItemBase.QuestObjective -= UpdateQuest;
            Enemy.QuestObjective -= UpdateQuest;
        }

        public void StartQuest(QuestInfo questInfo)
        {
            _questInfo = questInfo;
            _questStarted = true;
        }

        private void UpdateQuest(string objectiveSend)
        {
            if(!_questStarted) return;
            
            if(objectiveSend != _questInfo.questObjective) return;
            
            _questCurrentNumber += 1;
            
            if(_questCurrentNumber >= _questInfo.questNumberToFinish)
                FinishQuest();
        }

        private void FinishQuest()
        {
            playerBalance.UpdateBalance(_questInfo.rewardAmount);
            playerBalance.IncrementQuest();
            QuestFinished?.Invoke();
            ResetQuest();
        }

        private void ResetQuest()
        {
            _questStarted = false;
            _questCurrentNumber = 0;
        }
    }
}