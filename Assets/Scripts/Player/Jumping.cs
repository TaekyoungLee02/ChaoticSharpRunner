using UnityEngine;

public class Jumping : MonoBehaviour {
    [SerializeField] PlayerController controller;

    public void JumpingFunction () {
        controller.Jumping();
    }
}
