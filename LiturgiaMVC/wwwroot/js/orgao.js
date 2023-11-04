﻿var _acordeSelecionado = '';
var _acordeAntesSelecionado = '';
var _acompanhamentoSelecionado = '';
var _acompanhamentoSolo = false;
var _acompanhamentoFull = false;
var _acompanhamentoMao = false;

deixarAcompanhamentoSelecionado('mao');

function deixarAcompanhamentoSelecionado(funcao) {
	escolherAcompanhamento(funcao, document.getElementById(funcao));
}

function mudarTom() {
	var tomSelecionado = document.getElementById("tomSelect").value;
	window.location.href = '@Url.Action("Index", "Orgao")?tom=' + tomSelecionado;
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
		acordes[acorde + '_mao'].currentTime = 0;
		acordes[acorde + '_mao'].play();
		acordes[acorde + '_solo'].currentTime = 0;
		acordes[acorde + '_solo'].play();
		acordes[acorde + '_full'].currentTime = 0;
		acordes[acorde + '_full'].play();
	}
	else if (_acompanhamentoSelecionado == 'mao') {
		acordes[acorde + '_mao'].currentTime = 0;
		acordes[acorde + '_mao'].play();
		acordes[acorde + '_full'].pause();
		acordes[acorde + '_full'].currentTime = 0;
		acordes[acorde + '_solo'].pause();
		acordes[acorde + '_solo'].currentTime = 0;
	}
	else {
		acordes[acorde + '_solo'].currentTime = 0;
		acordes[acorde + '_solo'].play();
		acordes[acorde + '_mao'].pause();
		acordes[acorde + '_mao'].currentTime = 0;
		acordes[acorde + '_full'].pause();
		acordes[acorde + '_full'].currentTime = 0;
	}
}

function pararOsAcordes() {
	for (let [acorde, link] of Object.entries(acordes)) {
		if (acordes[acorde].paused == false) {
			acordes[acorde].pause();
			acordes[acorde].currentTime = 0;
		}
	}
	//for (let i = 0; i < acordes.length; i++) {
	//	acordes[i].pause();
	//	acordes[i].currentTime = 0.1;
	//}
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