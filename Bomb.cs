using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Putinator
{
    class Bomb
    {
        public int posY;
        public int posX; 
        public int hastighetUpp = 10;
        public int hastighet = 10;
        int blastDiameter = 40;
        int bombCenterX;
        int bombCenterY;
        int bombSkada = 1;
        public Rectangle bombRektangel;
        public Rectangle blastRektangel;
        Texture2D bombBild = Game1.bomb;
        Texture2D explosion = Game1.explosion;
        
        public Bomb (int posX, int posY)
        {
            this.posX = posX;
            this.posY = posY;
        }
        
        public void skapaBomb(SpriteBatch spriteBatch)
        {
            bombRektangel = new Rectangle(posX, posY, 70, 70);
            spriteBatch.Draw(bombBild, bombRektangel, Color.White);
        }

        public void Explode(SpriteBatch spriteBatch)
        {
            if (blastDiameter < 500)
            {
                bombCenterX = posX - blastDiameter / 2;
                bombCenterY = posY - blastDiameter / 2;
                blastRektangel = new Rectangle(bombCenterX, bombCenterY, blastDiameter, blastDiameter);
                blastDiameter += 30;

                spriteBatch.Draw(explosion, blastRektangel, Color.White);

                if (blastRektangel.Intersects(Match.spelare1.karaktärRektangelTotal))
                {
                    Match.spelare1.liv -= bombSkada;
                }
                if (blastRektangel.Intersects(Match.spelare2.karaktärRektangelTotal))
                {
                    Match.spelare2.liv -= bombSkada;
                }
            }
        }
    }
}
