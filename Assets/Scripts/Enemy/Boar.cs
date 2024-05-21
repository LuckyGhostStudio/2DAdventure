using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar : Enemy
{
    protected override void Awake()
    {
        base.Awake();

        patrolState = new BoarPatrolState();    // Ñ²Âß×´Ì¬
        chaseState = new BoarChaseState();      // Ñ²Âß×´Ì¬
    }
}
