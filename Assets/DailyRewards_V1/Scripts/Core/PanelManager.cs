using System.Collections.Generic;
using System.Linq;
using DailyRewards_V1.Scripts.DailyReward;
using UnityEngine;

namespace DailyRewards_V1.Scripts.Core
{
    public class PanelManager : Singleton<PanelManager>
    {
        private List<PanelTypeHolder> allPanels = new List<PanelTypeHolder>();

        protected override void Initialize()
        {
            base.Initialize();
            
            InitializePanelSystem();
        }

        private void InitializePanelSystem()
        {
            GetAllPanels();
            ActivateMenuPanel();
        }

        private void ActivateMenuPanel()
        {
            DisableAll();
            
            Activate(PanelType.Economy);
            Activate(PanelType.OpenDailyRewards);
        }
        
        public void ChangeDailyRewardsPanelEnabled(bool status)
        {
            DailyRewardManager.Instance.RefreshDateTime();
            DailyRewardManager.Instance.HasOpenRewardPanel = status;
            
            Activate(PanelType.OpenDailyRewards , !status);
            Activate(PanelType.DailyRewards , status);
        }
        
        private void Activate(PanelType panelType, bool activate = true)
        {
            List<PanelTypeHolder> panels = FindPanels(panelType);

            if (panels != null)
            {
                for (int i = 0; i < panels.Count; i++)
                {
                    panels[i].gameObject.SetActive(activate);
                }
            }
            else
            {
                Debug.LogWarning("Panel not found: " + panelType.ToString());
            }
        }
        
        private void DisableAll()
        {
            foreach (var panel in allPanels)
            {
                panel.gameObject.SetActive(false);
            }
        }
        
        private List<PanelTypeHolder> FindPanels(PanelType panelType)
        {
            return allPanels.FindAll(panel => panel.panelType == panelType);
        }
        
        private void GetAllPanels()
        {
            allPanels = transform.root.GetComponentsInChildren<PanelTypeHolder>(true).ToList();
        }
    }
}