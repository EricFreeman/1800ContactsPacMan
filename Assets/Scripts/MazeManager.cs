using UnityEngine;

namespace Assets.Scripts
{
    public class MazeManager : MonoBehaviour
    {
        public GameObject Wall;
        public GameObject Floor;
        public GameObject SolutionDroplet;

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

	                    if (column%2 == 0 && row%2 == 0)
	                    {
		                    var droplet = Instantiate(SolutionDroplet);
		                    droplet.transform.position = new Vector3(column, 0, row);
	                    }
                    }
                }
            }
        }

        private char[,] GenerateMap()
        {
	        const int size = 50;
			var mapArray = new char[size, size];
			for (var i = 0; i < size; i++)
	        {
		        for (var j = 0; j < size; j++)
		        {
			        var block = 'F';
					if (i == 0 || j == 0 || i == size - 1 || j == size - 1)
			        {
				        block = 'W';
			        }
			        mapArray[i, j] = block;
		        }
	        }

            return mapArray;
        }
    }
}