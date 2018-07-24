using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public List<GameObject> tilesInUse;
    public List<GameObject> tilesUsed;

    public Player player;

    public GameObject tile;

    public int mapLength;
    public int tileSize;

	// Use this for initialization
	void Start ()
    {
        for (int i = 0; i < mapLength; i++)
        {
            GameObject go = Instantiate(tile, new Vector3(i * tileSize, 0, 0), Quaternion.identity);
            go.transform.SetParent(transform);

            tilesInUse.Add(go);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        ManageTiles();

    }

    public void ManageTiles()
    {
        List<GameObject> manager = new List<GameObject>();
        for (int i = 0; i < tilesInUse.Count; i++)
        {
            if ((tilesInUse[i].transform.position.x - player.transform.position.x) < -tileSize)
            {
                manager.Add(tilesInUse[i]);
            }
        }

        for (int i = 0; i < manager.Count; i++)
        {
            tilesInUse.Remove(manager[i]);
            manager[i].SetActive(false);
            tilesUsed.Add(manager[i]);
        }

        manager.Clear();

        for (int i = 0; i < tilesInUse.Count; i++)
        {
            if (tilesInUse.Count < mapLength)
            {
                if ((tilesInUse[i].transform.position.x - player.transform.position.x) > (tileSize / 4))
                {
                    if (tilesUsed.Count > 0)
                    {
                        tilesUsed[0].SetActive(true);
                        tilesUsed[0].transform.position = new Vector3(tilesInUse[i].transform.position.x + tileSize, 0, 0);
                        tilesInUse.Add(tilesUsed[0]);
                        tilesUsed.Remove(tilesUsed[0]);

                        break;
                    }
                    else
                    {
                        GameObject go = Instantiate(tile, new Vector3(tilesInUse[i].transform.position.x + tileSize, 0, 0), Quaternion.identity);
                        go.transform.SetParent(transform);

                        tilesInUse.Add(go);

                        break;
                    }
                }
            }
        }


    }
}
