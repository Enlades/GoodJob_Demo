using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public int ShakeCount;
    public float ShakeDuration, ShakeStrength;

    private Transform _cameraTransform;

    private bool _isShaking;

    public void AssignCameraTransform(Transform p_cameraTransform){
        _cameraTransform = p_cameraTransform;
    }

    public void StartShaking(){
        if(_isShaking){
            return;
        }
        StartCoroutine(CameraShakeOperation());
    }

    private IEnumerator CameraShakeOperation(){
        Quaternion startRotation = _cameraTransform.rotation;

        Quaternion leftRotation = startRotation * Quaternion.Euler(Vector3.forward * -1f * ShakeStrength);
        Quaternion rightRotation = startRotation * Quaternion.Euler(Vector3.forward * 1f * ShakeStrength);

        Quaternion[] targetRotations = new Quaternion[ShakeCount + 2];
        targetRotations[0] = startRotation;
        targetRotations[targetRotations.Length - 1] = startRotation;

        for(int i = 1; i < ShakeCount; i++){
            if(i % 2 == 0){
                targetRotations[i] = leftRotation;
            }else{
                targetRotations[i] = rightRotation;
            }
        }

        float timer = ShakeDuration;
        float maxTimer = timer;

        _isShaking = true;

        for(int i = 0; i < ShakeCount - 1; i++){
            while (timer >= 0f)
            {
                _cameraTransform.rotation = Quaternion.Lerp(targetRotations[i], targetRotations[i + 1], (maxTimer - timer) / maxTimer);

                timer -= Time.deltaTime;

                yield return null;
            }

            timer = ShakeDuration;
        }

        _cameraTransform.rotation = startRotation;

        _isShaking = false;
    }
}
