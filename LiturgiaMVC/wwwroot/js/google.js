//client id 982717214287-u57uddj8lrd7dq0n5i4fquuvci8umd60.apps.googleusercontent.com
//secret id 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
//https://console.cloud.google.com/apis/credentials
//https://www.youtube.com/watch?v=bOd4eFqIg00
//Video em portugu�s: https://www.youtube.com/watch?v=Cwwd9iiMeQo&t
//Tela de permiss�o OAuth
//Publicar aplicativo
//Sites de autoriza��o para colocar no scope https://developers.google.com/oauthplayground/

function signIn() {
	let oauth2Endpoint = "https://accounts.google.com/o/oauth2/v2/auth";

	let form = document.createElement('form');
	form.setAttribute('method', 'GET');
	form.setAttribute('action', oauth2Endpoint);

	let params = {
		"client_id": "982717214287-u57uddj8lrd7dq0n5i4fquuvci8umd60.apps.googleusercontent.com",
		"redirect_uri": "https://localhost:7188/home",
		"response_type": "token",
		"scope": "https://www.googleapis.com/auth/userinfo.profile https://www.googleapis.com/auth/youtube.readonly https://www.googleapis.com/auth/drive.file",
		"include_granted_scopes": 'true',
		'state': 'pass-through-value'
	}

	for (var p in params) {
		var input = document.createElement('input');
		input.setAttribute('type', 'hidden');
		input.setAttribute('name', p);
		input.setAttribute('value', params[p]);
		form.appendChild(input);
	}

	document.body.appendChild(form)

	form.submit()
}