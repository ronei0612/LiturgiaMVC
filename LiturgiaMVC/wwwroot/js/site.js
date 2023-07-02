function carregarLinkUrl() {
	var url = window.location.href;

	if (url.includes('?')) {
		var urlRemover = url.split('?')[0];
		var link = url.replace(urlRemover, '');
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
