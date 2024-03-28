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
    const state = urlParams.get('state');

    //if (accessToken && state) {
    if (accessToken) {
        localStorage.setItem('accessToken', accessToken);
        //shareFolder(state, accessToken);
        console.log('Token de acesso obtido com sucesso:', accessToken);
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

    const accessToken = localStorage.getItem('accessToken');

    if (!accessToken) {
        console.error('Token de acesso não encontrado.');
        return;
    }

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
    const accessToken = localStorage.getItem('accessToken');

    if (!accessToken) {
        console.error('Token de acesso não encontrado.');
        return;
    }

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
    const accessToken = localStorage.getItem('accessToken');
    if (!accessToken) {
        console.error('Token de acesso não encontrado.');
        return;
    }

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
    if (!accessToken) {
        authorizeGoogle();
        console.error('Token de acesso não encontrado.');
        return;
    }

    // const url = `https://oauth2.googleapis.com/tokeninfo?id_token=${accessToken}`;

    // fetch(url)
    //     .then(response => {
    //         if (!response.ok) {
    //             throw new Error('Erro ao validar o token:', response.status);
    //         }
    //         return response.json();
    //     })
    //     .then(data => {
    //         console.log('Token válido. Informações do token:', data);
    //         return 'sucesso';
    //     })
    //     .catch(error => {
    //         console.error(error);
    //         return error;
    //     });

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

// // Função para compartilhar a pasta com o usuário
// function shareFolder(folderId, accessToken) {
//     const shareUrl = `https://www.googleapis.com/drive/v3/files/${folderId}/permissions`;
//     const requestBody = {
//         role: 'reader', // Define a permissão como leitura (reader)
//         type: 'anyone', // Compartilha com qualquer pessoa que tenha o link
//         allowFileDiscovery: false // Evita que os arquivos sejam visíveis nos resultados de pesquisa
//     };

//     fetch(shareUrl, {
//         method: 'POST',
//         headers: {
//             'Authorization': `Bearer ${accessToken}`,
//             'Content-Type': 'application/json'
//         },
//         body: JSON.stringify(requestBody)
//     })
//         .then(response => response.json())
//         .then(data => {
//             console.log('Pasta compartilhada com sucesso:', data);
//         })
//         .catch(error => {
//             console.error('Erro ao compartilhar a pasta:', error);
//         });
// }

// function uploadFile() {
//     const fileInput = document.getElementById('fileInput');
//     const file = fileInput.files[0];
//     const arquivoId = '1dfdDjTaIjxLxdmsBsz-Qc5WLnyH1zSoE'; // Substitua pelo ID do arquivo que você deseja substituir
//     const metadata = {
//         name: file.name,
//     };
//     const reader = new FileReader();
//     var url = 'https://www.googleapis.com/upload/drive/v3/files?uploadType=media';
//     url = `https://www.googleapis.com/upload/drive/v3/files/${arquivoId}?uploadType=media`;

//     reader.onload = function (e) {
//         const content = e.target.result;
//         const accessToken = 'ya29.a0Ad52N3_imuHZ0pdQjPYLNRQv0TTz2cstFBBkvJJNQXa44KjB7wwU6cHupYP1Y2b0lHdkjVTGc1gGQaVUhIimD3vpXHUVYdHOF2I77x1V_t41hM0q4UbHbk-Af0uAd8vYInq4buuZryvFYxd97p2D3isdyu_dXaVHZwaCgYKASESARASFQHGX2MiCDR6FN5ePCQKU96Y1H4yIQ0169';
//         const headers = new Headers();
//         headers.append('Authorization', 'Bearer ' + accessToken);
//         headers.append('Content-Type', file.type);

//         fetch(url, {
//             method: 'POST',
//             headers: headers,
//             body: content,
//         }).then(response => {
//             return response.json(); // Parse the response as JSON
//         }).then(data => {
//             const fileId = data.id; // Extract file ID from the response
//             const fileLink = `https://drive.google.com/file/d/${fileId}/view`; // Construct file link
//             localStorage.setItem('fileId', fileId);

//             console.log('File uploaded successfully, id:', fileId);
//             console.log('File uploaded link:', fileLink);
//             // You can now use 'fileLink' for further processing or display
//         }).catch(error => {
//             console.error('Error uploading file:', error);
//         });
//     };
//     reader.readAsArrayBuffer(file);
// }