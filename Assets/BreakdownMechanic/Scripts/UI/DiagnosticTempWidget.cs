using Systems.Persistence;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiagnosticTempWidget : Widget, IBind<FanOverpoweredMalfunction.FanData>, IBind<FilterCloggingMalfunction.FilterData>
{
    private FanOverpoweredMalfunction.FanData fanData;
    private FilterCloggingMalfunction.FilterData filterData;

    [SerializeField]
    private FanOverpoweredMalfunction fanOverpoweredMalfunction;
    [SerializeField]
    private FilterCloggingMalfunction filterCloggingMalfunction;

    [SerializeField]
    private Sprite greenMark;
    [SerializeField]
    private Sprite redCross;

    [Header("Is Fan Working")]
    [SerializeField]
    private string FanStateLabelText;
    [SerializeField]
    private TMP_Text FanStateLabelTextField;
    
    [SerializeField]
    private Image FanStateImage;
    
    [Header("Percentage of filter clogging")]
    [SerializeField]
    private string FilterCloggingLabelText;
    [SerializeField]
    private TMP_Text FilterCloggingLabelTextField;
    
    [SerializeField]
    private TMP_Text FilterCloggingValueTextField;
    
    [Header("Wind speed data")]
    [SerializeField]
    private string WindSpeedLabelText;
    [SerializeField]
    private TMP_Text WindSpeedLabelTextField;
    
    [SerializeField]
    private TMP_Text WindSpeedValueTextField;
    
    [Header("Global Mechanisms State")]
    [SerializeField]
    private string GlobalMechanismsStateLabelText;
    [SerializeField]
    private TMP_Text GlobalMechanismsStateLabelTextField;
    
    [SerializeField]
    private Image GlobalMechanismsStateImage;
    
    [Header("Fan Rotation Speed")]
    [SerializeField]
    private string FanRotationSpeedLabelText;
    [SerializeField]
    private TMP_Text FanRotationSpeedLabelTextField;
    
    [SerializeField]
    private TMP_Text FanRotationSpeedValueTextField;
    
    [Header("Fan engine work")]
    [SerializeField]
    private string FanEngineWorkLabelText;
    [SerializeField]
    private TMP_Text FanEngineWorkLabelTextField;
    
    [SerializeField]
    private Image FanEngineWorkStateImage;


    private void Start()
    {
        Setup();
    }

    private void Setup()
    {
        FanStateLabelTextField.text = FanStateLabelText;
        FanStateImage.sprite = redCross;

        FilterCloggingLabelTextField.text = FilterCloggingLabelText;
        FilterCloggingValueTextField.text = "???";

        WindSpeedLabelTextField.text = WindSpeedLabelText;
        WindSpeedValueTextField.text = "???";

        GlobalMechanismsStateLabelTextField.text = GlobalMechanismsStateLabelText;
        GlobalMechanismsStateImage.sprite = redCross;

        FanRotationSpeedLabelTextField.text = FanRotationSpeedLabelText;
        FanRotationSpeedValueTextField.text = "???";

        FanEngineWorkLabelTextField.text = FanEngineWorkLabelText;
        FanEngineWorkStateImage.sprite = redCross;
    }

    public SerializableGuid Id { get; set; }
    public void Bind(FilterCloggingMalfunction.FilterData data)
    {
        filterData = data;

        filterData.cloggingValue.OnChanged += OnFilterChange;
        Debug.Log($"Bind filter data for {GetType().Name}");
    }

    public void Bind(FanOverpoweredMalfunction.FanData data)
    {
        fanData = data;

        data.state.OnChanged += OnFanStateChange;
        Debug.Log($"Bind fan data for {GetType().Name}");
    }

    private void OnFanStateChange(EMalfunctionState eMalfunctionState)
    {
        if(FanStateImage == null)
            return;
        FanStateImage.sprite = eMalfunctionState is not EMalfunctionState.Undetected ? redCross : greenMark;
    }

    private void OnFilterChange(float clogging)
    {
        FilterCloggingValueTextField.text = $"{Mathf.InverseLerp(0, filterCloggingMalfunction.Config.CloggingCriticalValue, clogging) * 100f}%";
    }

    private void OnDestroy()
    {
        if(fanData != null)
            filterData.cloggingValue.OnChanged -= OnFilterChange;

        if (filterData != null)
            filterData.state.OnChanged -= OnFanStateChange;
    }
}