using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardActivator : MonoBehaviour {

    public GameObject billboard;

    private bool _shown = false;

    public void ShowTarget(bool show)
    {
        if(billboard != null) { 
            billboard.SetActive(show);
        }
        _shown = show;

    }

    public void ToggleTarget()
    {

        ShowTarget(!_shown);

    }
}
