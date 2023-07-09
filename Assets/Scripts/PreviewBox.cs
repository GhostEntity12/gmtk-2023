using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PreviewBox : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI header;
    [SerializeField] TextMeshProUGUI info;

    public void DisplayInfo(string header, string info)
    {
        this.header.SetText(header);
        this.info.SetText(info);
    }
}
