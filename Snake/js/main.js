var game;

// Create game instance
game = new Phaser.Game(600, 450, Phaser.AUTO, '');

// first param: How state is called
// second param: object containing necessary methods
game.state.add('Menu', Menu);
game.state.add('Game', Game);
game.state.add('GameOver', GameOver);
game.state.start('Menu');
