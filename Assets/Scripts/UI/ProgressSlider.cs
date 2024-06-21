using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ProgressSlider : MonoBehaviour
{
    [Inject] LevelManager levelManager;
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text progressText;

    private void Awake()
    {
        SetSlider(0, 1);
        levelManager.OnCompletionProgressChanged += LevelManager_OnCompletionProgressChanged;
    }

    private void SetSlider(float value, float maxVaue)
    {
        slider.value = value;
        slider.maxValue = maxVaue;
        SetCompletionText();
    }

    private void SetCompletionText()
    {
        progressText.text = Mathf.RoundToInt(slider.value * 100).ToString() + "%";
    }

    private void LevelManager_OnCompletionProgressChanged(float completion)
    {
        slider.value = completion;
        SetCompletionText();
    }

    private void OnDisable()
    {
        levelManager.OnCompletionProgressChanged -= LevelManager_OnCompletionProgressChanged;
    }
}
