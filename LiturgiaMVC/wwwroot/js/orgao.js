var _acordeSelecionado = '';
var _acordeAntesSelecionado = '';
var _acompanhamentoSolo = false;
var _acompanhamentoFull = false;
var _acompanhamentoMao = false;

function mudarTom() {
	var tomSelecionado = document.getElementById("tomSelect").value;
	window.location.href = '@Url.Action("Index", "Orgao")?tom=' + tomSelecionado;
}

function escolherAcorde(acorde, botao) {
	_acordeSelecionado = acorde;
	levantarOsBotoes();
	//verificarAcompanhamentoEtocar();
	pararOsAcordes();

	if (acorde != '')
		tocarAcorde(acorde, botao);

	_acordeAntesSelecionado = acorde;
}

function tocarAcorde(acorde, botao) {
	if (_acordeAntesSelecionado != acorde) {
		botao.classList.toggle('pressionado', true);
		verificarAcompanhamentoEtocar(acorde);
	}
}

function setTom(acorde = 'C') {
	document.getElementById('tomSelect').value = acorde;
}

function verificarAcompanhamentoEtocar() {
	if (_acompanhamentoSolo)
		acordes[acorde + '_solo'].play();
	if (_acompanhamentoMao)
		acordes[acorde].play();
	if (_acompanhamentoFull)
		acordes[acorde + '_full'].play();
}

function escolherAcompanhamento(funcao, botao) {
	if (funcao == 'solo')
		_acompanhamentoSolo = true;
	else if (funcao == 'mao')
		_acompanhamentoMao = true;
	else if (funcao == 'full')
		_acompanhamentoFull = true;
	setAcompanhamento(funcao, botao);
}

function pararOsAcordes() {
	for (let i = 0; i < acordes.length; i++) {
		acordes[i].pause();
		acordes[i].currentTime = 0.1;
	}
}

function levantarOsBotoes() {
	if (_acordeSelecionado.includes('orgao'))
		if (document.getElementsByClassName('pressionado').length > 0)
			document.getElementsByClassName('pressionado')[0].classList.toggle('pressionado', false);
}

function setAcompanhamento(funcao, botao) {
	if (botao.classList.contains('pressionado'))
		botao.classList.toggle('pressionado', false);
	else
		botao.classList.toggle('pressionado', true);
}