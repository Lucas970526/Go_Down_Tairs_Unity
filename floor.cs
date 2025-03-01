using UnityEngine;

public class Floor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] float moveSpeed = 2f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, moveSpeed * Time.deltaTime, 0);


        if (transform.position.y > 6f)
        {
            
            //transform.parent.GetComponent<Floormanager>().SpawnFloor();
            
            Destroy(gameObject);


            Floormanager floorManager = transform.parent.GetComponent<Floormanager>();

            floorManager.SpawnFloor();


        }
    }
}
