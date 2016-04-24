var Game = Game || {};
var Score;

Game.preload = function() {
    game.load.image('snake', './img/snake.png');
    game.load.image('apple', './img/apple.png');
}

Game.create = function() {
    this.snake = [];
    this.apple = {};
    this.squareSize = 15;
    Score = 0;
    this.speed = 0;
    this.updateDelay = 0;
    this.direction = 'right'; // initial direction
    this.newDirection = null; // buffer for new directions
    this.addNew = false; // check if apple has been eaten

    this.cursors = game.input.keyboard.createCursorKeys();

    game.stage.backgroundColor = '#061f27';

    // initial snake stack
    for (var i = 0; i < 10; i++) {
        this.snake[i] = game.add.sprite(150 + i * this.squareSize, 150, 'snake');
    }

    this.generateApple();

    // text
    var textStyle_Key = { font: 'bold 14px sans-serif', fill: '#46c0f9', align: 'center' };
    var textStyle_Value = { font: 'bold 18px sans-serif', fill: '#fff', align: 'center' };
    //score
    game.add.text(30, 20, 'SCORE', textStyle_Key);
    this.scoreTextValue = game.add.text(90, 18, Score.toString(), textStyle_Value);
    // speed
    game.add.text(500, 20, 'SPEED', textStyle_Key);
    this.speedTextValue = game.add.text(558, 18, this.speed.toString(), textStyle_Value);
}

Game.update = function() {
    // prevent illegal direction changes (180 degrees)
    if (this.cursors.right.isDown && this.direction != 'left') {
        this.newDirection = 'right';
    }
    else if (this.cursors.left.isDown && this.direction != 'right') {
        this.newDirection = 'left';
    }
    else if (this.cursors.up.isDown && this.direction != 'down') {
        this.newDirection = 'up';
    }
    else if (this.cursors.down.isDown && this.direction != 'up') {
        this.newDirection = 'down';
    }

    // speed based off of score, increasing as score increases
    this.speed = Math.min(10, Math.floor(Score / 5));
    this.speedTextValue = '' + this.speed;

    // delay gamespeed
    this.updateDelay++;

    if (this.updateDelay % (8 - this.speed) == 0) {
        var firstCell = this.snake[this.snake.length - 1],
            lastCell = this.snake.shift();
            oldLastCell_x = lastCell.x,
            oldLastCell_y = lastCell.y;

        if (this.newDirection) {
            this.direction = this.newDirection;
            this.newDirection = null;
        }

        switch (this.direction) {
            case 'right':
                lastCell.x = firstCell.x + 15;
                lastCell.y = firstCell.y;
                break;
            case 'left':
                lastCell.x = firstCell.x - 15;
                lastCell.y = firstCell.y;
                break;
            case 'up':
                lastCell.x = firstCell.x;
                lastCell.y = firstCell.y - 15;
                break;
            case 'down':
                lastCell.x = firstCell.x;
                lastCell.y = firstCell.y + 15;
                break;
        }

        // place last cell in front of stack
        this.snake.push(lastCell);
        firstCell = lastCell;

        // collision detection
        if (this.addNew) {
            this.snake.unshift(game.add.sprite(oldLastCell_x, oldLastCell_y, 'snake'));
            this.addNew = false;
        }

        this.appleCollision();
        this.selfCollision(firstCell);
        this.wallCollision(firstCell);
    }
}

Game.generateApple = function () {
    var randomX = Math.floor(Math.random() * 40) * this.squareSize,
		  randomY = Math.floor(Math.random() * 30) * this.squareSize;

	this.apple = game.add.sprite(randomX, randomY, 'apple');
}

Game.appleCollision = function() {
    // TODO: Check only the head of the snake, prevent apple spawning
    // within snake
    for (var i = 0; i < this.snake.length; i++) {
        if (this.snake[i].x === this.apple.x && this.snake[i].y === this.apple.y) {
            this.addNew = true;
            this.apple.destroy();
            this.generateApple();
            Score++;
            this.scoreTextValue.text = Score.toString();
        }
    }
}

Game.selfCollision = function(head) {
    for (var i = 0; i < this.snake.length - 1; i++) {
        if (head.x === this.snake[i].x && head.y === this.snake[i].y) {
            game.state.start('GameOver');
        }
    }
}

Game.wallCollision = function(head) {
    if (head.x >= 600 || head.x < 0 || head.y >= 450 || head.y < 0) {
            game.state.start('GameOver');
    }
}
