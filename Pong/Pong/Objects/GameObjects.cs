using System.Xml.Schema;
using Pong.Sprites;

namespace Pong
{
    public class GameObjects
    {
        public Paddle PlayerPaddle { get; set; }
        public Paddle ComputerPaddle { get; set; }
        public Ball Ball { get; set; }
        public Score Score { get; set; }
    }
}