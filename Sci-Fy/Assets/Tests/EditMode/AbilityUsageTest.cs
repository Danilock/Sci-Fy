using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Game.Ability;

namespace Tests
{
    public class AbilityUsageTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void AbilityUsageTestSimplePasses()
        {
            GameObject playerObj = new GameObject("Player");

            Dash dash = playerObj.AddComponent<Dash>();
            dash.SetCooldown(3f);
            dash.TriggerAbility();

            Assert.IsFalse(dash.CanUse);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator AbilityUsageTestWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
