using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.Experimental.AssetImporters;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class InteractiveObjectsManagerTests
    {
        [Test]
        public void InteractiveObjectsManagerInitiatesWithEmptyObject()
        {
            Player.InteractiveObjectsManager interactiveObjectsManager = new Player.InteractiveObjectsManager();
            if (interactiveObjectsManager.InteractiveObjectInRange)
            {
                Assert.Fail("Interactive object manager stores a non empty object as InteractiveObjectInRange value");
            }
        }

        [Test]
        public void InteractiveObjectManagerDetectsObject()
        {
            GameObject gameObject = new GameObject();
            BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
            gameObject.layer = 9;
            gameObject.name = "testInteractable";
            gameObject.transform.position = Vector3.zero;
            Player.InteractiveObjectsManager interactiveObjectsManager = new Player.InteractiveObjectsManager();
            
            // Act
            interactiveObjectsManager.CheckForInteractiveObjectsInRange(Vector3.zero, Vector3.right);
            
            // Assert
            Assert.AreEqual(collider, interactiveObjectsManager.InteractiveObjectInRange.collider);
            gameObject.SetActive(false);
        }

        [Test]
        public void InteractiveObjectManagerOverriderDetectedObject()
        {
            GameObject gameObject = new GameObject();
            BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
            IInteractive interactive = gameObject.AddComponent<Sign>();
            gameObject.layer = 9;
            gameObject.name = "testInteractable";
            gameObject.transform.position = Vector3.zero;
            Player.InteractiveObjectsManager interactiveObjectsManager = new Player.InteractiveObjectsManager();
            
            // Act
            interactiveObjectsManager.CheckForInteractiveObjectsInRange(Vector3.zero, Vector3.right);
            gameObject.SetActive(false);
            interactiveObjectsManager.CheckForInteractiveObjectsInRange(Vector3.zero, Vector3.right);
            if (interactiveObjectsManager.InteractiveObjectInRange)
            {
                Assert.Fail("Interactive object manager stores a non empty object as InteractiveObjectInRange value");
            }
        }
        
    }
}
