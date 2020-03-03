using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCombiner : MonoBehaviour
{
    public GameObject[] GroundParts;
    public Material GroundMat;

    private void Awake(){
        CombineInstance[] combine = new CombineInstance[GroundParts.Length];

        for(int i = 0; i < combine.Length; i++){
            combine[i].mesh = GroundParts[i].GetComponent<MeshFilter>().sharedMesh;
            combine[i].transform = GroundParts[i].transform.localToWorldMatrix;
            GroundParts[i].GetComponent<MeshRenderer>().enabled = false;
        }

        MeshFilter mf = gameObject.AddComponent<MeshFilter>();
        mf.mesh = new Mesh();
        mf.mesh.CombineMeshes(combine);

        MeshRenderer mr = gameObject.AddComponent<MeshRenderer>();
        mr.material = GroundMat;
    }
}
