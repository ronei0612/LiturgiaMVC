// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function carregarLinkUrl() {
	var url = window.location.href;

	if (url.includes('?')) {
		var link = url.replace(base, '');
		// link = https://www.youtube.com/embed/videoseries?list=PLc2L-_BdS7A5SHoqM4ZEMD95Prw1-LmRg&index=7?rel=0&autoplay=1

		if (link.includes('?')) {
			link = link + '&rel=0&autoplay=1&loop=1';
			console.log(link);
			document.getElementById('youtubeFrame').src = 'https://www.youtube.com/embed/' + link;
		} else {
			link = link + '?rel=0&autoplay=1&loop=1';
			console.log(link);
			document.getElementById('youtubeFrame').src = 'https://www.youtube.com/embed/' + link;
		}
		document.getElementById('youtubeFrame').style.display = 'block';
	}
}

function gerarLink() {
	var link = document.getElementById('input').value;

	if (link.includes("list=")) { //youtube.com/watch?v=0xNzlVHaEXY&list=PL_1F_fsE9bd6VY9DeflOcdqGld62uyt90
		link = link.split("list=")[1];
		link = "videoseries?list=" + link; //link = videoseries?list=PLc2L-_BdS7A5SHoqM4ZEMD95Prw1-LmRg&index=7
	}
	else if (link.includes("v=")) { //youtube.com/watch?v=sTTnkDKQIjM
		link = link.split("v=")[1];
		try {
			link = link.split('&')[0];
		} catch { }
	}
	else if (link.includes(".be")) { //youtu.be/sTTnkDKQIjM
		link = link.split(".be/")[1];
		try {
			link = link.split('&')[0];
		} catch { }
	}

	location.replace(base + link);
}

window.onload = function () {
};

/* Remover Comercial */
$(document).ready(function () {
	$("div[style='opacity: 0.9; z-index: 2147483647; position: fixed; left: 0px; bottom: 0px; height: 65px; right: 0px; display: block; width: 100%; background-color: #202020; margin: 0px; padding: 0px;']").remove();
	$("div[style='margin: 0px; padding: 0px; left: 0px; width: 100%; height: 65px; right: 0px; bottom: 0px; display: block; position: fixed; z-index: 2147483647; opacity: 0.9; background-color: rgb(32, 32, 32);']").remove();
	$("div[onmouseover='S_ssac();']").remove();
	$("center").remove();

	// Adapta ao celular
	if ($(window).width() <= 699) {
		$('img')
			.css('max-width', '100%')
			.css('min-width', '100%')
			.css('object-fit', 'fill')
			.css('height', 'auto');
	}
});
