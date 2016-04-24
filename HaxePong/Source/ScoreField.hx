package ;
import openfl.text.TextField;
import openfl.text.TextFormat;

class ScoreField extends TextField {
    public function new(width, y, format:TextFormat):Void {
        super();
        this.width = width;
        this.y = y;
        this.defaultTextFormat = format;
        this.selectable = false;
    }
}
