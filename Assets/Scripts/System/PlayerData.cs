using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float[] playerPosition;
    public string playerScene;

    public PlayerData(Vector3 position, SceneField newScene)
    {
        playerPosition = new float[3];
        playerPosition[0] = position.x;
        playerPosition[1] = position.y;
        playerPosition[2] = position.z;

        playerScene = newScene;
    }
}
