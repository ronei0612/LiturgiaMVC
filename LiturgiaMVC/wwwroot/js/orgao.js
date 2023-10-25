var _acordeSelecionado = '';
var _acordeAntesSelecionado = '';
var _acompanhamentoSelecionado = '';
var _acompanhamentoSolo = false;
var _acompanhamentoFull = false;
var _acompanhamentoMao = false;

deixarAcompanhamentoSelecionado('mao');

function deixarAcompanhamentoSelecionado(funcao) {
	escolherAcompanhamento(funcao, document.getElementById(funcao));
}

function mudarTom() {
	var tomSelecionado = document.getElementById("tomSelect").value;
	window.location.href = '@Url.Action("Index", "Orgao")?tom=' + tomSelecionado;
}

function escolherAcorde(acorde, botao) {
	_acordeSelecionado = acorde;
	levantarBotoesAcordes();
	pararOsAcordes();

	if (acorde != '')
		tocarAcorde(acorde, botao);

	_acordeAntesSelecionado = acorde;
}

function escolherAcompanhamento(funcao, botao) {
	_acompanhamentoSelecionado = funcao;
	pressionarBotaoAcompanhamento(botao);
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

function verificarAcompanhamentoEtocar(acorde) {
	if (_acompanhamentoSelecionado == 'full') {
		acordes[acorde].loop = true;
		acordes[acorde + '_solo'].loop = true;
		acordes[acorde + '_full'].loop = true;

		acordes[acorde].play();
		acordes[acorde + '_solo'].play();
		acordes[acorde + '_full'].play();
	}
	else {
		acordes[acorde + '_' + _acompanhamentoSelecionado].loop = true;
		acordes[acorde + '_' + _acompanhamentoSelecionado].play();
	}
}

function pararOsAcordes() {
	for (let [acorde, link] of Object.entries(acordes)) {
		if (acordes[acorde].paused == false) {
			acordes[acorde].pause();
			acordes[acorde].currentTime = 0.1;
		}
	}
	//for (let i = 0; i < acordes.length; i++) {
	//	acordes[i].pause();
	//	acordes[i].currentTime = 0.1;
	//}
}

function levantarBotoesAcordes() {
	if (document.getElementsByClassName('pressionado').length > 0)
		document.getElementsByClassName('pressionado')[0].classList.toggle('pressionado', false);
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