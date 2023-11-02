using Assets;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Script : MonoBehaviour
{
    // Enumération pour représenter les types de cube (rempli ou bloc d'air).
    public enum cubeType
    {
        filled = 0,
        airBlock = 1
    }

    // Références aux objets dans l'éditeur Unity.
    public GameObject parentFilled;
    public GameObject parentAirBlock;
    public GameObject prefab;
    public Material filledTexture;
    public Material airBlockTexture;
    public Text resultLabelP1;
    public Text resultLabelP2;

    // Caractères constants pour représenter la carte 3D.
    private const char EMPTY_SLOT_MAP = '.';
    private const char FILLED_SLOT_MAP = '#';
    private const char AIR_BLOCK = '@';

    // Tableau 3D pour stocker la carte.
    private char[,,] array3D;
    private List<Point> inputs = new List<Point>();
    private int maxCoord = 0;

    // Vecteur de directions pour la recherche de voisins.
    private Point[] dir = new Point[6]{
        new Point(1, 0, 0),
        new Point(-1, 0, 0),
        new Point(0, 1, 0),
        new Point(0, -1, 0),
        new Point(0, 0, 1),
        new Point(0, 0, -1),
    };
    // Fonction pour instancier un cube dans l'éditeur Unity en fonction du type.
    private void spawnCube(Point coord, cubeType ct)
    {
        GameObject cube = Instantiate(prefab);
        cube.transform.position = new Vector3(coord.x, coord.y, coord.z);
        switch(ct)
        {
            case cubeType.filled:
                cube.GetComponent<Renderer>().material = filledTexture;
                cube.transform.parent = parentFilled.transform;
                break;
            case cubeType.airBlock:
                cube.GetComponent<Renderer>().material = airBlockTexture;
                cube.transform.parent = parentAirBlock.transform;
                break;
        }
    }
    // Vérifie si les coordonnées se trouvent à l'intérieur des limites du tableau 3D.
    private bool inArrayLimit(int x, int y, int z)
    {
        return (x >= 0 && x < maxCoord && y >= 0 && y < maxCoord && z >= 0 && z < maxCoord);
    }
    // Obtient les voisins valides d'un point donné.
    private List<Point> getNeighbor(Point p)
    {
        List<Point> neighbors = new List<Point>();
        foreach(Point d in dir)
        {
            int x = p.x + d.x;
            int y = p.y + d.y;
            int z = p.z + d.z;

            if(inArrayLimit(x, y, z))
            {
                neighbors.Add(new Point(x, y, z));
            }
        }
        return neighbors;
    }
    // Charge les données d'entrée à partir d'un fichier.
    private bool loadInput()
    {
        string filePath = Application.dataPath + "/data.txt";
        try
        {
            using (StreamReader lecteur = new StreamReader(filePath))
            {
                string ligne;
                while ((ligne = lecteur.ReadLine()) != null)
                {
                    string[] splitedString = ligne.Split(',');
                    int[] coord = new int[splitedString.Length];
                    for (int i = 0; i < splitedString.Length; i++)
                    {
                        int value = int.Parse(splitedString[i]) + 1;
                        coord[i] = value;
                        if(value > maxCoord)
                        {
                            maxCoord = value;
                        }
                    }
                    inputs.Add(new Point(coord));
                }
                maxCoord += 2;
            }
        }
        catch (IOException e)
        {
            Console.WriteLine("Une erreur s'est produite lors de la lecture du fichier : " + e.Message);
            return false;
        }
        return true;
    }
    // Initialise le tableau 3D, remplit les valeurs d'entrée et les "blocs d'air", puis instancie les cubes dans l'éditeur Unity.
    private void initArray()
    {
        array3D = new char[maxCoord, maxCoord, maxCoord];
        for (int i = 0; i < maxCoord; i++)
        {
            for (int j = 0; j < maxCoord; j++)
            {
                for (int k = 0; k < maxCoord; k++)
                {
                    array3D[i, j, k] = EMPTY_SLOT_MAP;
                }
            }
        }
        foreach (Point p in inputs)
        {
            array3D[p.x, p.y, p.z] = FILLED_SLOT_MAP;
        }
        dfs(new Point(0, 0, 0));

        for(int x = 0; x < maxCoord; x++)
        {
            for(int  y = 0; y < maxCoord; y++)
            {
                for( int z = 0; z < maxCoord; z++)
                {
                    Point p = new Point(x, y, z);
                    if (array3D[p.x, p.y, p.z] == FILLED_SLOT_MAP)
                    {
                        spawnCube(p, cubeType.filled);
                    }
                    else if (array3D[p.x, p.y, p.z] == AIR_BLOCK)
                    {
                        spawnCube(p, cubeType.airBlock);
                    }
                }
            }
        }
    }
    // Parcours en profondeur (DFS) pour remplir les "blocs d'air".
    private void dfs(Point startPoint)
    {
        Stack<Point> stack = new Stack<Point>();
        stack.Push(startPoint);

        while (stack.Count > 0)
        {
            Point currentPoint = stack.Pop();

            if (array3D[currentPoint.x, currentPoint.y, currentPoint.z] == EMPTY_SLOT_MAP)
            {
                array3D[currentPoint.x, currentPoint.y, currentPoint.z] = AIR_BLOCK;
                List<Point> neighbors = getNeighbor(currentPoint);

                foreach (Point neighbor in neighbors)
                {
                    stack.Push(neighbor);
                }
            }
        }
    }
    // Calcule et affiche le résultat de la première partie.
    private void processP1()
    {
        int result = 0;
        foreach(Point p in inputs)
        {
            List<Point> neighbor = getNeighbor(p);
            foreach(Point n in neighbor)
            {
                if (array3D[n.x, n.y, n.z] != FILLED_SLOT_MAP)
                {
                    result++;
                }
            }
        }
        resultLabelP1.text += result;
    }
    // Calcule et affiche le résultat de la deuxième partie.
    private void processP2()
    {
        int result = 0;
        foreach (Point p in inputs)
        {
            List<Point> neighbor = getNeighbor(p);
            foreach (Point n in neighbor)
            {
                if (array3D[n.x, n.y, n.z] == AIR_BLOCK)
                {
                    result++;
                }
            }
        }
        resultLabelP2.text += result;
    }
    // Fonction appelée au démarrage du jeu.
    void Start()
    {
        
        if (loadInput())
        {
            initArray();
            processP1();
            processP2();
        }
    }
}
