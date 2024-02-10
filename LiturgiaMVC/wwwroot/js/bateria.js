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