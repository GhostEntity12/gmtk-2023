using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PreviewBox : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI header;
    [SerializeField] TextMeshProUGUI info;
    [SerializeField] TextMeshProUGUI neutral;

    public void DisplayInfo(string header, string info)
    {
        this.header.SetText(header);
        this.info.SetText(info);
        this.header.enabled = true;
        this.info.enabled = true;
        neutral.enabled = false;
    }

    public void ResetInfo()
    {
        header.enabled = false;
        info.enabled = false;
        neutral.enabled = true;
    }
}
