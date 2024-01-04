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

//for (const [key] of Object.entries(acordes)) {
//	var obj = `${key}`;
//	if (obj.includes('orgao'))
//		acordes[obj].addEffect();
//}

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
			verificarAcompanhamentoEtocar(botao.value);
			botao.classList.toggle('pressionado', true);
		}
		else
			verificarAcompanhamentoEtocar(acorde);
	}
}

function setTom(acorde = 'C') {
	document.getElementById('tomSelect').value = acorde;
}

function criarAcorde(acorde, grupoNotas, somenteTom = false) {
	if (acorde.includes('_'))
		acorde = acorde.split('_')[1];

	if (somenteTom) {
		acorde = acorde.replace('m', '').replace('7', '');
		acorde = acidentesCorrespondentesJson[acorde];

		if (_acompanhamentoSelecionado == 'full' || _acompanhamentoSelecionado == 'baixo')
			grupoNotas.addSound(acordes['strings_' + acorde + '_baixo']);

		if (_acompanhamentoSelecionado == 'full' || _acompanhamentoSelecionado == 'mao')
			grupoNotas.addSound(acordes['strings_' + acorde]);
	}

	else {
		var notas = notasAcordesJson[acorde];

		for (var i = 0, len = notas.length; i < len; i++) {
			if (_acompanhamentoSelecionado == 'full' || _acompanhamentoSelecionado == 'baixo')
				if (i != 1)
					grupoNotas.addSound(acordes['orgao_' + notas[i] + '_baixo']);

			if (_acompanhamentoSelecionado == 'full' || _acompanhamentoSelecionado == 'mao')
				grupoNotas.addSound(acordes['orgao_' + notas[i]]);
		}
	}

	return grupoNotas;
}

function verificarGrupoNotasInstanciado(grupoNotas, adicionarEfeito = true) {
	if (grupoNotas == null) {
		grupoNotas = new Pizzicato.Group();

		if (adicionarEfeito)
			grupoNotas.addEffect(flanger);

		grupoNotas.volume = _volume;
	}

	return grupoNotas;
}

function verificarAcompanhamentoEtocar(acorde) {
	pararOsAcordes(true);

	_acordeAntesSelecionado = acorde;

	if (_instrumentoSelecionado == 'orgao') {
		_grupoNotas = verificarGrupoNotasInstanciado(_grupoNotas);
		_grupoNotas = criarAcorde(acorde, _grupoNotas);
		//_grupoNotas.effects[0].mix = 0.2;
		_grupoNotas.play();
	}
	else if (_instrumentoSelecionado == 'orgaopad') {
		_grupoNotas = verificarGrupoNotasInstanciado(_grupoNotas);
		_grupoNotas = criarAcorde(acorde, _grupoNotas);

		_grupoNotasStrings = verificarGrupoNotasInstanciado(_grupoNotasStrings, false);
		_grupoNotasStrings = criarAcorde(acorde, _grupoNotasStrings, true);

		//acorde = acorde.replace('m', '').replace('7', '');		
		//_grupoNotasStrings.addSound(acordes['strings_' + acidentesCorrespondentesJson[acorde]]);

		_grupoNotasStrings.play();
		_grupoNotas.play();
	}		

	else if (_instrumentoSelecionado == 'strings') {
		//acorde = acorde.replace('m', '').replace('7', '');
		//_grupoNotasStrings = verificarGrupoNotasInstanciado(_grupoNotasStrings, false);
		//_grupoNotasStrings.addSound(acordes['strings_' + acidentesCorrespondentesJson[acorde]]);

		_grupoNotasStrings = verificarGrupoNotasInstanciado(_grupoNotasStrings, false);
		_grupoNotasStrings = criarAcorde(acorde, _grupoNotasStrings, true);

		_grupoNotasStrings.play();
		//_grupoNotas.effects[0].mix = 0;
	}
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

	if (_grupoNotasStrings != null) {
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

	var numero = document.getElementById('volumeTexto');

	if (volume == padrao)
		numero.innerHTML = volume * 10;
	else
		numero.innerHTML = (volume * 10) + '*';
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

function aumentarTom(aumentar, quant) {
	var tomElement = document.getElementById("tomSelect");
	var tomSelecionadoIndex = tomElement.selectedIndex;

	if (aumentar) {
		if (tomSelecionadoIndex == tomElement.length - quant)
			tomElement.value = acordesTons[0];
		else
			tomElement.value = acordesTons[tomSelecionadoIndex + quant];
	}
	else {
		if (tomSelecionadoIndex == 0)
			tomElement.value = acordesTons[tomElement.length - quant];
		else
			tomElement.value = acordesTons[tomSelecionadoIndex - quant];
	}

	mudarTom(tomElement.value);
}

function mostrarTextoArquivoCarregado(texto) {
	var frame = document.getElementById('textoCifras');
	frame.contentDocument.body.innerHTML = texto;

	addEventCifras(frame);

	//[].forEach.call(frame.contentDocument.getElementsByTagName("b"), function (el) {
	//	el.addEventListener("click", function (e, idx) {
	//		alert(e.target.textContent);
	//	});
	//});
}

function addEventCifras(frame) {
	//var frame = document.getElementById('textoCifras');	
	var elements = frame.contentDocument.getElementsByTagName("b");

	for (var i = 0; i < elements.length; i++) {
		/*elements[i].addEventListener('click', tocarCifra, false);*/
		elements[i].addEventListener("click", function (e) {
			var cifraSelecionada = document.getElementsByClassName('cifraSelecionada');

			if (cifraSelecionada.length > 0)
				cifraSelecionada.classList.remove('cifraSelecionada');

			e.target.classList.add('cifraSelecionada');
			e.target.scrollIntoView();
			parent.tocarCifra(e.target);
		});
	}
}

function tocarCifra(cifraElem) {
	escolherAcorde('orgao_' + cifraElem.innerHTML.trim(), null);
}

document.getElementById('instrumentoSelect').addEventListener('change', (e) => {
	var semacentos = document.getElementById('instrumentoSelect').value.normalize("NFD").replace(/[\u0300-\u036f]/g, '');
	_instrumentoSelecionado = semacentos.toLowerCase();
});

//[Deprecation] Listener added for a synchronous 'DOMNodeInserted' DOM Mutation Event.This event type is deprecated (https://w3c.github.io/uievents/#legacy-event-types) and work is underway to remove it from this browser. Usage of this event listener will cause performance issues today, and represents a risk of future incompatibility. Consider using MutationObserver instead.

//som do órgão no lmms até o número 5 exporta para ogg bits 160 e depois abre o audacity e  remove o começo do som do órgão até o 0,500130