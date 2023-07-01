﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
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