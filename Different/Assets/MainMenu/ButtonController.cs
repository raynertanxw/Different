using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DaburuTools;
using DaburuTools.Action;

public class ButtonController : MonoBehaviour
{
    private bool displayed = false;

    private void Awake()
    {
        PulseAction pulseAct = new PulseAction(this.transform, 1, Graph.SmoothStep, 1.0f, Vector3.one, 1.1f * Vector3.one);
        ActionRepeatForever repeatForever = new ActionRepeatForever(pulseAct);
        ActionHandler.RunAction(repeatForever);
    }

    public void DisplaySelection(bool display)
    {
        transform.FindChild("SelectionBox").GetComponent<Image>().enabled = display;
        displayed = display;
    }
}
