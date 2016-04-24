/*
    Graham Marlow
    Space Invaders Clone
    Programmed with Phaser
    4/9/2015
*/

var WINDOW_WIDTH = 400,
    WINDOW_HEIGHT = 500;

var game = new Phaser.Game(WINDOW_WIDTH, WINDOW_HEIGHT, Phaser.AUTO, 'gameDiv');


var mainState = function(game) {};
//var bullet;
mainState.prototype = {
    preload: function() {
        game.load.image('background', 'assets/starfield.png');
        game.load.spritesheet('player', 'assets/ship.png', 16, 24);
        game.load.spritesheet('alien', 'assets/enemy-medium.png', 32, 16);
        game.load.image('bullet', 'assets/bullet.png');
        game.load.spritesheet('explode', 'assets/explode.png', 128, 128);
    },

    create: function() {
        var bullet;
        this.bulletTime = 0;

        this.background = game.add.image(0, 0, 'background');
        game.physics.startSystem(Phaser.Physics.ARCADE);

        this.cursor = game.input.keyboard.createCursorKeys();

        // player
        this.player = game.add.sprite(game.world.centerX, WINDOW_HEIGHT-50, 'player');
        this.player.anchor.setTo(0.5, 0.5);
        game.physics.arcade.enable(this.player);
        this.player.body.collideWorldBounds = true;
        this.player.body.immovable = true;
        this.player.animations.add('left', [0, 1], 16, true);
        this.player.animations.add('right', [3, 4], 16, true);

        // enemies
        this.enemies = game.add.group();
        this.enemies.physicsBodyType = Phaser.Physics.ARCADE;
        this.enemies.enableBody = true;

        this.createEnemies();

        // bullet group
        this.bullets = game.add.group();
        this.bullets.enableBody = true;
        this.bullets.physicsBodyType = Phaser.Physics.ARCADE;
        this.bullets.createMultiple(30, 'bullet');
        this.bullets.setAll('outOfBoundsKill', true);
        this.bullets.setAll('checkWorldBounds', true);

        // define z as the key to shoot bullets
        this.shootKey = game.input.keyboard.addKey(Phaser.Keyboard.Z);

        // setup explosion animation
        this.explosions = game.add.group();
        this.explosions.createMultiple(30, 'explode');
        this.explosions.forEach(this.setupExplosion, this);

        // lets add some text for game state
        this.myText = game.add.text(game.world.centerX, game.world.centerY, ' ', { font: '20px Helvetica', fill: '#fff' });
        this.myText.anchor.setTo(0.5, 0.5);
        this.myText.text = "Game Over. Click to Restart";
        this.myText.visible = false;

    },

    update: function() {
        this.movePlayer();
        if (this.shootKey.isDown) {
            this.playerShoot();
        }

        // collision handling
        game.physics.arcade.overlap(this.bullets, this.enemies, this.bulletCollide, null, this);
        game.physics.arcade.overlap(this.player, this.enemies, this.enemyCollide, null, this);
    },

    movePlayer: function() {
        if(this.cursor.left.isDown) {
            this.player.body.velocity.x = -200;
            this.player.animations.play('left');
        }
        else if(this.cursor.right.isDown) {
            this.player.body.velocity.x = 200;
            this.player.animations.play('right');
        }
        else {
            this.player.body.velocity.x = 0;
            this.player.animations.stop();
            this.player.frame = 2;
        }
    },

    playerShoot: function() {
        // make sure to time out bullets
        if (game.time.now > this.bulletTime) {
            // get the first bullet from the group
            bullet = this.bullets.getFirstExists(false);

            if (bullet) {
                bullet.reset(this.player.x - 3, this.player.y - 26);
                bullet.body.velocity.y = -400;
                // setup a timer so that the player cannot shoot infinitely
                this.bulletTime = game.time.now + 650;
            }
        }
    },

    setupExplosion: function(alien) {
        alien.anchor.x = 0.5;
        alien.anchor.y = 0.5;
        alien.animations.add('explode');
    },

    createEnemies: function() {
        for (var row = 1; row < 5; row++) {
            for (var col = 1; col < 7; col++) {
                this.alien = this.enemies.create((col * 56), row * 40, 'alien');
                this.alien.anchor.setTo(0.5, 0.5);
                this.alien.animations.add('fly', [0, 1], 20, true);
                this.alien.animations.play('fly');
                this.alien.body.velocity.y = 15;
                //this.alien.body.velocity.x = 10;
            }
        }

        //var tween = game.add.tween(this.enemies).to( {x: 200 }, 2000, Phaser.Easing.Linear.None, true, 0, 1000, true);
        //tween.onLoop((function() { this.enemies.y += 10 })(), this);
    },

    bulletCollide: function(bullet, alien) {
        bullet.kill();
        alien.kill();

        // grab first of the explosions group
        var explosion = this.explosions.getFirstExists(false);
        // add explosion to the current alien
        explosion.reset(alien.body.x, alien.body.y);
        explosion.play('explode', 30, false, true);

        if (this.enemies.countLiving() === 0) {
            this.myText.text = "You Won! Click to play again.";
            this.myText.visible = true;

            game.input.onTap.addOnce(this.restart, this);
        }
    },

    enemyCollide: function(player, enemy) {
        var explosion = this.explosions.getFirstExists(false);
        explosion.reset(this.player.body.x, this.player.body.y);
        explosion.play('explode', 30, false, true);

        this.player.kill();
        //this.text = "Game Over \n Click to restart";
        this.myText.visible = true;
        game.input.onTap.addOnce(this.restart, this);
    },

    restart: function() {
        this.enemies.removeAll();
        this.createEnemies();
        this.player.revive();
        this.myText.text = "Game Over. Click to Restart";
        this.myText.visible = false;
    }

    //enemyMove: function() {
    //    this.enemies.y += 10;
    //}

};

game.state.add('main', mainState);
game.state.start('main');
