function setBeats(ritmoMatrix) {//compasso
    var numerosIndex = ritmosJson[ritmoMatrix];

    if (numerosIndex.includes(323))
        measureLength.value = 24;
    else if (numerosIndex.includes(219))
        measureLength.value = 16;
    else if (numerosIndex.includes(167))
        measureLength.value = 12;
    else if (numerosIndex.includes(96) || numerosIndex.includes(110))
        measureLength.value = 6;
        
    measureLength.dispatchEvent(eventoChange);
}

function fazerViradaBateria(ritmoSelecionado) {
    var viradaRitmo = _ritmoSelecionado + '_fill';
    return viradaRitmo;
}

function fecharChimbal(instrumentName, sourceChimbalAberto, triggerTime) {
    if (instrumentName == 'chimbal' || instrumentName == 'chimbal2')
        if (_chimbalIsAberto) {
            sourceChimbalAberto.stop(triggerTime);
            _chimbalIsAberto = false;
        }
}

function guardarChimbalAberto(instrumentName, instrument) {
    if (instrumentName == 'aberto') {
        _sourceChimbalAberto = instrument;
        _chimbalIsAberto = true;
    }
}

function mudarRitmo(ritmo) {
    _trocarRitmo = true;

    if (ritmo == '')
        _ritmoSelecionado = selectRitmo.value;
    else
        _ritmoSelecionado = selectRitmo.value + "_" + ritmo;

    selecionarRitmo(_ritmoSelecionado);
}

function selecionarRitmo(ritmo, virada = false) {
    if (_trocarRitmo) {
        _viradaRitmo = fazerViradaBateria(_ritmoSelecionado);

        if (virada == false)
            _trocarRitmo = false;

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
}

function gerarRitmosNomes(ritmosNomes) {
    for (var i = 0, len = ritmosNomes.length; i < len; i++) {
        if (ritmosNomes[i].includes('_') == false) {
            let opt = document.createElement('option');
            opt.value = ritmosNomes[i];
            opt.textContent += ritmosNomes[i];
            selectRitmo.appendChild(opt);
        }
    }
}