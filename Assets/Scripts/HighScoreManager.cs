using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class HighScoreManager : MonoBehaviour
{
    public ListContainer nameScore;
    public Text nameList;
    public Text scoreList;

    // Start is called before the first frame update
    void Start()
    {
        LoadHighScores();
        UpdateList();
        SaveHighScores();
    }

    [System.Serializable]
    public class NameScoreParings
    {
        public string name;
        public int score;

        public NameScoreParings(string n, int s)
        {
            this.name = n;
            this.score = s;
        }
    }

    [System.Serializable]
    public class ListContainer
    {
        public List<NameScoreParings> tempBS;
    }

    private static int ComparePairings(NameScoreParings x, NameScoreParings y)
    {
        if (x.score == y.score)
        {
            return 0;
        }
        else if(x.score > y.score)
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }

    public void UpdateList()
    {
        string updatedText = "\tName\n";
        for (int i = 0; i < 9; i++)
        {
            updatedText += (i + 1) + ". " + nameScore.tempBS[i].name + "\n";
        }
        nameList.text = updatedText;

        updatedText = "Score\n";
        for (int i = 0; i < 9; i++)
        {
            updatedText += nameScore.tempBS[i].score + "\n";
        }
        scoreList.text = updatedText;
    }

    public void SaveHighScores()
    {
        string json = JsonUtility.ToJson(nameScore);
        File.WriteAllText(Application.persistentDataPath + "/highScoreList.json", json);
    }

    public void LoadHighScores()
    {
        string path = Application.persistentDataPath + "/highScoreList.json";
        if (File.Exists(path)) 
        {
            string json = File.ReadAllText(path);
            nameScore = JsonUtility.FromJson<ListContainer>(json);
        }
        else
        {
            nameScore.tempBS = new List<NameScoreParings>();
            nameScore.tempBS.Add(new NameScoreParings("Danathan", 9));
            nameScore.tempBS.Add(new NameScoreParings("Dathanyal", 8));
            nameScore.tempBS.Add(new NameScoreParings("Dandypants", 7));
            nameScore.tempBS.Add(new NameScoreParings("Darth maul", 6));
            nameScore.tempBS.Add(new NameScoreParings("Darmanitan", 5));
            nameScore.tempBS.Add(new NameScoreParings("Danger zone", 4));
            nameScore.tempBS.Add(new NameScoreParings("Damn Danyal", 3));
            nameScore.tempBS.Add(new NameScoreParings("Dandastic", 2));
            nameScore.tempBS.Add(new NameScoreParings("Danimal", 10));
            nameScore.tempBS.Sort(ComparePairings);
        }
    }    
}
