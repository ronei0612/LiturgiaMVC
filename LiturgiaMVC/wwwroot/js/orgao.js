var _acordeSelecionado = '';
var _acordeAntesSelecionado = '';
var _acompanhamentoSelecionado = '';
var _acompanhamentoSolo = false;
var _volume = 0.9;
var _instrumentoSelecionado = 'orgao';
var _autoMudarRitmo = false;
var _cifraId = 0;
var _cifraParado = true;
var _grupoNotas;
var _grupoNotasStrings;
var _stringsSelecionado = false;
var _stringsParado = true;

const notasAcordes = Object.keys(notasAcordesJson);

//// Cache de elementos DOM frequentemente acessados
//const bpmRange = document.getElementById('bpmRange');
//const autoCheck = document.getElementById('autoCheck');
//const instrumentoSelect = document.getElementById('instrumentoSelect');
//const textoCifrasFrame = document.getElementById('textoCifrasFrame');
//const tomSelect = document.getElementById('tomSelect');

var tremolo = new Pizzicato.Effects.Tremolo({ speed: 1, depth: 0.6, mix: 0.8 });
var flanger = new Pizzicato.Effects.Flanger({ time: 0.45, speed: 0.2, depth: 0.1, feedback: 0.1, mix: 0.2 });
var pingPongDelay = new Pizzicato.Effects.PingPongDelay({ feedback: 0.6, time: 0.4, mix: 0.5 });
var delay = new Pizzicato.Effects.Delay({ feedback: 0.5, time: 0.33, mix: 0.1 });

initialize();

function initialize() {
    deixarAcompanhamentoSelecionado('full');
    addEventListeners();
}

function addEventListeners() {
    document.getElementById('selectRitmo').addEventListener('change', handleRitmoChange);
    document.getElementById('bpm').addEventListener('change', handleBPMChange);
    document.getElementById('autoCheck').addEventListener('change', handleAutoChange);
    document.getElementById('instrumentoSelect').addEventListener('change', handleInstrumentoChange);
    window.addEventListener("orientationchange", handleOrientationChange);
}

function deixarAcompanhamentoSelecionado(funcao) {
    escolherAcompanhamento(funcao, document.getElementById(funcao));
}

function handleRitmoChange(e) {
    adjustBPM();
}

function handleBPMChange(e) {
    adjustBPM();
}

function handleAutoChange(e) {
    _autoMudarRitmo = this.checked;
}

function handleInstrumentoChange(e) {
    var semacentos = this.value.normalize("NFD").replace(/[\u0300-\u036f]/g, '');
    _instrumentoSelecionado = semacentos.toLowerCase();
}

function handleOrientationChange(event) {
    var orientacao = event.target.screen.orientation.angle;
    mudarTamanhoFrameCifras(orientacao == 0);
}

function adjustBPM() {
    _grupoNotas = verificarGrupoNotasInstanciado(_grupoNotas);
    if (_grupoNotas.effects.length > 0) {
        var bpmRange_valor = document.getElementById('bpmRange').value;
        bpmRange_valor = 30 / bpmRange_valor;
        _grupoNotas.effects[0].time = bpmRange_valor;
    }
}

function escolherAcompanhamento(funcao, botao) {
    _acompanhamentoSelecionado = funcao;
    pressionarBotaoAcompanhamento(botao);
    autoMudarRitmo(botao);
}

function autoMudarRitmo(elementBotao = null) {
    var pararBateriaBotao = document.getElementById('pararBateriaBotao');

    if (pararBateriaBotao.style.display !== 'none' && _autoMudarRitmo) {
        var eventoClick = new Event('click');
        var selecionadoElement = elementBotao || document.querySelector('.selecionado');
        var instrumento = _instrumentoSelecionado === 'orgao' ? 'brush' : 'aro';
        var acompanharElement = selecionadoElement.id === 'full' ? instrumento : 'strings_' + selecionadoElement.id;

        if (_stringsSelecionado && acompanharElement === 'strings_solo') {
            acompanharElement = _instrumentoSelecionado === 'orgao' ? 'brush' : 'aro';
        }

        document.getElementById(acompanharElement).dispatchEvent(eventoClick);
    }
}

// Restante do código...

function escolherAcorde(acorde, botao) {
    if (_cifraId > 0) {
        _cifraParado = true;
        _cifraId--;
    }

    if (_acordeAntesSelecionado === acorde) {
        _acordeSelecionado = '';
    } else {
        _acordeSelecionado = acorde;
    }

    if (botao) {
        levantarBotoesAcordes();
    }

    if (_acordeSelecionado === '') {
        _acordeAntesSelecionado = _acordeSelecionado;
        pararOsAcordes();
        if (botao) {
            ocultarBotaoRec(false);
        }
    } else {
        tocarAcorde(acorde, botao);
        if (botao) {
            ocultarBotaoRec();
        }
    }
}

function escolherAcorde(acorde, botao) {
    if (_cifraId > 0) {
        _cifraParado = true;
        _cifraId--;
    }

    if (_acordeAntesSelecionado === acorde) {
        _acordeSelecionado = '';
    } else {
        _acordeSelecionado = acorde;
    }

    if (botao) {
        levantarBotoesAcordes();
    }

    if (_acordeSelecionado === '') {
        _acordeAntesSelecionado = _acordeSelecionado;
        pararOsAcordes();
        if (botao) {
            ocultarBotaoRec(false);
        }
    } else {
        tocarAcorde(acorde, botao);
        if (botao) {
            ocultarBotaoRec();
        }
    }
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

function autoMudarRitmo(elementBotao = null) {
    var pararBateriaBotao = document.getElementById('pararBateriaBotao');

    if (pararBateriaBotao.style.display !== 'none' && _autoMudarRitmo) {
        var eventoClick = new Event('click');
        var selecionadoElement = elementBotao || document.querySelector('.selecionado');

        if (_stringsSelecionado) {
            if ((selecionadoElement.id === 'solo' || selecionadoElement.id === 'mao') && _instrumentoSelecionado === 'epiano') {
                document.getElementById('chimbal').dispatchEvent(eventoClick);
            } else if ((selecionadoElement.id === 'solo' || selecionadoElement.id === 'mao') && _instrumentoSelecionado === 'orgao') {
                document.getElementById('brush').dispatchEvent(eventoClick);
            } else if (selecionadoElement.id === 'full' && _instrumentoSelecionado === 'epiano') {
                document.getElementById('meiaLua').dispatchEvent(eventoClick);
            } else if (selecionadoElement.id === 'full' && _instrumentoSelecionado === 'orgao') {
                document.getElementById('aro').dispatchEvent(eventoClick);
            }
        } else {
            if ((selecionadoElement.id === 'solo' || selecionadoElement.id === 'mao') && _instrumentoSelecionado === 'epiano') {
                document.getElementById('aro').dispatchEvent(eventoClick);
            } else if ((selecionadoElement.id === 'solo' || selecionadoElement.id === 'mao') && _instrumentoSelecionado === 'orgao') {
                document.getElementById('brush').dispatchEvent(eventoClick);
            } else if (selecionadoElement.id === 'full' && _instrumentoSelecionado === 'epiano') {
                document.getElementById('caixa').dispatchEvent(eventoClick);
            } else if (selecionadoElement.id === 'full' && _instrumentoSelecionado === 'orgao') {
                document.getElementById('aro').dispatchEvent(eventoClick);
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
        if (botao.value !== '') {
            verificarAcompanhamentoEtocar(botao.value);
        } else {
            verificarAcompanhamentoEtocar(acorde);
        }
        botao.classList.toggle('pressionado', true);
    } else {
        verificarAcompanhamentoEtocar(acorde);
    }
}

// Restante do código...

function setTom(acorde = 'C') {
    document.getElementById('tomSelect').value = acorde;
}

function montarAcorde(acorde, grupoNotas, instrumento = 'orgao') {
    if (instrumento === 'stringsSolo' && _stringsSelecionado) {
        instrumento = 'strings';
    }

    if (acorde.includes('_')) {
        acorde = acorde.split('_')[1];
    }

    if (acorde.length > 1 && acorde[1] === 'b') {
        var soNota = acorde[0] + acorde[1];
        acorde = acorde.replace(soNota, acidentesCorrespondentesJson[acorde[0] + acorde[1]]);
    }

    if (grupoNotas !== null) {
        var notas = notasAcordesJson[acorde];

        if (instrumento === 'stringsSolo') {
            grupoNotas.addSound(acordes['strings_' + notas[0] + '_baixo']);
            grupoNotas.addSound(acordes['strings_' + notas[0] + '_grave']);
        } else {
            for (var i = 0, len = notas.length; i < len; i++) {
                if ((_acompanhamentoSelecionado === 'full' || _acompanhamentoSelecionado === 'baixo') && i === 0) {
                    grupoNotas.addSound(acordes[instrumento + '_' + notas[i] + '_grave']);
                }

                if ((_acompanhamentoSelecionado === 'full' || _acompanhamentoSelecionado === 'baixo') && instrumento !== 'strings') {
                    grupoNotas.addSound(acordes[instrumento + '_' + notas[i] + '_baixo']);
                }

                if ((_acompanhamentoSelecionado === 'full' || _acompanhamentoSelecionado === 'mao')) {
                    if (instrumento === 'stringsSolo') {
                        grupoNotas.addSound(acordes['strings_' + notas[0]]);
                    } else {
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
        if (grupoNotas.effects.length == 0) {
            grupoNotas.addEffect(efeito);
        }
    } else if (grupoNotas.effects.length > 0) {
        grupoNotas.removeEffect(grupoNotas.effects[0]);
    }

    return grupoNotas;
}

function verificarAcompanhamentoEtocar(acorde, esperar = 0) {
    if (_acordeAntesSelecionado == acorde) {
        pararOsAcordes(true, true);
        esperar = 50;
    } else {
        pararOsAcordes(true);
    }

    setTimeout(function () {
        _acordeAntesSelecionado = acorde;

        if (_instrumentoSelecionado == 'strings') {
            _grupoNotasStrings = verificarGrupoNotasInstanciado(_grupoNotasStrings);
            _grupoNotasStrings = montarAcorde(acorde, _grupoNotasStrings, 'stringsSolo');
            _grupoNotasStrings.play();
        } else {
            if (_instrumentoSelecionado == 'epiano') {
                _grupoNotas = verificarGrupoNotasInstanciado(_grupoNotas, delay);
            } else {
                _grupoNotas = verificarGrupoNotasInstanciado(_grupoNotas);
            }

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
            for (let i = sons - 1; i > -1; i--) {
                _grupoNotas.removeSound(_grupoNotas.sounds[i]);
            }
        }
    }

    if (!continuarStrings) {
        if (_grupoNotasStrings != null) {
            _stringsParado = true;
            _grupoNotasStrings.stop();

            if (removerSons) {
                var sons = _grupoNotasStrings.sounds.length;
                for (let i = sons - 1; i > -1; i--) {
                    _grupoNotasStrings.removeSound(_grupoNotasStrings.sounds[i]);
                }
            }
        }
    }
}

function levantarBotoesAcordes() {
    var botoesPressionados = document.getElementsByClassName('pressionado');
    if (botoesPressionados.length > 0) {
        botoesPressionados[0].classList.toggle('pressionado', false);
    }
}

function levantarBotoesAcompanhamento() {
    var botoes = document.getElementsByClassName('selecionado');
    for (let i = 0; i < botoes.length; i++) {
        botoes[i].classList.toggle('selecionado', false);
    }
}

function pressionarBotaoAcompanhamento(botao) {
    if (!botaoAcompPressionado(botao)) {
        if (_acordeAntesSelecionado != '') {
            if (_instrumentoSelecionado != 'epiano') {
                verificarAcompanhamentoEtocar(_acordeAntesSelecionado);
            }
        }
        levantarBotoesAcompanhamento();
        pressionarBotaoAcomp(botao);
    }
}

function botaoAcompPressionado(botao) {
    return botao.classList.contains('selecionado');
}

function pressionarBotaoAcomp(botao) {
    botao.classList.toggle('selecionado', true);
}

function alterarVolume(volume, padrao) {
    _volume = parseFloat(volume);

    if (_grupoNotas != null) {
        _grupoNotas.volume = _volume;
    }

    if (_grupoNotasStrings != null) {
        _grupoNotasStrings.volume = _volume;
    }

    var volumeTexto = document.getElementById('volumeTexto');
    if (volume == padrao) {
        volumeTexto.innerHTML = volume * 10;
    } else {
        volumeTexto.innerHTML = (volume * 10) + '*';
    }
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
            else {
                alert(data.message);
            }
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

    var botaoSelecionado = document.querySelector('.pressionado');

    if (botaoSelecionado) {
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

    var tonsArray = tomElement.value.includes('m') ? tonsMenores : tonsMaiores;
    var newIndex;

    if (aumentar) {
        newIndex = (tomSelecionadoIndex + quant) % tomElement.length;
    } else {
        newIndex = (tomSelecionadoIndex - quant + tomElement.length) % tomElement.length;
    }

    tomElement.value = tonsArray[newIndex];

    if (document.getElementById('textoCifrasFrame').style.display === "block") {
        mudarTomCifra(aumentar, quant);
    } else {
        mudarTom(tomElement.value);
    }
}

function adicionarTonsSelect(element, index, maior) {
	var selectElem = document.getElementById(element);
	selectElem.innerHTML = "";

	var tons = maior ? tonsMaiores : tonsMenores;

	tons.forEach((ton) => {
		let opt = new Option(ton, ton);
		selectElem.add(opt);
	});

	selectElem.selectedIndex = index;
}

function mostrarTextoArquivoCarregado(tom = null, texto = null) {
    if (tom) {
        var index = tom.includes('m') ? tonsMenores.indexOf(tom) : tonsMaiores.indexOf(tom);
        adicionarTonsSelect('tomSelect', index, !tom.includes('m'));
    }

    var frame = document.getElementById('textoCifras');
    var textoCifrasFrame = document.getElementById('textoCifrasFrame');

    if (texto) {
        frame.contentDocument.body.innerHTML = texto;
    }

    var preElement = frame.contentWindow.document.querySelector('pre');
    preElement.style.color = document.body.classList.contains("bg-dark") ? '#fff' : '#000';

    if (textoCifrasFrame.style.display === 'none') {
        textoCifrasFrame.style.display = 'block';

        var elements = document.getElementsByClassName('orgaoBotoes');
        Array.from(elements).forEach((element) => {
            element.style.display = 'none';
        });

        var container = document.getElementById('container');
        container.classList.remove('d-sm-flex');

        var volumeDiv = document.getElementById('volumeDiv');
        volumeDiv.style.display = 'none';

        var voltarButton = document.getElementById('voltar');
        voltarButton.style.display = 'block';

        var botaoFonte = document.getElementById('botaoFonte');
        botaoFonte.style.display = 'block';

        var selectFonte = document.getElementById('selectFonte');
        selectFonte.style.display = 'none';

        var tomMenorSwitchDiv = document.getElementById('tomMenorSwitchDiv');
        tomMenorSwitchDiv.style.display = 'none';

        var orgaoCifrasBotoes = document.getElementById('orgaoCifrasBotoes');
        orgaoCifrasBotoes.style.display = '';

        var tdVolume = document.getElementById('tdVolume');
        tdVolume.setAttribute('rowspan', '');
        tdVolume.setAttribute('colspan', 5);

        volumeDiv.style.display = 'block';

        var textoVolume = document.getElementById('textoVolume');
        textoVolume.classList.remove('textoVertical');

        var volumeInput = document.getElementById('volumeInput');
        volumeInput.setAttribute('orient', '');

        $('#tdVolume').appendTo('#orgaoTable');
        $('#orgaoTable').prependTo('#bateriaBox');
    }

    addEventCifras(frame);
}

function addEventCifras(frame) {
    var elements = frame.contentDocument.getElementsByTagName("b");

    for (var i = 0; i < elements.length; i++) {
        elements[i].addEventListener("click", function (e) {
            var cifraElements = document.getElementById('textoCifras').contentDocument.getElementsByClassName('cifraSelecionada');

            if (cifraElements.length > 0) {
                cifraElements[0].classList.remove('cifraSelecionada');
            }

            e.target.classList.add('cifraSelecionada');
            e.target.scrollIntoView();
            parent.tocarCifraManualmente(e.target);
        });
    }
}

function tocarCifraManualmente(cifraElem) {
    _cifraId = cifraElem.id.split('cifra')[1] - 1;

    if (!_cifraParado) {
        avancarCifra('avancar', document.getElementById('cifraAvancar'));
    }
}

function avancarCifra(avancar_retroceder, botao) {
    var gravarElement = document.getElementById('gravar');
    var playPauseElement = document.getElementById('play-pause');
    var cifraRetrocederElement = document.getElementById('cifraRetroceder');
    var cifraAvancarElement = document.getElementById('cifraAvancar');

    if (avancar_retroceder === '') {
        escolherAcorde('', botao);
        gravarElement.style.display = 'block';
        playPauseElement.style.display = 'none';
    } else if (avancar_retroceder === 'repetir' && !_cifraParado) {
        verificarAcompanhamentoEtocar(_acordeAntesSelecionado, 50);
    } else {
        var frame = document.getElementById('textoCifras');
        var frameContent = frame.contentDocument;
        var elements_b = frameContent.getElementsByTagName('b');
        var cifraElems = frameContent.getElementsByClassName('cifraSelecionada');

        if (avancar_retroceder === 'avancar' && _cifraId < elements_b.length) {
            processarCifraAvancarRetroceder(cifraElems, cifraRetrocederElement, botao, elements_b);
        }

        if (avancar_retroceder === 'retroceder' && _cifraId - 1 > 0) {
            processarCifraAvancarRetroceder(cifraElems, cifraAvancarElement, botao, elements_b, true);
        }

        gravarElement.style.display = 'none';
        playPauseElement.style.display = 'block';
    }
}

function processarCifraAvancarRetroceder(cifraElems, cifraToggleButton, botao, elements_b, retroceder = false) {
	if (cifraElems.length > 0) {
		cifraElems[0].classList.remove('cifraSelecionada');
	}

	_cifraParado = retroceder ? _cifraParado : false;

	var cifraElem = elements_b[retroceder ? (_cifraId - 1) : _cifraId];
	var cifraToggleOther = retroceder ? document.getElementById('cifraAvancar') : document.getElementById('cifraRetroceder');

	tocarAcorde(cifraElem.innerHTML.trim(), null);

	if (cifraToggleOther.classList.contains('pressionado')) {
		cifraToggleOther.classList.remove('pressionado');
}

	botao.classList.toggle('pressionado', true);
	cifraElem.classList.add('cifraSelecionada');
	cifraElem.scrollIntoView();

	_cifraId += retroceder ? -1 : 1;
}

function mudarTamanhoFrameCifras(aumentar) {
    if (document.getElementById('textoCifrasFrame').style.display != 'none') {
        if (aumentar) {
            document.getElementById('textoCifras').style.height = '250px';
        } else {
            document.getElementById('textoCifras').style.height = '150px';
        }
    }
}

function selecionarStrings(stringsCheck) {
    if (stringsCheck) {
        _stringsSelecionado = true;

        if (_stringsParado) {
            if (_acordeAntesSelecionado != '') {
                _stringsParado = false;
                _grupoNotasStrings = verificarGrupoNotasInstanciado(_grupoNotas);
                _grupoNotasStrings = montarAcorde(_acordeAntesSelecionado, _grupoNotasStrings, 'strings');
                _grupoNotasStrings.play();
            }
        }
    } else {
        _stringsSelecionado = false;
    }

    autoMudarRitmo();
}

// Fim do código


//[Deprecation] Listener added for a synchronous 'DOMNodeInserted' DOM Mutation Event.This event type is deprecated (https://w3c.github.io/uievents/#legacy-event-types) and work is underway to remove it from this browser. Usage of this event listener will cause performance issues today, and represents a risk of future incompatibility. Consider using MutationObserver instead.

//som do órgão no lmms até o número 5 exporta para ogg bits 160 e depois abre o audacity e  remove o começo do som do órgão até o 0,500130