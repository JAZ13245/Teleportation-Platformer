using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private PlayerInput player;
    [SerializeField]
    private LineRenderer lineRenderer;
    [SerializeField]
    private float threshold = 8f;


    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.gameObject.transform.position;

        if (lineRenderer.enabled && player.chargeAmt > 0.1)
        {
            /*Vector3 lastPost = lineRenderer.GetPosition(lineRenderer.positionCount - 1);

            Vector3 diff = (lastPost - playerPos) / 2;
            diff = Vector3.ClampMagnitude(diff, threshold);
            transform.position = playerPos + diff;*/

            transform.position = new Vector3(playerPos.x + (player.shootDir.x * threshold), playerPos.y + (player.shootDir.y * threshold/1.5f));
        }
        else
        {
            transform.position = playerPos;
        }
    }
}
