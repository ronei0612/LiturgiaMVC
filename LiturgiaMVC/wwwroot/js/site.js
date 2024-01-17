function DarkMode() {
    if (document.body.classList.contains("bg-dark")) {
		document.body.classList = "bg-light text-dark orgao-background";
		document.getElementById("navBar").classList = "navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-light border-bottom box-shadow mb-3";
		document.getElementsByClassName("w3-modal-content")[0].style.cssText = 'background-color: #fff!important';

		if (document.getElementById('textoCifrasFrame').style.display != 'none')
			document.getElementById('textoCifras').contentWindow.document.querySelector('pre').style.color = '#000';
    }
    else {
		document.body.classList = "bg-dark text-light orgao-background-dark";
		document.getElementById("navBar").classList = "navbar navbar-expand-sm navbar-toggleable-sm navbar-dark bg-black box-shadow mb-3";
		document.getElementsByClassName("w3-modal-content")[0].style.cssText = 'background-color: #505050!important';

		if (document.getElementById('textoCifrasFrame').style.display != 'none')
			document.getElementById('textoCifras').contentWindow.document.querySelector('pre').style.color = '#fff';
    }
}

$(document).ready(function () {
	removerComercial();

	// Adapta ao celular
	if ($(window).width() <= 699) {
		$('img')
			.css('max-width', '100%')
			.css('min-width', '100%')
			.css('object-fit', 'fill')
			.css('height', 'auto');
		$('iframe')
			.css('width', '100%')
	}
});

function removerComercial() {
	setTimeout(function () {
		$("div[style='opacity: 0.9; z-index: 2147483647; position: fixed; left: 0px; bottom: 0px; height: 65px; right: 0px; display: block; width: 100%; background-color: #202020; margin: 0px; padding: 0px;']").remove();
		$("div[style='margin: 0px; padding: 0px; left: 0px; width: 100%; height: 65px; right: 0px; bottom: 0px; display: block; position: fixed; z-index: 2147483647; opacity: 0.9; background-color: rgb(32, 32, 32);']").remove();
		$("div[onmouseover='S_ssac();']").remove();
		$("center").remove();
	}, 500);
}

document.body.addEventListener('click', removerComercial);