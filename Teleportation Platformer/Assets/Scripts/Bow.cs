using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField]
    private GameObject _arrow;
    [SerializeField]
    private Transform _shootPoint;

    public void ShootArrow(Vector3 targetPoint)
    {
        Vector3 forwardVec = (targetPoint - transform.position).normalized;
        Vector3 upVec = Vector3.Cross(forwardVec, new Vector3(0, 0, 1));
        Quaternion rot = Quaternion.LookRotation(forwardVec, upVec);
        Instantiate(_arrow, _shootPoint.position, rot);
    }
}
