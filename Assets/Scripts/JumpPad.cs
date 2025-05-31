using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpPower;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().BoostJump(jumpPower);
        }
    }
}
