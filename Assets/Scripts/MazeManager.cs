using UnityEditor;
using UnityEngine;

namespace Assets.Scripts
{
    public class MazeManager : MonoBehaviour
    {
        public GameObject Wall;
        public GameObject Floor;

        void Start ()
        {
            var map = GenerateMap();
            CreateMapObjects(map);
        }

        private void CreateMapObjects(char[,] map)
        {
            for (var row = 0; row < map.GetLength(0); row++)
            {
                for (var column = 0; column < map.GetLength(1); column++)
                {
                    var tile = map[column, row];
                    if (tile == 'W')
                    {
                        var wall = Instantiate(Wall);
                        wall.transform.position = new Vector3(column, 0, row);
                    }
                    if (tile == 'F')
                    {
                        var floor = Instantiate(Floor);
                        floor.transform.position = new Vector3(column, 0, row);
                    }
                }
            }
        }

        private char[,] GenerateMap()
        {
            var mapArray = new char[5,5];
            mapArray[0, 0] = 'W';
            mapArray[0, 1] = 'W';
            mapArray[0, 2] = 'W';
            mapArray[0, 3] = 'W';
            mapArray[0, 4] = 'W';
            mapArray[1, 0] = 'W';
            mapArray[1, 1] = 'F';
            mapArray[1, 2] = 'F';
            mapArray[1, 3] = 'F';
            mapArray[1, 4] = 'W';
            mapArray[2, 0] = 'W';
            mapArray[2, 1] = 'F';
            mapArray[2, 2] = 'F';
            mapArray[2, 3] = 'F';
            mapArray[2, 4] = 'W';
            mapArray[3, 0] = 'W';
            mapArray[3, 1] = 'F';
            mapArray[3, 2] = 'F';
            mapArray[3, 3] = 'F';
            mapArray[3, 4] = 'W';
            mapArray[4, 0] = 'W';
            mapArray[4, 1] = 'W';
            mapArray[4, 2] = 'W';
            mapArray[4, 3] = 'W';
            mapArray[4, 4] = 'W';

            return mapArray;
        }
    }
}