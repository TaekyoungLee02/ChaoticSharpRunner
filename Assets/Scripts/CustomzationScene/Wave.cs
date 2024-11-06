using UnityEngine;

public class Wave : MonoBehaviour {
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] GameObject wave;

    bool producedNextWave = false;

    private void Update () {
        transform.Translate(-transform.right * moveSpeed * Time.deltaTime);

        if (!producedNextWave && transform.position.x <= -40f) {
            var pos = transform.position;
            pos.x = -40f;
            transform.position = pos;

            // create next wave
            var obj = Instantiate(wave);
            pos = transform.position;
            pos.x = 115f;
            obj.transform.position = pos;

            producedNextWave = true;
        }

        if (transform.position.x <= -100f) {
            Destroy(gameObject);
        }
    }
}
