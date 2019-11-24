using UnityEngine;
using DG.Tweening;
using MemeFight.Components.Utilities;

public static class TweenPresets {

    public static void Shake(Transform transform, float intensity, float duration, ShakeInstance.ShakeType shakeType = ShakeInstance.ShakeType.Linear) {
        float fromIntensity = shakeType == ShakeInstance.ShakeType.Ascending ? 0 : intensity;
        float toIntensity = shakeType == ShakeInstance.ShakeType.Descending ? 0 : intensity;
        Vector3 shakedObjectBasePosition = transform.localPosition;
        DOTween.To(() => fromIntensity, x => fromIntensity = x, toIntensity, duration).OnUpdate(() => {
            Vector3 shakePosition = shakedObjectBasePosition + ((Vector3)Random.insideUnitCircle * fromIntensity);
            transform.localPosition = shakePosition;
        }).OnComplete(() => {
            transform.localPosition = shakedObjectBasePosition;
        });
    }

    public static void Flash(SpriteRenderer renderer, Color color, float duration) {
        renderer.DOColor(color, duration * 0.5f).SetLoops(2, LoopType.Yoyo);
    }

    public enum RotationDirection {
        Clockwise,
        AntiClockwise
    }

    public static void RotateInDirection(Transform transform, float rotation, float duration, RotationDirection rotationDirection) {
        float baseRotation = transform.localEulerAngles.z;
        float toRotation = rotation;
        if (rotationDirection == RotationDirection.AntiClockwise) {
            while (toRotation < baseRotation) {
                toRotation += 360;
            }
        } else {
            while (toRotation > baseRotation) {
                toRotation -= 360;
            }
        }
        transform.DOLocalRotate(new Vector3(0, 0, toRotation), duration, RotateMode.FastBeyond360);
    }

}
