using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DefaultImageClickHandler : MonoBehaviour, IPointerClickHandler {

    public MonoBehaviour activateTarget;
    public GameObject deactivateParent;

    public void OnPointerClick(PointerEventData eventData) {
        if (activateTarget != null)
        {
            activateTarget.enabled = true;
        }
        if (deactivateParent != null)
        {
            deactivateParent.transform.parent.gameObject.SetActive(false);
        }
    }

}
