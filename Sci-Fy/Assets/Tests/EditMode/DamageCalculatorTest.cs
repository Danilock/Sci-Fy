﻿using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class DamageCalculatorTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void DamageCalculatorTestSimplePasses()
        {
            Damageable damageable = new Damageable();
            
            damageable.Team = DamageSourceTeam.Friendly;
            damageable.SetHealth(100f);
            damageable.SetArmor(30f);

            damageable.TakeDamage(10f, DamageSourceTeam.Enemy);

            float health = damageable.CurrentHealth;

            Assert.AreEqual(99f, health);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator DamageCalculatorTestWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
