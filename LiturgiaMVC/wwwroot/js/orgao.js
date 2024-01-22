﻿var _acordeSelecionado = '';
var _acordeAntesSelecionado = '';
var _acompanhamentoSelecionado = '';
var _acompanhamentoSolo = false;
var _acompanhamentoFull = false;
var _acompanhamentoMao = false;
var _grupoNotas;
var _grupoNotasStrings;
var _volume = 0.9;
var _instrumentoSelecionado = 'orgao';
var _stringsSelecionado = false;
var _stringsParado = true;

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

var delay = new Pizzicato.Effects.Delay({
	feedback: 0.4,
	time: 0.66,
	mix: 0.1
});

deixarAcompanhamentoSelecionado('full');

function deixarAcompanhamentoSelecionado(funcao) {
	escolherAcompanhamento(funcao, document.getElementById(funcao));
}

function ocultarBotaoRec(ocultar = true) {
	if (ocultar) {
		document.getElementById('gravar').style.display = 'none';
		document.getElementById('play-pause').style.display = 'block';
	}
	else {
		document.getElementById('gravar').style.display = 'block';
		document.getElementById('play-pause').style.display = 'none';
	}
}

function escolherAcorde(acorde, botao) {
	if (_cifraId > 0) {
		_cifraParado = true;
		_cifraId--;
	}

	if (_acordeAntesSelecionado == acorde)
		_acordeSelecionado = '';
	else
		_acordeSelecionado = acorde;

	if (botao)
		levantarBotoesAcordes();

	if (_acordeSelecionado == '') {
		_acordeAntesSelecionado = _acordeSelecionado;
		pararOsAcordes();

		if (botao)
			ocultarBotaoRec(false);
	}
	else {
		tocarAcorde(acorde, botao);

		if (botao)
			ocultarBotaoRec();
	}
}

function escolherAcompanhamento(funcao, botao) {
	_acompanhamentoSelecionado = funcao;
	pressionarBotaoAcompanhamento(botao);
}

function tocarAcorde(acorde, botao) {
	if (_acordeAntesSelecionado != acorde) {
		if (botao) {
			if (botao.value != '')
				verificarAcompanhamentoEtocar(botao.value);
			else
				verificarAcompanhamentoEtocar(acorde);

			botao.classList.toggle('pressionado', true);
		}
		else
			verificarAcompanhamentoEtocar(acorde);
	}
}

function setTom(acorde = 'C') {
	document.getElementById('tomSelect').value = acorde;
}

function montarAcorde(acorde, grupoNotas, instrumento = 'orgao') {
	if (instrumento == 'stringsSolo' && _stringsSelecionado)
		instrumento = 'strings';

	if (acorde.includes('_'))
		acorde = acorde.split('_')[1];

	acorde = acidentesCorrespondentesJson[acorde];

	if (grupoNotas != null) {
		var notas = notasAcordesJson[acorde];

		for (var i = 0, len = notas.length; i < len; i++) {
			if (_acompanhamentoSelecionado == 'full' || _acompanhamentoSelecionado == 'baixo') {
				if (instrumento == 'stringsSolo') {
					grupoNotas.addSound(acordes['strings_' + notas[0] + '_baixo']);
					grupoNotas.addSound(acordes['strings_' + notas[0] + '_grave']);
				}
				else {
					if (i != 1 && i != 3 && i != 4 && i != 5)
						grupoNotas.addSound(acordes[instrumento + '_' + notas[i] + '_baixo']);
					if (i == 0)
						grupoNotas.addSound(acordes[instrumento + '_' + notas[i] + '_grave']);
					//if (instrumento == 'strings')
					//	if (i == 0)
					//		grupoNotas.addSound(acordes[instrumento + '_' + notas[i] + '_gravissimo']);
				}
			}

			if (_acompanhamentoSelecionado == 'full' || _acompanhamentoSelecionado == 'mao')
				if (instrumento == 'stringsSolo')
					grupoNotas.addSound(acordes['strings_' + notas[0]]);
				else
					grupoNotas.addSound(acordes[instrumento + '_' + notas[i]]);
		}
	}

	return grupoNotas;
}

document.getElementById('bpm').addEventListener('change', function (e) {
	if (_grupoNotas.effects.length > 0) {
		var bpmRange_valor = document.getElementById('bpmRange').value;
		var bateriaSelecionado = document.getElementById('selectRitmo').value;
		bpmRange_valor = 60 / bpmRange_valor;

		if (bateriaSelecionado == '6/8')
			bpmRange_valor = bpmRange_valor / 2;

		_grupoNotas.effects[0].time = bpmRange_valor;
	}
});

function verificarGrupoNotasInstanciado(grupoNotas, efeito = null) {
	if (grupoNotas == null) {
		grupoNotas = new Pizzicato.Group();

		grupoNotas.volume = _volume;
	}

	if (efeito) {
		if (grupoNotas.effects.length == 0)
			grupoNotas.addEffect(efeito);
	}
	else if (grupoNotas.effects.length > 0)
		grupoNotas.removeEffect(grupoNotas.effects[0]);

	return grupoNotas;
}

function verificarAcompanhamentoEtocar(acorde, esperar = 0) {
	if (_acordeAntesSelecionado == acorde) {
		pararOsAcordes(true, true);
		esperar = 50;
	}
	else
		pararOsAcordes(true);

	setTimeout(function () { 
		_acordeAntesSelecionado = acorde;

		if (_instrumentoSelecionado == 'strings') {
			_grupoNotasStrings = verificarGrupoNotasInstanciado(_grupoNotasStrings);
			_grupoNotasStrings = montarAcorde(acorde, _grupoNotasStrings, 'stringsSolo');
			_grupoNotasStrings.play();
		}

		else {
			if (_instrumentoSelecionado == 'epiano')
				_grupoNotas = verificarGrupoNotasInstanciado(_grupoNotas, delay);
			else
				_grupoNotas = verificarGrupoNotasInstanciado(_grupoNotas);

			_grupoNotas = montarAcorde(acorde, _grupoNotas, _instrumentoSelecionado);
			_grupoNotas.play();

			if (_stringsSelecionado) {
				if (_stringsParado) {
					_stringsParado = false;
					_grupoNotasStrings = verificarGrupoNotasInstanciado(_grupoNotasStrings);
					_grupoNotasStrings = montarAcorde(acorde, _grupoNotasStrings, 'strings');
					_grupoNotasStrings.play();
				}
			}
		}
	}, esperar);
}

function pararOsAcordes(removerSons = false, continuarStrings = false) {
	if (_grupoNotas != null) {
		_grupoNotas.stop();

		if (removerSons) {
			var sons = _grupoNotas.sounds.length;
			for (let i = sons - 1; i > -1; i--)
				_grupoNotas.removeSound(_grupoNotas.sounds[i]);
		}
	}

	if (continuarStrings == false)
		if (_grupoNotasStrings != null) {
			_stringsParado = true;
			_grupoNotasStrings.stop();

			if (removerSons) {
				var sons = _grupoNotasStrings.sounds.length;
				for (let i = sons - 1; i > -1; i--)
					_grupoNotasStrings.removeSound(_grupoNotasStrings.sounds[i]);
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
			if (_instrumentoSelecionado != 'epiano')
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

	if (_grupoNotasStrings != null)
		_grupoNotasStrings.volume = _volume;

	if (volume == padrao)
		document.getElementById('volumeTexto').innerHTML = volume * 10;	
	else
		document.getElementById('volumeTexto').innerHTML = (volume * 10) + '*';
}

function mudarTomCifra(aumentar, quant) {
	var frame = document.getElementById('textoCifras');
	var texto = frame.contentDocument.body.innerHTML;

	$.ajax({
		type: "post",
		url: "Orgao/AlterarTom",
		data: {
			texto: texto,
			aumentar: aumentar,
			quant: quant
		},
		success: function (data) {
			if (data.success) {
				var frame = document.getElementById('textoCifras');
				frame.contentDocument.body.innerHTML = data.message;
				addEventCifras(frame);
			}
			else
				alert(data.message);
		}
	});
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

function aumentarTom(aumentar, quant, select) {
	var tomElement = document.getElementById(select);
	var tomSelecionadoIndex = tomElement.selectedIndex;
	
	if (tomElement.value.includes('m'))
		var tonsArray = tonsMenores;
	else
		var tonsArray = tonsMaiores;

	if (aumentar) {
		if (tomSelecionadoIndex + quant == tomElement.length)
			tomElement.value = tonsArray[0];
		else if (tomSelecionadoIndex + quant > tomElement.length)
			tomElement.value = tonsArray[-1 + quant];
		else
			tomElement.value = tonsArray[tomSelecionadoIndex + quant];
	}
	else {
		if (tomSelecionadoIndex - quant < 0)
			if (tomSelecionadoIndex == 0)
				tomElement.value = tonsArray[tomElement.length - quant];
			else
				tomElement.value = tonsArray[tomElement.length - 1];
		else
			tomElement.value = tonsArray[tomSelecionadoIndex - quant];
	}

	if (document.getElementById('textoCifrasFrame').style.display == "block")
		mudarTomCifra(aumentar, quant);
	else
		mudarTom(tomElement.value);
}

function adicionarTonsSelect(element, index, maior) {
	var selectElem = document.getElementById(element);
	selectElem.innerHTML = "";

	var tons = tonsMaiores;
	if (maior == false)
		tons = tonsMenores;

	for (var i = 0; i < tons.length; i++) {
		let opt = document.createElement('option');
		opt.value = tons[i];
		opt.textContent += tons[i];
		selectElem.appendChild(opt);
	};

	selectElem.selectedIndex = index;
}

function mostrarTextoArquivoCarregado(tom = null, texto = null) {
	if (tom) {
		if (tom.includes('m'))
			adicionarTonsSelect('tomSelect', tonsMenores.indexOf(tom), false);
		else
			adicionarTonsSelect('tomSelect', tonsMaiores.indexOf(tom), true);
	}

	var frame = document.getElementById('textoCifras');

	if (texto)
		frame.contentDocument.body.innerHTML = texto;

	document.getElementById('textoCifrasFrame').style.display = 'block';

	if (document.body.classList.contains("bg-dark"))
		frame.contentWindow.document.querySelector('pre').style.color = '#fff';
	else
		frame.contentWindow.document.querySelector('pre').style.color = '#000';

	var elements = document.getElementsByClassName('orgaoBotoes');
	for (var i = 0; i < elements.length; i++) {
		elements[i].style.display = 'none';
	};

	document.getElementById('container').classList.remove('d-sm-flex');
	document.getElementById('volumeDiv').style.display = 'none';
	document.getElementById('voltar').style.display = 'block';
	document.getElementById('botaoFonte').style.display = 'block';
	document.getElementById('selectFonte').style.display = "none";
	document.getElementById('tomMenorSwitchDiv').style.display = 'none';
	document.getElementById('orgaoCifrasBotoes').style.display = '';

	var tdVolume = document.getElementById('tdVolume');
	tdVolume.setAttribute('rowspan', '');
	tdVolume.setAttribute('colspan', 5);
	document.getElementById('volumeDiv').style.display = 'block';
	document.getElementById('textoVolume').classList.remove('textoVertical');
	document.getElementById('volumeInput').setAttribute('orient', '');
	$('#tdVolume').appendTo('#orgaoTable');

	$('#orgaoTable').prependTo('#bateriaBox');

	addEventCifras(frame);
}

function addEventCifras(frame) {
	var elements = frame.contentDocument.getElementsByTagName("b");

	for (var i = 0; i < elements.length; i++) {
		elements[i].addEventListener("click", function (e) {
			var cifraElements = document.getElementById('textoCifras').contentDocument.getElementsByClassName('cifraSelecionada');

			if (cifraElements.length > 0)
				cifraElements[0].classList.remove('cifraSelecionada');

			e.target.classList.add('cifraSelecionada');
			e.target.scrollIntoView();
			parent.tocarCifraManualmente(e.target);
		});
	}
}

function tocarCifraManualmente(cifraElem) {
	_cifraId = cifraElem.id.split('cifra')[1] - 1;

	if (_cifraParado == false)
		avancarCifra('avancar', document.getElementById('cifraAvancar'));
}

var _cifraId = 0;
var _cifraParado = true;

function avancarCifra(avancar_retroceder, botao) {
	if (avancar_retroceder == '') {
		escolherAcorde('', botao);
		document.getElementById('gravar').style.display = 'block';
		document.getElementById('play-pause').style.display = 'none';
	}
	
	else if (avancar_retroceder == 'repetir') {
		verificarAcompanhamentoEtocar(_acordeAntesSelecionado, 50);
	}

	else {
		var frame = document.getElementById('textoCifras');
		var frameContent = frame.contentDocument;
		var elements_b = frameContent.getElementsByTagName('b');
		var frame = document.getElementById('textoCifras');
		var cifraElems = frameContent.getElementsByClassName('cifraSelecionada');

		if (avancar_retroceder == 'avancar') {
			if (_cifraId < elements_b.length) {
				if (cifraElems.length > 0)
					cifraElems[0].classList.remove('cifraSelecionada');

				_cifraParado = false;

				var cifraElem = elements_b[_cifraId];
				var cifraRetroceder = document.getElementById('cifraRetroceder');

				tocarAcorde(cifraElem.innerHTML.trim(), null);

				if (cifraRetroceder.classList.contains('pressionado'))
					cifraRetroceder.classList.remove('pressionado');

				botao.classList.toggle('pressionado', true);
				cifraElem.classList.add('cifraSelecionada');
				cifraElem.scrollIntoView();

				_cifraId++;
			}
		}

		if (avancar_retroceder == 'retroceder') {
			if (_cifraId - 1 > 0) {
				if (cifraElems.length > 0)
					cifraElems[0].classList.remove('cifraSelecionada');

				if (_cifraParado == false)
					_cifraId--;

				var cifraElem = elements_b[_cifraId - 1];
				var cifraAvancar = document.getElementById('cifraAvancar');

				tocarAcorde(cifraElem.innerHTML.trim(), null);

				if (cifraAvancar.classList.contains('pressionado'))
					cifraAvancar.classList.remove('pressionado');

				botao.classList.toggle('pressionado', true);
				cifraElem.classList.add('cifraSelecionada');
				cifraElem.scrollIntoView();
			}
		}

		document.getElementById('gravar').style.display = 'none';
		document.getElementById('play-pause').style.display = 'block';
	}
}

function mudarTamanhoFrameCifras(aumentar) {
	if (document.getElementById('textoCifrasFrame').style.display != 'none') {
		if (aumentar)
			document.getElementById('textoCifras').style.height = '250px';
		else
			document.getElementById('textoCifras').style.height = '150px';
	}
}

function selecionarStrings(stringsCheck) {
	if (stringsCheck) {
		_stringsSelecionado = true;

		if (_stringsParado)
			if (_grupoNotas != null) {
				_stringsParado = false;
				_grupoNotasStrings = verificarGrupoNotasInstanciado(_grupoNotas);
				_grupoNotasStrings = montarAcorde(_acordeAntesSelecionado, _grupoNotasStrings, 'strings');
				_grupoNotasStrings.play();
			}
	}
	else {
		_stringsSelecionado = false;

		if (_grupoNotas != null) {
			_grupoNotasStrings.stop();
			_stringsParado = true;

			var sons = _grupoNotasStrings.sounds.length;
			for (let i = sons - 1; i > -1; i--)
				_grupoNotasStrings.removeSound(_grupoNotasStrings.sounds[i]);
		}
	}
}

document.getElementById('instrumentoSelect').addEventListener('change', (e) => {
	var semacentos = document.getElementById('instrumentoSelect').value.normalize("NFD").replace(/[\u0300-\u036f]/g, '');
	_instrumentoSelecionado = semacentos.toLowerCase();
});

window.addEventListener("orientationchange", (event) => {
	var orientacao = event.target.screen.orientation.angle;

	if (orientacao == 0)
		mudarTamanhoFrameCifras(true);
	else
		mudarTamanhoFrameCifras(false);
});

//[Deprecation] Listener added for a synchronous 'DOMNodeInserted' DOM Mutation Event.This event type is deprecated (https://w3c.github.io/uievents/#legacy-event-types) and work is underway to remove it from this browser. Usage of this event listener will cause performance issues today, and represents a risk of future incompatibility. Consider using MutationObserver instead.

//som do órgão no lmms até o número 5 exporta para ogg bits 160 e depois abre o audacity e  remove o começo do som do órgão até o 0,500130