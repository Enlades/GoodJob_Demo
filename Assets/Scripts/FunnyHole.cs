using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunnyHole : MonoBehaviour
{
    private void OnCollisionEnter(Collision col){
        StartCoroutine(SlowDestroy(col.gameObject));
    }

    private IEnumerator SlowDestroy(GameObject p_target){
        float timer = 0.5f;
        float maxTimer = timer;

        Vector3 startScale = p_target.transform.localScale;
        Vector3 targetScale = Vector3.zero;

        while (timer >= 0f)
        {
            if(p_target != null){
                p_target.transform.localScale = Vector3.Lerp(startScale, targetScale, (maxTimer - timer) / maxTimer);
            }

            timer -= Time.deltaTime;

            yield return null;
        }

        if (p_target != null)
        {
            Destroy(p_target);
        }
    }
}
