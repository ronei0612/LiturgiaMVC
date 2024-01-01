//var _ritmoSelecionado = 'aro';


function setBeats(ritmoMatrix) {//compasso
    var numerosIndex = ritmosJson[ritmoMatrix];
    var measureLengthElement = document.getElementById('measureLength');

    if (numerosIndex.includes(323))
        measureLengthElement.value = 24;
    else if (numerosIndex.includes(219))
        measureLengthElement.value = 16;
    else if (numerosIndex.includes(167))
        measureLengthElement.value = 12;
    else if (numerosIndex.includes(96) || numerosIndex.includes(110))
        measureLengthElement.value = 6;
    else
        measureLengthElement.value = 16;

    var novoEvento = new Event('change');
    measureLengthElement.dispatchEvent(novoEvento);
}


function fazerViradaBateria() {
    _viradaRitmo = _ritmoSelecionado + '_fill';
}

function selecionarRitmo(ritmo, trocarRitmo, virada = false) {
    if (trocarRitmo) {
        if (ritmo.includes('cravo') == false) {
            fazerViradaBateria();

            if (virada == false)
                trocarRitmo = false;
        }

        var ritmoMatrix = ritmo;
        if (ritmo.includes('_'))
            ritmoMatrix = ritmo.split('_')[0];

        setBeats(ritmoMatrix);

        var tabelaBateria = document.getElementById('tracker-table');
        var tdsAtivados = document.getElementsByClassName('tracker-enabled');

        Array.from(tdsAtivados).forEach((tdAtivado) => {
            tdAtivado.classList.remove('tracker-enabled');
        });

        var tdsAtivar = tabelaBateria.getElementsByTagName('td');
        var numerosIndex = ritmosJson[ritmo];

        numerosIndex.forEach((numeroIndex) => {
            tdsAtivar[numeroIndex].classList.add('tracker-enabled');
        });
    }

    return trocarRitmo;
}

function mudarRitmo(ritmo, trocarRitmo) {
    trocarRitmo = true;
    var selectRitmo = document.getElementById('selectRitmo');

    if (ritmo == '')
        _ritmoSelecionado = selectRitmo.value;
    else
        _ritmoSelecionado = selectRitmo.value + "_" + ritmo;

    selecionarRitmo(_ritmoSelecionado);

    return trocarRitmo;
}

function pressionarBotao(botao) {
    var botaoPressionadoAntes = document.getElementsByClassName('selecionadoDrum');
    if (botaoPressionadoAntes.length > 0) {
        var botaoPressionado = botaoPressionadoAntes[0];

        if (botao == botaoPressionado)
            fazerViradaBateria();
        else
            botaoPressionado.classList.toggle('selecionadoDrum', false);

        if (botao == '')
            return true;
    }

    return false;
}

function calcularBpm() {
    var bpmRange_valor = document.getElementById('bpmRange').value;
    bpmRange_valor = 60000 / bpmRange_valor;

    var measureLength_valor = document.getElementById('measureLength').value;
    if (measureLength_valor == 24)
        bpmRange_valor = bpmRange_valor / 2;

    document.getElementById('light').style.animation = 'blink ' + bpmRange_valor + 'ms infinite';
}

function alterarBpm() {
    var bpmRangeElem = document.getElementById('bpmRange');

    var bpmElem = document.getElementById('bpm');
    bpmElem.value = bpmRangeElem.value;

    var novoEvento = new Event('change');
    bpmElem.dispatchEvent(novoEvento);
}

function adicionarRitmosNoSelect(ritmosNomes, selecionarRitmoElem) {
    for (var i = 0, len = ritmosNomes.length; i < len; i++) {
        if (ritmosNomes[i].includes('_') == false) {
            let opt = document.createElement('option');
            opt.value = ritmosNomes[i];
            opt.textContent += ritmosNomes[i];
            selecionarRitmoElem.appendChild(opt);
        }
    }

    return selecionarRitmoElem;
}