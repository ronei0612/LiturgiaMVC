//Autorizar google api: https://console.developers.google.com/apis/api/drive.googleapis.com/overview?project=982717214287

// ID da pasta compartilhada no Google Drive
//const folderId = '1sA7VzOjxeFuF3fx7Ttf0GPqBQAop6G88';

// Configura��es de OAuth2
const clientId = '982717214287-u57uddj8lrd7dq0n5i4fquuvci8umd60.apps.googleusercontent.com';
const redirectUri = 'https://localhost:7188/orgao'; // URL de redirecionamento ap�s a autoriza��o
const scope = 'https://www.googleapis.com/auth/drive';

var accessToken;

// Fun��o para iniciar a autoriza��o com o Google
function authorizeGoogle() {
    //const authUrl = `https://accounts.google.com/o/oauth2/auth?client_id=${clientId}&redirect_uri=${redirectUri}&response_type=token&scope=${encodeURIComponent(scope)}&state=${encodeURIComponent(folderId)}`;
    const authUrl = `https://accounts.google.com/o/oauth2/auth?client_id=${clientId}&redirect_uri=${redirectUri}&response_type=token&scope=${encodeURIComponent(scope)}`;
    window.location.href = authUrl;
}

// Fun��o para processar o token de acesso e compartilhar a pasta
async function processToken() {
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

// Fun��o para ler um arquivo do Google Drive
function lerArquivo(arquivoId) {
    if (!arquivoId)
        arquivoId = localStorage.getItem('fileId');

    if (!arquivoId) {
        console.log('Arquivo ainda n�o criado');
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
            console.log('Conte�do do arquivo:', data);
            // Fa�a o que precisar com o conte�do do arquivo aqui
        })
        .catch(error => {
            console.error('Erro ao ler o arquivo:', error);
        });
}

function lerArquivoCompartilhado(arquivoId) {
    localStorage.removeItem('arquivoCompartilhado');
    // URL do arquivo compartilhado no Google Drive
    const url = 'https://drive.google.com/file/d/${arquivoId}/view?usp=sharing';

    // Enviar solicita��o GET para obter o conte�do do arquivo
    fetch(url)
        .then(response => {
            if (response.ok) {
                return response.text(); // Se a solicita��o for bem-sucedida, leia o conte�do do arquivo como texto
            } else {
                throw new Error('Erro ao obter o arquivo:', response.statusText);
            }
        })
        .then(data => {
            console.log('Conte�do do arquivo:', data); // Fa�a o que quiser com o conte�do do arquivo
        })
        .catch(error => {
            console.error('Erro:', error);
        });
}

// Verificar se a p�gina foi carregada com um token de acesso
window.onload = function () {
    if (window.location.hash) {
        processToken();

        if (window.location.href.includes('#access_token')) {
            document.location.hash = '';

            //const criandoArquivo = localStorage.getItem('criandoArquivo');
            //if (criandoArquivo)
            criarArquivodoStorage();
        }
    }

    //const compartilhandoId = localStorage.getItem('compartilhando');
    //if (compartilhandoId) {
    //    compartilharArquivo(compartilhandoId);
    //}

    //na primeira vez n�o chega aqui

    

    const arquivoCompartilhado = localStorage.getItem('arquivoCompartilhadoId');
    if (arquivoCompartilhado) {
        lerArquivoCompartilhado(arquivoCompartilhado);
    }
    
    //validarToken();
};

async function criarArquivoNoGoogleDrive(texto) {
    fetch('https://www.googleapis.com/upload/drive/v3/files?uploadType=media', {
        method: 'POST',
        headers: {
            'Authorization': `Bearer ${accessToken}`,
            'Content-Type': 'text/plain'
        },
        body: texto
    })
        .then(response => response.json())
        .then(data => {
            const fileId = data.id;
            const fileLink = `https://drive.google.com/file/d/${fileId}/view`; // Construct file link

            console.log('Arquivo criado com sucesso. ID do arquivo:', fileId);
            console.log('File uploaded link:', fileLink);
            //alert(document.location.href.replace('#','') + '?compartilhando=true');

            localStorage.setItem('fileId', fileId); // Armazena o ID do arquivo no localStorage
            mostrarModal('compartilhado');

            //window.location.href = document.location.href.replace('#', '') + '?compartilhado=1';
            //window.location.href = window.location.origin + window.location.pathname + '?compartilhado=1';;

            //compartilharArquivo(fileId);
        })
        .catch(error => {
            console.error('Erro ao criar o arquivo:', error);
        });
}

// Fun��o para criar um arquivo no Google Drive
function criarArquivodoStorage() {
    const texto = verificarSeJaCompartilhado();

    if (texto !== '') {
        localStorage.setItem('criandoArquivo', 'true');

        if (validarToken()) {
            localStorage.removeItem('criandoArquivo');
            let textoDoStorageCriptografado = criptografarTexto(texto);
            localStorage.setItem('compartilhadoMD5', textoDoStorageCriptografado);

            criarArquivoNoGoogleDrive(texto);

            //if (localStorage.getItem('accessToken')) {
            //    debugger;
            //    mostrarModal('compartilhado');
            //}
        }
    }
    else {
        debugger;
        mostrarModal('compartilhado');
    }
}

function compartilharArquivo(arquivoId) {
    localStorage.setItem('compartilhando', arquivoId);
    validarToken();

    localStorage.removeItem('compartilhando');

    const permission = {
        role: 'reader',
        type: 'anyone',
        allowFileDiscovery: false
    };

    const url = `https://www.googleapis.com/drive/v3/files/${arquivoId}/permissions`;

    fetch(url, {
        method: 'PATCH',
        headers: {
            'Authorization': `Bearer ${accessToken}`,
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(permission)
    })
        .then(response => {
            if (response.ok) {
                console.log('Arquivo compartilhado com sucesso.');
            } else {
                console.error('Erro ao editar o arquivo:', response.statusText);
            }
        })
        .catch(error => {
            console.error('Erro ao editar o arquivo:', error);
        });
}

function editarArquivo() {
    validarToken();

    const arquivoId = localStorage.getItem('fileId');
    if (!arquivoId) {        
        const texto = carregarSalvosLocalStorage();
        if (texto)
            //criarArquivodoStorage(texto);
        return;
    }

    const texto = carregarSalvosLocalStorage();
    if (texto) {
        // URL da solicita��o para editar o arquivo
        const url = `https://www.googleapis.com/upload/drive/v3/files/${arquivoId}`;

        // Fazer a solicita��o PATCH para editar o arquivo
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
}

function validarToken() {
    accessToken = localStorage.getItem('accessToken');
    const accessTokenExpiry = localStorage.getItem('accessTokenExpiry');

    if (accessToken)
        if (accessTokenExpiry)
            if (!(Date.now() < parseInt(accessTokenExpiry)))
                localStorage.removeItem('accessToken');

    
    //const accessTokenExpiry = localStorage.getItem('accessTokenExpiry');

    if (!accessToken) {
        authorizeGoogle();
        return false;
    }

    return true;

    //fetch(`https://www.googleapis.com/oauth2/v1/tokeninfo?access_token=${accessToken}`)
    //    .then(response => {
    //        if (!response.ok) {
    //            throw new Error('Erro ao validar o token:', response.status);
    //        }
    //        return response.json();
    //    })
    //    .then(data => {
    //        console.log('Informa��es do token:', data);
    //    })
    //    .catch(error => {
    //        console.error(error);
    //        localStorage.removeItem('accessToken');
    //        authorizeGoogle();
    //    });
}

function carregarSalvosLocalStorage() {
    const localStorageSalvamentos = localStorage.getItem('salvamentos');
    const localStorageKeys = localStorageSalvamentos.split(',');
    localStorageKeys.shift();
    var retorno = '';

    // Passo 2: Verificar se existem dados no localStorage
    if (localStorageKeys && localStorageKeys.length > 0) {
        // Passo 3: Criar um objeto para armazenar os dados
        const dataObject = {};

        // Passo 4: Iterar sobre as chaves do localStorage
        localStorageKeys.forEach(key => {
            // Obter os dados para cada chave
            const data = localStorage.getItem(key);
            // Adicionar os dados ao objeto
            dataObject[key] = data;
        });

        // Passo 5: Converter o objeto em uma string no formato desejado (JSON)
        const jsonData = JSON.stringify(dataObject);
        retorno = jsonData;
    }

    return retorno;
}

function criptografarTexto(texto) {
    return CryptoJS.MD5(texto).toString();
}

function verificarSeJaCompartilhado() {
    let textoDoStorage = carregarSalvosLocalStorage();
    let textoDoStorageCriptografado = criptografarTexto(textoDoStorage);
    const compartilhadoMD5 = localStorage.getItem('compartilhadoMD5');

    if (compartilhadoMD5) {
        if (textoDoStorageCriptografado === compartilhadoMD5) {
            return '';
        }
        else {
            //localStorage.setItem('compartilhadoMD5', textoDoStorageCriptografado);
            return textoDoStorage;
        }            
    }
    else
        return textoDoStorage;
}

function copiarTextoParaClipboard(texto) {
    // Cria uma �rea de sele��o para copiar o texto
    var areaSelecao = document.createElement("textarea");
    areaSelecao.value = texto;
    document.body.appendChild(areaSelecao);

    // Seleciona o texto na �rea de sele��o
    areaSelecao.select();

    // Executa o comando de c�pia
    document.execCommand("copy");

    // Remove a �rea de sele��o
    document.body.removeChild(areaSelecao);

    alert("Texto copiado!");
    ocultarModal();
}