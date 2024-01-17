using UnityEngine;

public class CoinManager : MonoBehaviour
{
    private const string CoinsKey = "Coins";

    public static int GetCoins()
    {
        return PlayerPrefs.GetInt(CoinsKey, 0);
    }

    public static void AddCoins(int amount)
    {
        int currentCoins = GetCoins();
        currentCoins += amount;
        PlayerPrefs.SetInt(CoinsKey, currentCoins);
        PlayerPrefs.Save(); // Ensure changes are saved immediately
    }

    public static void RemoveCoins(int amount)
    {
        int currentCoins = GetCoins();
        currentCoins = Mathf.Max(0, currentCoins - amount); // Ensure coins don't go below zero
        PlayerPrefs.SetInt(CoinsKey, currentCoins);
        PlayerPrefs.Save(); // Ensure changes are saved immediately
    }
}
