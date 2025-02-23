using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOrderController : MonoBehaviour
{
    public Image image;

    public void BringToFront()
    {
        image.transform.SetAsLastSibling();
    }

    public void SendToBack()
    {
        image.transform.SetAsFirstSibling();
    }
}
