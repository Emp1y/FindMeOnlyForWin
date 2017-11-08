using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FindMeOnlyForWin
{
    class SpriteClass
    {
        public Texture2D texture
        {
            get;
        }
        //координаты центра спрайта
        public float x
        {
            get;
            set;
        }

        public float y
        {
            get;
            set;
        }
       //угол поворота спрайта
        public float angle
        {
            get;
            set;
        }
        //скорость изменения переменных за секунду
        public float dX
        {
            get;
            set;
        }

        public float dY
        {
            get;
            set;
        }

        public float dA
        {
            get;
            set;
        }
        //масштаб спрайта
        public float scale
        {
            get;
            set;
        }

        public SpriteClass(GraphicsDevice graphicsDevice, string textureName, float scale)
        {
      
            this.scale = scale;
             //Content.RootDirectory = "Content";
            if (texture == null)
            {
                using (var stream = TitleContainer.OpenStream(textureName))
                {
                    texture = Texture2D.FromStream(graphicsDevice, stream);
                }
            }
        }

        public void Update(float elapsedTime)
        {
            this.x += this.dX * elapsedTime;
            this.y += this.dY * elapsedTime;
            this.angle += this.dA * elapsedTime;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 spritePosition = new Vector2(this.x, this.y);
            spriteBatch.Draw(texture, spritePosition, null, Color.White, this.angle, new Vector2(texture.Width / 2, texture.Height / 2), new Vector2(scale, scale), SpriteEffects.None, 0f);
        }
    }
}
