using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Game.Ability;

namespace Tests
{
    public class AbilityUsagePlaymodeTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void AbilityUsagePlaymodeTestSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator AbilityUsagePlaymodeTestWithEnumeratorPasses()
        {
            GameObject playerObj = new GameObject("Player");

            Rigidbody2D rgb = playerObj.AddComponent<Rigidbody2D>();
            CharacterController2D ch2D = playerObj.AddComponent<CharacterController2D>();
            Damageable dmg = playerObj.AddComponent<Damageable>();

            Dash dash = playerObj.AddComponent<Dash>();
            dash.SetCooldown(3f);
            dash.TriggerAbility();

            yield return new WaitForSeconds(dash.Cooldown);
            Assert.IsTrue(dash.CanUse);
        }
    }
}
