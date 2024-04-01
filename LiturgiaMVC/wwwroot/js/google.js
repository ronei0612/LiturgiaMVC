//Autorizar google api: https://console.developers.google.com/apis/api/drive.googleapis.com/overview?project=982717214287

// ID da pasta compartilhada no Google Drive
//const folderId = '1sA7VzOjxeFuF3fx7Ttf0GPqBQAop6G88';

// Configurações de OAuth2
const clientId = '982717214287-u57uddj8lrd7dq0n5i4fquuvci8umd60.apps.googleusercontent.com';
const redirectUri = 'https://localhost:7188/orgao'; // URL de redirecionamento após a autorização
const scope = 'https://www.googleapis.com/auth/drive';

// Função para iniciar a autorização com o Google
function authorizeGoogle() {
    //const authUrl = `https://accounts.google.com/o/oauth2/auth?client_id=${clientId}&redirect_uri=${redirectUri}&response_type=token&scope=${encodeURIComponent(scope)}&state=${encodeURIComponent(folderId)}`;
    const authUrl = `https://accounts.google.com/o/oauth2/auth?client_id=${clientId}&redirect_uri=${redirectUri}&response_type=token&scope=${encodeURIComponent(scope)}`;
    window.location.href = authUrl;
}

// Função para processar o token de acesso e compartilhar a pasta
function processToken() {
    const urlParams = new URLSearchParams(window.location.hash.substr(1));
    const accessToken = urlParams.get('access_token');
    //const state = urlParams.get('state');

    //if (accessToken && state) {
    if (accessToken) {
        localStorage.setItem('accessToken', accessToken);
        localStorage.setItem('accessTokenExpiry', Date.now() + 3600000);
    } else {
        console.error('Erro ao obter o token de acesso ou ID da pasta.');
    }
}

// Função para ler um arquivo do Google Drive
function lerArquivo(arquivoId) {
    if (!arquivoId)
        arquivoId = localStorage.getItem('fileId');

    if (!arquivoId) {
        console.log('Arquivo ainda não criado');
        return;
    }

    validarToken();

    fetch(`https://www.googleapis.com/drive/v3/files/${arquivoId}?alt=media`, {
        method: 'GET',
        headers: {
            'Authorization': `Bearer ${accessToken}`,
        }
    })
        .then(response => response.text())
        .then(data => {
            console.log('Conteúdo do arquivo:', data);
            // Faça o que precisar com o conteúdo do arquivo aqui
        })
        .catch(error => {
            console.error('Erro ao ler o arquivo:', error);
        });
}

// Verificar se a página foi carregada com um token de acesso
window.onload = function () {
    if (window.location.hash) {
        processToken();
    }

    //validarToken();
};

// Função para criar um arquivo no Google Drive
function criarArquivoComTexto(texto) {
    validarToken();

    fetch('https://www.googleapis.com/upload/drive/v3/files?uploadType=media', {
        method: 'POST',
        headers: {
            'Authorization': `Bearer ${accessToken}`,
            'Content-Type': 'text/plain' // Especifica o tipo de conteúdo como texto plano
        },
        body: texto
    })
        .then(response => response.json())
        .then(data => {
            const fileId = data.id;
            const fileLink = `https://drive.google.com/file/d/${fileId}/view`; // Construct file link

            console.log('Arquivo criado com sucesso. ID do arquivo:', fileId);
            console.log('File uploaded link:', fileLink);

            localStorage.setItem('fileId', fileId); // Armazena o ID do arquivo no localStorage
        })
        .catch(error => {
            console.error('Erro ao criar o arquivo:', error);
        });
}

function editarArquivo(texto) {
    validarToken();

    const arquivoId = localStorage.getItem('fileId');
    if (!arquivoId) {
        criarArquivoComTexto('bbb');
        return;
    }

    // URL da solicitação para editar o arquivo
    const url = `https://www.googleapis.com/upload/drive/v3/files/${arquivoId}`;

    // Fazer a solicitação PATCH para editar o arquivo
    fetch(url, {
        method: 'PATCH',
        headers: {
            'Authorization': `Bearer ${accessToken}`,
            'Content-Type': 'application/json'
        },
        body: texto
    })
        .then(response => {
            if (response.ok) {
                console.log('Arquivo editado com sucesso.');
            } else {
                console.error('Erro ao editar o arquivo:', response.statusText);
            }
        })
        .catch(error => {
            console.error('Erro ao editar o arquivo:', error);
        });
}

var accessToken;

function validarToken() {
    accessToken = localStorage.getItem('accessToken');
    let accessTokenExpiry = localStorage.getItem('accessTokenExpiry');

    if (!accessToken && !(Date.now() < parseInt(accessTokenExpiry))) {
        authorizeGoogle();
        return;
    }

    fetch(`https://www.googleapis.com/oauth2/v1/tokeninfo?access_token=${accessToken}`)
        .then(response => {
            if (!response.ok) {
                throw new Error('Erro ao validar o token:', response.status);
            }
            return response.json();
        })
        .then(data => {
            console.log('Informações do token:', data);
        })
        .catch(error => {
            console.error(error);
            localStorage.removeItem('accessToken');
            authorizeGoogle();
        });
}
