using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StageTransitionHandler : MonoBehaviour
{
    public void StartTransition(Transform p_transform, Vector3 p_startPosition, Vector3 p_targetPosition, Action p_callback){
        StartCoroutine(TransitionOperation(p_transform, p_startPosition, p_targetPosition, p_callback));
    }

    private IEnumerator TransitionOperation(Transform p_transform, Vector3 p_startPosition, Vector3 p_targetPosition, Action p_callback){

        yield return StartCoroutine(MovementOperation(p_transform, p_transform.position, p_startPosition, 0.5f));

        yield return StartCoroutine(MovementOperation(p_transform, p_startPosition, p_targetPosition, 1f));

        if(p_callback != null){
            p_callback.Invoke();
        }
    }

    private IEnumerator MovementOperation(Transform p_transform, Vector3 p_startPosition, Vector3 p_targetPosition, float p_duration){
        float timer = p_duration;
        float maxTimer = timer;

        while (timer >= 0f)
        {
            p_transform.position = Vector3.Lerp(p_startPosition, p_targetPosition, (maxTimer - timer) / maxTimer);

            timer -= Time.deltaTime;

            yield return null;
        }

        p_transform.transform.position = p_targetPosition;

    }
}
