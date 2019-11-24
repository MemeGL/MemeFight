using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Singleton<CameraController> {

    // Runtime variables
    private Transform cameraTransform;
	public Transform CameraTransform { get { return cameraTransform; } }
    private Vector3 cameraInitialLocalPosition;
    private List<ShakeInstance> shakeInstances;

	private void Awake () {
        cameraTransform = Camera.main.transform;
        cameraInitialLocalPosition = cameraTransform.localPosition;
		shakeInstances = new List<ShakeInstance> ();
	}

	public void Shake(float intensity, float duration, ShakeInstance.ShakeType type = ShakeInstance.ShakeType.Linear) {
		shakeInstances.Add (new ShakeInstance (intensity, duration, type));
	}

    private void Update() {
        float currentShakeIntensity = 0;
        for (int i = 0; i < shakeInstances.Count; i++) {
            ShakeInstance currentShakeInstance = shakeInstances[i];
            if (currentShakeInstance.currentDuration <= 0) {
                shakeInstances.RemoveAt(i);
            } else {
                //float currentShakeInstanceIntensity = currentShakeInstance.shakeIntensity * currentShakeInstance.currentShakeDuration / currentShakeInstance.shakeDuration;
                //currentShakeIntensity = Mathf.Max(currentShakeInstanceIntensity, currentShakeIntensity);
                currentShakeIntensity = Mathf.Max(currentShakeInstance.GetCurrentShakeIntensity(), currentShakeIntensity);
                currentShakeInstance.currentDuration -= Time.deltaTime;
            }
        }
        cameraTransform.localPosition = cameraInitialLocalPosition + Random.insideUnitSphere * currentShakeIntensity;
    }

}

public class ShakeInstance {

    public enum ShakeType {
        Linear,
        Descending,
        Ascending
    }

    public float intensity;
    public float duration;
    public float currentDuration;
    public ShakeType type;

    public ShakeInstance(float intensity, float duration, ShakeType type) {
        this.intensity = intensity;
        this.duration = duration;
        currentDuration = duration;
        this.type = type;
    }

    public float GetCurrentShakeIntensity() {
        if (type == ShakeType.Linear) {
            return intensity;
        } else {
            float percentDuration = currentDuration / duration;
            if (type == ShakeType.Descending) {
                return intensity * percentDuration;
            } else {
                return intensity * (1 - percentDuration);
            }
        }
    }

}