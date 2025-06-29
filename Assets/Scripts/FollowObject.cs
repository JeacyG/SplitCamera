using Unity.VisualScripting;
using UnityEngine;

[ExecuteAlways]
public class FollowObject : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void Update()
    {
        if (!target.IsUnityNull())
        {
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        }
    }
}
