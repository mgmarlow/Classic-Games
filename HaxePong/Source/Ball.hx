package ;
import openfl.display.Sprite;

class Ball extends Sprite {
    public function new(x, y) {
        super();
        this.graphics.beginFill(0xffffff);
        this.graphics.drawCircle(0, 0, 10);
        this.graphics.endFill();
        this.x = x;
        this.y = y;
    }
}
