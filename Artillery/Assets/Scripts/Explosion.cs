using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] GameObject Particles;
    [SerializeField] float TimeDestroy = 1.5f;
    [SerializeField] float TimeStartCollision = 0.1f;

    bool Exploded;
    Collider Col;

    void Start()
    {
        Col = GetComponent<Collider>();

        if (Col != null)
        {
            Col.enabled = false;
            Invoke(nameof(EnableCollision), TimeStartCollision);
        }
    }

    void EnableCollision()
    {
        if (Col != null)
        {
            Col.enabled = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (Exploded)
        {
            return;
        }

        Exploded = true;

        Vector3 pos = transform.position;

        if (collision.contacts.Length > 0)
        {
            pos = collision.contacts[0].point;
        }

        GameObject explosion = Instantiate(Particles, pos, Quaternion.identity);
        Destroy(explosion, TimeDestroy);
        Destroy(gameObject);
    }
}