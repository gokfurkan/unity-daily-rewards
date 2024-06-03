using DG.Tweening;
using TMPro;
using UnityEngine;

namespace DailyRewards_V1.Scripts.Core
{
    public class EconomyManager : Singleton<EconomyManager>
    {
        [SerializeField] private TextMeshProUGUI moneyText;
        [SerializeField] private RectTransform spawnPos;
        [SerializeField] private RectTransform targetPos;
        
        [Space(10)]
        [SerializeField] private bool useEconomyAnim;
        [SerializeField] private float economyAnimDuration;
        [SerializeField] private float spawnMoneyUIAmount;
        

        private int oldMoneyTarget, newMoneyTarget;
        
        private void OnEnable()
        {
            BusSystem.OnSetMoneys += SetMoneyText;
        }

        private void OnDisable()
        {
            BusSystem.OnSetMoneys -= SetMoneyText;
        }

        private void Start()
        {
            BusSystem.CallSetMoneys();
        }
        
        public void AddMoneys(int amount)
        {
            var oldAmount =  SaveManager.Instance.saveData.moneys;
            var newAmount = oldAmount + amount;

            oldMoneyTarget = oldAmount;
            newMoneyTarget = newAmount;
            
            SaveManager.Instance.saveData.moneys = newAmount;
            SaveManager.Instance.Save();
            
            BusSystem.CallSetMoneys();
        }

        public void ResetMoneys()
        {
            SaveManager.Instance.saveData.moneys = 0;
            SaveManager.Instance.Save();

            BusSystem.CallSetMoneys();
        }

        private void SetMoneyText()
        {
            if (oldMoneyTarget == 0 || newMoneyTarget == 0)
            {
                oldMoneyTarget = 0;
                newMoneyTarget = SaveManager.Instance.saveData.moneys;
            }
            
            if (useEconomyAnim)
            {
                AnimateMoneyText(oldMoneyTarget, newMoneyTarget);
            }
            else
            {
                moneyText.text = MoneyCalculator.NumberToStringFormatter(newMoneyTarget);
            }
            
            // BusSystem.CallRefreshUpgradeValues();
        }
        
        public void SpawnMoneys(RectTransform spawn , float scale = 80)
        {
            if (spawn == null)
            {
                spawn = spawnPos;
            }
            
            for (int i = 0; i < spawnMoneyUIAmount; i++)
            {
                var money = ObjectPooling.Instance.poolObjects[(int)ObjectPoolType.MoneyUI].GetItem();
                var moneyController = money.GetComponent<Money>();
                money.GetComponent<RectTransform>().sizeDelta = new Vector2(scale , scale);
                moneyController.InitMoney(targetPos , spawn);
            }
        }
            
        private void AnimateMoneyText(int startAmount, int targetAmount)
        {
            DOTween.To(() => startAmount, x => startAmount = x, targetAmount,economyAnimDuration)
                .OnUpdate(() => moneyText.text = MoneyCalculator.NumberToStringFormatter(startAmount))
                .SetEase(Ease.Linear)
                .SetUpdate(true)
                .SetSpeedBased(false);
        }
    }
}