using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Tetris_new
{
    public static class Serialization
    {
        public static void SaveGameState(string filename, GameState gameState) //serialization func to write to a file, saves the current gamestate
        {
            try
            {
                using (FileStream stream = new FileStream(filename, FileMode.Create))  //(filemode.create) if the file exists it should be overwritten
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, gameState);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save game state: {ex.Message}");
            }
        }

        public static GameState LoadGameState(string filename) //deserialization func to open a file and load last saved gamestate
        {
            try
            {
                using (FileStream stream = new FileStream(filename, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    return (GameState)formatter.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load game state: {ex.Message}");
                return null;
            }
        }

    }
}
