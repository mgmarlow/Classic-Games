package ;
import openfl.display.Sprite;

class Platform extends Sprite {
    public function new(x, y) {
        super(); //Required
        // Prepare to fill shape with color
        this.graphics.beginFill(0xffffff);
        // draw rectangle
        this.graphics.drawRect(0, 0, 15, 100);
        this.graphics.endFill();
        // Setup constructor variables
        // Note: graphics drawRect needs 0 for x and y for the initial drawing
        // location on the object. The code below changes the *Sprite* position
        this.x = x;
        this.y = y;
    }
}
