using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisualSpawn : MonoBehaviour {

    [SerializeField] private PlateCounter _plateCounter;
    [SerializeField] private GameObject _platePrefab;
    [SerializeField] private Transform _spawnLocation;
    private List<GameObject> _plateList;


    private void Awake() {
        _plateList = new List<GameObject>();
    }

    private void Start() {
        _plateCounter.SpawnPlateOnCounterEvent += _plateCounter_SpawnPlateOnCounter;
        _plateCounter.PlatePickedUpEvent += _plateCounter_PlatePickedUpEvent;
    }

    private void _plateCounter_PlatePickedUpEvent(object sender, System.EventArgs e) {
        // get the plate game object saved into the list
        GameObject plateGameObject = _plateList[_plateList.Count - 1];
        // remove the plate game object from the list
        _plateList.Remove(plateGameObject);
        // Destroy the plate game object visual
        Destroy(plateGameObject);
    }

    private void _plateCounter_SpawnPlateOnCounter(object sender, System.EventArgs e) {
        // spawn the plate game object
        GameObject plateGameObject = Instantiate(_platePrefab, _spawnLocation);
        // control the space between each plate.
        float plateOffSetY = 0.1f;
        // place the plates on top of each other.
        plateGameObject.transform.localPosition = new Vector3(0f, plateOffSetY * _plateList.Count, 0f);
        // add the spawned plate to the list to update the visual later using this list
        _plateList.Add(plateGameObject);
    }


}
