using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFov : MonoBehaviour
{
    [Header("GhostView")]
    public float radius;
    [Range(0, 360)]
    public float angle;
    public GameObject PlayerPos;
    public LayerMask layerPLayer;
    public LayerMask obstructionMask;
    public bool canSeePlayer;

    private void Start()
    {
        PlayerPos = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        StartCoroutine(FovRountine());
    }

    IEnumerator FovRountine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    public void FieldOfViewCheck()
    {

        Collider[] rangeCheck = Physics.OverlapSphere(transform.position, radius, layerPLayer);

        if (rangeCheck.Length != 0)
        {
            Transform target = rangeCheck[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    canSeePlayer = true;
                else
                {
                    canSeePlayer = false;
                }
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }
}
