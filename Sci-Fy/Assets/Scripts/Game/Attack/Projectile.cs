﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public void DesactivateProjectile()
    {
        gameObject.SetActive(false);
    }
}
