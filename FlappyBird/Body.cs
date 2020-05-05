using System;

namespace FlappyBird
{
    class Body
    {
        public int x, y;
        public string rep = "*";

        public Body()
        {
        }

        public Body(Random r)
        {
            x = 100;
            y = r.Next(0, 40);
        }
    }
}
