using UnityEngine;
using System.IO;

public static class Guardado
{
    private static string SaveFilePath => Path.Combine(Application.persistentDataPath, "savefile.json");

    public static void SaveGame(JugadorNave playerData)
    {

        //JugadorNave playerData = new JugadorNave
        //{
        //    name = "Pepe",
        //    health = playerController.health,
        //    movimiento = new MovimientoNave(3, 75) // Save other components
        //};

        //playerData.SetGun(playerController.GetCurrentGunData());

        playerData.GetDisparoNave();

        string json = JsonUtility.ToJson(playerData, true);

        File.WriteAllText(SaveFilePath, json);

        Debug.Log("Guardado " + SaveFilePath);
    }

    public static JugadorNave LoadGame()
    {
        if (!File.Exists(SaveFilePath))
        {
            Debug.LogWarning("Save file not found!");
            return null;
        }

        string json = File.ReadAllText(SaveFilePath);

        JugadorNave playerData = JsonUtility.FromJson<JugadorNave>(json);

        //player.disparo = player.GetGun();
        //if (player.disparo != null)
        //{
        //    player.disparo = DeserializeGun(player.disparo);
        //}

        Debug.Log("Game loaded from " + SaveFilePath);
        Debug.Log("Loaded data from JSON: " + json);
        return playerData;
    }

    //private static DisparoNave DeserializeDisparo(DisparoNave disparoGenerico)
    //{
    //    switch (disparoGenerico.tipoDisparo)
    //    {
    //        case "DisparoPreciso":
    //            return JsonUtility.FromJson<DisparoPreciso>(JsonUtility.ToJson(disparoGenerico));
    //        case "DisparoCargado":
    //            return JsonUtility.FromJson<DisparoCargado>(JsonUtility.ToJson(disparoGenerico));
    //        default:
    //            Debug.LogWarning("No hay");
    //            return null;
    //    }
    //}
}