using UnityEngine;

public class GroundMove : MonoBehaviour
{
    public static float speed = 5f;

    void Update()
    {
        transform.Translate(-speed*Time.deltaTime ,0f,0f); //有無+上TIME，會影響:每偵移動5單位，或是每秒移動5單位。
    
        if (transform.position.x < -25f)
        {
            PlayerMovement pm = GetComponentInChildren<PlayerMovement>();
            if (pm != null)
            {
                pm.transform.SetParent(null);
            } 
            Destroy(gameObject);
        }
    }
}
