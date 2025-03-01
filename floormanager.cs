using UnityEngine;

public class Floormanager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject[] FloorPrefabs;
    public void SpawnFloor()
    {
        int r = Random.Range(0, FloorPrefabs.Length);
        
        GameObject floor = Instantiate(FloorPrefabs[r], transform);
        floor.transform.position = new Vector3(Random.Range(-3.8f, 3.8f), -6f, 0f);

    }
    

}
