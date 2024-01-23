using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    // Manager is our entry point and will spawn in the intital prefabs, etc. As such it should 
    // beat them all to the scene so having an instance like this is fairly safe. Just dont delete the manager!
    public static Manager instance;

    public Canvas canvas;
    public SwipeManager swipeManager;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        DataImporter importer = new DataImporter();
        importer.requestAllData();

        Catalog catalog = Resources.Load<Catalog>(Catalog.CATALOG_PREFAB_PATH);

        Instantiate(catalog, canvas.transform);

     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
