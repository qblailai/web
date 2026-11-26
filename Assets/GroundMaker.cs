using UnityEngine;

public class GroundMaker : MonoBehaviour
{
    public GameObject[] prefabs;
    public float offsetMin = -1f;
    public float offsetMax = 3f;
    int groundCount = 0;

    [Header("加速設定")] //此行只是 Inspector 面板上的標題，方便閱讀
    public int blocksPerSpeedup = 8; //每生成幾塊平台加速一次
    public float speedIncrease = 1f; //每次加速增加多少
    public float maxspeed = 15f; //速度上限

    void Start()
    {
        InvokeRepeating(nameof(MakeGrounds), 1f, 2f);
    }
    
    void MakeGrounds()
    {
        int number = Random.Range(0,3);
        Instantiate(prefabs [number], 
        transform.position + new Vector3(Random.Range(offsetMin,offsetMax), 0, 0), 
        transform.rotation);
        groundCount ++;
        //Debug.Log("第" + groundCount + "個地面");
        if (groundCount % blocksPerSpeedup == 0)
        {
            GroundMove.speed = Mathf.Min(GroundMove.speed + speedIncrease, maxspeed);
            Debug.Log("地面加速囉！目前速度：" + GroundMove.speed);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
