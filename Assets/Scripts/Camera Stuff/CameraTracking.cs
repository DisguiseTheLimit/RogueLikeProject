using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour
{

    public Vector3 offset;

    void LateUpdate()
    {
        if (StageManager.Instance.Player != null)
        {
            transform.position = StageManager.Instance.Player.transform.position + offset;
        }
    }
}
