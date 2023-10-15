function DarkMode() {
    if (document.body.classList.contains("bg-dark")) {
        document.body.classList = "bg-light text-dark";
        document.getElementById("navBar").classList = "navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-light border-bottom box-shadow mb-3";
    }
    else {
        document.body.classList = "bg-dark text-light";
        document.getElementById("navBar").classList = "navbar navbar-expand-sm navbar-toggleable-sm navbar-dark bg-black box-shadow mb-3";
    }
}

$(document).ready(function () {
	//Remover Comercial
	setTimeout(function () {
		$("div[style='opacity: 0.9; z-index: 2147483647; position: fixed; left: 0px; bottom: 0px; height: 65px; right: 0px; display: block; width: 100%; background-color: #202020; margin: 0px; padding: 0px;']").remove();
		$("div[style='margin: 0px; padding: 0px; left: 0px; width: 100%; height: 65px; right: 0px; bottom: 0px; display: block; position: fixed; z-index: 2147483647; opacity: 0.9; background-color: rgb(32, 32, 32);']").remove();
		$("div[onmouseover='S_ssac();']").remove();
		$("center").remove();
	}, 300);

	// Adapta ao celular
	if ($(window).width() <= 699) {
		$('img')
			.css('max-width', '100%')
			.css('min-width', '100%')
			.css('object-fit', 'fill')
			.css('height', 'auto');
		//alert('aa');
		$('iframe')
			.css('width', '100%')
	}
});

