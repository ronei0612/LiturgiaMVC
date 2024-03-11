function DarkMode() {
	localStorage.setItem('darkMode', switchDark.checked);

	if (document.body.classList.contains("bg-dark")) {
		document.body.classList = "bg-light text-dark orgao-background";
		navBar.classList = "navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-light border-bottom box-shadow mb-3";
		document.getElementsByClassName("w3-modal-content")[0].style.cssText = 'background-color: #fff!important';

		if (textoCifrasFrame.style.display !== 'none')
			textoCifras.contentWindow.document.querySelector('pre').style.color = '#000';
			
		//document.getElementById('bateria').style.display = '';
		//document.getElementById('bateria').style.color = '#000';
		//document.getElementById('textoRitmo').style.color = '#fff';

		var elements = document.getElementsByClassName("nav-link");
		for (var i = 0; i < elements.length; i++)
			elements[i].style.color = '#0d6efd';

		elements = document.getElementsByClassName("TextoMissal");
		for (var i = 0; i < elements.length; i++)
			elements[i].style.color = '#9c7b3e';
    }
    else {
		document.body.classList = "bg-dark text-light orgao-background-dark";
		navBar.classList = "navbar navbar-expand-sm navbar-toggleable-sm navbar-dark bg-black box-shadow mb-3";
		document.getElementsByClassName("w3-modal-content")[0].style.cssText = 'background-color: #505050!important';

		if (textoCifrasFrame.style.display !== 'none')
			textoCifras.contentWindow.document.querySelector('pre').style.color = '#fff';
				
		var elements = document.getElementsByClassName("nav-link");
		for (var i = 0; i < elements.length; i++)
			elements[i].style.color = '#74acff';

		elements = document.getElementsByClassName("TextoMissal");
		for (var i = 0; i < elements.length; i++)
			elements[i].style.color = '#b3a58a';
    }
}

window.addEventListener('DOMContentLoaded', function () {
	removerComercial();
	if (699 >= window.innerWidth) {
		var imgs = document.querySelectorAll('img');
		imgs.forEach(function (img) {
			img.style.maxWidth = '100%';
			img.style.minWidth = '100%';
			img.style.objectFit = 'fill';
			img.style.height = 'auto';
		});

		var iframes = document.querySelectorAll('iframe');
		iframes.forEach(function (iframe) {
			iframe.style.width = '100%';
		});
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