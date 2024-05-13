using TMPro;
using UnityEngine;

public class PhaseCount : MonoBehaviour
{
    public TextMeshProUGUI phaseText;
    public GameFlow gameFlow;

    void Update()
    {
        phaseText.text = "Phase: " + gameFlow.currentPhase;
    }
}