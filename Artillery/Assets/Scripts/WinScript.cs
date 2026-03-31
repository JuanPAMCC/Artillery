using UnityEngine;

public class WinScript : MonoBehaviour
{
    public float rotValue = 10f;
    public float waitTime = 1.5f;
    public GameObject objActive;

    private float timer;
    private bool started;
    private bool done;

    void Update()
    {
        if (done)
            return;

        if (!started)
        {
            if (transform.childCount == 0)
                return;

            bool allHit = true;

            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                float zRot = Mathf.DeltaAngle(0f, child.eulerAngles.z);

                if (Mathf.Abs(zRot) < rotValue)
                {
                    allHit = false;
                    break;
                }
            }

            if (!allHit)
                return;

            started = true;
        }

        timer += Time.deltaTime;

        if (timer < waitTime)
            return;

        done = true;

        if (objActive != null)
            objActive.SetActive(true);

        Time.timeScale = 0f;
    }
}