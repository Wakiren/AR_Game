using UnityEngine;
using UnityEngine.XR.ARFoundation;


public class SceneReloader : MonoBehaviour
{
    [SerializeField] private Transform spawnedObjects;
    [SerializeField] private Transform spawnedEnemies;
    [SerializeField] private ARSession arSession;

    public void ResetAR()
    {
        for (int i = spawnedObjects.childCount - 1; i >= 0; i--)
        {
            Destroy(spawnedObjects.GetChild(i).gameObject);
        }
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        Game_Manager.Instance.money = 20;
        Game_Manager.Instance.tiempoAguantado = 0;
        arSession.Reset();
    }
}