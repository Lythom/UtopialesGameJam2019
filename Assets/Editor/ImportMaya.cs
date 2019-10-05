using UnityEngine;
using UnityEditor;

class ImportMaya : AssetPostprocessor {

    void OnPreprocessModel () {
        (assetImporter as ModelImporter).globalScale = 30;
    }

}