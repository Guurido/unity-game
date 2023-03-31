using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [Tooltip("Define where the player will spawn if there is an issue"), ReadOnlyInspector]
    public Vector3 currentSpawnPosition;

    [Tooltip("Define where the player started the level"), ReadOnlyInspector]
    public Vector3 initialSpawnPosition;

    public VoidEventChannelSO OnEventLastCheckpoint;
    private void Awake()
    {     
        currentSpawnPosition = gameObject.transform.position;
        initialSpawnPosition = gameObject.transform.position;
    }
    private void OnEnable() {
        OnEventLastCheckpoint.OnEventRaised += MoveToLastCheckPoint;
    }
    private void MoveToLastCheckPoint() {
        gameObject.transform.position = currentSpawnPosition;
        
    }
    private void OnDisable() {
        OnEventLastCheckpoint.OnEventRaised -= MoveToLastCheckPoint;
    }
}