using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack
{
    AttackForce AttackForceSetter { get; set; }
    LayerMask DamageLayers { get; set; }
}

public enum AttackForce
{
    Weak, Medium, Strong
}
