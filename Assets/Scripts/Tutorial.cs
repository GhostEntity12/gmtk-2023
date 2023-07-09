using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] RectTransform panel;
    int panelCount = 5;
    int index = 0;

    public void NextPanel()
    {
        index = Mathf.Min(index + 1, panelCount - 1);
        LeanTween.moveX(panel, -1920 * index, 0.3f).setEaseOutBack();
    }
    public void PrevPanel()
    {
        index = Mathf.Max(index - 1, 0);
        LeanTween.moveX(panel, -1920 * index, 0.3f).setEaseOutBack();
    }

    public void Close()
    {
        index = 0;
        LeanTween.moveY(panel, 1080, 0.3f).setEaseInBack();
    }
    public void Open()
    {
        index = 0;
        LeanTween.move(panel, new(0, 1080, 0), 0);
        LeanTween.moveY(panel, 0, 0.3f).setEaseOutBack();
    }
}
