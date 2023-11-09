var _acordeSelecionado = '';
var _acordeAntesSelecionado = '';
var _acompanhamentoSelecionado = '';
var _acompanhamentoSolo = false;
var _acompanhamentoFull = false;
var _acompanhamentoMao = false;
var _grupoNotas;

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
	pararOsAcordes();

	if (_acordeSelecionado != '')
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
	if (_acompanhamentoSelecionado == 'full') {
		_grupoNotas = new Pizzicato.Group([acordes[acorde + '_mao'], acordes[acorde + '_baixo']]);
		_grupoNotas.addEffect(tremolo);
		_grupoNotas.addEffect(flanger);
		_grupoNotas.play();
	}
	else if (_acompanhamentoSelecionado == 'mao') {
		_grupoNotas = new Pizzicato.Group([acordes[acorde + '_mao']]);
		_grupoNotas.addEffect(tremolo);
		_grupoNotas.addEffect(flanger);
		_grupoNotas.play();
	}
	else {
		_grupoNotas = new Pizzicato.Group([acordes[acorde + '_baixo']]);
		_grupoNotas.addEffect(tremolo);
		_grupoNotas.addEffect(flanger);
		_grupoNotas.play();
	}
}

function pararOsAcordes() {
	_grupoNotas.stop();
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

//[Deprecation] Listener added for a synchronous 'DOMNodeInserted' DOM Mutation Event.This event type is deprecated (https://w3c.github.io/uievents/#legacy-event-types) and work is underway to remove it from this browser. Usage of this event listener will cause performance issues today, and represents a risk of future incompatibility. Consider using MutationObserver instead.

