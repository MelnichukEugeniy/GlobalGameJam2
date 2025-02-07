using System.Collections.Generic;
using Systems.Persistence;
using UnityEngine;

namespace BreakdownMechanic.Scripts.Malfunctions.Vents.Fan
{
    public class FanController : MonoBehaviour, IInteractable, ILookInteractable, IBind<FanOverpoweredMalfunction.FanData>
    {
        [SerializeField]
        private FanOverpoweredMalfunctionConfig config;

        [SerializeField]
        private List<FixAction> fixActions;

        private void Awake()
        {
            
        }

        public void Interact()
        {
            FixSelectionsListWidget.Instance.PerformSelectedAction();
        }

        public void OnLookEnter()
        {
            FixSelectionsListWidget.Instance.ShowSelectionsList(fixActions);
        }

        public void OnLookExit()
        {
            FixSelectionsListWidget.Instance.Hide();
        }

        public SerializableGuid Id { get; set; }
        private FanOverpoweredMalfunction.FanData data;
        public void Bind(FanOverpoweredMalfunction.FanData data)
        {
            this.data = data;
            Id = data.Id;
        }
    }
}