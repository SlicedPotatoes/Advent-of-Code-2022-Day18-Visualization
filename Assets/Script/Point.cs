namespace Assets
{
    // Une classe simple pour représenter un point en 3D avec des coordonnées x, y et z.
    internal class Point
    {
        // Les coordonnées x, y et z du point.
        public int x;
        public int y;
        public int z;

        // Constructeur prenant un tableau d'entiers pour initialiser les coordonnées.
        public Point(int[] coord)
        {
            x = coord[0];
            y = coord[1];
            z = coord[2];
        }
        // Constructeur prenant des valeurs individuelles pour les coordonnées x, y et z.
        public Point(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
}
