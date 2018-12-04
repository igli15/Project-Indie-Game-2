using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Enemy))]
public class EnemySeekState : AbstractState<EnemyFSM>
{

    //EACH 0.1 seconds updating its destination if it reach previous
    public IEnumerator FollowTarget(Transform target,EnemyMovement enemyMovement)
    {
        Vector3 previousTargetPosition = new Vector3(float.PositiveInfinity, float.PositiveInfinity);

        while (true)
        {
            if (Vector3.SqrMagnitude(previousTargetPosition - target.position) > 0.1f)
            {
                enemyMovement.SetDestination(target.position);
                previousTargetPosition = target.position;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

}
