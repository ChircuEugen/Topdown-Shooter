using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    void ChasePlayer();
    void AttackPlayer();
    void Disable();
}
