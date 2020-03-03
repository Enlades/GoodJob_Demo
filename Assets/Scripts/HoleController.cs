using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour
{
    private bool _getInput;

    private Collider _triggerSphere;

    private void Awake(){
        _triggerSphere = GetComponent<SphereCollider>();
    }

    public void SetInput(bool p_state){
        _getInput = p_state;
        _triggerSphere.enabled = p_state;
    }

    private void Update(){
        if(_getInput && Input.GetMouseButton(0)){
            RaycastHit hit = default;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, float.MaxValue, LayerMask.GetMask("Ground"), QueryTriggerInteraction.Ignore)){
                transform.position = hit.point + Vector3.forward * 6f;
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag.CompareTo("Player") == 0 || col.tag.CompareTo("Death") == 0)
        {
            col.gameObject.layer = LayerMask.NameToLayer("Cubes");
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag.CompareTo("Player") == 0 || col.tag.CompareTo("Death") == 0)
        {
            col.gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }
}
