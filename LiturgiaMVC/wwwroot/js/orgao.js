var _acordeSelecionado = '';
var _acordeAntesSelecionado = '';
var _acompanhamentoSelecionado = '';
var _acompanhamentoSolo = false;
var _acompanhamentoFull = false;
var _acompanhamentoMao = false;
var _grupoNotas;
var _volume = 0.9;

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

function escolherAcorde(acorde, botao) {
	if (_acordeAntesSelecionado == acorde)
		_acordeSelecionado = '';
	else
		_acordeSelecionado = acorde;

	levantarBotoesAcordes();

	if (_acordeSelecionado == '') {
		_acordeAntesSelecionado = _acordeSelecionado;
		pararOsAcordes();
	}
	else
		tocarAcorde(acorde, botao);
}

function escolherAcompanhamento(funcao, botao) {
	_acompanhamentoSelecionado = funcao;
	pressionarBotaoAcompanhamento(botao);
}

function tocarAcorde(acorde, botao) {
	if (_acordeAntesSelecionado != acorde) {
		verificarAcompanhamentoEtocar(botao.value);
		botao.classList.toggle('pressionado', true);
	}
}

function setTom(acorde = 'C') {
	document.getElementById('tomSelect').value = acorde;
}

function criarAcorde(acorde, grupoNotas) {
	if (acorde.includes('_'))
		acorde = acorde.split('_')[1];

	var notas = notasAcordesJson[acorde];

	if (grupoNotas == null) {
		grupoNotas = new Pizzicato.Group();
		grupoNotas.addEffect(flanger);
		grupoNotas.volume = _volume;
	}

	for (var i = 0, len = notas.length; i < len; i++) {
		if (_acompanhamentoSelecionado == 'full' || _acompanhamentoSelecionado == 'baixo')
			if (i != 1)
				grupoNotas.addSound(acordes['orgao_' + notas[i] + '_baixo']);

		if (_acompanhamentoSelecionado == 'full' || _acompanhamentoSelecionado == 'mao')
			grupoNotas.addSound(acordes['orgao_' + notas[i]]);
	}

	

	return grupoNotas;
}

function verificarAcompanhamentoEtocar(acorde) {
	pararOsAcordes(true);

	_acordeAntesSelecionado = acorde;

	_grupoNotas = criarAcorde(acorde, _grupoNotas);
	_grupoNotas.play();
}

function pararOsAcordes(removerSons = false) {
	if (_grupoNotas != null) {
		_grupoNotas.stop();

		if (removerSons) {
			var sons = _grupoNotas.sounds.length;
			for (let i = sons - 1; i > -1; i--)
				_grupoNotas.removeSound(_grupoNotas.sounds[i]);
		}
	}
}

function levantarBotoesAcordes() {
	var botoesPressionados = document.getElementsByClassName('pressionado');
	if (botoesPressionados.length > 0)
		botoesPressionados[0].classList.toggle('pressionado', false);
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

function alterarVolume(volume, padrao) {
	_volume = parseFloat(volume);

	if (_grupoNotas != null)
		_grupoNotas.volume = _volume;

	var numero = document.getElementById('volumeTexto');
	numero.innerHTML = volume * 10;

	if (volume == padrao)
		numero.style.color = '#00008b';
	else
		numero.style.color = '#8b0000';
}



function mudarTom(tomSelecionado) {
	var acordesCampoHarmonico = acordesCampoHarmonicoJson[tomSelecionado];
	var acordesElements = document.getElementsByClassName('acorde');

	Array.from(acordesElements).forEach((acordesElement) => {
		var acordeIndex = acordesElement.id.split('_')[1];
		acordesElement.value = acordesCampoHarmonico[acordeIndex];
	});

	var botoesSelecionados = document.getElementsByClassName('pressionado');

	if (botoesSelecionados.length > 0) {
		var botaoSelecionado = botoesSelecionados[0];
		var acordeSelecionado = botaoSelecionado.value;
		escolherAcorde('orgao_' + acordeSelecionado, botaoSelecionado);
	}

	mudarTomMenor(document.getElementById("tomSelect").selectedIndex);
}

function mudarTomMenor(acordeIndex) {
	document.getElementById('textoAcordeMenor').innerText = acordesTons[acordeIndex + 12];
}

function aumentarTom(aumentar) {
	var tomElement = document.getElementById("tomSelect");
	var tomSelecionadoIndex = tomElement.selectedIndex;

	if (aumentar) {
		if (tomSelecionadoIndex == tomElement.length - 1)
			tomElement.value = acordesTons[0];
		else
			tomElement.value = acordesTons[tomSelecionadoIndex + 1];
	}
	else {
		if (tomSelecionadoIndex == 0)
			tomElement.value = acordesTons[tomElement.length - 1];
		else
			tomElement.value = acordesTons[tomSelecionadoIndex - 1];
	}

	mudarTom(tomElement.value);
}

//[Deprecation] Listener added for a synchronous 'DOMNodeInserted' DOM Mutation Event.This event type is deprecated (https://w3c.github.io/uievents/#legacy-event-types) and work is underway to remove it from this browser. Usage of this event listener will cause performance issues today, and represents a risk of future incompatibility. Consider using MutationObserver instead.

//som do órgão no lmms até o número 5 exporta para ogg bits 160 e depois abre o audacity e  remove o começo do som do órgão até o 0,500130