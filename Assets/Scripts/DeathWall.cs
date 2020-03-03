using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeathWall : MonoBehaviour
{
    private Action _scoreCallback, _deathCallback;

    public void AssignCallbacks(Action p_scoreCallback, Action p_deathCallback){
        _scoreCallback = p_scoreCallback;
        _deathCallback = p_deathCallback;
    }

    private void OnTriggerEnter(Collider col){
        if (col.tag.CompareTo("Player") == 0)
        {
            //Debug.Log("Good anakin good");
            Destroy(col.gameObject);

            if (_scoreCallback != null)
            {
                _scoreCallback.Invoke();
            }
        }else if(col.tag.CompareTo("Death") == 0){
            //Debug.Log("You have become the very thing you swore to destroy");
            Destroy(col.gameObject);

            if (_deathCallback != null)
            {
                _deathCallback.Invoke();
            }
        }
    }
}
