var GameOver = GameOver || {};

GameOver.preload = function() {
    game.load.image('gameover', './img/gameover.png');
}

GameOver.create = function() {
    this.add.button(0, 0, 'gameover', this.startGame, this);

    game.add.text(235, 350, 'LAST SCORE', { font: 'bold 16px sans-serif', fill: '#46c0f9', align: 'center' });
    game.add.text(350, 348, Score.toString(), { font: 'bold 20px sans-serif', fill: '#fff', align: 'center' });
}

GameOver.startGame = function() {
    this.state.start('Game');
}
