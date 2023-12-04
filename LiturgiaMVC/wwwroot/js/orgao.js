var _acordeSelecionado = '';
var _acordeAntesSelecionado = '';
var _acompanhamentoSelecionado = '';
var _acompanhamentoSolo = false;
var _acompanhamentoFull = false;
var _acompanhamentoMao = false;
var _grupoNotas;
var _volume = 0.7;

const notasAcordes = Object.keys(notasAcordesJson);

debugger;
var tremolo = new Pizzicato.Effects.Tremolo({
	speed: 1,
	depth: 0.6,
	mix: 0.8
});

var flanger = new Pizzicato.Effects.Flanger({
	time: 0.45,
	speed: 0.2,
	depth: 0.1,
	feedback: 0.1,
	mix: 0.2
});

var pingPongDelay = new Pizzicato.Effects.PingPongDelay({
	feedback: 0.6,
	time: 0.4,
	mix: 0.5
});

deixarAcompanhamentoSelecionado('full');

function deixarAcompanhamentoSelecionado(funcao) {
	escolherAcompanhamento(funcao, document.getElementById(funcao));
}

function mudarTom() {
	var tomSelecionado = document.getElementById("tomSelect").value;
	window.location.href = "Orgao?tom=" + tomSelecionado;
}

function escolherAcorde(acorde, botao) {
	if (_acordeAntesSelecionado == acorde)
		_acordeSelecionado = '';
	else
		_acordeSelecionado = acorde;

	levantarBotoesAcordes();

	if (_acordeSelecionado == '')
		pararOsAcordes();
	else
		tocarAcorde(acorde, botao);

	_acordeAntesSelecionado = _acordeSelecionado;
}

function escolherAcompanhamento(funcao, botao) {
	_acompanhamentoSelecionado = funcao;
	pressionarBotaoAcompanhamento(botao);
}

function tocarAcorde(acorde, botao) {
	if (_acordeAntesSelecionado != acorde) {
		botao.classList.toggle('pressionado', true);
		verificarAcompanhamentoEtocar(acorde);
	}
}

function setTom(acorde = 'C') {
	document.getElementById('tomSelect').value = acorde;
}

function verificarAcompanhamentoEtocar(acorde) {
	pararOsAcordes();	

	if (_acompanhamentoSelecionado == 'full') {
		acordes[acorde + '_mao'].attack = 0.1;
		acordes[acorde + '_mao'].release = 0.4;
		acordes[acorde + '_baixo'].attack = 0.1;
		acordes[acorde + '_baixo'].release = 0.4;

		if (_grupoNotas == null) {
			_grupoNotas = new Pizzicato.Group([acordes[acorde + '_mao']]);
			_grupoNotas.addSound(acordes[acorde + '_baixo']);
			_grupoNotas.addEffect(flanger);
		} else {
			_grupoNotas.addSound(acordes[acorde + '_mao']);
			_grupoNotas.addSound(acordes[acorde + '_baixo']);
		}
	}

	else if (_acompanhamentoSelecionado == 'mao') {
		acordes[acorde + '_mao'].attack = 0.2;
		acordes[acorde + '_mao'].release = 0.5;

		if (_grupoNotas == null) {
			_grupoNotas = new Pizzicato.Group([acordes[acorde + '_mao']]);
			_grupoNotas.addEffect(flanger);
			_grupoNotas.attack = 1;
			_grupoNotas.release = 1;
		} else
			_grupoNotas.addSound(acordes[acorde + '_mao']);
	}

	else {
		acordes[acorde + '_baixo'].attack = 0.2;
		acordes[acorde + '_baixo'].release = 0.5;

		if (_grupoNotas == null) {
			_grupoNotas = new Pizzicato.Group([acordes[acorde + '_baixo']]);
			_grupoNotas.addEffect(flanger);
			_grupoNotas.attack = 1;
			_grupoNotas.release = 1;
		} else
			_grupoNotas.addSound(acordes[acorde + '_baixo']);
	}
	
	_grupoNotas.play();
}

function pararOsAcordes() {
	if (_grupoNotas != null) {
		_grupoNotas.stop();

		_grupoNotas.removeSound(_grupoNotas.sounds[0]);
		_grupoNotas.removeSound(_grupoNotas.sounds[0]);
	}
}

function levantarBotoesAcordes() {
	if (document.getElementsByClassName('pressionado').length > 0)
		document.getElementsByClassName('pressionado')[0].classList.toggle('pressionado', false);
}

function levantarBotoesAcompanhamento() {
	var botoes = document.getElementsByClassName('selecionado');
	for (let i = 0; i < botoes.length; i++)
		botoes[i].classList.toggle('selecionado', false);
}

function pressionarBotaoAcompanhamento(botao) {
	if (botaoAcompPressionado(botao) == false) {
		if (_acordeAntesSelecionado != '')
			verificarAcompanhamentoEtocar(_acordeAntesSelecionado);
		levantarBotoesAcompanhamento();
		pressionarBotaoAcomp(botao);
	}
}

function botaoAcompPressionado(botao) {
	if (botao.classList.contains('selecionado'))
		return true;
	else
		return false;
}

function pressionarBotaoAcomp(botao) {
	botao.classList.toggle('selecionado', true);
}

function alterarVolume(volume) {
	var numero = document.getElementById('volumeTexto');
	numero.innerHTML = volume;
	if (volume == 7)
		numero.style.color = '#00008b';
	else
		numero.style.color = '#8b0000';

	volume = volume / 10;
	if (_grupoNotas != null)
		_grupoNotas.volume = volume;
	_volume = volume;
}

//[Deprecation] Listener added for a synchronous 'DOMNodeInserted' DOM Mutation Event.This event type is deprecated (https://w3c.github.io/uievents/#legacy-event-types) and work is underway to remove it from this browser. Usage of this event listener will cause performance issues today, and represents a risk of future incompatibility. Consider using MutationObserver instead.

