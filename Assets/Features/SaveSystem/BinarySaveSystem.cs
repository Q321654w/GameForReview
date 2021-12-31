using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Scores;
using UnityEngine;

namespace SaveSystem
{
    public class BinarySaveSystem : ISaveSystem
    {
        private string _path = Application.persistentDataPath + "/Save.dat";

        public void Save(Score score)
        {
            var scoreBoard = new ScoreBoard(score.CurrentScore);
            
            var binaryFormatter = new BinaryFormatter();

            using var stream = new FileStream(_path, FileMode.Create);
            binaryFormatter.Serialize(stream, scoreBoard);
        }

        public ScoreBoard Load()
        {
            if (!File.Exists(_path)) 
                return null;
            
            var binaryFormatter = new BinaryFormatter();
            using var stream = new FileStream(_path, FileMode.Open);
            
            var scoreBoard = binaryFormatter.Deserialize(stream) as ScoreBoard;
            return scoreBoard;
        }
    }
}