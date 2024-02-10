function setBeats(ritmoMatrix) {//compasso
    var numerosIndex = ritmosJson[ritmoMatrix];
    //var measureLengthElement = document.getElementById('measureLength');

    if (numerosIndex.includes(323))
        measureLength.value = 24;
    else if (numerosIndex.includes(219))
        measureLength.value = 16;
    else if (numerosIndex.includes(167))
        measureLength.value = 12;
    else if (numerosIndex.includes(96) || numerosIndex.includes(110))
        measureLength.value = 6;

    //var novoEvento = new Event('change');
    measureLength.dispatchEvent(eventoChange);
}
