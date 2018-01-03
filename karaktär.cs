using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Putinator
{
    class karaktär
    {
        public string karaktärNamn;
        public Vector2 storlek;
        public Texture2D huvud;
        private Texture2D kropp;
        public int posX = 50;
        public int posY = 50;
        public int hastighetUpp = 0;
        public int borderXMax = Game1.borderXMax;
        public int borderYMax = Game1.borderYmax;
        public int borderYMin = 0;
        int bombHastighetAbs = 20;
        int bombHastighet;
        public int liv = 100;
        public Rectangle karaktärRektangelKropp;
        private Rectangle karaktärRektangelHuvud;
        public Rectangle karaktärRektangelTotal;       
        public Bomb bomb;
        public bool drawBomb = false;
        public bool explodera = false;
        

        public karaktär(string namn, Vector2 storlek, Texture2D huvud, Texture2D kropp, int posX, int posY)
        {
            this.karaktärNamn = namn;
            this.storlek = storlek;
            this.huvud = huvud;
            this.kropp = kropp;
            this.posX = posX;
            this.posY = posY;
        }
        // placerar huvudet på kroppen och ritar ut dem
        public void Draw(SpriteBatch spriteBatch)
        {
            if (karaktärNamn == "Clinton")
            {
                karaktärRektangelHuvud = new Rectangle(posX + (2 * (int)storlek.X / 8), posY - ((int)storlek.Y / 3),
                ((int)storlek.X) / 2, ((int)storlek.Y) / 2);
            }
            else
            {
                karaktärRektangelHuvud = new Rectangle(posX + (2 * (int)storlek.X / 5), posY - ((int)storlek.Y / 4),
                ((int)storlek.X) / 2, ((int)storlek.Y) / 2);
            }
            karaktärRektangelKropp = new Rectangle(posX, posY, (int)storlek.X, (int)storlek.Y);
            spriteBatch.Draw(kropp, karaktärRektangelKropp, Color.White);
            spriteBatch.Draw(huvud, karaktärRektangelHuvud, Color.White);
            karaktärRektangelTotal = new Rectangle(posX, posY - ((int)storlek.Y / 4), (int)storlek.X, (int)storlek.Y + 80);
        }

        // alla kontroller till karaktären
        public void kontroller(karaktär spelare, KeyboardState tangentbord,int spelarNummer,karaktär motståndare)
        {
            int hastighet = 10;
            int tyngdacceleration = 1;
            Keys vänster;
            Keys höger;
            Keys upp;
            Keys attack;
            string riktning = " ";
            
            if (spelarNummer == 2)
            {
                vänster = Keys.Left;
                höger = Keys.Right;
                upp = Keys.Up;
                attack = Keys.RightControl;
            }
            else
            {
                vänster = Keys.A;
                höger = Keys.D;
                upp = Keys.W;
                attack = Keys.Space;
            }

            if (tangentbord.IsKeyDown(vänster) && spelare.posX > 0)
            {
                spelare.posX -= hastighet;
                riktning = "vänster";
            }
            if (tangentbord.IsKeyDown(höger) && spelare.posX < borderXMax)
            {
                spelare.posX += hastighet;
                riktning = "höger";
            }
            if (spelare.posY < borderYMax)
            {
                spelare.posY -= hastighetUpp;
                spelare.posY += tyngdacceleration;
                hastighetUpp -= tyngdacceleration;
            }
            if (tangentbord.IsKeyDown(upp) && spelare.posY > borderYMax)
            {
                hastighetUpp = 40;
                spelare.posY -= hastighetUpp;
            }
            if (tangentbord.IsKeyDown(attack) && drawBomb == false)
            {
                bomb = new Bomb(spelare.posX, spelare.posY);
                bomb.posX = spelare.posX;
                bomb.posY = spelare.posY;
                drawBomb = true;
                if (riktning == "vänster")
                {
                    bombHastighet = -bombHastighetAbs;
                }
                else if (riktning == "höger")
                {
                   bombHastighet = bombHastighetAbs;
                }
                else
                {
                    bombHastighet = 0;
                }
            }

            if (drawBomb == true && bomb.posY <= borderYMax+130)
            {
                bomb.posY -= bomb.hastighetUpp;
                bomb.posY += tyngdacceleration;
                bomb.hastighetUpp -= tyngdacceleration;
                bomb.posX += bombHastighet;

                if (bomb.posY >= borderYMax+100 || bomb.bombRektangel.Intersects(motståndare.karaktärRektangelTotal))
                {
                    explodera = true;
                    drawBomb = false;             
                }
                else
                {
                    explodera = false;
                }

                if (bomb.posX <= 0 || bomb.posX >= borderXMax + 70)
                {
                    bombHastighet *= -1;
                }
            }
            
        }
        public void reset() { 
}
    }
}
