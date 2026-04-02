using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class leaderboard : MonoBehaviour
{
    public TextMeshProUGUI[] rowtext;
    void Start()
    {
        if (!PlayerPrefs.HasKey("1name"))
        {
            for (int i = 1; i <= 12; i++)
            {
                PlayerPrefs.SetString(i + "name", "AAA");
                PlayerPrefs.SetInt(i + "score", 0);
            }
            PlayerPrefs.Save();
        }

        UpdateLeaderboardDisplay();
    }

    public void UpdateLeaderboardDisplay()
    {
        List<KeyValuePair<string, int>> validScores = new();

        for (int i = 1; i <= 12; i++)
        {
            string savedName = PlayerPrefs.GetString(i + "name", "");
            int savedScore = PlayerPrefs.GetInt(i + "score", 0);

            if (string.IsNullOrEmpty(savedName) == false && savedName != "AAA" && savedScore != 0)
            {
                validScores.Add(new KeyValuePair<string, int>(savedName, savedScore));
            }
        }

        var sortedScroes = validScores.OrderByDescending(s => s.Value).ToList();

        for (int i = 0; i < rowtext.Length; i++)
        {
            if (i < sortedScroes.Count)
            {
                rowtext[i].text = i + ". " + $"{sortedScroes[i].Key}: {sortedScroes[i].Value}";
            }
            else
            {
                rowtext[i].text = "";
            }

            scrollBar scroller = rowtext[i].GetComponent<scrollBar>();
            if (scroller != null)
            {
                scroller.SetupScroll();
            }
        }
    }
}
