using UnityEngine;

public class AnimationRotaion : MonoBehaviour
{
    public Transform parent;
    private Quaternion fixedRotation;
    private Vector3 fixedPosition = new Vector3(0, 0.5f, 0);
    void Start()
    {
        fixedRotation = transform.rotation;
    }
    void LateUpdate()
    {
        transform.rotation = fixedRotation;
        transform.position = fixedPosition + parent.position;
    }
}
