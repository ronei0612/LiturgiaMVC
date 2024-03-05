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
var _violaoSelecionado = false;
var _stringsParado = true;
var _autoMudarRitmo = false;
var _orientacaoCelularPe = true;
var _tomSelectedIndexCifra = 0;
var timer;
var _tomIndex = '';
var _cifraId = 0;
var _cifraParado = true;
var _acordeBaixo;
var _acordeNotas;

var _chimbalIsAberto = false;
var _sourceChimbalAberto;
var _sourceBaixo;
var _cravoSelecionado = true;
var _viradaRitmo = '';
var _trocarRitmo = false;
var _configurandoTeclas = false;
var _configuracaoElemento;
var _configuracaoEvento;
var _teclasConfiguracao = {};

const eventoClick = new Event('click');
const eventoChange = new Event('change');
const eventoMousedown = new Event('mousedown');
const eventoMouseup = new Event('mouseup');
const eventoTouchstart = new Event('touchstart');
const eventoInput = new Event('input');

const eventos = {
	eventoClick: eventoClick,
	eventoChange: eventoChange,
	eventoMousedown: eventoMousedown,
	eventoMouseup: eventoMouseup,
	eventoTouchstart: eventoTouchstart,
	eventoInput: eventoInput
};

const notasAcordes = Object.keys(notasAcordesJson);

const selectConfiguracao = document.getElementById('selectConfiguracao');

const instrumentoSelect = document.getElementById('instrumentoSelect');
const autoCheck = document.getElementById('autoCheck');
const autoCheckDiv = document.getElementById('autoCheckDiv');
const acompCheck = document.getElementById('acompCheck');
const acompCheckDiv = document.getElementById('acompCheckDiv');
const bpm = document.getElementById('bpm');
const selectRitmo = document.getElementById('selectRitmo');
const botaobotaoGravar = document.getElementById('botaoGravar');
const play_pause = document.getElementById('play_pause');
const bpmRange = document.getElementById('bpmRange');
const chimbal = document.getElementById('chimbal');
const brush = document.getElementById('brush');
const meiaLua = document.getElementById('meiaLua');
const prato = document.getElementById('prato');
const aro = document.getElementById('aro');
const caixa = document.getElementById('caixa');
const cravo = document.getElementById('cravo');
const pratoCravo = document.getElementById('pratoCravo');
const brushCravo = document.getElementById('brushCravo');
const violaoBotao = document.getElementById('violaoBotao');
const pianoBotao = document.getElementById('pianoBotao');
const baixoBotao = document.getElementById('baixoBotao');
const tomSelect = document.getElementById('tomSelect');
const volumeTexto = document.getElementById('volumeTexto');
const textoCifras = document.getElementById('textoCifras');
const textoAcordeMenor = document.getElementById('textoAcordeMenor');
const textoCifrasFrame = document.getElementById('textoCifrasFrame');
const container = document.getElementById('container');
const voltar = document.getElementById('voltar');
const botaoFonte = document.getElementById('botaoFonte');
const selectFonte = document.getElementById('selectFonte');
const botaoTamanhoIframe = document.getElementById('botaoTamanhoIframe');
const selectTamanhoIframe = document.getElementById('selectTamanhoIframe');
const tomMenorSwitchDiv = document.getElementById('tomMenorSwitchDiv');
const orgaoCifrasBotoes = document.getElementById('orgaoCifrasBotoes');
const tdVolume = document.getElementById('tdVolume');
const volumeDiv = document.getElementById('volumeDiv');
const textoVolume = document.getElementById('textoVolume');
const volumeInput = document.getElementById('volumeInput');
const cifraAvancar = document.getElementById('cifraAvancar');
const cifraRepetir = document.getElementById('cifraRepetir');
const cifraRetroceder = document.getElementById('cifraRetroceder');
const exitfullscreen = document.getElementById('exitfullscreen');
const fullscreenDiv = document.getElementById('fullscreenDiv');
const botaoFullscreen = document.getElementById('botaoFullscreen');
const trackerControls = document.getElementById('trackerControls');
const divBateriaSwitch = document.getElementById('divBateriaSwitch');
const bateria = document.getElementById('bateria');
const linhaSelectTom = document.getElementById('linhaSelectTom');
const navBar = document.getElementById('navBar');
const oracoesEucaristicasDiv = document.getElementById('oracoesEucaristicasDiv');
const acorde_10 = document.getElementById('acorde_10');
const acorde_7 = document.getElementById('acorde_7');
const acorde_9 = document.getElementById('acorde_9');
const muteDiv = document.getElementById('muteDiv');
const botaoMute = document.getElementById('botaoMute');
const iconVolumeMute = document.getElementById('iconVolumeMute');
const iconVolume = document.getElementById('iconVolume');
const modal01 = document.getElementById('modal01');
const linksCifraClubList = document.getElementById('linksCifraClubList');
const botaoIniciar = document.getElementById('botaoIniciar');
const escreverCifraTextArea = document.getElementById('escreverCifraTextArea');
const selectInstrumento = document.getElementById('selectInstrumento');
const salvarDiv = document.getElementById('salvarDiv');
const selectOpcoes = document.getElementById('selectOpcoes');
const modalGravar = document.getElementById('modalGravar');
const musicaSearch = document.getElementById('musicaSearch');
const titulo = document.getElementById('titulo');
const bateriaBotoes = document.getElementById('bateriaBotoes');
const selectSalvamento = document.getElementById('selectSalvamento');
const lightCompasso = document.getElementById('lightCompasso');
const modal_loading = document.getElementById('modal_loading');
const prepararBateriaBotao = document.getElementById('prepararBateriaBotao');
const pararBateriaBotao = document.getElementById('pararBateriaBotao');
const play_pause_bateria = document.getElementById('play_pause_bateria');
const textoRitmo = document.getElementById('textoRitmo');
const listaMusicasCifra = document.getElementById('listaMusicasCifra');
const orgaoBox = document.getElementById('orgaoBox');
const notaTuner = document.getElementById('notaTuner');
const tunerDiv = document.getElementById('tunerDiv');
const autoTunerCheck = document.getElementById('autoTunerCheck');
const liturgiaDiariaDiv = document.getElementById('liturgiaDiariaDiv');
const violinoDesenho = document.getElementById('violinoDesenho');
const violaoDesenho = document.getElementById('violaoDesenho');
const switchDark = document.getElementById('switchDark');
const baixo = document.getElementById('baixo');
const mao = document.getElementById('mao');
const full = document.getElementById('full');
const stringsCheck = document.getElementById('stringsCheck');
const violaoCheck = document.getElementById('stringsCheck');
const measureLength = document.getElementById('measureLength');
const musicaAcordesTextArea = document.getElementById('musicaAcordesTextArea');
const acorde_0 = document.getElementById('acorde_0');
const acorde_3 = document.getElementById('acorde_3');
const acorde_4 = document.getElementById('acorde_4');
const acorde_5 = document.getElementById('acorde_5');
const acorde_1 = document.getElementById('acorde_1');
const acorde_2 = document.getElementById('acorde_2');
const aumentarTomMais = document.getElementById('aumentarTomMais');
const diminuirTomMenos = document.getElementById('diminuirTomMenos');
const tomMenorSwitch = document.getElementById('tomMenorSwitch');
const tituloConfiguracaoTeclas = document.getElementById('tituloConfiguracaoTeclas');
const textoConfiguracao = document.getElementById('textoConfiguracao');
const tecladoTeclasDiv = document.getElementById('tecladoTeclasDiv');
const inputTecla = document.getElementById('inputTecla');

const elementos = {
	instrumentoSelect: instrumentoSelect,
	autoCheck: autoCheck,
	bpm: bpm,
	selectRitmo: selectRitmo,
	botaobotaoGravar: botaobotaoGravar,
	play_pause: play_pause,
	bpmRange: bpmRange,
	chimbal: chimbal,
	brush: brush,
	meiaLua: meiaLua,
	prato: prato,
	aro: aro,
	caixa: caixa,
	cravo: cravo,
	brushCravo: brushCravo,
	pratoCravo: pratoCravo,
	violaoBotao: violaoBotao,
	pianoBotao: pianoBotao,
	baixoBotao: baixoBotao,
	tomSelect: tomSelect,
	volumeTexto: volumeTexto,
	textoCifras: textoCifras,
	textoAcordeMenor: textoAcordeMenor,
	textoCifrasFrame: textoCifrasFrame,
	container: container,
	voltar: voltar,
	botaoFonte: botaoFonte,
	selectFonte: selectFonte,
	botaoTamanhoIframe: botaoTamanhoIframe,
	selectTamanhoIframe: selectTamanhoIframe,
	tomMenorSwitchDiv: tomMenorSwitchDiv,
	orgaoCifrasBotoes: orgaoCifrasBotoes,
	tdVolume: tdVolume,
	volumeDiv: volumeDiv,
	textoVolume: textoVolume,
	volumeInput: volumeInput,
	cifraAvancar: cifraAvancar,
	cifraRepetir: cifraRepetir,
	cifraRetroceder: cifraRetroceder,
	exitfullscreen: exitfullscreen,
	fullscreenDiv: fullscreenDiv,
	botaoFullscreen: botaoFullscreen,
	trackerControls: trackerControls,
	divBateriaSwitch: divBateriaSwitch,
	bateria: bateria,
	linhaSelectTom: linhaSelectTom,
	navBar: navBar,
	oracoesEucaristicasDiv: oracoesEucaristicasDiv,
	acorde_10: acorde_10,
	acorde_7: acorde_7,
	acorde_9: acorde_9,
	muteDiv: muteDiv,
	botaoMute: botaoMute,
	iconVolumeMute: iconVolumeMute,
	iconVolume: iconVolume,
	modal01: modal01,
	linksCifraClubList: linksCifraClubList,
	botaoIniciar: botaoIniciar,
	escreverCifraTextArea: escreverCifraTextArea,
	selectInstrumento: selectInstrumento,
	salvarDiv: salvarDiv,
	selectOpcoes: selectOpcoes,
	modalGravar: modalGravar,
	musicaSearch: musicaSearch,
	titulo: titulo,
	bateriaBotoes: bateriaBotoes,
	selectSalvamento: selectSalvamento,
	lightCompasso: lightCompasso,
	modal_loading: modal_loading,
	prepararBateriaBotao: prepararBateriaBotao,
	pararBateriaBotao: pararBateriaBotao,
	play_pause_bateria: play_pause_bateria,
	textoRitmo: textoRitmo,
	listaMusicasCifra: listaMusicasCifra,
	orgaoBox: orgaoBox,
	notaTuner: notaTuner,
	tunerDiv: tunerDiv,
	autoTunerCheck: autoTunerCheck,
	liturgiaDiariaDiv: liturgiaDiariaDiv,
	violinoDesenho: violinoDesenho,
	violaoDesenho: violaoDesenho,
	switchDark: switchDark,
	baixo: baixo,
	mao: mao,
	full: full,
	stringsCheck: stringsCheck,
	violaoCheck: stringsCheck,
	measureLength: measureLength,
	musicaAcordesTextArea: musicaAcordesTextArea,
	acorde_0: acorde_0,
	acorde_3: acorde_3,
	acorde_4: acorde_4,
	acorde_5: acorde_5,
	acorde_1: acorde_1,
	acorde_2: acorde_2,
	aumentarTomMais: aumentarTomMais,
	diminuirTomMenos: diminuirTomMenos,
	tomMenorSwitch: tomMenorSwitch,
	tituloConfiguracaoTeclas: tituloConfiguracaoTeclas,
	textoConfiguracao: textoConfiguracao,
	tecladoTeclasDiv: tecladoTeclasDiv,
	inputTecla: inputTecla
};

deixarAcompanhamentoSelecionado('full');
verificarOrientacaoCelular();
manterTelaLigada_v2();
carregarConfiguracaoTeclas();

document.addEventListener("visibilitychange", function () {
	if (isMobileDevice())
		if (document.visibilityState === 'visible')
			manterTelaLigada_v2();
});

document.addEventListener('keydown', function (event) {
	capturarTeclaPressionada(event.key);
});

window.onerror = function (message, source, lineno, colno, error) {
	if (isMobileDevice())
		alert("Erro!\n" + message);
};

selectRitmo.addEventListener('change', function (e) {
	_grupoNotas = verificarGrupoNotasInstanciado(_grupoNotas);
});

bpm.addEventListener('change', function (e) {
	_grupoNotas = verificarGrupoNotasInstanciado(_grupoNotas);
});

autoCheck.addEventListener('change', function (e) {
	if (_cravoSelecionado)
		_autoMudarRitmo = false;
	else {
		_autoMudarRitmo = this.checked;
		if (_autoMudarRitmo)
			ocultarBotoesRitmo();
		else
			ocultarBotoesRitmo(false);
	}
});

instrumentoSelect.addEventListener('change', (e) => {
	var semacentos = instrumentoSelect.value.normalize("NFD").replace(/[\u0300-\u036f]/g, '');
	_instrumentoSelecionado = semacentos.toLowerCase();
});

window.addEventListener("orientationchange", (event) => {
	orientacaoCelularAlterado(event);
});

function capturarTeclaPressionada(tecla) {
	if (_configurandoTeclas)
		armazenarTeclaConfiguracaoTeclas(tecla);
	else if (_teclasConfiguracao[tecla]) {
		const elemento = elementos[_teclasConfiguracao[tecla][0]];
		const evento = eventos[_teclasConfiguracao[tecla][1]];

		if (_teclasConfiguracao[tecla][0].includes('Check'))
			elemento.checked = !elemento.checked;

		elemento.dispatchEvent(evento);		
	}
}

function limparConfiguracaoTeclas() {
	if (confirm('Apagar as Configurações de Teclas?'))
		localStorage.removeItem('teclasConfiguracao');
}

function mostrarSalvarConfiguracaoTeclas() {
	_configurandoTeclas = true;
	modal01.style.display = 'none';
	tituloConfiguracaoTeclas.style.display = '';
	titulo.style.display = 'none';
	botaoGravar.style.display = 'none';
	play_pause.style.display = '';
	prepararBateriaBotao.style.display = 'none';
	pararBateriaBotao.style.display = '';
}

function ocultarSalvarConfiguracaoTeclas() {
	_configurandoTeclas = false;
	tituloConfiguracaoTeclas.style.display = 'none';
	titulo.style.display = '';
	tecladoTeclasDiv.style.display = 'none';
	botaoGravar.style.display = '';
	play_pause.style.display = 'none';
	prepararBateriaBotao.style.display = '';
	pararBateriaBotao.style.display = 'none';
}

function capturarTeclaConfiguracaoTeclas(elementoCapturado) {
	_configuracaoEvento = elementoCapturado.getAttribute('dataEventoTecla');
	selectConfiguracao.style.display = 'none';
	modal01.style.display = 'block';
	_configuracaoElemento = elementoCapturado.id;
	selectOpcoes.style.display = 'none';
	tecladoTeclasDiv.style.display = '';
	inputTecla.focus();
}

function verificarSeTemValor(dicionario, elementoProcurar) {
	for (let chave in dicionario) {
		if (dicionario[chave][0].includes(elementoProcurar))
			delete dicionario[chave];
	}
}

function armazenarTeclaConfiguracaoTeclas(tecla) {
	verificarSeTemValor(_teclasConfiguracao, _configuracaoElemento);
	let array = [_configuracaoElemento, _configuracaoEvento];
	_teclasConfiguracao[tecla] = array;
	ocultarModal();
}

function salvarConfiguracaoTeclas() {
	if (_teclasConfiguracao) {
		localStorage.setItem('teclasConfiguracao', JSON.stringify(_teclasConfiguracao));
		ocultarSalvarConfiguracaoTeclas();
	}
	else
		alert('Nada foi configurado. Saindo...');

	carregarConfiguracaoTeclas();
}

function carregarConfiguracaoTeclas() {
	const dadosStorage = localStorage.getItem('teclasConfiguracao');
	if (dadosStorage)
		_teclasConfiguracao = JSON.parse(dadosStorage);	
}

function handleTouchStart(event, element, bateria = false) {
	event.preventDefault();

	element.dispatchEvent(eventoMousedown);

	if (bateria)
		element.dispatchEvent(eventoClick);
}

function isMobileDevice() {
	return /Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent);
}

function darkModeLocalStorage() {
	var darkMode = localStorage.getItem('darkMode');
	if (darkMode)
		if (darkMode === 'true') {
			switchDark.checked = true;
			switchDark.dispatchEvent(eventoClick);
		}
}

function manterTelaLigada_v2() {
	let wakeLock = null;
	try {
		wakeLock = navigator.wakeLock.request("screen");
	} catch { }
}

function verificarOrientacaoCelular() {
	if (isMobileDevice()) {
		if (window.matchMedia("(orientation: portrait)").matches)
			_orientacaoCelularPe = true;

		if (window.matchMedia("(orientation: landscape)").matches)
			_orientacaoCelularPe = false;
	}
}

function orientacaoCelularAlterado(event) {
	if (event.target.screen.orientation.angle === 0) {
		_orientacaoCelularPe = true;

		if (textoCifrasFrame.style.display !== 'none')
			mostrarNavBar();
	}
	else {
		_orientacaoCelularPe = false;

		if (textoCifrasFrame.style.display !== 'none')
			ocultarNavBar();
	}

	if (exitfullscreen.style.display !== 'none')
		mudarParaFullscreen();
}

function mostrarBateria(mostrar = true) {
	if (mostrar)
		bateriaBotoes.style.display = '';
	else
		bateriaBotoes.style.display = 'none';

	localStorage.setItem('acompCheck', acompCheck.checked);
}

function ocultarBotoesCravo(ocultar = true) {
	let cravoBotoes = document.getElementsByClassName('trCravoBotoes');

	if (ocultar) {
		for (let i = 0; i < cravoBotoes.length; i++)
			cravoBotoes[i].style.display = 'none';

		autoCheckDiv.style.display = '';
		//acompCheckDiv.style.display = 'none';
		baixo.style.display = 'none';
		mao.style.display = 'none';
		full.style.display = 'none';
		violaoBotao.style.display = '';
		pianoBotao.style.display = '';
		baixoBotao.style.display = '';
	}
	else {
		for (let i = 0; i < cravoBotoes.length; i++)
			cravoBotoes[i].style.display = '';

		autoCheckDiv.style.display = 'none';
		//acompCheckDiv.style.display = '';
		baixo.style.display = '';
		mao.style.display = '';
		full.style.display = '';
		violaoBotao.style.display = 'none';
		pianoBotao.style.display = 'none';
		baixoBotao.style.display = 'none';
	}
}

function ocultarBotoesRitmo(ocultar = true) {
	var bateriaBotoes = document.getElementsByClassName('trBateriaBotoes');

	if (ocultar) {
		for (let i = 0; i < bateriaBotoes.length; i++)
			bateriaBotoes[i].style.display = 'none';
	}
	else {
		for (let i = 0; i < bateriaBotoes.length; i++)
			bateriaBotoes[i].style.display = '';
	}

	//if (_cravoSelecionado) {
	//	cravo.style.display = '';
	//	violaoBotao.style.display = 'none';
	//}
	//else {
	//	cravo.style.display = 'none';
	//	violaoBotao.style.display = '';
	//}
}

function pressionarBotaoCravo() {
	var botaoCravoSelecionado = document.getElementsByClassName('selecionadoDrum');
	if (botaoCravoSelecionado.length > 0)
		botaoCravoSelecionado[0].dispatchEvent(eventoClick);
}

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
	if (_configurandoTeclas) {
		capturarTeclaConfiguracaoTeclas(botao);
		return;
	}

	if (_cifraId > 0) {
		_cifraParado = true;
		_cifraId--;
	}

	if (_acordeAntesSelecionado === acorde)
		_acordeSelecionado = '';
	else
		_acordeSelecionado = acorde;

	if (botao)
		levantarBotoesAcordes();

	if (_acordeSelecionado === '') {
		_acordeAntesSelecionado = _acordeSelecionado;
		pararOsAcordes();

		if (botao)
			ocultarBotaoRec(false);
	}
	else {
		tocarAcorde(acorde, botao);
		//pressionarBotaoCravo();

		if (botao)
			ocultarBotaoRec();
	}
}

function autoMudarRitmo(elementBotao = null) {
	if (pararBateriaBotao.style.display !== 'none' && _autoMudarRitmo) {
		var selecionadoElement = elementBotao || document.querySelector('.selecionado');

		if (_stringsSelecionado) {
			if ((selecionadoElement.id === 'baixo' || selecionadoElement.id === 'mao') && _instrumentoSelecionado === 'epiano') {
				caixa.dispatchEvent(eventoClick);
			} else if ((selecionadoElement.id === 'baixo' || selecionadoElement.id === 'mao') && _instrumentoSelecionado === 'orgao') {
				brush.dispatchEvent(eventoClick);
			} else if (selecionadoElement.id === 'full' && _instrumentoSelecionado === 'epiano') {
				meiaLua.dispatchEvent(eventoClick);
			} else if (selecionadoElement.id === 'full' && _instrumentoSelecionado === 'orgao') {
				aro.dispatchEvent(eventoClick);
			}
		} else {
			if ((selecionadoElement.id === 'baixo' || selecionadoElement.id === 'mao') && _instrumentoSelecionado === 'epiano') {
				aro.dispatchEvent(eventoClick);
			} else if ((selecionadoElement.id === 'baixo' || selecionadoElement.id === 'mao') && _instrumentoSelecionado === 'orgao') {
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
	if (_configurandoTeclas) {
		capturarTeclaConfiguracaoTeclas(botao);
		return;
	}
	_acompanhamentoSelecionado = funcao;
	pressionarBotaoAcompanhamento(botao);

	autoMudarRitmo(botao);
}

function tocarAcorde(acorde, botao) {
	if (botao) {
		if (botao.value !== '')
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

function refinarAcorde(acorde) {
	acorde = acorde.includes('_') ? acorde.split('_')[1] : acorde;

	if (acorde.length > 1) {
		if (acorde[1] === 'b') {
			var soNota = acorde.slice(0, 2);
			acorde = acorde.replace(soNota, acidentesCorrespondentesJson[soNota]);
		}
	}

	_acordeSelecionado = acorde;
	return acorde;
}

function getNotaBaixo(acorde) {
	let notaBaixo;
	if (acorde.includes('/')) {
		notaBaixo = refinarAcorde(acorde.split('/')[1]);
		acorde = refinarAcorde(acorde.split('/')[0]);
		notaBaixo = notasAcordesJson[notaBaixo][0];
	}		
	else {
		acorde = refinarAcorde(acorde);
		notaBaixo = notasAcordesJson[acorde][0];
	}
	return [acorde, notaBaixo];
}

function montarAcorde(acorde, grupoNotas, instrumento = 'orgao') {
	if (instrumento === 'stringsSolo' && _stringsSelecionado)
		instrumento = 'strings';

	acorde = acorde.replace('E#', 'F');
	let retorno = getNotaBaixo(acorde);
	acorde = retorno[0];
	_acordeBaixo = retorno[1];
	_acordeNotas = notasAcordesJson[acorde];

	if (grupoNotas) {

		if (instrumento === 'stringsSolo') {
			grupoNotas.addSound(acordes['strings_' + _acordeNotas[0]]);
			grupoNotas.addSound(acordes['strings_' + _acordeNotas[0] + '_baixo']);
			grupoNotas.addSound(acordes['strings_' + _acordeNotas[0] + '_grave']);
		}

		//else if (instrumento === 'epianoSolo') {
		//	grupoNotas.addSound(acordes['epiano_' + notas[0]]);
		//	grupoNotas.addSound(acordes['epiano_' + notas[2]]);
		//	grupoNotas.addSound(acordes['epiano_' + notas[0] + '_baixo']);
		//	grupoNotas.addSound(acordes['epiano_' + notas[2] + '_baixo']);
		//	grupoNotas.addSound(acordes['epiano_' + notas[0] + '_grave']);
		//	//grupoNotas.addSound(acordes['epiano_' + notas[2] + '_grave']);
		//}

		//else if (instrumento === 'epianoSoloPad') {
		//	grupoNotas.addSound(acordes['epiano_' + notas[0]]);
		//	grupoNotas.addSound(acordes['epiano_' + notas[2]]);
		//	grupoNotas.addSound(acordes['epiano_' + notas[0] + '_baixo']);
		//	grupoNotas.addSound(acordes['epiano_' + notas[2] + '_baixo']);
		//	grupoNotas.addSound(acordes['epiano_' + notas[0] + '_grave']);
		//	//grupoNotas.addSound(acordes['epiano_' + notas[2] + '_grave']);

		//	grupoNotas.addSound(acordes['strings_' + notas[0]]);
		//	grupoNotas.addSound(acordes['strings_' + notas[2]]);
		//	grupoNotas.addSound(acordes['strings_' + notas[0] + '_baixo']);
		//	grupoNotas.addSound(acordes['strings_' + notas[2] + '_baixo']);
		//	grupoNotas.addSound(acordes['strings_' + notas[0] + '_grave']);
		//	//grupoNotas.addSound(acordes['strings_' + notas[2] + '_grave']);
		//}

		else {
			for (var i = 0, len = _acordeNotas.length; i < len; i++) {
				if (_acompanhamentoSelecionado === 'full' || _acompanhamentoSelecionado === 'baixo') {
					if (instrumento !== 'epiano') {
						if (i === 0)
							grupoNotas.addSound(acordes[instrumento + '_' + _acordeBaixo + '_grave']);
					}

					if (instrumento !== 'strings')
						grupoNotas.addSound(acordes[instrumento + '_' + _acordeNotas[i] + '_baixo']);
					else {
						if (i === 1)
							grupoNotas.addSound(acordes[instrumento + '_' + _acordeNotas[i] + '_baixo']);
					}
				}

				if (_acompanhamentoSelecionado === 'full' || _acompanhamentoSelecionado === 'mao')
					if (instrumento === 'stringsSolo')
						grupoNotas.addSound(acordes['strings_' + _acordeNotas[0]]);
					else {
						if (instrumento !== 'strings')
							grupoNotas.addSound(acordes[instrumento + '_' + _acordeNotas[i]]);
						else {
							if (i !== 0 && i !== 1)
								grupoNotas.addSound(acordes[instrumento + '_' + _acordeNotas[i]]);
						}
					}
			}
		}
	}

	if (instrumento === 'orgao')
		grupoNotas.attack = 0.1;

	return grupoNotas;
}

function verificarGrupoNotasInstanciado(grupoNotas, efeito = null) {
	if (!grupoNotas) {
		grupoNotas = new Pizzicato.Group();

		grupoNotas.volume = _volume;
	}

	return grupoNotas;
}

function verificarAcompanhamentoEtocar(acorde, esperar = 0) {
	if (_acordeAntesSelecionado === acorde) {
		pararOsAcordes(true, _stringsSelecionado);
		esperar = 50;
	}
	else
		pararOsAcordes(true);

	_acordeAntesSelecionado = acorde;

	if (_instrumentoSelecionado === 'strings') {
		_grupoNotasStrings = verificarGrupoNotasInstanciado(_grupoNotasStrings);
		_grupoNotasStrings = montarAcorde(acorde, _grupoNotasStrings, 'stringsSolo');
		_grupoNotasStrings.play();
	}

	else {
		if (_instrumentoSelecionado === 'epiano')
			_grupoNotas = verificarGrupoNotasInstanciado(_grupoNotas);
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
}

function pararOsAcordes(removerSons = false, continuarStrings = false) {
	if (_grupoNotas) {
		_grupoNotas.stop();

		if (removerSons) {
			var sons = _grupoNotas.sounds.length;
			for (let i = sons - 1; i > -1; i--)
				_grupoNotas.removeSound(_grupoNotas.sounds[i]);
		}
	}

	if (continuarStrings === false)
		if (_grupoNotasStrings) {
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
	if (botaoAcompPressionado(botao) === false) {
		if (_acordeAntesSelecionado !== '')
			if (_instrumentoSelecionado !== 'epiano')
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

	if (_grupoNotas)
		_grupoNotas.volume = _volume;

	if (_grupoNotasStrings)
		_grupoNotasStrings.volume = _volume;

	if (volume === padrao)
		volumeTexto.innerHTML = volume * 10;
	else
		volumeTexto.innerHTML = (volume * 10) + '*';
}

function mudarTomCifra(aumentar, quant) {
	_tomSelectedIndexCifra = tomSelect.selectedIndex;
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

	localStorage.setItem('tomSelecionadoIndex', tomSelect.selectedIndex)
}

function mudarTomMenor(acordeIndex) {
	textoAcordeMenor.innerText = acordesTons[acordeIndex + 12];
}

function aumentarTom(aumentar, quant, select) {
	if (_configurandoTeclas) {
		capturarTeclaConfiguracaoTeclas(document.activeElement);
		return;
	}
	var tomElement = document.getElementById(select);
	var tomSelecionadoIndex = tomElement.selectedIndex;

	if (tomElement.value.includes('m'))
		var tonsArray = tonsMenores;
	else
		var tonsArray = tonsMaiores;

	if (aumentar) {
		if (tomSelecionadoIndex + quant === tomElement.length)
			tomElement.value = tonsArray[0];
		else if (tomSelecionadoIndex + quant > tomElement.length)
			tomElement.value = tonsArray[-1 + quant];
		else
			tomElement.value = tonsArray[tomSelecionadoIndex + quant];
	}
	else {
		if (tomSelecionadoIndex - quant < 0)
			if (tomSelecionadoIndex === 0)
				tomElement.value = tonsArray[tomElement.length - quant];
			else
				tomElement.value = tonsArray[tomElement.length - 1];
		else
			tomElement.value = tonsArray[tomSelecionadoIndex - quant];
	}

	if (textoCifrasFrame.style.display === "block")
		mudarTomCifra(aumentar, quant);
	else
		mudarTom(tomElement.value);
}

function adicionarTonsSelect(element, index, maior) {
	var selectElem = document.getElementById(element);
	selectElem.innerHTML = "";

	var tons = tonsMaiores;
	if (maior === false)
		tons = tonsMenores;

	for (var i = 0; i < tons.length; i++) {
		let opt = document.createElement('option');
		opt.value = tons[i];
		opt.textContent += tons[i];
		selectElem.appendChild(opt);
	};

	selectElem.selectedIndex = index;
}

function mostrarTextoCifrasCarregado(tom = null, texto = null) {
	if (tom) {
		if (tom.includes('m'))
			adicionarTonsSelect('tomSelect', tonsMenores.indexOf(tom), false);
		else
			adicionarTonsSelect('tomSelect', tonsMaiores.indexOf(tom), true);

		_tomSelectedIndexCifra = tomSelect.selectedIndex;
	}

	if (texto)
		textoCifras.contentDocument.body.innerHTML = texto;

	if (document.body.classList.contains("bg-dark"))
		textoCifras.contentWindow.document.querySelector('pre').style.color = '#fff';
	else
		textoCifras.contentWindow.document.querySelector('pre').style.color = '#000';

	if (textoCifrasFrame.style.display === 'none') {
		mudarParaTelaFrame();
	}

	addEventCifras(textoCifras);
	mudarTamanhoFrameCifras(_orientacaoCelularPe);
}

function selecionarCifraId() {
	var cifraElems = textoCifras.contentDocument.getElementsByClassName('cifraSelecionada');

	if (cifraElems.length > 0)
		_cifraId = cifraElems[0].id.split('cifra')[1] - 1;
}

function mudarParaTelaFrame() {
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
	botaoTamanhoIframe.style.display = 'block';
	selectTamanhoIframe.style.display = "none";
	tomMenorSwitchDiv.style.display = 'none';
	orgaoCifrasBotoes.style.display = '';

	tdVolume.setAttribute('rowspan', '');
	tdVolume.setAttribute('colspan', 5);
	volumeDiv.style.display = 'block';
	textoVolume.style.display = 'none';
	volumeInput.setAttribute('orient', '');

	$('#tdVolume').appendTo('#orgaoTable');
	$('#orgaoTable').prependTo('#bateriaBox');

	if (_orientacaoCelularPe === false)
		ocultarNavBar();
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

	if (_cifraParado === false)
		avancarCifra('avancar', cifraAvancar);
}

function avancarCifra(avancar_retroceder, botao) {
	if (_configurandoTeclas) {
		capturarTeclaConfiguracaoTeclas(botao);
		return;
	}
	if (avancar_retroceder === '') {
		escolherAcorde('', botao);
		botaoGravar.style.display = 'block';
		play_pause.style.display = 'none';
	}

	else if (avancar_retroceder === 'repetir') {
		if (_cifraParado === false)
			verificarAcompanhamentoEtocar(_acordeAntesSelecionado, 50);
	}

	else {
		var frameContent = textoCifras.contentDocument;
		var elements_b = frameContent.getElementsByTagName('b');
		var cifraElems = frameContent.getElementsByClassName('cifraSelecionada');

		if (avancar_retroceder === 'avancar') {
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

		if (avancar_retroceder === 'retroceder') {
			if (_cifraId - 1 > 0) {
				if (cifraElems.length > 0)
					cifraElems[0].classList.remove('cifraSelecionada');

				if (_cifraParado === false)
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

function mudarTempoCompasso() {
	var bpmValor = bpm.value;
	bpmValor = 60000 / bpmValor;

	if (selectRitmo.value === '6/8')
		bpmValor = bpmValor / 2;

	lightCompasso.style.animation = 'blink ' + bpmValor + 'ms infinite';
	bpmRange.value = bpm.value;
}

function mudarTamanhoFrameCifras(aumentar) {
	textoCifras.contentWindow.document.querySelector('pre').style.fontSize = selectFonte.value + 'px';

	//if (textoCifrasFrame.style.display != 'none') {
	//	var altura = parseInt(textoCifras.style.height);
	//	if (aumentar)
	//		altura = altura * 1.5;
	//	else
	//		altura = altura / 1.5;

	//	textoCifras.style.height = altura + 'px';
	//	textoCifrasFrame.style.height = altura + 'px';
	//}
}

function rolagemTelaOracaoEucaristica(guardar = true) {
	if (oracoesEucaristicasDiv.style.display !== 'none') {
		var nomeVar = 'scrollOracoesEuc';

		if (guardar)
			localStorage.setItem(nomeVar, modal01.scrollTop);
		else {
			var scrollPosition = localStorage.getItem(nomeVar);
			if (scrollPosition)
				modal01.scrollTo(0, parseInt(scrollPosition));
		}
	}
}

function selecionarStrings(checked) {
	if (_configurandoTeclas) {
		stringsCheck.checked = false;
		capturarTeclaConfiguracaoTeclas(stringsCheck);
		return;
	}
	if (checked) {
		_stringsSelecionado = true;

		if (_stringsParado)
			if (_acordeAntesSelecionado !== '') {
				_stringsParado = false;
				_grupoNotasStrings = verificarGrupoNotasInstanciado(_grupoNotasStrings);
				_grupoNotasStrings = montarAcorde(_acordeAntesSelecionado, _grupoNotasStrings, 'strings');
				_grupoNotasStrings.play();
			}
	}
	else
		_stringsSelecionado = false;

	autoMudarRitmo();
}

function selecionarViolao(checked) {
	if (_configurandoTeclas) {
		violaoCheck.checked = false;
		capturarTeclaConfiguracaoTeclas(violaoCheck);
		return;
	}
	if (checked)
		_violaoSelecionado = true;
	else
		_violaoSelecionado = false;
}


function voltarParaOrgao() {
	voltar.style.display = 'none';
	botaoFonte.style.display = 'none';
	selectFonte.style.display = "none";
	botaoTamanhoIframe.style.display = 'none';
	selectTamanhoIframe.style.display = "none";
	orgaoCifrasBotoes.style.display = 'none';

	textoCifrasFrame.style.display = 'none';
	tomMenorSwitchDiv.style.display = '';

	var elements = document.getElementsByClassName('orgaoBotoes');
	for (var i = 0; i < elements.length; i++) {
		elements[i].style.display = '';
	};

	container.classList.add('d-sm-flex');

	tdVolume.setAttribute('rowspan', 5);
	tdVolume.setAttribute('colspan', '');
	volumeDiv.style.display = 'flex';
	textoVolume.style.display = '';
	volumeInput.setAttribute('orient', 'vertical');

	$('#tdVolume').appendTo('#orgaoControle');
	$('#orgaoTable').appendTo('#orgaoBox');

	mostrarNavBar();

	ultimoTomSelecionadoStorage();
}

function mudarParaFullscreen() {
	exitfullscreen.style.display = 'flex';
	botaoFullscreen.style.display = 'none';

	divBateriaSwitch.style.display = 'none';
	bateria.style.display = 'none';
	switchDarkDiv.style.display = 'none';
	muteDiv.style.display = '';

	if (textoCifrasFrame.style.display !== 'none' && _orientacaoCelularPe === false && isMobileDevice())
		ocultarNavBar();

	var el = document.body;
	var requestMethod = el.requestFullScreen || el.webkitRequestFullScreen
		|| el.mozRequestFullScreen || el.msRequestFullScreen;

	if (requestMethod)
		requestMethod.call(el);
}

function sairDeFullscreen() {
	if (exitfullscreen.style.display !== 'none') {
		if (textoCifrasFrame.style.display === 'none')
			mostrarNavBar();

		exitfullscreen.style.display = 'none';
		botaoFullscreen.style.display = 'flex';
		switchDarkDiv.style.display = '';
		muteDiv.style.display = 'none';
		document.exitFullscreen();

		linhaSelectTom.classList.add('d-flex');

		mutarVolume(false);
	}
}

function mostrarNavBar() {
	$('#fullscreenDiv').prependTo('#linhaNavBar');
	$('#botaoOpcoes').prependTo('#linhaNavBar');
	$('#titulo').appendTo('#linhaNavBar');
	$('#botaoSalvar').appendTo('#linhaNavBar');
	$('#switchDarkDiv').appendTo('#linhaNavBar');
	$('#muteDiv').appendTo('#linhaNavBar');
	navBar.style.display = 'block';
	linhaSelectTom.style.width = '';
}

function ocultarNavBar() {
	$('#titulo').prependTo('#linhaSelectTom');
	$('#fullscreenDiv').prependTo('#linhaSelectTom');
	$('#botaoOpcoes').prependTo('#linhaSelectTom');
	$('#botaoSalvar').appendTo('#linhaSelectTom');
	$('#muteDiv').appendTo('#linhaSelectTom');
	navBar.style.display = 'none';
	linhaSelectTom.style.width = '100%';
}

function ultimoTomSelecionadoStorage() {
	var tomSelecionadoIndex = localStorage.getItem('tomSelecionadoIndex');
	if (tomSelecionadoIndex) {
		tomSelect.selectedIndex = tomSelecionadoIndex;
		tomSelect.dispatchEvent(new Event('change'));
	}
}

function carregarConfiguracoesDoStorage() {
	darkModeLocalStorage();
	ultimoTomSelecionadoStorage();

	let storage = localStorage.getItem('acompCheck');
	if (storage && storage === 'true') {
		acompCheck.checked = true;
		acompCheck.dispatchEvent(eventoClick);
		mostrarBateria(true);
	}
	else
		acompCheck.checked = false;
}

function selecionarTomMenor(selecionadoMenor) {
	if (selecionadoMenor) {
		textoAcordeMenor.style.color = 'black';
		tomSelect.style.color = 'grey';
		acorde_10.style.display = 'block';
		acorde_7.style.display = 'block';
		acorde_9.style.display = 'block';
	}
	else {
		textoAcordeMenor.style.color = 'grey';
		tomSelect.style.color = 'black';
		acorde_10.style.display = 'none';
		acorde_7.style.display = 'none';
		acorde_9.style.display = 'none';
	}
}

function aumentarTom_click(aumentar) {
	if (timer)
		clearTimeout(timer);
	timer = setTimeout(function () {
		aumentarTom(aumentar, 1, "tomSelect");
	}, 240);
}

function aumentarTom_clickDuplo(aumentar) {
	clearTimeout(timer);
	aumentarTom(aumentar, 2, "tomSelect");
}

function showselectFonte(mostrar) {
	if (mostrar) {
		botaoFonte.style.display = "none";
		selectFonte.style.display = "";
	}
	else {
		if (textoCifrasFrame.style.display !== 'none') {
			textoCifras.contentWindow.document.querySelector('pre').style.fontSize = selectFonte.value + 'px';
		}
	}
}

function showselectIframe(mostrar) {
	if (mostrar) {
		botaoTamanhoIframe.style.display = "none";
		selectTamanhoIframe.style.display = "";
	}
	else {
		if (textoCifrasFrame.style.display !== 'none') {
			textoCifrasFrame.style.height = selectTamanhoIframe.value + 'px';
			textoCifras.style.height = selectTamanhoIframe.value + 'px';
		}
	}
}

function prepararMudarTomCifra(tomSelecionado) {
	var esperar = 0;
	if (typeof mudarTom !== 'function' || typeof mudarTomCifra !== 'function') //1º carregamento
		esperar = 500;

	setTimeout(function () {
		if (textoCifrasFrame.style.display == "none")
			mudarTom(tomSelecionado);

		else {
			if (tomSelecionado.includes('m'))
				var index = tonsMenores.indexOf(tomSelecionado);
			else
				var index = tonsMaiores.indexOf(tomSelecionado);

			index = index - _tomIndex;

			if (index < 0)
				mudarTomCifra(false, Math.abs(index));
			else
				mudarTomCifra(true, index);
		}
	}, esperar);
}

function pegarTomCifra(tomSelecionado) {
	if (tomSelecionado.includes('m'))
		_tomIndex = tonsMenores.indexOf(tomSelecionado);
	else
		_tomIndex = tonsMaiores.indexOf(tomSelecionado);
}


//[Deprecation] Listener added for a synchronous 'DOMNodeInserted' DOM Mutation Event.This event type is deprecated (https://w3c.github.io/uievents/#legacy-event-types) and work is underway to remove it from this browser. Usage of this event listener will cause performance issues today, and represents a risk of future incompatibility. Consider using MutationObserver instead.

//som do órgão no lmms até o número 5 exporta para ogg bits 160 e depois abre o audacity e  remove o começo do som do órgão até o 0,500130