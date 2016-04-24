package;

import openfl.Lib;
import openfl.events.Event;
import openfl.display.Sprite;
import openfl.geom.Point;
import openfl.text.TextFormatAlign;
import openfl.text.TextField;
import openfl.text.TextFormat;
import openfl.events.KeyboardEvent;
import openfl.display.Window;

enum GameState {
    Paused;
    Playing;
}

enum Player {
    Human;
    AI;
}

class Main extends Sprite {
    var inited:Bool;

    // Initialize graphics
    private var platform1:Platform;
    private var platform2:Platform;
    private var ball:Ball;

    // Keep track of current state (play/pause)
    private var currentGameState:GameState;

    // Score variables
    private var scorePlayer:Int;
    private var scoreAI:Int;
    // Score text
    private var scoreField:ScoreField;
    /*private var messageField:TextField;*/
    private var messageField:MessageField;

    // Paddle Movement
    private var arrowKeyUp:Bool;
    private var arrowKeyDown:Bool;
    private var platformSpeed:Int;

    // Ball movement
    private var ballMovement:Point;
    private var ballSpeed:Int;

    function resize(e) {
        if (!inited) init();
        // else (resize or orientation change)
    }

    function init() {
        if (inited) return;
        inited = true;

        // Left paddle
        platform1 = new Platform(5, 200);
        this.addChild(platform1);

        // Right paddle
        platform2 = new Platform(480, 200);
        this.addChild(platform2);

        // Ball
        ball = new Ball(250, 250);
        this.addChild(ball);

        // text
        // Score Format
        var scoreFormat:TextFormat = new TextFormat("_sans", 24, 0xbbbbbb, true);
        scoreFormat.align = TextFormatAlign.CENTER;

        // Message format
        var messageFormat:TextFormat = new TextFormat("_sans", 18, 0xbbbbbb, true);
        messageFormat.align = TextFormatAlign.CENTER;

        // Draw score
        scoreField = new ScoreField(500, 30, scoreFormat);
        addChild(scoreField);

        // Draw Message
        messageField = new MessageField(500, 450, messageFormat);
        addChild(messageField);
        messageField.text = "Press SPACE to start\nUse ARROW KEYS to move your platform";

        // Initial score
        scorePlayer = 0;
        scoreAI = 0;

        // Initial Movement variables
        arrowKeyUp = false;
        arrowKeyDown = false;
        platformSpeed = 7;

        ballSpeed = 7;
        ballMovement = new Point(0,0);

        setGameState(Paused);

        stage.addEventListener(KeyboardEvent.KEY_DOWN, keyDown);
        stage.addEventListener(KeyboardEvent.KEY_UP, keyUp);

        // Game loop, add to Main instance
        this.addEventListener(Event.ENTER_FRAME, update);
    }

    private function update(event:Event):Void {
        if (currentGameState == Playing) {
            if (arrowKeyUp) {
                platform1.y -= platformSpeed;
            }
            if (arrowKeyDown) {
                platform1.y += platformSpeed;
            }
            if (platform1.y < 5) platform1.y = 5;
            if (platform1.y > 395) platform1.y = 395;

            ball.x += ballMovement.x;
            ball.y += ballMovement.y;
            if (ball.y < 5 || ball.y > 495) ballMovement.y *= -1;
            if (ball.x < 5) winGame(AI);
            if (ball.x > 495) winGame(Human);

            // Player ball collision
            if (ballMovement.x < 0 && ball.x < 30 &&
                ball.y >= platform1.y && ball.y <= platform1.y + 100) {
                    bounceBall();
                    ball.x = 30;
            }

            // AI ball collision
            if (ballMovement.x > 0 && ball.x > 470 &&
                ball.y >= platform2.y && ball.y <= platform2.y + 100) {
                    bounceBall();
                    ball.x = 470;
            }

            // AI
            // Move platform when ball is 2/5 of screen's width away
            if (ball.x > 300 && ball.y > platform2.y + 70) {
                platform2.y += platformSpeed;
            }

            if (ball.x > 300 && ball.y < platform2.y + 30) {
                platform2.y -= platformSpeed;
            }

            if (platform2.y < 5) platform2.y = 5;
            if (platform2.y > 395) platform2.y = 395;
        }
    }

    private function keyDown(event:KeyboardEvent):Void {
        if (currentGameState == Paused && event.keyCode == 32) { // Space
            setGameState(Playing);
        } else if (event.keyCode == 38) { // Up
            arrowKeyUp = true;
        } else if (event.keyCode == 40) { // Down
            arrowKeyDown = true;
        }
    }

    private function keyUp(event:KeyboardEvent):Void {
        if (event.keyCode == 38) { // Up
            arrowKeyUp = false;
        } else if (event.keyCode == 40) { // Down
            arrowKeyDown = false;
        }
    }

    private function bounceBall():Void {
        var direction:Int = (ballMovement.x > 0)?(-1):(1);
        var randomAngle:Float = (Math.random() * Math.PI/2) - 45;
        ballMovement.x = direction * Math.cos(randomAngle) * ballSpeed;
        ballMovement.y = Math.sin(randomAngle) * ballSpeed;
    }

    private function winGame(player:Player) {
        if (player == Human) {
            scorePlayer++;
        } else {
            scoreAI++;
        }
        setGameState(Paused);
    }

    private function updateScore():Void {
        scoreField.text = scorePlayer + ":" + scoreAI;
    }

    private function setGameState(state:GameState):Void {
        currentGameState = state;
        updateScore();
        if (state == Paused) {
            messageField.alpha = 1;
        } else {
            messageField.alpha = 0;
            platform1.y = 200;
            platform2.y = 200;
            ball.x = 250;
            ball.y = 250;
            var direction:Int = (Math.random() > .5) ? (1):(-1);
            var randomAngle:Float = (Math.random() * Math.PI / 2) - 45;
            ballMovement.x = direction * Math.cos(randomAngle) * ballSpeed;
            ballMovement.y = Math.sin(randomAngle) * ballSpeed;
        }
    }

    public function new () {
		super ();
        addEventListener(Event.ADDED_TO_STAGE, added);
	}

    function added(e) {
        removeEventListener(Event.ADDED_TO_STAGE, added);
        stage.addEventListener(Event.RESIZE, resize);
        #if ios
        haxe.Timer.delay(init, 100);
        #else
        init();
        #end
    }

    public static function main() {
        Lib.current.stage.align = flash.display.StageAlign.TOP_LEFT;
        Lib.current.stage.scaleMode = flash.display.StageScaleMode.NO_SCALE;
        Lib.current.addChild(new Main());
    }
}
