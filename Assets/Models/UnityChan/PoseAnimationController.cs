using UnityEngine;

public class PoseAnimationController : MonoBehaviour {
    int baseState;

    [SerializeField] string[] triggerNames;
    int[] triggerHahs;
    int triggerCount;

    [SerializeField] float attemptDelay = 1f;
    float attemptDelayCounter;

    [SerializeField, Range(0f, 1f)] float triggerChance = 0.1f;

    Animator anim;

    private void Awake () {
        baseState = Animator.StringToHash("Base Layer.WAIT00");

        triggerCount = triggerNames.Length;
        triggerHahs = new int[triggerCount];
        for (int i=0; i<triggerCount; i++) {
            triggerHahs[i] = Animator.StringToHash(triggerNames[i]);
        }

        attemptDelayCounter = attemptDelay;

        anim = GetComponent<Animator>();
    }

    private void Update () {
        if (anim.GetCurrentAnimatorStateInfo(0).fullPathHash == baseState) {
            attemptDelayCounter += Time.deltaTime;
            if (attemptDelayCounter >= attemptDelay) {
                attemptDelayCounter = 0f;
                TryTriggerRandomAnimation();
            }
        }
    }

    void TryTriggerRandomAnimation () {
        if (triggerCount == 0) return;
        if (anim.GetCurrentAnimatorStateInfo(0).fullPathHash != baseState) return;
        if (Random.value > triggerChance) return;

        int idx = Random.Range(0, triggerHahs.Length);
        anim.SetTrigger(triggerHahs[idx]);
    }
}
