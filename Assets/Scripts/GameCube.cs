using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCube : MonoBehaviour
{
    private void Start(){
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
