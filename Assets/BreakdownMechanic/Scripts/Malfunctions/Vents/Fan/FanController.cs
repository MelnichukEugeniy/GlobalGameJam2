using System.Collections.Generic;
using Systems.Persistence;
using UnityEngine;

namespace BreakdownMechanic.Scripts.Malfunctions.Vents.Fan
{
    public class FanController : MonoBehaviour, IInteractable, ILookInteractable, IBind<FanOverpoweredMalfunction.FanData>
    {
        [SerializeField]
        private UndergroundLocalVolume undergroundLocalVolume;
        [SerializeField]
        private FanOverpoweredMalfunction malfunction;

        [SerializeField]
        private List<FixAction> fixActions;

        [SerializeField]
        private string workingKeyName = "Working";
        private int workingKey;
        [SerializeField]
        private Animator animator;

        private List<FixAction> runtimeFixActions;
        
        private void Awake()
        {
            workingKey = Animator.StringToHash(workingKeyName);
            runtimeFixActions = new List<FixAction>(fixActions);
        }

        public void Interact()
        {
            var performedAction = FixSelectionsListWidget.Instance.PerformSelectedAction();
            if (performedAction is EnableEngineCoolingAction)
            {
                runtimeFixActions.Remove(performedAction);
                if(FixSelectionsListWidget.Instance.IsActive)
                    FixSelectionsListWidget.Instance.ShowSelectionsList(runtimeFixActions);
                return;
            }

            if (performedAction is RebootFanAction)
            {
                if (runtimeFixActions.Exists(x => x is EnableEngineCoolingAction))
                    return;
                
                runtimeFixActions.Clear();
                runtimeFixActions.AddRange(fixActions);
                malfunction.Fix();
                FixSelectionsListWidget.Instance.Hide();
            }
        }

        public void OnLookEnter()
        {
            if (data == null)
            {
                Debug.Log("Data null");
            }
            
            if(data.state.Value is EMalfunctionState.CanBeFixed)
                FixSelectionsListWidget.Instance.ShowSelectionsList(runtimeFixActions);
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
        
            data.state.OnChanged += OnMalfunctionStateChanged;
            OnMalfunctionStateChanged(data.state.Value);
            Debug.Log($"Bind fan data for {gameObject.name}");
        }

        private void OnMalfunctionStateChanged(EMalfunctionState state)
        {
            animator.SetBool(workingKey, state is EMalfunctionState.Undetected);
            if(state is not EMalfunctionState.Undetected)
                undergroundLocalVolume.SetHotTemperature();
        }

        private void OnDestroy()
        {
            if (data != null)
            {
                data.state.OnChanged -= OnMalfunctionStateChanged;
            }
        }
    }
}