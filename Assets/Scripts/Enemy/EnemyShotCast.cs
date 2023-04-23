using UnityEngine;

public class EnemyShotCast : MonoBehaviour
{
    public Transform shotPosition;
    public GameObject shotPrefab;
    private Enemy enemyScript;

    private void Start()
    {
        enemyScript = this.GetComponent<Enemy>();
    }

    public void InstantiateShot()
    {
        if(enemyScript !=null)
        {
            enemyScript.rangeAttackTimer = enemyScript.rangeAttackTimerValue;
            //Debug.Log("range attack timer set");
        }
       
        Instantiate(shotPrefab, shotPosition.position, shotPosition.rotation);
    }
}
