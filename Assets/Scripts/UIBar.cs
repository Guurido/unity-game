using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBar : MonoBehaviour
{
    public Image bar;
    public FloatVariable currentValue;
    public FloatVariable maxValue;

    public VoidEventChannelSO onValueUpdate;
    public Gradient gradient;

    private void Awake() {
        ValueUpdated();
    }

    private void OnEnable() {
        onValueUpdate.OnEventRaised += ValueUpdated;
    }

    private void OnDisable() {
        onValueUpdate.OnEventRaised -= ValueUpdated;
    }

    void ValueUpdated() {
        float ratio = Mathf.Clamp01(currentValue.CurrentValue / maxValue.CurrentValue);
        bar.fillAmount = ratio;
        bar.color = gradient.Evaluate(ratio);
        Debug.Log(currentValue.CurrentValue);
    } 
}
