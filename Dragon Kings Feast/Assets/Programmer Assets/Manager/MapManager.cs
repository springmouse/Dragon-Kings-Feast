using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public List<GameObject> tilesInUse;
    public List<GameObject> tilesUsed;

    public List<GameObject> leftClouds;
    public List<GameObject> rightClouds;

    public List<GameObject> activeBoosts;
    public List<GameObject> inactiveBoosts;

    public Player player;

    public GameObject tile;
    public GameObject cloud;
    public GameObject boost;
    public GameObject enemy;

    public float minCloudYRange;
    public float maxCloudYRange;
    
    public float maxCloudZRange;

    public int cloudCount;

    public int pickUpCount;
    //NEW NAMES
    public int minRange;
    public int MaxRange;

    public int enemySpawnCount;

    public int mapLength;
    public int tileSize;
    
    // Use this for initialization
    void Start ()
    {
        for (int i = 0; i < mapLength; i++)
        {
            GameObject go = Instantiate(tile, new Vector3(i * tileSize, 0, 0), Quaternion.identity);
            go.transform.SetParent(transform);

            for (int u = 0; u < pickUpCount; u++)
            {
                int spawn = UnityEngine.Random.Range(minRange, MaxRange);

                if (spawn > 0)
                {
                    GameObject bo = Instantiate(boost, new Vector3(i * tileSize, 0, 0), Quaternion.identity);
                    bo.transform.position = new Vector3(i * tileSize + UnityEngine.Random.Range(-tileSize, tileSize), 
                        player.startPos.y + UnityEngine.Random.Range(-player.maxVertical, player.maxVertical),
                        player.startPos.z + UnityEngine.Random.Range(-player.maxHorizontal, player.maxHorizontal));

                    bo.GetComponent<Boost>().map = this;

                    activeBoosts.Add(bo);
                }
            }

            for (int w = 0; w < enemySpawnCount; w++)
            {
                GameObject en = Instantiate(enemy, new Vector3(i * tileSize, 0, 0), Quaternion.identity);
                en.transform.SetParent(transform);

                en.transform.position = new Vector3(i * tileSize + UnityEngine.Random.Range(-tileSize, tileSize),
                            player.startPos.y + UnityEngine.Random.Range(-player.maxVertical, player.maxVertical),
                            player.startPos.z + UnityEngine.Random.Range(-player.maxHorizontal, player.maxHorizontal));

            }

            tilesInUse.Add(go);
        }

        for (int i = 0; i < cloudCount; i++)
        {
            GameObject go = Instantiate(cloud, new Vector3(i * tileSize, 0, 0), Quaternion.identity);
            go.transform.SetParent(transform);

            go.transform.position = new Vector3(UnityEngine.Random.Range(0, tileSize * 2),
                        UnityEngine.Random.Range(minCloudYRange, maxCloudYRange) + 7.5f,
                        UnityEngine.Random.Range(0, maxCloudZRange) + (tileSize / 2));

            rightClouds.Add(go);
        }

        for (int i = 0; i < cloudCount; i++)
        {
            GameObject go = Instantiate(cloud, new Vector3(i * tileSize, 0, 0), Quaternion.identity);
            go.transform.SetParent(transform);

            go.transform.position = new Vector3(UnityEngine.Random.Range(0, tileSize * 2),
                        UnityEngine.Random.Range(minCloudYRange, maxCloudYRange) + 7.5f,
                        UnityEngine.Random.Range(-maxCloudZRange, 0) - (tileSize / 2));

            leftClouds.Add(go);
        }

        
    }
	
	// Update is called once per frame
	void Update ()
    {
        ManageTiles();
        ManageClouds();
        ManageBoosts();
    }

    public void DeactivateBoost(GameObject go)
    {
        go.SetActive(false);
        inactiveBoosts.Add(go);
        activeBoosts.Remove(go);
    }

    private void ManageBoosts()
    {
        List<GameObject> KillBoosts = new List<GameObject>();

        for (int i = 0; i < activeBoosts.Count; i++)
        {
            if (activeBoosts[i].transform.position.x < player.transform.position.x)
            {
                KillBoosts.Add(activeBoosts[i]);
            }
        }

        for (int i = 0; i < KillBoosts.Count; i++)
        {
            activeBoosts.Remove(KillBoosts[i]);
            inactiveBoosts.Add(KillBoosts[i]);
        }
    }

    private void ManageClouds()
    {
        for (int i = 0; i < rightClouds.Count; i++)
        {
            if (rightClouds[i].transform.position.x < player.transform.position.x)
            {
                rightClouds[i].transform.position = new Vector3(UnityEngine.Random.Range(0, tileSize * 2) + player.transform.position.x + (tileSize * 2), 
                    UnityEngine.Random.Range(minCloudYRange, maxCloudYRange) + 7.5f,
                    UnityEngine.Random.Range(0, maxCloudZRange) + (tileSize / 2));
            }
        }

        for (int i = 0; i < leftClouds.Count; i++)
        {
            if (leftClouds[i].transform.position.x < player.transform.position.x)
            {
                leftClouds[i].transform.position = new Vector3(UnityEngine.Random.Range(0, tileSize * 2) + player.transform.position.x + (tileSize * 2),
                    UnityEngine.Random.Range(minCloudYRange, maxCloudYRange) + 7.5f,
                    UnityEngine.Random.Range(-maxCloudZRange, 0) - (tileSize / 2));
            }
        }
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

                        GenPickUp((int)(tilesInUse[i].transform.position.x + tileSize));
                        SpawnEnemy((int)(tilesInUse[i].transform.position.x + tileSize));

                        break;
                    }
                    else
                    {
                        GameObject go = Instantiate(tile, new Vector3(tilesInUse[i].transform.position.x + tileSize, 0, 0), Quaternion.identity);
                        go.transform.SetParent(transform);

                        tilesInUse.Add(go);

                        GenPickUp((int)(tilesInUse[i].transform.position.x + tileSize));
                        SpawnEnemy((int)(tilesInUse[i].transform.position.x + tileSize));

                        break;
                    }
                }
            }
        }
    }

    public void SpawnEnemy(int xPos)
    {
        for (int w = 0; w < enemySpawnCount; w++)
        {
            GameObject en = Instantiate(enemy, new Vector3(xPos, 0, 0), Quaternion.identity);
            en.transform.SetParent(transform);

            en.transform.position = new Vector3(xPos + UnityEngine.Random.Range(-tileSize, tileSize),
                        player.startPos.y + UnityEngine.Random.Range(-player.maxVertical, player.maxVertical),
                        player.startPos.z + UnityEngine.Random.Range(-player.maxHorizontal, player.maxHorizontal));

        }
    }

    public void GenPickUp(int xPos)
    {
        for (int u = 0; u < pickUpCount; u++)
        {
            int spawn = UnityEngine.Random.Range(minRange, MaxRange);

            if (spawn > 0)
            {
                if (inactiveBoosts.Count <= 0)
                {
                    GameObject bo = Instantiate(boost, new Vector3(xPos, 0, 0), Quaternion.identity);
                    bo.transform.position = new Vector3(xPos + UnityEngine.Random.Range(-tileSize, tileSize),
                        player.startPos.y + UnityEngine.Random.Range(-player.maxVertical, player.maxVertical),
                        player.startPos.z + UnityEngine.Random.Range(-player.maxHorizontal, player.maxHorizontal));

                    bo.GetComponent<Boost>().map = this;

                    activeBoosts.Add(bo);
                }
                else
                {
                    inactiveBoosts[0].transform.position = new Vector3(xPos + UnityEngine.Random.Range(-tileSize, tileSize),
                        player.startPos.y + UnityEngine.Random.Range(-player.maxVertical, player.maxVertical),
                        player.startPos.z + UnityEngine.Random.Range(-player.maxHorizontal, player.maxHorizontal));
                    inactiveBoosts.Remove(inactiveBoosts[0]);
                }
            }
        }
    }
}
