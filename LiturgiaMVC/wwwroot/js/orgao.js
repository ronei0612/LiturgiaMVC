var _acordeSelecionado = '';
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
var _autoMudarRitmo = false;
var _orientacaoCelularPe = true;

var delay = new Pizzicato.Effects.Delay({ feedback: 0.5, time: 0.33, mix: 0.1 });

const eventoClick = new Event('click');
const notasAcordes = Object.keys(notasAcordesJson);
const instrumentoSelect = document.getElementById('instrumentoSelect');
const autoCheck = document.getElementById('autoCheck');
const bpm = document.getElementById('bpm');
const selectRitmo = document.getElementById('selectRitmo');
const botaobotaoGravar = document.getElementById('botaoGravar');
const play_pause = document.getElementById('play-pause');
const bpmRange = document.getElementById('bpmRange');
const pararBateriaBotao = document.getElementById('pararBateriaBotao');
const chimbal = document.getElementById('chimbal');
const brush = document.getElementById('brush');
const meiaLua = document.getElementById('meiaLua');
const aro = document.getElementById('aro');
const caixa = document.getElementById('caixa');
const tomSelect = document.getElementById('tomSelect');
const volumeTexto = document.getElementById('volumeTexto');
const textoCifras = document.getElementById('textoCifras');
const textoAcordeMenor = document.getElementById('textoAcordeMenor');
const textoCifrasFrame = document.getElementById('textoCifrasFrame');
const container = document.getElementById('container');
const voltar  = document.getElementById('voltar');
const botaoFonte  = document.getElementById('botaoFonte');
const selectFonte  = document.getElementById('selectFonte');
const tomMenorSwitchDiv  = document.getElementById('tomMenorSwitchDiv');
const orgaoCifrasBotoes  = document.getElementById('orgaoCifrasBotoes');
const tdVolume = document.getElementById('tdVolume');
const volumeDiv = document.getElementById('volumeDiv');
const textoVolume = document.getElementById('textoVolume');
const volumeInput = document.getElementById('volumeInput');
const cifraAvancar = document.getElementById('cifraAvancar');
const cifraRetroceder = document.getElementById('cifraRetroceder');
const exitfullscreen = document.getElementById('exitfullscreen');
const exitfullscreenDiv = document.getElementById('exitfullscreenDiv');
const botaoFullscreen = document.getElementById('botaoFullscreen');
const trackerControls = document.getElementById('trackerControls');
const divBateriaSwitch = document.getElementById('divBateriaSwitch');
const bateria = document.getElementById('bateria');
const linhaSelectTom = document.getElementById('linhaSelectTom');
const navBar = document.getElementById('navBar');
const tracker_container = document.getElementById('tracker-container');
const acorde_10 = document.getElementById('acorde_10');
const acorde_7 = document.getElementById('acorde_7');
const acorde_9 = document.getElementById('acorde_9');
const muteDiv = document.getElementById('muteDiv');
const iconVolumeMute = document.getElementById('iconVolumeMute');
const iconVolume = document.getElementById('iconVolume');

deixarAcompanhamentoSelecionado('full');
verificarOrientacaoCelular();

function verificarOrientacaoCelular() {
	if (window.matchMedia("(orientation: portrait)").matches) {
		_orientacaoCelularPe = true;
	}

	if (window.matchMedia("(orientation: landscape)").matches) {
		_orientacaoCelularPe = false;
	}
}

selectRitmo.addEventListener('change', function (e) {
	_grupoNotas = verificarGrupoNotasInstanciado(_grupoNotas);

	if (_grupoNotas.effects.length > 0) {
		var bpmRange_valor = bpmRange.value;
		bpmRange_valor = 30 / bpmRange_valor;

		_grupoNotas.effects[0].time = bpmRange_valor;
	}
});

bpm.addEventListener('change', function (e) {
	_grupoNotas = verificarGrupoNotasInstanciado(_grupoNotas);

	if (_grupoNotas.effects.length > 0) {
		var bpmRange_valor = bpmRange.value;
		bpmRange_valor = 30 / bpmRange_valor;

		_grupoNotas.effects[0].time = bpmRange_valor;
	}
});

autoCheck.addEventListener('change', function (e) {
	_autoMudarRitmo = this.checked;
});

instrumentoSelect.addEventListener('change', (e) => {
	var semacentos = instrumentoSelect.value.normalize("NFD").replace(/[\u0300-\u036f]/g, '');
	_instrumentoSelecionado = semacentos.toLowerCase();
});

window.addEventListener("orientationchange", (event) => {
	if (event.target.screen.orientation.angle == 0)
		_orientacaoCelularPe = true;
	else
		_orientacaoCelularPe = false;

	mudarTamanhoFrameCifras(_orientacaoCelularPe);
});

function deixarAcompanhamentoSelecionado(funcao) {
	escolherAcompanhamento(funcao, document.getElementById(funcao));
}

function ocultarBotaoRec(ocultar = true) {
	if (ocultar) {
		botaoGravar.style.display = 'none';
		play_pause.style.display = 'block';
	}
	else {
		botaoGravar.style.display = 'block';
		play_pause.style.display = 'none';
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

function autoMudarRitmo(elementBotao = null) {
	if (pararBateriaBotao.style.display !== 'none' && _autoMudarRitmo) {
		var selecionadoElement = elementBotao || document.querySelector('.selecionado');

		if (_stringsSelecionado) {
			if ((selecionadoElement.id === 'solo' || selecionadoElement.id === 'mao') && _instrumentoSelecionado === 'epiano') {
				chimbal.dispatchEvent(eventoClick);
			} else if ((selecionadoElement.id === 'solo' || selecionadoElement.id === 'mao') && _instrumentoSelecionado === 'orgao') {
				brush.dispatchEvent(eventoClick);
			} else if (selecionadoElement.id === 'full' && _instrumentoSelecionado === 'epiano') {
				meiaLua.dispatchEvent(eventoClick);
			} else if (selecionadoElement.id === 'full' && _instrumentoSelecionado === 'orgao') {
				aro.dispatchEvent(eventoClick);
			}
		} else {
			if ((selecionadoElement.id === 'solo' || selecionadoElement.id === 'mao') && _instrumentoSelecionado === 'epiano') {
				aro.dispatchEvent(eventoClick);
			} else if ((selecionadoElement.id === 'solo' || selecionadoElement.id === 'mao') && _instrumentoSelecionado === 'orgao') {
				brush.dispatchEvent(eventoClick);
			} else if (selecionadoElement.id === 'full' && _instrumentoSelecionado === 'epiano') {
				caixa.dispatchEvent(eventoClick);
			} else if (selecionadoElement.id === 'full' && _instrumentoSelecionado === 'orgao') {
				aro.dispatchEvent(eventoClick);
			}
		}
	}
}

function escolherAcompanhamento(funcao, botao) {
	_acompanhamentoSelecionado = funcao;
	pressionarBotaoAcompanhamento(botao);

	autoMudarRitmo(botao);
}

function tocarAcorde(acorde, botao) {
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

function setTom(acorde = 'C') {
	tomSelect.value = acorde;
}

function montarAcorde(acorde, grupoNotas, instrumento = 'orgao') {
	if (instrumento == 'stringsSolo' && _stringsSelecionado)
		instrumento = 'strings';

	if (acorde.includes('_'))
		acorde = acorde.split('_')[1];

	if (acorde.length > 1) {
		if (acorde[1] == 'b') {
			var soNota = acorde.slice(0, 2);
			acorde = acorde.replace(soNota, acidentesCorrespondentesJson[soNota]);
		}
	}

	if (grupoNotas != null) {
		var notas = notasAcordesJson[acorde];

		if (instrumento == 'stringsSolo') {
			grupoNotas.addSound(acordes['strings_' + notas[0] + '_baixo']);
			grupoNotas.addSound(acordes['strings_' + notas[0] + '_grave']);
		}

		else {
			for (var i = 0, len = notas.length; i < len; i++) {
				if (_acompanhamentoSelecionado == 'full' || _acompanhamentoSelecionado == 'baixo') {
					if (i == 0)
						grupoNotas.addSound(acordes[instrumento + '_' + notas[i] + '_grave']);

					if (instrumento != 'strings')
						grupoNotas.addSound(acordes[instrumento + '_' + notas[i] + '_baixo']);
					else {
						if (i == 1)
							grupoNotas.addSound(acordes[instrumento + '_' + notas[i] + '_baixo']);
					}
				}

				if (_acompanhamentoSelecionado == 'full' || _acompanhamentoSelecionado == 'mao')
					if (instrumento == 'stringsSolo')
						grupoNotas.addSound(acordes['strings_' + notas[0]]);
					else {
						if (instrumento != 'strings')
							grupoNotas.addSound(acordes[instrumento + '_' + notas[i]]);
						else {
							if (i != 0 && i != 1)
								grupoNotas.addSound(acordes[instrumento + '_' + notas[i]]);
						}
					}
			}
		}
	}

	return grupoNotas;
}

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
				if (_stringsParado || _grupoNotas.sounds.length < 10) {
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
		volumeTexto.innerHTML = volume * 10;	
	else
		volumeTexto.innerHTML = (volume * 10) + '*';
}

function mudarTomCifra(aumentar, quant) {
	var texto = textoCifras.contentDocument.body.innerHTML;

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
				textoCifras.contentDocument.body.innerHTML = data.message;
				addEventCifras(textoCifras);
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

	mudarTomMenor(tomSelect.selectedIndex);
}

function mudarTomMenor(acordeIndex) {
	textoAcordeMenor.innerText = acordesTons[acordeIndex + 12];
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
	
	if (textoCifrasFrame.style.display == "block")
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

	if (texto)
		textoCifras.contentDocument.body.innerHTML = texto;

	if (document.body.classList.contains("bg-dark"))
		textoCifras.contentWindow.document.querySelector('pre').style.color = '#fff';
	else
		textoCifras.contentWindow.document.querySelector('pre').style.color = '#000';

	if (textoCifrasFrame.style.display == 'none') {
		textoCifrasFrame.style.display = 'block';

		var elements = document.getElementsByClassName('orgaoBotoes');
		for (var i = 0; i < elements.length; i++) {
			elements[i].style.display = 'none';
		}

		container.classList.remove('d-sm-flex');
		volumeDiv.style.display = 'none';
		voltar.style.display = 'block';
		botaoFonte.style.display = 'block';
		selectFonte.style.display = "none";
		tomMenorSwitchDiv.style.display = 'none';
		orgaoCifrasBotoes.style.display = '';

		tdVolume.setAttribute('rowspan', '');
		tdVolume.setAttribute('colspan', 5);
		volumeDiv.style.display = 'block';
		textoVolume.classList.remove('textoVertical');
		volumeInput.setAttribute('orient', '');

		$('#tdVolume').appendTo('#orgaoTable');
		$('#orgaoTable').prependTo('#bateriaBox');
	}

	addEventCifras(textoCifras);
	mudarTamanhoFrameCifras(_orientacaoCelularPe);
}

function addEventCifras(frame) {
	var elements = frame.contentDocument.getElementsByTagName("b");

	for (var i = 0; i < elements.length; i++) {
		elements[i].addEventListener("click", function (e) {
			var cifraElements = textoCifras.contentDocument.getElementsByClassName('cifraSelecionada');

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
		avancarCifra('avancar', cifraAvancar);
}

var _cifraId = 0;
var _cifraParado = true;

function avancarCifra(avancar_retroceder, botao) {
	if (avancar_retroceder == '') {
		escolherAcorde('', botao);
		botaoGravar.style.display = 'block';
		play_pause.style.display = 'none';
	}
	
	else if (avancar_retroceder == 'repetir') {
		if (_cifraParado == false)
			verificarAcompanhamentoEtocar(_acordeAntesSelecionado, 50);
	}

	else {
		var frameContent = textoCifras.contentDocument;
		var elements_b = frameContent.getElementsByTagName('b');
		var cifraElems = frameContent.getElementsByClassName('cifraSelecionada');

		if (avancar_retroceder == 'avancar') {
			if (_cifraId < elements_b.length) {
				if (cifraElems.length > 0)
					cifraElems[0].classList.remove('cifraSelecionada');

				_cifraParado = false;

				var cifraElem = elements_b[_cifraId];

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

				tocarAcorde(cifraElem.innerHTML.trim(), null);

				if (cifraAvancar.classList.contains('pressionado'))
					cifraAvancar.classList.remove('pressionado');

				botao.classList.toggle('pressionado', true);
				cifraElem.classList.add('cifraSelecionada');
				cifraElem.scrollIntoView();
			}
		}

		botaoGravar.style.display = 'none';
		play_pause.style.display = 'block';
	}
}

function mudarTamanhoFrameCifras(aumentar) {
	if (textoCifrasFrame.style.display != 'none') {
		if (aumentar)
			textoCifras.style.height = '250px';
		else
			textoCifras.style.height = '150px';
	}
}

function selecionarStrings(stringsCheck) {
	if (stringsCheck) {
		_stringsSelecionado = true;

		if (_stringsParado)
			if (_acordeAntesSelecionado != '') {
				_stringsParado = false;
				_grupoNotasStrings = verificarGrupoNotasInstanciado(_grupoNotas);
				_grupoNotasStrings = montarAcorde(_acordeAntesSelecionado, _grupoNotasStrings, 'strings');
				_grupoNotasStrings.play();
			}
	}
	else
		_stringsSelecionado = false;

	autoMudarRitmo();
}

//[Deprecation] Listener added for a synchronous 'DOMNodeInserted' DOM Mutation Event.This event type is deprecated (https://w3c.github.io/uievents/#legacy-event-types) and work is underway to remove it from this browser. Usage of this event listener will cause performance issues today, and represents a risk of future incompatibility. Consider using MutationObserver instead.

//som do órgão no lmms até o número 5 exporta para ogg bits 160 e depois abre o audacity e  remove o começo do som do órgão até o 0,500130