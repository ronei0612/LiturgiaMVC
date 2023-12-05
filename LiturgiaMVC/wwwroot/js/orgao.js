﻿var _acordeSelecionado = '';
var _acordeAntesSelecionado = '';
var _acompanhamentoSelecionado = '';
var _acompanhamentoSolo = false;
var _acompanhamentoFull = false;
var _acompanhamentoMao = false;
var _grupoNotas;
var _volume = 0.8;

const notasAcordes = Object.keys(notasAcordesJson);

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

function criarAcorde(acorde, grupoNotas) {
	if (acorde.includes('_'))
		acorde = acorde.split('_')[1];

	var notas = notasAcordesJson[acorde];

	for (var i = 0, len = notas.length; i < len; i++) {
		if (_acompanhamentoSelecionado == 'full' || _acompanhamentoSelecionado == 'baixo') {
			var nota = acordes['orgao_' + notas[i] + '_baixo'];

			if (grupoNotas == null)
				grupoNotas = new Pizzicato.Group(nota);
			else
				grupoNotas.addSound(nota);
		}

		if (_acompanhamentoSelecionado == 'full' || _acompanhamentoSelecionado == 'mao') {
			var nota = acordes['orgao_' + notas[i]];

			if (grupoNotas == null)
				grupoNotas = new Pizzicato.Group(nota);
			else
				grupoNotas.addSound(nota);
		}
	}

	grupoNotas.addEffect(flanger);

	return grupoNotas;
}

function verificarAcompanhamentoEtocar(acorde) {
	pararOsAcordes();

	_grupoNotas = criarAcorde(acorde, _grupoNotas);
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

//som do órgão no lmms até o número 5 exporta para ogg bits 160 e depois abre o audacity e  remove o começo do som do órgão até o 0,500130