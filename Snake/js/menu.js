var Menu = Menu || {};

Menu.preload = function () {
	game.load.image('menu', './img/menu.png');	
}

Menu.create = function() {
	//this.add.sprite(0, 0, 'menu');
	this.add.button(0, 0, 'menu', this.startGame, this);
}

Menu.startGame = function () {
	this.state.start('Game');
}