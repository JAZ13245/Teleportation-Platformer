using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public Transform _shootPoint;
    public float minShootSpeed = 2f;
    public float maxShootSpeed = 15f;

    [SerializeField]
    private GameObject _arrow;


    public void ShootArrow(Vector3 shootDir, float chargeAmt)
    {
        GameObject arrowGO = Instantiate(_arrow, _shootPoint.position, Quaternion.identity);
        Arrow arrow = arrowGO.GetComponent<Arrow>();
        if(arrow != null )
        {
            arrow.arrowSpeed = GetBowPower(chargeAmt);
            arrow.arrowDirection = shootDir;
            arrow.OnFire();
        }
    }

    private float GetBowPower(float chargeAmt)
    {
        return Mathf.Lerp(minShootSpeed, maxShootSpeed, chargeAmt);
    }
}
